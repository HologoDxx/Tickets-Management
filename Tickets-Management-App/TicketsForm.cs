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
    public partial class TicketsForm : Form
    {
        private readonly DatabaseHelper _dbHelper = new DatabaseHelper();
        private readonly int _userID;
        private readonly string _roleName;

        public TicketsForm(int userID, string roleName)
        {
            InitializeComponent();
            _userID = userID;
            _roleName = roleName;

            btnAddTicket.Visible = _roleName == "Client";
        }

        private void TicketsForm_Load(object sender, EventArgs e)
        {
            LoadTickets();
        }

        private void LoadTickets()
        {
            try
            {
                string query = "";
                SqlParameter[] parameters = null;

                if (_roleName == "Client")
                {
                    query = "SELECT * FROM Tickets WHERE ClientID = @ClientID";
                    parameters = new SqlParameter[] { new SqlParameter("@ClientID", _userID) };
                }
                else
                {
                    query = "SELECT * FROM Tickets";
                }

                DataTable tickets = _dbHelper.ExecuteSqlQuery(query, parameters);
                dgvTickets.DataSource = tickets;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке заявок: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddTicket_Click(object sender, EventArgs e)
        {
            if (_roleName == "Client")
            {
                AddTicketForm addTicketForm = new AddTicketForm(_userID);
                DialogResult result = addTicketForm.ShowDialog();

                if (result == DialogResult.OK)
                {
                    LoadTickets();
                }
            }
            else
            {
                MessageBox.Show("Только клиенты могут добавлять заявки.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchTickets();
        }

        private void SearchTickets()
        {
            string searchText = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(searchText))
            {
                LoadTickets();
                return;
            }

            try
            {
                string query = "SELECT * FROM Tickets WHERE EquipmentName LIKE @SearchText OR FaultTypeName LIKE @SearchText OR Description LIKE @SearchText";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@SearchText", "%" + searchText + "%")
                };

                if (_roleName == "Client")
                {
                    query += " AND ClientID = @ClientID";
                    Array.Resize(ref parameters, parameters.Length + 1);
                    parameters[parameters.Length - 1] = new SqlParameter("@ClientID", _userID);
                }

                DataTable tickets = _dbHelper.ExecuteSqlQuery(query, parameters);
                dgvTickets.DataSource = tickets;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при поиске заявок: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                SearchTickets();
            }
        }

        private void dgvTickets_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (dgvTickets.SelectedRows.Count > 0)
                {
                    int ticketId = Convert.ToInt32(dgvTickets.SelectedRows[0].Cells["TicketID"].Value);
                    DetailsForm detailsForm = new DetailsForm(ticketId, _userID, _roleName);
                    detailsForm.ShowDialog();
                    LoadTickets();
                }
            }
        }
    }
}
