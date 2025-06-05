using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tickets_Management_App
{
    public partial class RegistrationForm : Form
    {
        private readonly DatabaseHelper _dbHelper = new DatabaseHelper();

        public RegistrationForm()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            string firstName = txtFirstName.Text.Trim();
            string lastName = txtLastName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string roleName = "Client";

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string query = "INSERT INTO Users (Username, Password, FirstName, LastName, Email, RoleName) " +
                               "VALUES (@Username, @Password, @FirstName, @LastName, @Email, @RoleName)";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@Username", username),
                    new SqlParameter("@Password", password),
                    new SqlParameter("@FirstName", firstName),
                    new SqlParameter("@LastName", lastName),
                    new SqlParameter("@Email", email),
                    new SqlParameter("@RoleName", roleName)
                };

                int rowsAffected = _dbHelper.ExecuteNonQuery(query, parameters);

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Регистрация прошла успешно.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK; // Устанавливаем результат OK
                    Close(); // Закрываем форму
                }
                else
                {
                    MessageBox.Show("Не удалось зарегистрировать пользователя.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при регистрации: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
