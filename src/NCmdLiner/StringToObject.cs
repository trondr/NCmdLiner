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
using System.Threading;
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
            _culture = Thread.CurrentThread.CurrentCulture;
        }

        public object ConvertValue(string value, Type argumentType)
        {
#if NET4_0
         Contract.Requires(value != null);
         Contract.Requires(argumentType != null);
#endif
            if (argumentType.IsArray)
            {
                var arrayItemType = argumentType.GetElementType();
                string[] array = _arrayParser.Parse(value);
                if (arrayItemType != null)
                {
                    var valuesArray = Array.CreateInstance(arrayItemType, array.Length);
                    for (var i = 0; i < array.Length; i++)
                    {
                        var arrayItem = ConvertSingleValue(array[i], arrayItemType);
                        valuesArray.SetValue(arrayItem, i);
                    }
                    return valuesArray;
                }
            }

            return ConvertSingleValue(value, argumentType);
        }

        private object ConvertSingleValue(string value, Type argumentType)
        {
            if (value == String.Empty)
            {
                return GetDefault(argumentType);
            }

            if (IsNullableType(argumentType))
            {
                argumentType = Nullable.GetUnderlyingType(argumentType);
            }

            if (argumentType == typeof (String))
            {
                return value;
            }

            if (argumentType == typeof (DateTime))
            {
                return ConvertToDateTime(value);
            }

            // The primitive types are Boolean, Byte, SByte, Int16, UInt16, Int32,
            // UInt32, Int64, UInt64, Char, Double, and Single
            if (argumentType.IsPrimitive || argumentType == typeof (decimal) || argumentType.IsEnum)
            {
                var converter = TypeDescriptor.GetConverter(argumentType);
                try
                {
                    // ReSharper disable AssignNullToNotNullAttribute
                    return converter.ConvertFromString(null, _culture, value);
                    // ReSharper restore AssignNullToNotNullAttribute
                }
                catch (Exception ex)
                {
                    //trondr: The type converter throws a System.Exception and not a specific exception. Special handling of exceptions are therefore needed:
                    if (ex is OverflowException ||
                        (ex.InnerException != null & (ex.InnerException is OverflowException)))
                    {
                        throw new InvalidValueException(string.Format("Value '{0}' is too big or too small", value));
                    }
                    if (ex is FormatException || (ex.InnerException != null & (ex.InnerException is FormatException)))
                    {
                        try
                        {
                            //Try a new convert with invariant culture
                            // ReSharper disable AssignNullToNotNullAttribute
                            return converter.ConvertFromString(null, CultureInfo.InvariantCulture, value);
                            // ReSharper restore AssignNullToNotNullAttribute
                        }
                        catch (Exception ex2)
                        {
                            if (ex2.InnerException != null &
                                (ex2.InnerException is OverflowException | ex2 is OverflowException))
                            {
                                throw new InvalidValueException(string.Format("Value '{0}' is too big or too small",
                                                                              value));
                            }
                            if (ex2.InnerException != null &
                                (ex2.InnerException is FormatException | ex2 is FormatException))
                            {
                                throw new InvalidConversionException(string.Format(
                                    "Could not convert '{0}' to {1}. {2}", value, argumentType, ex2.Message));
                            }
                            //Not a format or overflow exception, throw the orginal ex2 exception
                            throw;
                        }
                    }
                    //Not a format or overflow exception, throw the orginal ex exception
                    throw;
                }
            }
            throw new UnknownTypeException("Unknown type is used in your method: {0}", argumentType.FullName);
        }

        private DateTime ConvertToDateTime(string parameter)
        {
#if NET4_0
         Contract.Requires(parameter != null);
#endif
            try
            {
                return DateTime.Parse(parameter, _culture, DateTimeStyles.AssumeLocal);
            }
            catch (FormatException)
            {
                try
                {
                    return Convert.ToDateTime(parameter);
                }
                catch (FormatException ex)
                {
                    throw new InvalidDateTimeFormatException("Could not convert '{0}' to DateTime. {1}", parameter, ex.Message);
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
            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }
    }
}