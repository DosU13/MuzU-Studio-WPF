using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MuzU_Studio.util
{
    internal class MusicalNameRule : ValidationRule
    {
        public static readonly int[] Denominators = new[]{ 2,3,4,5,6,7,8,9,10,12,16,32,64 };
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value is not string str) return new ValidationResult(false, "It should be string");
            bool slashAppeared = false;
            foreach(var c in str)
            {
                if (char.IsLetterOrDigit(c) || char.IsWhiteSpace(c)) continue;
                if (c == '/' && !slashAppeared) slashAppeared = true;
                else return new ValidationResult(false, "n/n wwww");
            }
            str = str.ToLower().Replace(" ", "");
            if (str == "none") return ValidationResult.ValidResult;
            var newStr = str.Replace("bar", "");
            int wordCount = (str.Length - newStr.Length)/3;
            str = newStr;
            newStr = str.Replace("beat", "");
            newStr = newStr.Replace("step", "");
            wordCount += (str.Length - newStr.Length) / 4;
            str = newStr;
            if (wordCount != 0 && wordCount != 1) return new ValidationResult(false, "n/n wwww");
            if (str.Contains('/'))
            {
                int ind = str.IndexOf('/');
                string num = str[..ind], den = str[(ind + 1)..];
                if (!int.TryParse(num, out int _) 
                    || !int.TryParse(den, out int denInt) || !Denominators.Contains(denInt))
                        return new ValidationResult(false, "n/n wwww");
            }
            else
            {
                if(str == "" && wordCount == 1) return ValidationResult.ValidResult;
                if (!int.TryParse(str, out int _)) return new ValidationResult(false, "n/n wwww");
            }

            return ValidationResult.ValidResult;
        }
    }
}
