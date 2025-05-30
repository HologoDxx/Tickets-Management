using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tickets_Management_App
{
    public partial class StatisticsForm : Form
    {
        private readonly DatabaseHelper _dbHelper = new DatabaseHelper();

        public StatisticsForm()
        {
            InitializeComponent();
        }

        private void StatisticsForm_Load(object sender, EventArgs e)
        {
            LoadStatistics();
        }

        private void LoadStatistics()
        {
            try
            {
                string completedTicketsQuery = "SELECT StatusName, COUNT(*) FROM Tickets GROUP BY StatusName";
                DataTable completedTicketsResult = _dbHelper.ExecuteSqlQuery(completedTicketsQuery);
                int completedCount = 0;

                foreach (DataRow row in completedTicketsResult.Rows)
                {
                    if (row["StatusName"].ToString() == "Выполнено")
                    {
                        completedCount = Convert.ToInt32(row[1]);
                        break;
                    }
                }
                txtCompletedTickets.Text = completedCount.ToString();

                string averageCompletionTimeQuery = @"
                    SELECT DateCreated, DateCompleted, StatusName FROM Tickets";
                DataTable averageCompletionTimeResult = _dbHelper.ExecuteSqlQuery(averageCompletionTimeQuery);
                double totalDays = 0;
                int validTickets = 0;

                foreach (DataRow row in averageCompletionTimeResult.Rows)
                {
                    if (row["StatusName"].ToString() == "Выполнено" && row["DateCompleted"] != DBNull.Value)
                    {
                        DateTime dateCreated = Convert.ToDateTime(row["DateCreated"]);
                        DateTime dateCompleted = Convert.ToDateTime(row["DateCompleted"]);
                        totalDays += (dateCompleted - dateCreated).TotalDays;
                        validTickets++;
                    }
                }

                if (validTickets > 0)
                {
                    double averageDays = totalDays / validTickets;
                    txtAverageCompletionTime.Text = Math.Round(averageDays, 2).ToString() + " дней";
                }
                else
                {
                    txtAverageCompletionTime.Text = "Нет данных";
                }

                string faultTypeStatisticsQuery = @"
                    SELECT FaultTypeName AS 'Тип неисправности', COUNT(*) AS 'Количество'
                    FROM Tickets
                    GROUP BY FaultTypeName
                    ORDER BY COUNT(*) DESC"; //Сортируем по убыванию количества
                DataTable faultTypeStatisticsResult = _dbHelper.ExecuteSqlQuery(faultTypeStatisticsQuery);
                dgvFaultTypeStatistics.DataSource = faultTypeStatisticsResult;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке статистики: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
