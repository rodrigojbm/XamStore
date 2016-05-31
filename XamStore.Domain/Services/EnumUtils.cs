using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamStore.Domain.Enums;

namespace XamStore.Domain.Services
{
    public static class EnumUtils
    {
        public static string GetEnumDescription(PedidoStatusEnum enumValue)
        {
            var enumValueAsString = enumValue.ToString();

            var type = enumValue.GetType();
            var fieldInfo = type.GetField(enumValueAsString);
            var attributes = fieldInfo.GetCustomAttributes(typeof (DescriptionAttribute), false);

            if (attributes.Length <= 0) return enumValueAsString;
            var attribute = (DescriptionAttribute) attributes[0];
            return attribute.Description;
        }
    }
}
