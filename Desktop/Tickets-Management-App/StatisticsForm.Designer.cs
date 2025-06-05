namespace Tickets_Management_App
{
    partial class StatisticsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblCompletedTickets = new System.Windows.Forms.Label();
            this.txtCompletedTickets = new System.Windows.Forms.TextBox();
            this.lblAverageCompletionTime = new System.Windows.Forms.Label();
            this.txtAverageCompletionTime = new System.Windows.Forms.TextBox();
            this.lblFaultTypeStatistics = new System.Windows.Forms.Label();
            this.dgvFaultTypeStatistics = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFaultTypeStatistics)).BeginInit();
            this.SuspendLayout();
            // 
            // lblCompletedTickets
            // 
            this.lblCompletedTickets.AutoSize = true;
            this.lblCompletedTickets.Location = new System.Drawing.Point(10, 10);
            this.lblCompletedTickets.Name = "lblCompletedTickets";
            this.lblCompletedTickets.Size = new System.Drawing.Size(180, 13);
            this.lblCompletedTickets.TabIndex = 0;
            this.lblCompletedTickets.Text = "Количество выполненных заявок:";
            // 
            // txtCompletedTickets
            // 
            this.txtCompletedTickets.Location = new System.Drawing.Point(200, 3);
            this.txtCompletedTickets.Name = "txtCompletedTickets";
            this.txtCompletedTickets.ReadOnly = true;
            this.txtCompletedTickets.Size = new System.Drawing.Size(150, 20);
            this.txtCompletedTickets.TabIndex = 1;
            // 
            // lblAverageCompletionTime
            // 
            this.lblAverageCompletionTime.AutoSize = true;
            this.lblAverageCompletionTime.Location = new System.Drawing.Point(10, 40);
            this.lblAverageCompletionTime.Name = "lblAverageCompletionTime";
            this.lblAverageCompletionTime.Size = new System.Drawing.Size(192, 13);
            this.lblAverageCompletionTime.TabIndex = 2;
            this.lblAverageCompletionTime.Text = "Среднее время выполнения заявки:";
            // 
            // txtAverageCompletionTime
            // 
            this.txtAverageCompletionTime.Location = new System.Drawing.Point(200, 33);
            this.txtAverageCompletionTime.Name = "txtAverageCompletionTime";
            this.txtAverageCompletionTime.ReadOnly = true;
            this.txtAverageCompletionTime.Size = new System.Drawing.Size(150, 20);
            this.txtAverageCompletionTime.TabIndex = 3;
            // 
            // lblFaultTypeStatistics
            // 
            this.lblFaultTypeStatistics.AutoSize = true;
            this.lblFaultTypeStatistics.Location = new System.Drawing.Point(10, 70);
            this.lblFaultTypeStatistics.Name = "lblFaultTypeStatistics";
            this.lblFaultTypeStatistics.Size = new System.Drawing.Size(203, 13);
            this.lblFaultTypeStatistics.TabIndex = 4;
            this.lblFaultTypeStatistics.Text = "Статистика по типам неисправностей:";
            // 
            // dgvFaultTypeStatistics
            // 
            this.dgvFaultTypeStatistics.AllowUserToAddRows = false;
            this.dgvFaultTypeStatistics.AllowUserToDeleteRows = false;
            this.dgvFaultTypeStatistics.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvFaultTypeStatistics.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFaultTypeStatistics.Location = new System.Drawing.Point(10, 100);
            this.dgvFaultTypeStatistics.Name = "dgvFaultTypeStatistics";
            this.dgvFaultTypeStatistics.ReadOnly = true;
            this.dgvFaultTypeStatistics.Size = new System.Drawing.Size(360, 150);
            this.dgvFaultTypeStatistics.TabIndex = 5;
            // 
            // StatisticsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(387, 261);
            this.Controls.Add(this.dgvFaultTypeStatistics);
            this.Controls.Add(this.lblFaultTypeStatistics);
            this.Controls.Add(this.txtAverageCompletionTime);
            this.Controls.Add(this.lblAverageCompletionTime);
            this.Controls.Add(this.txtCompletedTickets);
            this.Controls.Add(this.lblCompletedTickets);
            this.Name = "StatisticsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Статистика";
            this.Load += new System.EventHandler(this.StatisticsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFaultTypeStatistics)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCompletedTickets;
        private System.Windows.Forms.TextBox txtCompletedTickets;
        private System.Windows.Forms.Label lblAverageCompletionTime;
        private System.Windows.Forms.TextBox txtAverageCompletionTime;
        private System.Windows.Forms.Label lblFaultTypeStatistics;
        private System.Windows.Forms.DataGridView dgvFaultTypeStatistics;
    }
}