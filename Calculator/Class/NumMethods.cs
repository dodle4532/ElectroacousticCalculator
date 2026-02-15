using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Class
{
    /// <summary>
    /// Методы для работы с числами
    /// </summary>
    public class NumMethods
    {
        public static string FormatDouble(double value)
        {
            // Округляем до двух знаков после запятой
            string formatted = value.ToString("0.##", System.Globalization.CultureInfo.InvariantCulture).Replace(".", ",");

            // Если число заканчивается на .00 или ,00 - убираем дробную часть
            if (formatted.Contains('.') || formatted.Contains(','))
            {
                // Определяем разделитель
                char separator = formatted.Contains('.') ? '.' : ',';

                string[] parts = formatted.Split(separator);

                // Если после запятой только нули или пусто - возвращаем целую часть
                if (parts.Length == 2 && (string.IsNullOrEmpty(parts[1]) || parts[1].All(c => c == '0')))
                {
                    return parts[0];
                }

                // Убираем лишние нули в конце дробной части
                if (parts.Length == 2)
                {
                    parts[1] = parts[1].TrimEnd('0');
                    if (string.IsNullOrEmpty(parts[1]))
                        return parts[0];
                    else
                        return parts[0] + separator + parts[1];
                }
            }

            return formatted;
        }
    }
}
