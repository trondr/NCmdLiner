using System;

namespace NCmdLiner
{
    internal interface IStringToObject
    {
        bool CanBeConvertedToDate(string parameter);
        object ConvertValue(string value, Type argumentType);
        object GetDefault(Type type);
    }
}