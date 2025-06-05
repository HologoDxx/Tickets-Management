using Microsoft.VisualStudio.TestTools.UnitTesting;
using TicketsManagement;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Reflection;
using Tickets_Management_App;

namespace TicketsManagement.Tests
{
    internal class MockDatabaseHelper : DatabaseHelper
    {
        public Func<string, SqlParameter[], DataTable> ExecuteSqlQueryFunc { get; set; }

        public MockDatabaseHelper()
        {
            ExecuteSqlQueryFunc = (query, parameters) => { return null; };
        }

        public virtual DataTable ExecuteSqlQuery(string query, SqlParameter[] parameters)
        {
            return ExecuteSqlQueryFunc(query, parameters);
        }
    }

    [TestClass]
    public class StatisticsFormTests
    {
        [TestMethod]
        public void LoadStatistics_WithCompletedTickets_UpdatesCompletedTicketsText()
        {

            DataTable completedTicketsResult = new DataTable();
            completedTicketsResult.Columns.Add("StatusName", typeof(string));
            completedTicketsResult.Columns.Add("COUNT(*)", typeof(int));
            completedTicketsResult.Rows.Add("Выполнено", 5);

            var mockDbHelper = new MockDatabaseHelper();
            mockDbHelper.ExecuteSqlQueryFunc = (query, parameters) =>
            {
                if (query.Contains("SELECT StatusName, COUNT(*) FROM Tickets GROUP BY StatusName"))
                {
                    return completedTicketsResult;
                }
                return null;
            };

            StatisticsForm statisticsForm = new StatisticsForm();

            var fieldInfo = statisticsForm.GetType().GetField("_dbHelper", BindingFlags.NonPublic | BindingFlags.Instance);

            if (fieldInfo != null)
            {
                fieldInfo.SetValue(statisticsForm, mockDbHelper);
            }
            else
            {
                Assert.Fail("_dbHelper field not found");
            }

            statisticsForm.Controls.Add(new TextBox() { Name = "txtCompletedTickets" });

            statisticsForm.GetType().GetMethod("LoadStatistics", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).Invoke(statisticsForm, null);

            Assert.AreEqual("2", statisticsForm.Controls["txtCompletedTickets"].Text);
        }

        [TestMethod]
        public void LoadStatistics_NoCompletedTickets_UpdatesAverageCompletionTimeText()
        {

            DataTable completedTicketsResult = new DataTable();
            completedTicketsResult.Columns.Add("StatusName", typeof(string));
            completedTicketsResult.Columns.Add("COUNT(*)", typeof(int));
            completedTicketsResult.Rows.Add("Выполнено", 0);

            DataTable averageCompletionTimeResult = new DataTable();
            averageCompletionTimeResult.Columns.Add("DateCreated", typeof(DateTime));
            averageCompletionTimeResult.Columns.Add("DateCompleted", typeof(DateTime));
            averageCompletionTimeResult.Columns.Add("StatusName", typeof(string));

            var mockDbHelper = new MockDatabaseHelper();
            mockDbHelper.ExecuteSqlQueryFunc = (query, parameters) =>
            {
                if (query.Contains("SELECT StatusName, COUNT(*) FROM Tickets GROUP BY StatusName"))
                {
                    return completedTicketsResult;
                }
                if (query.Contains(@"SELECT DateCreated, DateCompleted, StatusName FROM Tickets"))
                {
                    return averageCompletionTimeResult;
                }
                return null;
            };

            StatisticsForm statisticsForm = new StatisticsForm();

            var fieldInfo = statisticsForm.GetType().GetField("_dbHelper", BindingFlags.NonPublic | BindingFlags.Instance);

            if (fieldInfo != null)
            {
                fieldInfo.SetValue(statisticsForm, mockDbHelper);
            }
            else
            {
                Assert.Fail("_dbHelper field not found");
            }

            statisticsForm.Controls.Add(new TextBox() { Name = "txtAverageCompletionTime" });

            statisticsForm.GetType().GetMethod("LoadStatistics", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).Invoke(statisticsForm, null);

            Assert.AreEqual("2,5 дней", statisticsForm.Controls["txtAverageCompletionTime"].Text);
        }

        [TestMethod]
        public void LoadStatistics_WithFaultTypeStatistics_DataGridViewNotEmpty()
        {

            DataTable completedTicketsResult = new DataTable();
            completedTicketsResult.Columns.Add("StatusName", typeof(string));
            completedTicketsResult.Columns.Add("COUNT(*)", typeof(int));
            completedTicketsResult.Rows.Add("Выполнено", 0);

            DataTable averageCompletionTimeResult = new DataTable();
            averageCompletionTimeResult.Columns.Add("DateCreated", typeof(DateTime));
            averageCompletionTimeResult.Columns.Add("DateCompleted", typeof(DateTime));
            averageCompletionTimeResult.Columns.Add("StatusName", typeof(string));

            DataTable faultTypeStatisticsResult = new DataTable();
            faultTypeStatisticsResult.Columns.Add("Тип неисправности", typeof(string));
            faultTypeStatisticsResult.Columns.Add("Количество", typeof(int));
            faultTypeStatisticsResult.Rows.Add("Fault1", 2);

            var mockDbHelper = new MockDatabaseHelper();
            mockDbHelper.ExecuteSqlQueryFunc = (query, parameters) =>
            {
                if (query.Contains("SELECT StatusName, COUNT(*) FROM Tickets GROUP BY StatusName"))
                {
                    return completedTicketsResult;
                }
                if (query.Contains(@"SELECT DateCreated, DateCompleted, StatusName FROM Tickets"))
                {
                    return averageCompletionTimeResult;
                }
                if (query.Contains("SELECT FaultTypeName AS 'Тип неисправности', COUNT(*) AS 'Количество'"))
                {
                    return faultTypeStatisticsResult;
                }
                return null;
            };
            StatisticsForm statisticsForm = new StatisticsForm();

            var fieldInfo = statisticsForm.GetType().GetField("_dbHelper", BindingFlags.NonPublic | BindingFlags.Instance);

            if (fieldInfo != null)
            {
                fieldInfo.SetValue(statisticsForm, mockDbHelper);
            }
            else
            {
                Assert.Fail("_dbHelper field not found");
            }

            statisticsForm.Controls.Add(new DataGridView() { Name = "dgvFaultTypeStatistics" });

            statisticsForm.GetType().GetMethod("LoadStatistics", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).Invoke(statisticsForm, null);

            Assert.IsTrue(statisticsForm.Controls.ContainsKey("dgvFaultTypeStatistics"));
            DataGridView dgv = (DataGridView)statisticsForm.Controls["dgvFaultTypeStatistics"];
            Assert.AreEqual(2, dgv.Rows.Count);
        }
    }
}