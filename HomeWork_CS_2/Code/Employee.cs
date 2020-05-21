using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_CS_2
{
    /// <summary>
    /// Класс сотрудника
    /// </summary>
    public class Employee : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        string name;
        string sName;
        Department employeeDep;

        public Employee()
        {

        }
        public Employee(string _name, string _sName, string _dep)
        {
            Name = _name;
            SName = _sName;
            EmployeeDep = new Department(_dep);
        }

        /// <summary>
        /// Имя сотрудника
        /// </summary>
        public string Name 
        { 
            get 
            {
                return name;
            } 
            set
            {
                name = value;                
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
            }
        }

        /// <summary>
        /// Фамилия сотрудника
        /// </summary>
        public string SName
        {
            get
            {
                return sName;
            }
            set
            {
                sName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SName)));
            }
        }

        /// <summary>
        /// Отдел сотрудника
        /// </summary>
        public Department EmployeeDep
        {
            get
            {
                return employeeDep;
            }
            set
            {
                employeeDep = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.EmployeeDep.DepName)));
            }
        }

        /// <summary>
        /// Получение названия отдела
        /// </summary>
        public string GetDepName 
        { 
            get { return EmployeeDep?.DepName; }
            set { EmployeeDep = new Department(value); } 
        }

        
    }
}
