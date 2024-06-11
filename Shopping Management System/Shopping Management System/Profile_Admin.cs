﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Xml.Linq;
namespace Shopping_Management_System
{
    public partial class Profile_Admin : Form
    {
        public Profile_Admin()
        {
            InitializeComponent();
            update_user_info();
            
           fill_categories_combobox();
        }

        private void Profile_Admin_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button_back_Click(object sender, EventArgs e)
        {
            Cart cart = new Cart();
            cart.Show();
            this.Hide();
        }

        private string usernametext;
        private string image_location;
        private bool pic_changed = false;
        private bool pic_isremoved = false;
        private bool gender = false;

        private void button2_editinfo_profile_Click(object sender, EventArgs e)
        {
            panel_add_category.Visible = false;
            panel_product.Visible = false;
            panel_edit_category.Visible = false;
            panel_categories.Visible = false;
            panel40_update_userinfo.Visible = true;

            using (SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=Shopping System;Integrated Security=True"))
            {
                con.Open();
                string query = "SELECT Picture, Name, Email, Address, Gender, Password, Pocket_Money FROM User_Info WHERE Password = @CurrentPassword AND Name = @CurrentUsername";
                using (SqlCommand com = new SqlCommand(query, con))
                {
                    com.Parameters.AddWithValue("@CurrentPassword", var.current_pass);
                    com.Parameters.AddWithValue("@CurrentUsername", var.current_username);

                    using (SqlDataReader dr = com.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            if (dr["Picture"] == DBNull.Value || string.IsNullOrEmpty(dr["Picture"].ToString()))
                            {
                                pictureBox6_update.Image = Properties.Resources.username_logo2;
                            }
                            else
                            {
                                pictureBox6_update.Image = Image.FromFile(dr["Picture"].ToString());
                            }

                            // Populate your controls with data from the reader
                            textBox_user_update.Text = dr["Name"].ToString();
                            textBox_email_update.Text = dr["Email"].ToString();
                            textBox_address_update.Text = dr["Address"].ToString();
                            gender = Convert.ToBoolean(dr["Gender"]);
                            textBox_pass_update.Text = dr["Password"].ToString();
                        }
                        else
                        {
                            // Handle the case where no data is returned
                            MessageBox.Show("No data found for the specified criteria.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }

            if (gender == false)
            {
                radioButton1_male_update.Checked = true;
            }
            else
            {
                radioButton2_female_update.Checked = true;
            }

            usernametext = textBox_user_update.Text;
        }

        private void button_update_userinfo_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=Shopping System;Integrated Security=True"))
                {
                    con.Open();

                    bool gender = radioButton1_male_update.Checked ? false : true;

                    if (textBox_pass_update.Text != textBox_repass_update.Text)
                    {
                        MessageBox.Show("Please enter password correctly");
                        return;
                    }

                    SqlCommand com;
                    string query;

                    if (usernametext != textBox_user_update.Text)
                    {
                        query = "SELECT COUNT(*) FROM User_Info WHERE Name = @UserName";
                        com = new SqlCommand(query, con);
                        com.Parameters.AddWithValue("@UserName", textBox_user_update.Text);
                        int result = Convert.ToInt32(com.ExecuteScalar());

                        if (result > 0)
                        {
                            MessageBox.Show("Username already exists. Please choose another one.");
                            return;
                        }
                    }

                    if (pic_changed && !pic_isremoved)
                    {
                        query = "UPDATE User_Info SET Picture = @ImageLocation, Name = @UserName, Email = @Email, Address = @Address, Password = @Password, Gender = @Gender WHERE Password = @CurrentPassword AND Name = @CurrentUserName";
                    }
                    else if (!pic_changed && !pic_isremoved)
                    {
                        query = "UPDATE User_Info SET Name = @UserName, Email = @Email, Address = @Address, Password = @Password, Gender = @Gender WHERE Password = @CurrentPassword AND Name = @CurrentUserName";
                    }
                    else
                    {
                        query = "UPDATE User_Info SET Picture = NULL, Name = @UserName, Email = @Email, Address = @Address, Password = @Password, Gender = @Gender WHERE Password = @CurrentPassword AND Name = @CurrentUserName";
                    }

                    com = new SqlCommand(query, con);
                    com.Parameters.AddWithValue("@UserName", textBox_user_update.Text);
                    com.Parameters.AddWithValue("@Email", textBox_email_update.Text);
                    com.Parameters.AddWithValue("@Address", textBox_address_update.Text);
                    com.Parameters.AddWithValue("@Password", textBox_pass_update.Text);
                    com.Parameters.AddWithValue("@Gender", gender);
                    com.Parameters.AddWithValue("@CurrentPassword", var.current_pass);
                    com.Parameters.AddWithValue("@CurrentUserName", var.current_username);

                    if (pic_changed && !pic_isremoved)
                    {
                        com.Parameters.AddWithValue("@ImageLocation", image_location);
                    }

                    com.ExecuteNonQuery();

                    MessageBox.Show("Update Information Done");
                    int parsedPassword;
                    if (int.TryParse(textBox_pass_update.Text, out parsedPassword))
                    {
                        var.current_pass = parsedPassword;
                    }
                    else
                    {
                        MessageBox.Show("Invalid password format. Please enter a numeric password.");
                        return;
                    }
                    //var.current_pass = textBox_pass_update.Text;
                    textBox_repass_update.Text = null;
                    panel40_update_userinfo.Visible = false;
                    var.current_username = textBox_user_update.Text;

                    update_user_info();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void update_user_info()
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=Shopping System;Integrated Security=True"))
            {
                con.Open();
                string query = "SELECT Picture, Name, Email, Address, Gender, Password, Pocket_Money FROM User_Info WHERE Name = @CurrentUsername";
                using (SqlCommand com = new SqlCommand(query, con))
                {
                    com.Parameters.AddWithValue("@CurrentUsername", var.current_username);

                    using (SqlDataReader dr = com.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            if (dr["Picture"] == DBNull.Value || string.IsNullOrEmpty(dr["Picture"].ToString()))
                            {
                                pictureBox1_profile.Image = Properties.Resources.username_logo2;
                            }
                            else
                            {
                                pictureBox1_profile.Image = Image.FromFile(dr["Picture"].ToString());
                            }

                            // Retrieve other data from the reader
                            label_username.Text = dr["Name"].ToString();
                            label_email.Text = dr["Email"].ToString();
                            label_address.Text = dr["Address"].ToString();
                            bool userGender = Convert.ToBoolean(dr["Gender"]);
                            label_pass.Text = dr["Password"].ToString();
                            label_pocket.Text = dr["Pocket_Money"].ToString();

                            label_gender.Text = userGender ? "Female" : "Male";
                        }
                        else
                        {
                            // Handle the case where no data is present
                            // Set default values for your labels
                            pictureBox1_profile.Image = Properties.Resources.username_logo2;
                            label_username.Text = "N/A";
                            label_email.Text = "N/A";
                            label_address.Text = "N/A";
                            label_gender.Text = "N/A";
                            label_pass.Text = "N/A";
                            label_pocket.Text = "N/A";
                        }
                    }
                }
            }
        }


        private void button_upload_pic_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Images Files|*.JpG;*.JPEG;*.PNG";
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox6_update.Image = Image.FromFile(openFileDialog.FileName);
                image_location = openFileDialog.FileName.ToString();
                pic_changed = true;
                pic_isremoved = false;
            }
        }

        private void button_remove_pic_Click(object sender, EventArgs e)
        {
            pictureBox6_update.Image = Properties.Resources.username_logo2;
            image_location = default(string);
            pic_isremoved = true;
            pic_changed = false;
        }

        private void button_addcat_Click(object sender, EventArgs e)
        {
            panel_add_category.Visible = true;
            panel_product.Visible = false;
            panel_edit_category.Visible = false;
            panel_categories.Visible = false;
            panel40_update_userinfo.Visible = false;

        }

        bool pic_cat_add = false;
        string img_location_add_cat;

        private void button_uplaod_pic_addcat_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Images Files|*.JpG;*.JPEG;*.PNG";
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox_addcat.Image = Image.FromFile(openFileDialog.FileName);
                img_location_add_cat = openFileDialog.FileName.ToString();
                pic_cat_add = true;
            }
        }

