﻿//hi i am raghad
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OurSystemCode.Properties;
//using WMS;


namespace OurSystemCode
{
    public partial class Dashboard : Form
    {
            
        private bool isDragging = false;
        private Point mouseOffset;

        public Dashboard()
        {
            InitializeComponent();
            this.Size = new Size(811, 490);
            this.StartPosition = FormStartPosition.CenterScreen;

        }

        String name;
        String role;
        public Dashboard(String role , String name)
        {
            InitializeComponent();
            this.Size = new Size(811, 490); 
            this.role = role;
            this.name = name;
            
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            usernameBox.Text = name;
            userroleBox.Text= role;
            usernameBox.TabStop = false;
            userroleBox.TabStop = false;

            UpdateItemCount();
            UpdateCatogiryCount();
            LowStockCount();
            UpcomingCount();

            int cornerRadius = 20;
            Form1.ApplyRoundedCorners(this, cornerRadius);

          
            this.MouseDown += new MouseEventHandler(Dash_MouseDown);
            this.MouseMove += new MouseEventHandler(Dash_MouseMove);
            this.MouseUp += new MouseEventHandler(Dash_MouseUp);

         
            if (string.IsNullOrEmpty(role))
            {
                MessageBox.Show("Role is not set properly.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

           
            if ("EMPLOYEE".Equals(role, StringComparison.OrdinalIgnoreCase))
            {

                DashTitle.Text = "Employee Dashboard ";
                btnEmployeeMang.Visible = false;
                btnSittings.Location = new System.Drawing.Point(5, 459);
            }
            else if ("IT".Equals(role, StringComparison.OrdinalIgnoreCase))
            {

                DashTitle.Text = "IT Dashboard ";
                btnEmployeeMang.Visible = false;
                btnSittings.Location = new System.Drawing.Point(5, 459);
            }
            else
            {

                DashTitle.Text = "Welcome Admin !";
                btnEmployeeMang.Visible = true;
                btnSittings.Location = new System.Drawing.Point(5, 509);
            }


        }

        private void Dash_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                mouseOffset = e.Location;  
            }
        }

      
        private void Dash_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging) 
            {
                this.Left = this.Left + (e.X - mouseOffset.X);  
                this.Top = this.Top + (e.Y - mouseOffset.Y);    
            }
        }

       
        private void Dash_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;  
        }

        private void UpdateItemCount()
        {
            try
            {
               
                string query = "SELECT COUNT(*) FROM whms_schema.Item";

                
                DatabaseOperations dbOps = new DatabaseOperations();
                DataSet ds = dbOps.getData(query);

                
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    int totalItems = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                    TotalItemsBox.Text = totalItems.ToString();  
                }
                else
                {
                    TotalItemsBox.Text = "Null";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void UpcomingCount()
        {
            try
            {

                string query = "SELECT COUNT(*) FROM whms_schema.PurchaseOrders  WHERE CAST(ExpectedDeliveryDate AS DATE) = CAST(GETDATE() AS DATE)";


                DatabaseOperations dbOps = new DatabaseOperations();
                DataSet ds = dbOps.getData(query);


                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    int IntUpcomingCount = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                    UpcomingDelBox.Text = IntUpcomingCount.ToString();
                }
                else
                {
                    UpcomingDelBox.Text = "Null";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void UpdateCatogiryCount()
        {
            try
            {
                
                string query = "SELECT COUNT(*) FROM whms_schema.Categories";

                
                DatabaseOperations dbOps = new DatabaseOperations();
                DataSet ds = dbOps.getData(query);

                
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    int totalCategory = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                    CategoriesBox.Text = totalCategory.ToString();  
                }
                else
                {
                    CategoriesBox.Text = "Null";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void LowStockCount()
        {
            try
            {

                string query = "SELECT * FROM whms_schema.Item WHERE Quantity < 50";


                DatabaseOperations dbOps = new DatabaseOperations();
                DataSet ds = dbOps.getData(query);


                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    int LowStockCount = ds.Tables[0].Rows.Count;
                    LowStockBox.Text = LowStockCount.ToString();
                }
                else
                {
                    LowStockBox.Text = "Null";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            InventoryMan InventoryManScreen = new InventoryMan(role , name);
            this.Hide();
            InventoryManScreen.Show();
        }

        private void BtnDashboard_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataEntry DataEntryScreen = new DataEntry(role,name);
            this.Hide();
            DataEntryScreen.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Reports ReportsScreen = new Reports(role,name);
            this.Hide();
            ReportsScreen.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Suppliers SuppliersScreen = new Suppliers(role,name);
            this.Hide();
            SuppliersScreen.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Employees EmployeesScreen = new Employees(role , name);
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
                Sittings SittingsScreen = new Sittings(role , name);
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

        private void BtnDashboard_Leave(object sender, EventArgs e)
        {
          
        }

        private void button1_Leave(object sender, EventArgs e)
        {
            button1.BackColor = Color.Black;
        }

        private void panel3_Resize(object sender, EventArgs e)
        {
            OurSystemCode.Form1.ApplyRoundedCorners(panel3, 20);
        }

        private void panel4_Resize(object sender, EventArgs e)
        {
            OurSystemCode.Form1.ApplyRoundedCorners(panel4, 20);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel5_Resize(object sender, EventArgs e)
        {
            OurSystemCode.Form1.ApplyRoundedCorners(panel5, 20);
        }

        private void panel6_Resize(object sender, EventArgs e)
        {
            OurSystemCode.Form1.ApplyRoundedCorners(panel6, 20);
        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {
            OurSystemCode.Form1.ApplyRoundedCorners(panel7, 20);
        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {
            OurSystemCode.Form1.ApplyRoundedCorners(panel8, 20);
        }

        private void panel11_Paint(object sender, PaintEventArgs e)
        {
            OurSystemCode.Form1.ApplyRoundedCorners(panel11, 20);
        }

        private void panel12_Paint(object sender, PaintEventArgs e)
        {
            OurSystemCode.Form1.ApplyRoundedCorners(panel12, 20);
        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {
            OurSystemCode.Form1.ApplyRoundedCorners(panel9, 20);
        }

        private void panel10_Paint(object sender, PaintEventArgs e)
        {
            OurSystemCode.Form1.ApplyRoundedCorners(panel10, 20);
        }

        private void panel13_Paint(object sender, PaintEventArgs e)
        {
            OurSystemCode.Form1.ApplyRoundedCorners(panel13, 20);
        }

        private void panel14_Paint(object sender, PaintEventArgs e)
        {
            OurSystemCode.Form1.ApplyRoundedCorners(panel14, 20);
        }

        private void panel15_Paint(object sender, PaintEventArgs e)
        {
            OurSystemCode.Form1.ApplyRoundedCorners(panel15, 20);
        }

        private void buttonMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form1 LogoutScreen = new Form1();
            this.Close();
           LogoutScreen.Show();
        }

        private void usernameBox_TextChanged(object sender, EventArgs e)
        {
            
        }

   
    }



}

        

