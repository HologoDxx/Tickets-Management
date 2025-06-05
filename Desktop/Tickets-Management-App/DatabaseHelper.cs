using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Tickets_Management_App
{
    internal class DatabaseHelper
    {
        private readonly string _connectionString;

        public DatabaseHelper()
        {
            _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Tickets_Management;Integrated Security=True;";
        }

        public DataTable ExecuteSqlQuery(string query, SqlParameter[] parameters = null)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.CommandType = CommandType.Text;
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    try
                    {
                        connection.Open();
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Ошибка при выполнении запроса: {ex.Message}");
                        throw;
                    }
                }
            }
            return dataTable;
        }

        public int ExecuteNonQuery(string query, SqlParameter[] parameters = null)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.CommandType = CommandType.Text;
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    try
                    {
                        connection.Open();
                        return command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Ошибка при выполнении запроса: {ex.Message}");
                        throw;
                    }
                }
            }
        }
    }
}
