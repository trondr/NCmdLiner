// File: StringToObject.cs
// Project Name: NCmdLiner
// Project Home: https://github.com/trondr/NCmdLiner/blob/master/README.md
// License: New BSD License (BSD) https://github.com/trondr/NCmdLiner/blob/master/License.md
// Credits: See the Credit folder in this project
// Copyright © <github.com/trondr> 2013 
// All rights reserved.

using System;
using System.ComponentModel;
using System.Globalization;
using NCmdLiner.Exceptions;

namespace NCmdLiner
{
    internal class StringToObject : IStringToObject
    {
        private readonly IArrayParser _arrayParser;
        private readonly CultureInfo _culture;

        public StringToObject(IArrayParser arrayParser)
        {
            _arrayParser = arrayParser;
            _culture = CultureInfo.CurrentCulture;
        }

        public Result<Maybe<object>> ConvertValue(string value, Type argumentType)
        {
#if NET4_0
         Contract.Requires(value != null);
         Contract.Requires(argumentType != null);
#endif
            if (argumentType.IsArray)
            {
                var arrayItemType = argumentType.GetElementType();
                var array = _arrayParser.Parse(value);
                if (array.IsFailure)
                    return Result.Fail<Maybe<object>>(array.Exception);
                if (arrayItemType != null)
                {
                    var valuesArray = Array.CreateInstance(arrayItemType, array.Value.Length);
                    for (var i = 0; i < array.Value.Length; i++)
                    {
                        var arrayItemResult = ConvertSingleValue(array.Value[i], arrayItemType);
                        if (arrayItemResult.IsFailure)
                            return arrayItemResult;

                        valuesArray.SetValue(arrayItemResult.Value.Value, i);
                    }
                    return Result.Ok(Maybe<object>.From((object)valuesArray));
                }
            }

            return ConvertSingleValue(value, argumentType);
        }

        private Result<Maybe<object>> ConvertSingleValue(string value, Type argumentType)
        {
            if (value == String.Empty)
            {
               return Result.Ok(Maybe<object>.From(GetDefault(argumentType)));
            }

            if (IsNullableType(argumentType))
            {
                argumentType = Nullable.GetUnderlyingType(argumentType);
            }

            if (argumentType == typeof (String))
            {
                return Result.Ok(Maybe<object>.From((object)value));
            }

            if (argumentType == typeof (DateTime))
            {
                var dateTimeResult = ConvertToDateTime(value);
                if (dateTimeResult.IsFailure)
                    return Result.Fail<Maybe<object>>(dateTimeResult.Exception);
                return Result.Ok(Maybe<object>.From(dateTimeResult.Value));
            }

            // The primitive types are Boolean, Byte, SByte, Int16, UInt16, Int32,
            // UInt32, Int64, UInt64, Char, Double, and Single
            if (argumentType != null && (
                argumentType.IsPrimitive() || 
                argumentType == typeof (decimal) || 
                argumentType.IsEnum()))
            {
                var converter = TypeDescriptor.GetConverter(argumentType);
                try
                {
                    // ReSharper disable AssignNullToNotNullAttribute
                    return Result.Ok(Maybe<object>.From(converter.ConvertFromString(null, _culture, value)));
                    // ReSharper restore AssignNullToNotNullAttribute
                }
                catch (Exception ex)
                {
                    //trondr: The type converter throws a System.Exception and not a specific exception. Special handling of exceptions are therefore needed:
                    if (ex is OverflowException ||
                        (ex.InnerException != null & (ex.InnerException is OverflowException)))
                    {
                        return Result.Fail<Maybe<object>>(new InvalidValueException($"Value '{value}' is too big or too small"));
                    }
                    if (ex is FormatException || (ex.InnerException != null & (ex.InnerException is FormatException)))
                    {
                        try
                        {
                            //Try a new convert with invariant culture
                            // ReSharper disable AssignNullToNotNullAttribute
                            return Result.Ok(Maybe<object>.From(converter.ConvertFromString(null, CultureInfo.InvariantCulture, value)));
                            // ReSharper restore AssignNullToNotNullAttribute
                        }
                        catch (Exception ex2)
                        {
                            if (ex2.InnerException != null &
                                (ex2.InnerException is OverflowException | ex2 is OverflowException))
                            {
                                return Result.Fail<Maybe<object>>(new InvalidValueException($"Value '{value}' is too big or too small"));
                            }
                            if (ex2.InnerException != null &
                                (ex2.InnerException is FormatException | ex2 is FormatException))
                            {
                                return Result.Fail<Maybe<object>>(new InvalidConversionException($"Could not convert '{value}' to {argumentType}. {ex2.Message}"));
                            }
                            //Not a format or overflow exception, throw the orginal ex2 exception
                            return Result.Fail<Maybe<object>>(ex2);
                        }
                    }
                    //Not a format or overflow exception, throw the orginal ex exception
                    return Result.Fail<Maybe<object>>(ex);
                }
            }
            return Result.Fail<Maybe<object>>(new UnknownTypeException("Unknown type is used in your method: " + argumentType?.FullName));
        }

        private Result<object> ConvertToDateTime(string parameter)
        {
#if NET4_0
         Contract.Requires(parameter != null);
#endif
            try
            {
                return Result.Ok((object)DateTime.Parse(parameter, _culture, DateTimeStyles.AssumeLocal));
            }
            catch (FormatException)
            {
                try
                {
                    return Result.Ok((object)Convert.ToDateTime(parameter));
                }
                catch (FormatException ex)
                {
                    return Result.Fail<object>(new InvalidDateTimeFormatException("Could not convert " + parameter + " to DateTime. " + ex.Message));
                }
            }
        }

        public bool CanBeConvertedToDate(string parameter)
        {
#if NET4_0
         Contract.Requires(parameter != null);
#endif
            try
            {
                ConvertToDateTime(parameter);
                return true;
            }
            catch (NCmdLinerException)
            {
                return false;
            }
        }

        private bool IsNullableType(Type type)
        {
#if NET4_0
         Contract.Requires(type != null);
#endif
            return Nullable.GetUnderlyingType(type) != null;
        }

        public object GetDefault(Type type)
        {
#if NET4_0
         Contract.Requires(type != null);
#endif
            return type.IsValueType() ? Activator.CreateInstance(type) : null;
        }
    }
}