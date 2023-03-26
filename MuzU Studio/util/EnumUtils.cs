using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuzU_Studio.util
{
    internal static class EnumUtils
    {
        public static string GetDisplayName<E>(E value) where E : Enum
        {
            var field = value.GetType().GetField(value.ToString());
            var attributes = field!.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];
            if (attributes != null && attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return value.ToString();
            }
        }
        public static IEnumerable<string> GetDisplayNames<E>()
        {
            return Enum.GetValues(typeof(E)).Cast<Enum>()
                .Select(e => GetDisplayName(e));
        }

        public static E GetValue<E>(string displayName) where E : Enum
        {
            foreach (E enumValue in Enum.GetValues(typeof(E)))
            {
                if (GetDisplayName(enumValue) == displayName)
                {
                    return enumValue;
                }
            }
            throw new ArgumentException($"No enum value found with display name '{displayName}'");
        }
    }
}
