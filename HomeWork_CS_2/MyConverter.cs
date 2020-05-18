using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace HomeWork_CS_2
{
    /// <summary>
    /// Попытка реализовать конвертер для вывода департаментов в DataGridComboBoxColumn
    /// </summary>
    class MyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value as Department).DepName.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value as Department;//new Department(value.ToString());
        }
    }
}
