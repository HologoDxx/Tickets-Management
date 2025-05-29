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
using System.Xml.Linq;

namespace Tickets_Management_App
{
    public partial class DetailsForm : Form
    {
        private readonly DatabaseHelper _dbHelper = new DatabaseHelper();
        private readonly int _ticketId;
        private readonly int _currentUserId;
        private readonly string _currentUserRole;
        private string _currentStatus;

        private readonly string[] _statusOptions = { "В ожидании", "В работе", "Выполнено", "Отклонено" };

        public DetailsForm(int ticketId, int currentUserId, string currentUserRole)
        {
            InitializeComponent();
            _ticketId = ticketId;
            _currentUserId = currentUserId;
            _currentUserRole = currentUserRole;
        }

        private void DetailsForm_Load(object sender, EventArgs e)
        {
            LoadTicketDetails();
            LoadAssignedUsers();
            ApplyPermissions();
            cmbStatus.Items.AddRange(_statusOptions);
            cmbStatus.SelectedItem = _currentStatus;
            LoadComments();
        }

        private void LoadTicketDetails()
        {
            try
            {
                string query = "SELECT * FROM Tickets WHERE TicketID = @TicketID";
                SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@TicketID", _ticketId) };

                DataTable result = _dbHelper.ExecuteSqlQuery(query, parameters);

                if (result.Rows.Count == 0)
                {
                    MessageBox.Show("Заявка не найдена.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                DataRow row = result.Rows[0];

                txtTicketNumber.Text = row["TicketID"].ToString();
                txtEquipmentName.Text = row["EquipmentName"].ToString();
                txtFaultTypeName.Text = row["FaultTypeName"].ToString();
                txtDescription.Text = row["Description"].ToString();
                _currentStatus = row["StatusName"].ToString();

                if (row["AssignedUserID"] != DBNull.Value)
                {
                    // Если есть назначенный пользователь, выбираем его
                    cmbAssignedUser.SelectedValue = Convert.ToInt32(row["AssignedUserID"]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке деталей заявки: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadAssignedUsers()
        {
            try
            {
                string query = "SELECT UserID, FirstName, LastName FROM Users WHERE RoleName = 'Executor'";
                DataTable users = _dbHelper.ExecuteSqlQuery(query);

                cmbAssignedUser.DataSource = users;
                cmbAssignedUser.DisplayMember = "FirstName";
                cmbAssignedUser.ValueMember = "UserID";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке исполнителей: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyPermissions()
        {
            if (_currentUserRole == "Client")
            {
                cmbStatus.Enabled = false;
                cmbAssignedUser.Enabled = false;
                btnSave.Visible = false;
                txtDescription.ReadOnly = true;
                txtComment.Enabled = false;
                btnAddComment.Visible = false;
            }
            else
            {
                cmbStatus.Enabled = true;
                cmbAssignedUser.Enabled = true;
                btnSave.Visible = true;
                txtDescription.ReadOnly = false;
                txtComment.Enabled = true;
                btnAddComment.Visible = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string newStatus = cmbStatus.Text.Trim();
            int? assignedUserId = (cmbAssignedUser.SelectedValue != null) ? (int?)Convert.ToInt32(cmbAssignedUser.SelectedValue) : null;
            string newDescription = txtDescription.Text.Trim();

            if (string.IsNullOrEmpty(newStatus))
            {
                MessageBox.Show("Пожалуйста, выберите статус.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string query = "UPDATE Tickets SET StatusName = @StatusName, AssignedUserID = @AssignedUserID, Description = @Description WHERE TicketID = @TicketID";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@TicketID", _ticketId),
                    new SqlParameter("@StatusName", newStatus),
                    new SqlParameter("@AssignedUserID", assignedUserId ?? (object)DBNull.Value), // Если assignedUserId == null, то DBNull.Value
                    new SqlParameter("@Description", newDescription)
                };

                int rowsAffected = _dbHelper.ExecuteNonQuery(query, parameters);

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Заявка успешно обновлена.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Не удалось обновить заявку.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении заявки: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoadComments()
        {
            try
            {
                string query = @"
                    SELECT c.CommentText, u.FirstName, u.LastName, c.DateCreated
                    FROM Comments c
                    JOIN Users u ON c.UserID = u.UserID
                    WHERE c.TicketID = @TicketID
                    ORDER BY c.DateCreated DESC";

                SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@TicketID", _ticketId) };
                DataTable comments = _dbHelper.ExecuteSqlQuery(query, parameters);

                dgvComments.DataSource = comments;

                dgvComments.Columns["CommentText"].HeaderText = "Комментарий";
                dgvComments.Columns["FirstName"].HeaderText = "Имя";
                dgvComments.Columns["LastName"].HeaderText = "Фамилия";
                dgvComments.Columns["DateCreated"].HeaderText = "Дата";

                dgvComments.Columns["CommentText"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке комментариев: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddComment_Click(object sender, EventArgs e)
        {
            string commentText = txtComment.Text.Trim();
            if (string.IsNullOrEmpty(commentText))
            {
                MessageBox.Show("Пожалуйста, введите текст комментария.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string query = "INSERT INTO Comments (TicketID, UserID, CommentText, DateCreated) " +
                               "VALUES (@TicketID, @UserID, @CommentText, @DateCreated)";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@TicketID", _ticketId),
                    new SqlParameter("@UserID", _currentUserId),
                    new SqlParameter("@CommentText", commentText),
                    new SqlParameter("@DateCreated", DateTime.Now)
                };

                int rowsAffected = _dbHelper.ExecuteNonQuery(query, parameters);

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Комментарий успешно добавлен.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtComment.Clear();
                    LoadComments();
                }
                else
                {
                    MessageBox.Show("Не удалось добавить комментарий.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении комментария: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
