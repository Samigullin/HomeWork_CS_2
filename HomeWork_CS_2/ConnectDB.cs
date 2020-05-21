using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_CS_2
{
    class ConnectDB
    {
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=EmployeesDB;
                                        Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;
                                        ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        /// <summary>
        /// Запись коллекции сотрудников в базу
        /// </summary>
        /// <param name="_employees">Коллекция</param>
        /// <param name="_needTruncate">Необходимость предварительной очистки базы</param>
        public void Insert(Collection<Employee> _employees, bool _needTruncate)
        {
            try
            {
                //проверяем необходимость предварительной очистки базы
                string _query = _needTruncate ? "TRUNCATE TABLE Employees;" : "";
                _query += "INSERT INTO Employees(Name,SName,EmployeeDep) VALUES ";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    //Строим строку запроса
                    for (int i = 0; i < _employees.Count - 1; i++)
                    {
                        _query += $"(N'{_employees[i].Name}', N'{_employees[i].SName}', N'{_employees[i].GetDepName}'),";
                    }
                    _query += $"(N'{_employees[_employees.Count - 1].Name}', N'{_employees[_employees.Count - 1].SName}', N'{_employees[_employees.Count - 1].GetDepName}');";

                    //открываем соединение с базой
                    connection.Open();
                    //создаем SQL команду
                    SqlCommand command = new SqlCommand($"{_query}", connection);
                    //выполняем SQL команду
                    command.ExecuteReader();
                    //закрываем соездинение с базой
                    connection.Close();
                }

                Debug.WriteLine("Данные сохранены в SQL DB");
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }    
        }
        public void Read(Collection<Employee> _employees)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand($"SELECT * FROM Employees", connection);
                    var reader = command.ExecuteReader();
                    connection.Close();
                }
                Debug.WriteLine("Данные прочитаны из SQL DB");
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
        }
    }
}
