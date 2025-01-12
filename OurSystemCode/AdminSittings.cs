﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OurSystemCode
{
    public partial class AdminSittings : Form
    {
       

        private bool isDragging = false;
        private Point mouseOffset;
        public AdminSittings()
        {
            InitializeComponent();
            this.Size = new Size(811, 490);
        }

        String name;
        String role;
        public AdminSittings(String role,String name)
        {
            InitializeComponent();
            this.Size = new Size(811, 490);
            this.role = role;
            this.name = name;

        }


        private void panel3_Resize(object sender, EventArgs e)
        {
            OurSystemCode.Form1.ApplyRoundedCorners(panel3, 20);
        }

        private void panel7_Resize(object sender, EventArgs e)
        {
            OurSystemCode.Form1.ApplyRoundedCorners(panel7, 20);
        }

        private void panel8_Resize(object sender, EventArgs e)
        {
            OurSystemCode.Form1.ApplyRoundedCorners(panel8, 20);
        }

        private void panel6_Resize(object sender, EventArgs e)
        {
            OurSystemCode.Form1.ApplyRoundedCorners(panel6, 20);
        }

        private void panel5_Resize(object sender, EventArgs e)
        {
            
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel4_Resize(object sender, EventArgs e)
        {
            OurSystemCode.Form1.ApplyRoundedCorners(panel5, 20);
        }

       
        private void AdminSittings_Load_1(object sender, EventArgs e)
        {
         
            usernameBox.Text = name;
            userroleBox.Text = role;
            usernameBox.TabStop = false;
            userroleBox.TabStop = false;

            if (string.IsNullOrEmpty(role))
            {
                MessageBox.Show("Role is not set properly.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

         
            if ("EMPLOYEE".Equals(role, StringComparison.OrdinalIgnoreCase) || "IT".Equals(role, StringComparison.OrdinalIgnoreCase))
            {

                btnEmployeeMang.Visible = false;
                btnSittings.Location = new System.Drawing.Point(5, 509);
            }
            else
            {

                btnEmployeeMang.Visible = true;
                btnEmployeesTasks.Visible = false;
                btnEmployeeMang.Location = new System.Drawing.Point(5, 459);
                btnSittings.Location = new System.Drawing.Point(5, 509);
            }

            int cornerRadius = 20;
            Form1.ApplyRoundedCorners(this, cornerRadius);


            this.MouseDown += new MouseEventHandler(AdminSittings_MouseDown);
            this.MouseMove += new MouseEventHandler(AdminSittings_MouseMove);
            this.MouseUp += new MouseEventHandler(AdminSittings_MouseUp);

            toolTip1.SetToolTip(BackupBtn, "Database backup");
            toolTip1.SetToolTip(RestoreBtn, "Database restore");
            toolTip1.SetToolTip(button12, "Change your password");
            toolTip1.SetToolTip(button8, "Close applacation");
            toolTip1.SetToolTip(buttonMinimize, "Minimize window");

        }

        private void AdminSittings_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                mouseOffset = e.Location;
            }
        }


        private void AdminSittings_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                this.Left = this.Left + (e.X - mouseOffset.X);
                this.Top = this.Top + (e.Y - mouseOffset.Y);
            }
        }


        private void AdminSittings_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

      


        private void buttonMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            ForgetPassword changePass = new ForgetPassword();
            this.Hide();
            changePass.FormClosed += (s, args) => this.Show(); 
            changePass.Show();

        }

        private void BtnDashboard_Click(object sender, EventArgs e)
        {
            Dashboard dashboardScreen = new Dashboard(role, name);
            this.Hide();
            dashboardScreen.Show();
        }

        private void BtnDataEntry_Click(object sender, EventArgs e)
        {
            DataEntry dataEntryScreen = new DataEntry(role, name);
            this.Hide();
            dataEntryScreen.Show();
        }

        private void BtnReports_Click(object sender, EventArgs e)
        {
            Reports reportsScreen = new Reports(role, name);
            this.Hide();
            reportsScreen.Show();
        }

        private void BtnSuoliers_Click(object sender, EventArgs e)
        {
            Suppliers suppliersScreen = new Suppliers(role, name);
            this.Hide();
            suppliersScreen.Show();
        }

        private void BtnInventoryMan_Click(object sender, EventArgs e)
        {
            InventoryMan inventoryManScreen = new InventoryMan(role, name);
            this.Hide();
            inventoryManScreen.Show();
        }

        private void btnEmployeesTasks_Click(object sender, EventArgs e)
        {
            Employees_tasks EmployeesTasksScreen = new Employees_tasks(role, name);
            this.Hide();
            EmployeesTasksScreen.Show();
        }

        private void btnEmployeeMang_Click(object sender, EventArgs e)
        {
            Employees EmployeesScreen = new Employees(role, name);
            this.Hide();
            EmployeesScreen.Show();
        }

        private void btnSittings_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form1 logoutScreen = new Form1();
            this.Close();
            logoutScreen.Show();
        }

       
    }
}
