﻿using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace OurSystemCode
{
    public partial class Form1 : Form
    {
        DatabaseOperations databaseOperation = new DatabaseOperations();
        String query;

        private bool isDragging = false;
        private Point mouseOffset;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int cornerRadius = 20;
            ApplyRoundedCorners(this, cornerRadius);
            this.StartPosition = FormStartPosition.CenterScreen;

            this.MouseDown += new MouseEventHandler(FormLogin_MouseDown);
            this.MouseMove += new MouseEventHandler(FormLogin_MouseMove);
            this.MouseUp += new MouseEventHandler(FormLogin_MouseUp);

            if (Properties.Settings.Default.UserEmail != string.Empty)
            {
                textBoxEmail.Text = Properties.Settings.Default.UserEmail;
                textBoxPassword.Text = Properties.Settings.Default.UserPassword;
                checkBoxRemember.Checked = true;
            }
        }

        public static void ApplyRoundedCorners(Control control, int cornerRadius)
        {
            GraphicsPath path = new GraphicsPath();
            path.StartFigure();
            path.AddArc(0, 0, cornerRadius, cornerRadius, 180, 90);
            path.AddArc(control.Width - cornerRadius, 0, cornerRadius, cornerRadius, 270, 90);
            path.AddArc(control.Width - cornerRadius, control.Height - cornerRadius, cornerRadius, cornerRadius, 0, 90);
            path.AddArc(0, control.Height - cornerRadius, cornerRadius, cornerRadius, 90, 90);
            control.Region = new Region(path);
        }

        public void FormLogin_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                mouseOffset = e.Location;
            }
        }

        public void FormLogin_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                this.Left = this.Left + (e.X - mouseOffset.X);
                this.Top = this.Top + (e.Y - mouseOffset.Y);
            }
        }

        public void FormLogin_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        private void panel1_Resize(object sender, EventArgs e)
        {
            int cornerRadius = 20;
            ApplyRoundedCorners(panelBlogin, cornerRadius);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonLogin_Paint(object sender, PaintEventArgs e)
        {
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (textBoxPassword.PasswordChar != '\0')
            {
                textBoxPassword.PasswordChar = '\0';
            }
            else
            {
                textBoxPassword.PasswordChar = '*';
            }
        }

        private void Forget_Click(object sender, EventArgs e)
        {
            ForgetPassword forget = new ForgetPassword();
            forget.FormClosed += (s, args) => this.Show();
            forget.Show();
            this.Hide();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            if (checkBoxRemember.Checked == true)
            {
                Properties.Settings.Default.UserEmail = textBoxEmail.Text;
                Properties.Settings.Default.UserPassword = textBoxPassword.Text;
                Properties.Settings.Default.RememberMe = true;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.UserEmail = "";
                Properties.Settings.Default.UserPassword = "";
                Properties.Settings.Default.RememberMe = false;
                Properties.Settings.Default.Save();
            }

            try
            {
                query = $"SELECT * FROM whms_schema.Users WHERE Email = '{textBoxEmail.Text}'";
                DataSet ds = databaseOperation.getData(query);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    string storedPassword = ds.Tables[0].Rows[0]["Password"].ToString();
                    if (storedPassword == textBoxPassword.Text)
                    {
                        string name = ds.Tables[0].Rows[0]["UserName"].ToString();
                        string role = ds.Tables[0].Rows[0]["Role"].ToString();
                        Console.WriteLine("Role: " + role);

                        if (string.Equals(role, "Administrator", StringComparison.OrdinalIgnoreCase))
                        {
                            Console.WriteLine("Admin role detected.");
                        }
                        else if (string.Equals(role, "employee", StringComparison.OrdinalIgnoreCase))
                        {
                            Console.WriteLine("Employee role detected.");
                        }

                        Console.WriteLine("User Role: " + role);
                        Int64 appUserPK = Int64.Parse(ds.Tables[0].Rows[0][0].ToString());
                        Dashboard mdash = new Dashboard(role, name);
                        mdash.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Incorrect password. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Bad Credentials. Email not found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in btnLogin Click : " + ex);
                MessageBox.Show("Something went wrong: " + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void textBoxEmail_TextChanged(object sender, EventArgs e)
        {
            ClearSettingsIfEmpty();
        }

        private void textBoxPassword_TextChanged(object sender, EventArgs e)
        {
            ClearSettingsIfEmpty();
        }

        private void ClearSettingsIfEmpty()
        {
            if (string.IsNullOrWhiteSpace(textBoxEmail.Text) && string.IsNullOrWhiteSpace(textBoxPassword.Text))
            {
                Properties.Settings.Default.UserEmail = "";
                Properties.Settings.Default.UserPassword = "";
                Properties.Settings.Default.RememberMe = false;
                Properties.Settings.Default.Save();
            }
        }
    }
}
