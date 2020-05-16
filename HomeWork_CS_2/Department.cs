using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_CS_2
{
    /// <summary>
    /// Класс отдела
    /// </summary>
    public class Department : IComparable
    {
        /// <summary>
        /// Название отдела
        /// </summary>
        public string DepName { get; set; }

        public Department()
        {
            
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="_name"></param>
        public Department(string _name)
        {
            DepName = _name;
        }

        /// <summary>
        /// Перегружаем метод вывода ToString(), для вывода названия отдела
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return DepName;
        }

        /// <summary>
        /// Сравнение отделов (по названию отдела)
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            return String.Compare(this.DepName, (obj as Department)?.DepName);
        }
    }
}