        private void button_remove_pic_addcat_Click(object sender, EventArgs e)
        {
            pictureBox_addcat.Image = Properties.Resources.images;
            img_location_add_cat = default(string);
            pic_cat_add = false;
        }

        private void fill_youradver_combobox()
        {
            SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=Shopping System;Integrated Security=True");
            con.Open();

            string strCmd2 = ("select Name from Product where Status = 0");
            SqlCommand cmd2 = new SqlCommand(strCmd2, con);
            SqlDataReader rdr = cmd2.ExecuteReader();

            DataTable productname = new DataTable();
            productname.Columns.Add("Name");
            DataRow row;
            while (rdr.Read())
            {
                row = productname.NewRow();
                row["Name"] = rdr["Name"];
                productname.Rows.Add(row);
            }
            rdr.Close();
            youradvertisements_combobox.Items.Clear();
            foreach (DataRow row1 in productname.Rows)
            {
                youradvertisements_combobox.Items.Add(row1[0].ToString());
            }

        }

        private void button_addcat_panel_Click(object sender, EventArgs e)
        {
            if (textBox_add_cat_name.Text.Length == 0 || textBox_des_addcat.Text.Length == 0)
            {
                MessageBox.Show("Please Enter Valid Information");
            }
            else
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=Shopping System;Integrated Security=True"))
                    {
                        con.Open();
                        SqlCommand com = new SqlCommand("SELECT COUNT(*) FROM Category WHERE Name=@Name", con);
                        com.Parameters.AddWithValue("@Name", textBox_add_cat_name.Text);
                        int count = Convert.ToInt32(com.ExecuteScalar());

                        if (count == 0)
                        {
                            com = new SqlCommand("INSERT INTO Category (Name) VALUES (@Name)", con);
                            com.Parameters.AddWithValue("@Name", textBox_add_cat_name.Text);
                            com.ExecuteNonQuery();
                            MessageBox.Show("Category Added Successfully");

                            textBox_add_cat_name.Text = "";
                            textBox_des_addcat.Text = "";
                            panel_add_category.Visible = false;

                            fill_categories_combobox(); // Optional: Update category combobox if necessary
                        }
                        else
                        {
                            MessageBox.Show("Category Name Already Exists. Please Enter Another Name.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }


        private void button_cancel_panel_addcat_Click(object sender, EventArgs e)
        {
            panel_add_category.Visible = false;
        }

        private void button1_cancel_updateuser_Click(object sender, EventArgs e)
        {
            panel40_update_userinfo.Visible = false;
        }

        private void youradvertisements_combobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            panel_add_category.Visible = false;
            panel_product.Visible = true;
            panel_edit_category.Visible = false;
            panel_categories.Visible = false;
            panel40_update_userinfo.Visible = false;


            

            // Connection string
            string connectionString = @"Data Source=.;Initial Catalog=Shopping System;Integrated Security=True";

            // SQL insert statement
            string query = "INSERT INTO Products (Name, Category_ID, Publisher_ID, Description, Price, Status) " +
                           "VALUES (@Name, @Category_ID, @Publisher_ID, @Description, @Price)";

            // Create and open the connection
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    // Create the SQL command
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        // Add parameters to the SQL command
                        cmd.Parameters.AddWithValue("@Name", textBox1.Text);
                        cmd.Parameters.AddWithValue("@Category_ID", textBox2.Text);
                        cmd.Parameters.AddWithValue("@Publisher_ID", textBox3.Text);
                        cmd.Parameters.AddWithValue("@Description", textBox4.Text);
                        cmd.Parameters.AddWithValue("@Price", textBox5.Text);
                        

                        // Execute the command
                        cmd.ExecuteNonQuery();

                        // Show success message
                        MessageBox.Show("Product details added successfully!");
                    }
                }
                catch (Exception ex)
                {
                    // Show error message
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }


        }

        private void fill_categories_combobox()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=Shopping System;Integrated Security=True"))
                {
                    con.Open();
                    string strCmd3 = "SELECT Name FROM Category";
                    SqlCommand cmd3 = new SqlCommand(strCmd3, con);
                    SqlDataReader rdr1 = cmd3.ExecuteReader();

                    categories_combobox.Items.Clear(); // Clear existing items before adding new ones

                    while (rdr1.Read())
                    {
                        categories_combobox.Items.Add(rdr1["Name"].ToString());
                    }

                    rdr1.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while filling categories: " + ex.Message);
            }
        }


