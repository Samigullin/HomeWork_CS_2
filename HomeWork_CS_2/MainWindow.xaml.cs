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
    /// Логика взаимодействия  для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //попытка разделить представление от модели
        Presenter p;
        public MainWindow()
        {            
            InitializeComponent();
            p = new Presenter();
            p.LoadFromXML("..\\..\\Employee.xml");

            Save.Click += delegate { p.SaveToXML(); };
            Open.Click += delegate { p.LoadFromXML(); };

            cbAddDepartment.ItemsSource = p.Departments;
            dgEmloyeeList.ItemsSource = p.Employees;
            dgComboBox.ItemsSource = p.Departments;
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

        /// <summary>
        /// Нажатие на кнопку добавить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddEmloyee_Click(object sender, RoutedEventArgs e)
        {
            //проверяем, чтоб имя, фамилия и отдел не были пустыми
            if (tbAddName.Text == "Имя" || tbAddName.Text == "")                MessageBox.Show("Введите имя сотрудника", "Ошибка ввода", MessageBoxButton.OK);            
            else if (tbAddSName.Text == "Фамилия" || tbAddName.Text == "")      MessageBox.Show("Введите фамилию сотрудника","Ошибка ввода", MessageBoxButton.OK);
            else if (cbAddDepartment.SelectedIndex == -1)                       MessageBox.Show("Выберите отдел сотрудника", "Ошибка ввода", MessageBoxButton.OK);
            else
            {
                //добавляем нового сотрудника
                p.AddEmployee(tbAddName.Text, tbAddSName.Text, cbAddDepartment.SelectedItem.ToString());
                MessageBox.Show($"Сотрудник {tbAddName.Text} {tbAddSName.Text} добавлен в {cbAddDepartment.SelectedItem}.");

                //очищаем и засериваем поля ввода
                tbAddName.Foreground = Brushes.Gray;
                tbAddSName.Foreground = Brushes.Gray;
                tbAddName.Text = "Имя";
                tbAddSName.Text = "Фамилия";
                cbAddDepartment.SelectedIndex = -1;
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
                p.AddDepartment(tbAddNewDepartment.Text);
                tbAddNewDepartment.Foreground = Brushes.Gray;
                tbAddNewDepartment.Text = "Название отдела";
            }
        }

        /// <summary>
        /// Закрытие приложения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!(MessageBox.Show("Вы уверены, что хотите выйти из приложения?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes))
            {
                e.Cancel = true;
            }
        }

        #region реализация засеривания подсказки в полях ввода  TODO: переделать засеривание через триггеры
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
    }
}
