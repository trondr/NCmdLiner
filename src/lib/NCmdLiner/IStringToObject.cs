using System;

namespace NCmdLiner
{
    internal interface IStringToObject
    {
        bool CanBeConvertedToDate(string parameter);
        Result<Maybe<object>> ConvertValue(string value, Type argumentType);
        object GetDefault(Type type);
    }
}