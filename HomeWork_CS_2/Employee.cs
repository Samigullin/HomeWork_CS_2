using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_CS_2
{
    /// <summary>
    /// Класс сотрудника
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// Имя сотрудника
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Фамилия сотрудника
        /// </summary>
        public string SName { get; set; }

        /// <summary>
        /// Отдел сотрудника
        /// </summary>
        public Department EmployeeDep { get; set; }

        /// <summary>
        /// Получение названия отдела
        /// </summary>
        public string GetDepName 
        { 
            get { return EmployeeDep.DepName; }
            set { EmployeeDep.DepName = value; } 
        }

    }
}
