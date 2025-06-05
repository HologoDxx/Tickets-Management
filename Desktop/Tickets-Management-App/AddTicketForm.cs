using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tickets_Management_App
{
    public partial class AddTicketForm : Form
    {
        private readonly DatabaseHelper _dbHelper = new DatabaseHelper();
        private readonly int _clientID;
        public AddTicketForm(int clientID)
        {
            InitializeComponent();
            _clientID = clientID;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string equipmentName = txtEquipmentName.Text.Trim();
            string faultTypeName = txtFaultTypeName.Text.Trim();
            string description = txtDescription.Text.Trim();
            string statusName = "В ожидании";

            if (string.IsNullOrEmpty(equipmentName) || string.IsNullOrEmpty(faultTypeName) || string.IsNullOrEmpty(description))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string query = "INSERT INTO Tickets (DateCreated, EquipmentName, FaultTypeName, Description, StatusName, ClientID) " +
                               "VALUES (@DateCreated, @EquipmentName, @FaultTypeName, @Description, @StatusName, @ClientID)";

                SqlParameter equipmentNameParam = new SqlParameter("@EquipmentName", SqlDbType.NVarChar);
                equipmentNameParam.Value = equipmentName;

                SqlParameter faultTypeNameParam = new SqlParameter("@FaultTypeName", SqlDbType.NVarChar);
                faultTypeNameParam.Value = faultTypeName;

                SqlParameter descriptionParam = new SqlParameter("@Description", SqlDbType.NVarChar);
                descriptionParam.Value = description;

                SqlParameter statusNameParam = new SqlParameter("@StatusName", SqlDbType.NVarChar);
                statusNameParam.Value = statusName;

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@DateCreated", DateTime.Now),
                    equipmentNameParam,
                    faultTypeNameParam,
                    descriptionParam,
                    statusNameParam,
                    new SqlParameter("@ClientID", _clientID)
                };

                int rowsAffected = _dbHelper.ExecuteNonQuery(query, parameters);

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Заявка успешно добавлена.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show("Не удалось добавить заявку.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении заявки: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
