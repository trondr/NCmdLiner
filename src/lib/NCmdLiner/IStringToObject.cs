using System;
using LanguageExt;
using LanguageExt.Common;

namespace NCmdLiner
{
    internal interface IStringToObject
    {
        bool CanBeConvertedToDate(string parameter);
        Result<Option<object>> ConvertValue(string value, Type argumentType);
        object GetDefault(Type type);
    }
}