using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace Shopping_Management_System
{
    public partial class hello_window : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=Shopping System;Integrated Security=True");

        public hello_window()
        {
            InitializeComponent();
        }

        private void textBox_username_Click(object sender, EventArgs e)
        {
            if (textBox_pass.TextLength == 0)
            {
                textBox_pass.Text = "Password";
                textBox_pass.PasswordChar = '\0';
            }
            if (textBox_username.Text == "Username")
            {
                textBox_username.Clear();
            }

            pic_user.Image = Properties.Resources.username_logo2;
            panel1.BackColor = Color.FromArgb(255, 255, 255);
            textBox_username.ForeColor = Color.FromArgb(255, 255, 255);

            pic_pas.Image = Properties.Resources.pass_icon;
            panel2.BackColor = Color.FromArgb(245, 173, 91);
            textBox_pass.ForeColor = Color.FromArgb(245, 173, 91);
        }

        private void textBox_pass_Click(object sender, EventArgs e)
        {
            if (textBox_username.TextLength == 0)
            {
                textBox_username.Text = "Username";
            }
            if (textBox_pass.Text == "Password")
            {
                textBox_pass.Clear();
                textBox_pass.PasswordChar = '*';
            }

            pic_pas.Image = Properties.Resources.pass_icon2;
            panel2.BackColor = Color.FromArgb(255, 255, 255);
            textBox_pass.ForeColor = Color.FromArgb(255, 255, 255);

            pic_user.Image = Properties.Resources.username_logo;
            panel1.BackColor = Color.FromArgb(245, 173, 91);
            textBox_username.ForeColor = Color.FromArgb(245, 173, 91);
        }

        private void button_sign_in_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();

                string query = "SELECT User_type FROM User_Info WHERE Name=@username AND Password=@password";
                using (SqlCommand com = new SqlCommand(query, con))
                {
                    com.Parameters.AddWithValue("@username", textBox_username.Text);
                    com.Parameters.AddWithValue("@password", textBox_pass.Text);

                    object result = com.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        int userType = Convert.ToInt32(result);
                        var.current_username = textBox_username.Text;

                        if (userType == 1) // Admin
                        {
                            Profile_Admin profile_Admin = new Profile_Admin();
                            profile_Admin.Show();
                        }
                        else // Regular user
                        {
                            Cart cart = new Cart();
                            cart.Show();
                            MessageBox.Show("Welcome" + var.current_username + " to our Online Shopping ^^");

                           // SoundPlayer splayer = new SoundPlayer(@"C:\Users\pc\Downloads\Shopping-Management-System-master\Shopping-Management-System-master\Shopping Management System\Shopping Management System/bells.wav");
                           // splayer.Play();
                        }
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Please check your username and password.");
                        ResetLoginFields();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
            finally
            {
                con.Close();
            }
        }



        private void button_register_Click(object sender, EventArgs e)
        {
            Register register = new Register();
            register.Show();
            this.Hide();
        }

        private void hello_window_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void ResetLoginFields()
        {
            textBox_username.Text = "Username";
            textBox_pass.Text = "Password";
            textBox_pass.PasswordChar = '\0';

            pic_pas.Image = Properties.Resources.pass_icon;
            panel2.BackColor = Color.FromArgb(245, 173, 91);
            textBox_pass.ForeColor = Color.FromArgb(245, 173, 91);

            pic_user.Image = Properties.Resources.username_logo;
            panel1.BackColor = Color.FromArgb(245, 173, 91);
            textBox_username.ForeColor = Color.FromArgb(245, 173, 91);
        }

        private void hello_window_Load(object sender, EventArgs e)
        {

        }
    }
}
