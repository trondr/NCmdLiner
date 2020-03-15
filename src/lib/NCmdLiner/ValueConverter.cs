using System;
using System.Text;

namespace NCmdLiner
{
    public class ValueConverter : IValueConverter
    {
        public string ObjectValue2String(object objectValue)
        {
            if (objectValue is Array)
            {
                var arrayString = new StringBuilder();
                var array = (Array)objectValue;
                arrayString.Append("[");
                for (var i = 0; i < array.Length; i++)
                {
                    var value = array.GetValue(i);
                    if (value is string || value is char)
                        arrayString.Append("'");
                    arrayString.Append(array.GetValue(i));
                    if (value is string || value is char)
                        arrayString.Append("'");
                    if (i < array.Length - 1)
                        arrayString.Append(";");
                }
                arrayString.Append("]");
                return arrayString.ToString().TrimEnd(';');
            }
            if (objectValue != null)
            {
                return objectValue.ToString();
            }
            return string.Empty;
        }
    }
}