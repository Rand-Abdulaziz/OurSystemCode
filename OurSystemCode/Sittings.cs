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
    public partial class Sittings : Form
    {
        private bool isDragging = false;
        private Point mouseOffset;

        public Sittings()
        {
            InitializeComponent();
            this.Size = new Size(811, 490);
        }

        String name;
        String role;
        public Sittings(String role ,String name)
        {
            InitializeComponent();
            this.Size = new Size(811, 490);
            this.role = role;
            this.name = name;

        }
        private void panel7_Resize(object sender, EventArgs e)
        {
            OurSystemCode.Form1.ApplyRoundedCorners(panel7, 20);
        }

        private void panel6_Resize(object sender, EventArgs e)
        {
            OurSystemCode.Form1.ApplyRoundedCorners(panel6, 20);
        }

        private void panel5_Resize(object sender, EventArgs e)
        {
            OurSystemCode.Form1.ApplyRoundedCorners(panel5, 20);
        }

        private void panel4_Resize(object sender, EventArgs e)
        {
            OurSystemCode.Form1.ApplyRoundedCorners(panel4, 20);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BtnDashboard_Click(object sender, EventArgs e)
        {

            Dashboard DashboardScreen = new Dashboard(role, name);
            this.Hide();
            DashboardScreen.Show();
        }

        private void Sittings_Load(object sender, EventArgs e)
        {
            usernameBox.Text = name;
            userroleBox.Text = role;
            usernameBox.TabStop = false;
            userroleBox.TabStop = false;

            int cornerRadius = 20;
            Form1.ApplyRoundedCorners(this, cornerRadius);

            if (string.IsNullOrEmpty(role))
            {
                MessageBox.Show("Role is not set properly.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Console.WriteLine("SSSS Role: " + role);
            if ("EMPLOYEE".Equals(role, StringComparison.OrdinalIgnoreCase))
            {

                btnEmployeeMang.Visible = false;
                btnSittings.Location = new System.Drawing.Point(5, 459);
            }
            else
            {

                btnEmployeeMang.Visible = true;
                btnSittings.Location = new System.Drawing.Point(5, 509);
            }

            this.MouseDown += new MouseEventHandler(Sittings_MouseDown);
            this.MouseMove += new MouseEventHandler(Sittings_MouseMove);
            this.MouseUp += new MouseEventHandler(Sittings_MouseUp);

        }

        private void Sittings_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                mouseOffset = e.Location;
            }
        }


        private void Sittings_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                this.Left = this.Left + (e.X - mouseOffset.X);
                this.Top = this.Top + (e.Y - mouseOffset.Y);
            }
        }


        private void Sittings_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            DataEntry DataEntryScreen = new DataEntry(role, name);
            this.Hide();
            DataEntryScreen.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Reports ReportsScreen = new Reports(role, name);
            this.Hide();
            ReportsScreen.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Suppliers SuppliersScreen = new Suppliers(role, name);
            this.Hide();
            SuppliersScreen.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            InventoryMan InventoryManScreen = new InventoryMan(role, name);
            this.Hide();
            InventoryManScreen.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Employees EmployeesScreen = new Employees(role, name);
            this.Hide();
            EmployeesScreen.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(role))
            {
                MessageBox.Show("Role is not set properly.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if ("EMPLOYEE".Equals(role, StringComparison.OrdinalIgnoreCase))
            {
                Sittings SittingsScreen = new Sittings(role, name);
                this.Hide();
                SittingsScreen.Show();

            }
            else
            {

                AdminSittings ASittingsScreen = new AdminSittings(role, name);
                this.Hide();
                ASittingsScreen.Show();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form1 LogoutScreen = new Form1();
            this.Close();
            LogoutScreen.Show();
        }

        private void buttonMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
