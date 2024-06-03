using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Shopping_Management_System
{
    public partial class Register : Form
       
    {
        string image_location;
        public Register()
        {
            InitializeComponent();
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            ResetFields("Username", textBox_username, pic_user, panel1, Color.FromArgb(255, 255, 255), Properties.Resources.username_logo2);
            ResetFields("Email", textBox_email, pic_email, panel2, Color.FromArgb(245, 173, 91), Properties.Resources.email_icon2);
            ResetPasswordField("Password", textBox_pass, pic_pas, panel3, Color.FromArgb(245, 173, 91), Properties.Resources.pass_icon);
            ResetPasswordField("Re-Enter Passsword", textBox_re_pass, pic_pas2, panel4, Color.FromArgb(245, 173, 91), Properties.Resources.pass_icon);
            ResetFields("Address", textBox_address, pic_address, panel5, Color.FromArgb(245, 173, 91), Properties.Resources.address_icon2);
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            ResetFields("Username", textBox_username, pic_user, panel1, Color.FromArgb(245, 173, 91), Properties.Resources.username_logo);
            ResetFields("Email", textBox_email, pic_email, panel2, Color.FromArgb(255, 255, 255), Properties.Resources.email_icon);
            ResetPasswordField("Password", textBox_pass, pic_pas, panel3, Color.FromArgb(245, 173, 91), Properties.Resources.pass_icon);
            ResetPasswordField("Re-Enter Passsword", textBox_re_pass, pic_pas2, panel4, Color.FromArgb(245, 173, 91), Properties.Resources.pass_icon);
            ResetFields("Address", textBox_address, pic_address, panel5, Color.FromArgb(245, 173, 91), Properties.Resources.address_icon2);
        }

        private void textBox3_Click(object sender, EventArgs e)
        {
            ResetFields("Username", textBox_username, pic_user, panel1, Color.FromArgb(245, 173, 91), Properties.Resources.username_logo);
            ResetFields("Email", textBox_email, pic_email, panel2, Color.FromArgb(245, 173, 91), Properties.Resources.email_icon2);
            ResetPasswordField("Password", textBox_pass, pic_pas, panel3, Color.FromArgb(255, 255, 255), Properties.Resources.pass_icon2);
            ResetPasswordField("Re-Enter Passsword", textBox_re_pass, pic_pas2, panel4, Color.FromArgb(245, 173, 91), Properties.Resources.pass_icon);
            ResetFields("Address", textBox_address, pic_address, panel5, Color.FromArgb(245, 173, 91), Properties.Resources.address_icon2);
        }

        private void textBox4_Click(object sender, EventArgs e)
        {
            ResetFields("Username", textBox_username, pic_user, panel1, Color.FromArgb(245, 173, 91), Properties.Resources.username_logo);
            ResetFields("Email", textBox_email, pic_email, panel2, Color.FromArgb(245, 173, 91), Properties.Resources.email_icon2);
            ResetPasswordField("Password", textBox_pass, pic_pas, panel3, Color.FromArgb(245, 173, 91), Properties.Resources.pass_icon);
            ResetPasswordField("Re-Enter Passsword", textBox_re_pass, pic_pas2, panel4, Color.FromArgb(255, 255, 255), Properties.Resources.pass_icon2);
            ResetFields("Address", textBox_address, pic_address, panel5, Color.FromArgb(245, 173, 91), Properties.Resources.address_icon2);
        }

        private void textBox5_Click(object sender, EventArgs e)
        {
            ResetFields("Username", textBox_username, pic_user, panel1, Color.FromArgb(245, 173, 91), Properties.Resources.username_logo);
            ResetFields("Address", textBox_address, pic_address, panel5, Color.FromArgb(255, 255, 255), Properties.Resources.address_icon);
            ResetPasswordField("Password", textBox_pass, pic_pas, panel3, Color.FromArgb(245, 173, 91), Properties.Resources.pass_icon);
            ResetPasswordField("Re-Enter Passsword", textBox_re_pass, pic_pas2, panel4, Color.FromArgb(245, 173, 91), Properties.Resources.pass_icon);
            ResetFields("Email", textBox_email, pic_email, panel2, Color.FromArgb(245, 173, 91), Properties.Resources.email_icon2);
        }

        private void ResetFields(string defaultText, TextBox textBox, PictureBox pictureBox, Panel panel, Color color, Image image)
        {
            if (textBox.Text == defaultText)
            {
                textBox.Clear();
            }
            pictureBox.Image = image;
            panel.BackColor = color;
            textBox.ForeColor = color;
        }

        private void ResetPasswordField(string defaultText, TextBox textBox, PictureBox pictureBox, Panel panel, Color color, Image image)
        {
            if (textBox.Text == defaultText)
            {
                textBox.Clear();
                textBox.PasswordChar = '*';
            }
            pictureBox.Image = image;
            panel.BackColor = color;
            textBox.ForeColor = color;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox_pass.Text == textBox_re_pass.Text)
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=Shopping System;Integrated Security=True"))
                {
                    try
                    {
                        con.Open();
                        EnsureUserInfoTableExists(con);

                        SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM User_Info WHERE Name=@username", con);
                        cmd.Parameters.AddWithValue("@username", textBox_username.Text);
                        int result = Convert.ToInt32(cmd.ExecuteScalar());

                        if (result == 0)
                        {
                            bool gender = radioButton_female.Checked;
                            bool userType = radioButton_admin.Checked;

                            string query = "INSERT INTO User_Info (Picture, Name, Email, Address, Password, Gender, User_Type, Pocket_Money) " +
                                           "VALUES (@picture, @name, @Email, @address, @password, @gender, @userType, 0)";
                            cmd = new SqlCommand(query, con);
                            cmd.Parameters.AddWithValue("@picture", image_location ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@name", textBox_username.Text);
                            cmd.Parameters.AddWithValue("@Email", textBox_email.Text);
                            cmd.Parameters.AddWithValue("@address", textBox_address.Text);
                            cmd.Parameters.AddWithValue("@password", textBox_pass.Text);
                            cmd.Parameters.AddWithValue("@gender", gender);
                            cmd.Parameters.AddWithValue("@userType", userType);

                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Registration successful!");

                            hello_window helloWindow = new hello_window();
                            helloWindow.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Username already exists. Please choose another username.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An error occurred: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Passwords do not match. Please enter the password correctly.");
            }
        }

        private void EnsureUserInfoTableExists(SqlConnection con)
        {
            string checkTableQuery = @"
                IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[User_Info]') AND type in (N'U'))
                BEGIN
                    CREATE TABLE User_Info (
                        UserID INT IDENTITY(1,1) PRIMARY KEY,
                        Picture NVARCHAR(MAX) NULL,
                        Name NVARCHAR(100) NOT NULL,
                        Email NVARCHAR(100) NOT NULL,
                        Address NVARCHAR(255) NULL,
                        Password NVARCHAR(100) NOT NULL,
                        Gender BIT NOT NULL,
                        User_Type BIT NOT NULL,
                        Pocket_Money DECIMAL(18, 2) NOT NULL DEFAULT 0
                    );
                END";
            SqlCommand cmd = new SqlCommand(checkTableQuery, con);
            cmd.ExecuteNonQuery();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            pictureBox_upload.Image = Properties.Resources.username_logo;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Images Files|*.JpG;*.JPEG;*.PNG";
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox_upload.Image = Image.FromFile(openFileDialog.FileName);
                image_location = openFileDialog.FileName.ToString();
            }
        }

        private void Register_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
