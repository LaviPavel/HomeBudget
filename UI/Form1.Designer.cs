using System;
using System.Windows.Forms;
using System.Windows.Media;

namespace UI
{
    partial class Form1
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.metroTabPage3 = new MetroFramework.Controls.MetroTabPage();
            this.metroTabPage2 = new MetroFramework.Controls.MetroTabPage();
            this.metroTabPage1 = new MetroFramework.Controls.MetroTabPage();
            this.ExpensesPie = new LiveCharts.WinForms.PieChart();
            this.BalanceGauge = new LiveCharts.WinForms.SolidGauge();
            this.MonthDataGrid = new MetroFramework.Controls.MetroGrid();
            this.Category = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.SubCategory = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ExpectedAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SpentAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.metroTabControl1 = new MetroFramework.Controls.MetroTabControl();
            this.metroTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MonthDataGrid)).BeginInit();
            this.metroTabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // metroTabPage3
            // 
            this.metroTabPage3.HorizontalScrollbarBarColor = true;
            this.metroTabPage3.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage3.HorizontalScrollbarSize = 10;
            this.metroTabPage3.Location = new System.Drawing.Point(4, 38);
            this.metroTabPage3.Name = "metroTabPage3";
            this.metroTabPage3.Size = new System.Drawing.Size(1109, 494);
            this.metroTabPage3.TabIndex = 2;
            this.metroTabPage3.Text = "Configuration";
            this.metroTabPage3.VerticalScrollbarBarColor = true;
            this.metroTabPage3.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage3.VerticalScrollbarSize = 10;
            // 
            // metroTabPage2
            // 
            this.metroTabPage2.HorizontalScrollbarBarColor = true;
            this.metroTabPage2.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage2.HorizontalScrollbarSize = 10;
            this.metroTabPage2.Location = new System.Drawing.Point(4, 38);
            this.metroTabPage2.Name = "metroTabPage2";
            this.metroTabPage2.Size = new System.Drawing.Size(1109, 494);
            this.metroTabPage2.TabIndex = 1;
            this.metroTabPage2.Text = "History&Statistics";
            this.metroTabPage2.VerticalScrollbarBarColor = true;
            this.metroTabPage2.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage2.VerticalScrollbarSize = 10;
            // 
            // metroTabPage1
            // 
            this.metroTabPage1.Controls.Add(this.ExpensesPie);
            this.metroTabPage1.Controls.Add(this.BalanceGauge);
            this.metroTabPage1.Controls.Add(this.MonthDataGrid);
            this.metroTabPage1.HorizontalScrollbarBarColor = true;
            this.metroTabPage1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage1.HorizontalScrollbarSize = 10;
            this.metroTabPage1.Location = new System.Drawing.Point(4, 38);
            this.metroTabPage1.Name = "metroTabPage1";
            this.metroTabPage1.Size = new System.Drawing.Size(1109, 494);
            this.metroTabPage1.TabIndex = 0;
            this.metroTabPage1.Text = "February";
            this.metroTabPage1.VerticalScrollbarBarColor = true;
            this.metroTabPage1.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage1.VerticalScrollbarSize = 10;
            this.metroTabPage1.Click += new System.EventHandler(this.metroTabPage1_Click);
            // 
            // ExpensesPie
            // 
            this.ExpensesPie.Location = new System.Drawing.Point(741, 251);
            this.ExpensesPie.Name = "ExpensesPie";
            this.ExpensesPie.Size = new System.Drawing.Size(350, 240);
            this.ExpensesPie.TabIndex = 4;
            this.ExpensesPie.Text = "ExpensesPie";
            this.ExpensesPie.ChildChanged += new System.EventHandler<System.Windows.Forms.Integration.ChildChangedEventArgs>(this.ExpensesPie_ChildChanged_1);
            // 
            // BalanceGauge
            // 
            this.BalanceGauge.AccessibleName = "Balance";
            this.BalanceGauge.Location = new System.Drawing.Point(741, 19);
            this.BalanceGauge.Name = "BalanceGauge";
            this.BalanceGauge.Size = new System.Drawing.Size(350, 206);
            this.BalanceGauge.TabIndex = 3;
            this.BalanceGauge.Text = "Balance";
            this.BalanceGauge.ChildChanged += new System.EventHandler<System.Windows.Forms.Integration.ChildChangedEventArgs>(this.BalanceGauge_ChildChanged_1);
            // 
            // MonthDataGrid
            // 
            this.MonthDataGrid.AllowUserToOrderColumns = true;
            this.MonthDataGrid.AllowUserToResizeRows = false;
            this.MonthDataGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.MonthDataGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.MonthDataGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.MonthDataGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.MonthDataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.MonthDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.MonthDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Category,
            this.SubCategory,
            this.ExpectedAmount,
            this.SpentAmount});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.MonthDataGrid.DefaultCellStyle = dataGridViewCellStyle2;
            this.MonthDataGrid.EnableHeadersVisualStyles = false;
            this.MonthDataGrid.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.MonthDataGrid.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.MonthDataGrid.Location = new System.Drawing.Point(0, 70);
            this.MonthDataGrid.Name = "MonthDataGrid";
            this.MonthDataGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.MonthDataGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.MonthDataGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.MonthDataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.MonthDataGrid.Size = new System.Drawing.Size(704, 424);
            this.MonthDataGrid.TabIndex = 2;
            this.MonthDataGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.monthDataGrid_CellContentClick_1);
            this.MonthDataGrid.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.monthDataGrid_CellEndEdit);
            this.MonthDataGrid.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.monthDataGrid_RowsRemoved);

            this.MonthDataGrid.Columns[2].ValueType = typeof(int);
            this.MonthDataGrid.Columns[3].ValueType = typeof(int);
            // 
            // Category
            // 
            this.Category.HeaderText = "Category";
            this.Category.Name = "Category";
            this.Category.Width = 150;
            // 
            // SubCategory
            // 
            this.SubCategory.HeaderText = "Sub Category";
            this.SubCategory.Name = "SubCategory";
            this.SubCategory.Width = 150;
            this.SubCategory.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            // 
            // ExpectedAmount
            // 
            this.ExpectedAmount.HeaderText = "Expected Amount";
            this.ExpectedAmount.Name = "ExpectedAmount";
            this.ExpectedAmount.Width = 150;
            // 
            // SpentAmount
            // 
            this.SpentAmount.HeaderText = "Spent Amount";
            this.SpentAmount.Name = "SpentAmount";
            this.SpentAmount.Width = 150;
            // 
            // metroTabControl1
            // 
            this.metroTabControl1.Controls.Add(this.metroTabPage1);
            this.metroTabControl1.Controls.Add(this.metroTabPage2);
            this.metroTabControl1.Controls.Add(this.metroTabPage3);
            this.metroTabControl1.Location = new System.Drawing.Point(0, 63);
            this.metroTabControl1.Name = "metroTabControl1";
            this.metroTabControl1.SelectedIndex = 0;
            this.metroTabControl1.Size = new System.Drawing.Size(1117, 536);
            this.metroTabControl1.TabIndex = 0;
            this.metroTabControl1.UseSelectable = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1118, 602);
            this.Controls.Add(this.metroTabControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.metroTabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MonthDataGrid)).EndInit();
            this.metroTabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroTabPage metroTabPage3;
        private MetroFramework.Controls.MetroTabPage metroTabPage2;
        private MetroFramework.Controls.MetroTabPage metroTabPage1;
        private LiveCharts.WinForms.PieChart ExpensesPie;
        private LiveCharts.WinForms.SolidGauge BalanceGauge;
        private MetroFramework.Controls.MetroGrid MonthDataGrid;
        private MetroFramework.Controls.MetroTabControl metroTabControl1;
        private System.Windows.Forms.DataGridViewComboBoxColumn Category;
        private System.Windows.Forms.DataGridViewComboBoxColumn SubCategory;
        private System.Windows.Forms.DataGridViewTextBoxColumn ExpectedAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn SpentAmount;
    }
}

