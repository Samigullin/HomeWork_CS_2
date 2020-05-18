using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml.Serialization;
using System.Diagnostics;
using Microsoft.Win32;


namespace HomeWork_CS_2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //создаем и заполняем список отделов:
        ObservableCollection<string> Departments = new ObservableCollection<string>()
        {
            "Финанасовый отдел",
            "Отдел маркетинга",
            "Отдел закупок",
            "Коммерческий отдел",
            "Отдел персонала",
            "Юридический отдел"
        };

        //ObservableCollection<Department> Departments = new ObservableCollection<Department>()
        //{
        //    new Department("Финанасовый отдел"),
        //    new Department("Отдел маркетинга"),
        //    new Department("Отдел закупок"),
        //    new Department("Коммерческий отдел"),
        //    new Department("Отдел персонала"),
        //    new Department("Юридический отдел"),
        //};

        ///создаем и заполняем список сотрудников
        ObservableCollection<Employee> Employees = new ObservableCollection<Employee>();

        public MainWindow()
        {            
            InitializeComponent();

            LoadFromXML("..\\..\\Employee.xml");
            cbAddDepartment.SelectedIndex = 0;
            //привязываем источники
            cbAddDepartment.ItemsSource = Departments;
            dgEmloyeeList.ItemsSource = Employees;                           
            dataGridComboBox.ItemsSource = Departments;
        }


        private void Change_Click(object sender, RoutedEventArgs e)
        {
            dgEmloyeeList.IsReadOnly = !dgEmloyeeList.IsReadOnly;
            if (dgEmloyeeList.IsReadOnly)
            {
                Change.Content = "Разрешить редактирование";
            }
            else
            {
                Change.Content = "Запретить редактирование";
            }
        }

        private void btnAddEmloyee_Click(object sender, RoutedEventArgs e)
        {
            //проверяем, чтоб имя и фамилия не были пустыми
            if (tbAddName.Text == "Имя" || tbAddName.Text == "")
            {
                MessageBox.Show("Введите имя сотрудника", "Ошибка ввода", MessageBoxButton.OK);
            }
            else if (tbAddSName.Text == "Фамилия" || tbAddName.Text == "")
            {
                MessageBox.Show("Введите фамилию сотрудника","Ошибка ввода", MessageBoxButton.OK);
            }
            else
            {
                //добавляем нового сотрудника
                Employees.Add(new Employee()
                {
                    Name = tbAddName.Text,
                    SName = tbAddSName.Text,
                    GetDepName = cbAddDepartment.SelectedItem.ToString()
                });

                MessageBox.Show($"Сотрудник {tbAddName.Text} {tbAddSName.Text} добавлен в {cbAddDepartment.SelectedItem}.");
                tbAddName.Foreground = Brushes.Gray;
                tbAddSName.Foreground = Brushes.Gray;
                //очищаем поля ввода
                tbAddName.Text = "Имя";
                tbAddSName.Text = "Фамилия";
                cbAddDepartment.SelectedIndex = 0;
            }
        }

        private void btnAddDepartment_Click(object sender, RoutedEventArgs e)
        {
            //проверяем, чтоб отдел не был пустой
            if (tbAddNewDepartment.Text == "Название отдела" || tbAddNewDepartment.Text == "")
            {
                MessageBox.Show("Введите название отдела", "Ошибка ввода", MessageBoxButton.OK);
            }
            else
            {
                MessageBox.Show($"Отдел {tbAddNewDepartment.Text} добавлен.");
                Departments.Add(tbAddNewDepartment.Text);
                tbAddNewDepartment.Foreground = Brushes.Gray;
                tbAddNewDepartment.Text = "Название отдела";
            }
        }

        #region реализация засеривания подсказки в полях ввода 
        private void tbAddName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbAddName.Text == "Имя" || tbAddName.Text == "")
            {
                tbAddName.Foreground = Brushes.Black;
                tbAddName.Text = "";
            }
            
        }

        private void tbAddName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (tbAddName.Text == "Имя" || tbAddName.Text == "")
            {
                tbAddName.Foreground = Brushes.Gray;
                tbAddName.Text = "Имя";
            }
            
        }

        private void tbAddSName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbAddSName.Text == "Фамилия" || tbAddSName.Text == "")
            {
                tbAddSName.Foreground = Brushes.Black;
                tbAddSName.Text = "";                
            }
        }

        private void tbAddSName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (tbAddSName.Text == "Фамилия" || tbAddSName.Text == "")
            {
                tbAddSName.Foreground = Brushes.Gray;
                tbAddSName.Text = "Фамилия";
            }
            
        }

        private void tbAddNewDepartment_LostFocus(object sender, RoutedEventArgs e)
        {
            if (tbAddNewDepartment.Text == "Название отдела" || tbAddNewDepartment.Text == "")
            {
                tbAddNewDepartment.Foreground = Brushes.Gray;
                tbAddNewDepartment.Text = "Название отдела";
            }
        }

        private void tbAddNewDepartment_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbAddNewDepartment.Text == "Название отдела" || tbAddNewDepartment.Text == "")
            {
                tbAddNewDepartment.Foreground = Brushes.Black;
                tbAddNewDepartment.Text = "";
            }
        }
        #endregion

        #region сохранение/открытие файла

        /// <summary>
        /// Сохранение в XML файл
        /// </summary>
        /// <param name="fileName"></param>
        public void SaveToXML(string fileName)
        {
            XmlSerializer xmlFormat = new XmlSerializer(typeof(ObservableCollection<Employee>));
            //Создаем файловый поток(проще говоря создаем файл)
            try
            {
                using (FileStream fStream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    xmlFormat.Serialize(fStream, Employees);
                    Debug.WriteLine("Data has been saved to file");
                }

                #region Если захочу сериализовать в JSON:
                //using (FileStream fStream = new FileStream("..\\..\\Employee.json", FileMode.Create, FileAccess.Write))
                //{
                //    JsonSerializer.SerializeAsync(fStream, Employees);
                //    Debug.WriteLine("Data has been saved to file");
                //}
                #endregion
            }
            catch { }
        }

        /// <summary>
        /// Загрузка из XML файла
        /// </summary>
        /// <param name="fileName"></param>
        public void LoadFromXML(string fileName)
        {
            XmlSerializer xmlFormat = new XmlSerializer(typeof(ObservableCollection<Employee>));
            try
            {
                using (FileStream fStream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Read))
                {
                    Employees = (ObservableCollection<Employee>)xmlFormat.Deserialize(fStream);
                    Debug.WriteLine("Data has been read from file");
                }
            }
            catch { }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "XML файл|*.XML|Все файлы|*.*";
            if (dialog.ShowDialog() == true)
            {
                SaveToXML(dialog.FileName);
            }
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "XML файл|*.XML|Все файлы|*.*";
            if (dialog.ShowDialog() == true)
            {
                //database = new Trips();
                //database.Load(dialog.FileName);
                //UpdateInfo();
                LoadFromXML(dialog.FileName);
                dgEmloyeeList.ItemsSource = Employees;
            }
        }
        #endregion

    }
}