        private void button_approve_Click(object sender, EventArgs e)
        {



            // Connection string
            string connectionString = @"Data Source=.;Initial Catalog=Shopping System;Integrated Security=True";

            // SQL insert statement
            string query = "INSERT INTO Product (Name, Category_ID, Publisher_ID, Description, Price, Minimum_Amount) " +
                           "VALUES (@Name, @Category_ID, @Publisher_ID, @Description, @Price,@Minimum_Amount)";

            // Create and open the connection
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    // Create the SQL command
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        // Add parameters to the SQL command
                        cmd.Parameters.AddWithValue("@Name", textBox1.Text);
                        cmd.Parameters.AddWithValue("@Category_ID", textBox2.Text);
                        cmd.Parameters.AddWithValue("@Publisher_ID", 1);
                        cmd.Parameters.AddWithValue("@Minimum_Amount", textBox3.Text);
                        cmd.Parameters.AddWithValue("@Description", richTextBox1.Text);
                        cmd.Parameters.AddWithValue("@Price", textBox6.Text);


                        // Execute the command
                        cmd.ExecuteNonQuery();

                        // Show success message
                        MessageBox.Show("Product details added successfully!");
                    }
                }
                catch (Exception ex)
                {
                    // Show error message
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }


        private void button7_delete_adver_Click(object sender, EventArgs e)
        {
           

           
       
        }

