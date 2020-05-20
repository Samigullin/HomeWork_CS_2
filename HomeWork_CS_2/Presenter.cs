using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HomeWork_CS_2
{ 
    class Presenter
    {
        //создаем и заполняем список отделов:
        ObservableCollection<string> departments = new ObservableCollection<string>()
        {
            "Финанасовый отдел",
            "Отдел маркетинга",
            "Отдел закупок",
            "Коммерческий отдел",
            "Отдел персонала",
            "Юридический отдел"
        };

        ///создаем и заполняем список сотрудников
        ObservableCollection<Employee> employees = new ObservableCollection<Employee>();
        public ObservableCollection<Employee> Employees { get { return employees; } set { employees = value; } }
        public ObservableCollection<string> Departments { get { return departments; } set{ departments = value; } }

        public Presenter()
        { 

        }

        /// <summary>
        /// Сохранение в XML файл
        /// </summary>
        /// <param name="fileName"></param>
        public void SaveToXML()
        {
            try
            {
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.Filter = "XML файл|*.XML|Все файлы|*.*";

                if (dialog.ShowDialog() == true)
                {
                    XmlSerializer xmlFormat = new XmlSerializer(typeof(ObservableCollection<Employee>));
                    using (FileStream fStream = new FileStream(dialog.FileName, FileMode.Create, FileAccess.Write))
                    {
                        xmlFormat.Serialize(fStream, Employees);
                        Debug.WriteLine("Data has been saved to file");
                    }
                }
            }
            catch { }
        }

        /// <summary>
        /// Загрузка из XML файла
        /// </summary>
        /// <param name="fileName"></param>
        public void LoadFromXML()
        {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "XML файл|*.XML|Все файлы|*.*";
                if (dialog.ShowDialog() == true)
                {
                    LoadFromXML(dialog.FileName);
                }
        }

        /// <summary>
        /// Загрузка из XML файла
        /// </summary>
        /// <param name="_fileName"></param>
        public void LoadFromXML(string _fileName)
        {
            try
            {
                XmlSerializer xmlFormat = new XmlSerializer(typeof(ObservableCollection<Employee>));

                using (FileStream fStream = new FileStream(_fileName, FileMode.OpenOrCreate, FileAccess.Read))
                {
                    Employees = (ObservableCollection<Employee>)xmlFormat.Deserialize(fStream);
                    Debug.WriteLine("Data has been read from file");
                }
            }
            catch { }
        }

        /// <summary>
        /// Добавление нового сотрудника
        /// </summary>
        /// <param name="_name">Имя сотрудника</param>
        /// <param name="_sName">Фамилия сотрудника</param>
        /// <param name="_department">Название отдела</param>
        public void AddEmployee(string _name, string _sName, string _department)
        {
            //добавляем нового сотрудника
            Employees.Add(new Employee()
            {
                Name = _name,
                SName = _sName,
                GetDepName = _department
            });
        }

        /// <summary>
        /// Добавление новго отдела
        /// </summary>
        /// <param name="_depName">Название отдела</param>
        public void AddDepartment(string _depName)
        {
            Departments.Add(_depName);
        }
    }
}