        private void categories_combobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            panel_add_category.Visible = false;
            panel_product.Visible = false;
            panel_edit_category.Visible = false;
            panel_categories.Visible = true;
            panel40_update_userinfo.Visible = false;


            SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=Shopping System;Integrated Security=True");
            con.Open();

            SqlCommand cmd = new SqlCommand("select ID from Category where Name='" + categories_combobox.SelectedItem + "'", con);
            int id_cat = Convert.ToInt32(cmd.ExecuteScalar());

             cmd = new SqlCommand("select Count(*) from Product where Category_ID='" + id_cat + "'", con);
            int num_products =Convert.ToInt32( cmd.ExecuteScalar());

            cmd = new SqlCommand("select Name from Category where Name='" + categories_combobox.SelectedItem + "'", con);
           SqlDataReader reader = cmd.ExecuteReader();

           
          
            con.Close();
        }

        private void button_edit_cat_Click(object sender, EventArgs e)
        {
            panel_add_category.Visible = false;
            panel_product.Visible = false;
            panel_edit_category.Visible = true;
            panel_categories.Visible = false;
            panel40_update_userinfo.Visible = false;


            SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=Shopping System;Integrated Security=True");
            con.Open();

            SqlCommand cmd = new SqlCommand("select Name from Category where Name='" + categories_combobox.SelectedItem + "'", con);
            SqlDataReader reader = cmd.ExecuteReader();

            reader.Read();
            textBox_editnamecat.Text = reader["Name"].ToString();


            reader.Close();

            con.Close();
        }

        private void button_deletecat_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=Shopping System;Integrated Security=True");
            con.Open();

            SqlCommand cmd = new SqlCommand("delete Category where Name='" + categories_combobox.SelectedItem + "'", con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Done");
            con.Close();

            fill_categories_combobox();

            pictureBox_cat.Image = Properties.Resources.images;
            label_namecat.Text = null;
            label_descat.Text = null;
            label_numcat.Text = null;
            panel_categories.Visible = false;
        }

        private void button_cancel_panelupdatecat_Click(object sender, EventArgs e)
        {
            panel_edit_category.Visible = false;
        }

        bool pic_cat_update = false;
        string img_cat_update_location;

        private void button_update_cat_Click(object sender, EventArgs e)
        {
            if (textBox_editnamecat.Text.Length == 0 || textBox_editdescat.Text.Length == 0)
            {
                MessageBox.Show("Please Enter A Valid Information");
            }
            else
            {
                    SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=Shopping System;Integrated Security=True");
                    con.Open();

                    SqlCommand com = new SqlCommand("select Count(*) from Category where Name='"+ textBox_editnamecat.Text+"'", con);
                   int count = Convert.ToInt32(com.ExecuteScalar());

                if (count == 0)
                {
                    
                    if (pic_cat_update == true)
                    {
                         com = new SqlCommand("update Category set Description ='" + textBox_editdescat.Text  + "', Name ='" + textBox_editnamecat.Text + "' where Name='"+categories_combobox.SelectedItem+"'", con);
                        com.ExecuteNonQuery();
                    }
                    else
                    {
                         com = new SqlCommand("update Category set Description ='" + textBox_editdescat.Text + "', Name ='" + textBox_editnamecat.Text + "' where Name='" + categories_combobox.SelectedItem + "'", con);
                        com.ExecuteNonQuery();
                    }
                    MessageBox.Show("Update Category Is Done");
                    textBox_editdescat.Text = null;
                    textBox_editnamecat.Text = null;
                    pictureBox_editcat.Image = Properties.Resources.images;

                    panel_edit_category.Visible = false;

                    fill_categories_combobox();
                }
                else
                {
                    MessageBox.Show("Category Name Is Exist,Please Enter Another one");
                }
                   

                    con.Close();
                    
            }
        }

        private void button2_upload_pic_updateproduct_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Images Files|*.JpG;*.JPEG;*.PNG";
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox_editcat.Image = Image.FromFile(openFileDialog.FileName);
                img_cat_update_location = openFileDialog.FileName.ToString();
                pic_cat_update = true;
            }
        }

        private void button_remove_updateproduct_Click(object sender, EventArgs e)
        {
            pictureBox_editcat.Image = Properties.Resources.images;
            img_cat_update_location = default(string);
            pic_cat_update = false;
        }

        private void Profile_Admin_Load(object sender, EventArgs e)
        {

        }

        private void panel_advertisement_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            panel_product.Visible = true;
            panel_edit_category.Visible = false;
            panel_categories.Visible = false;
            panel40_update_userinfo.Visible = false;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel_edit_category_Paint(object sender, PaintEventArgs e)
        {

        }
    }

}
