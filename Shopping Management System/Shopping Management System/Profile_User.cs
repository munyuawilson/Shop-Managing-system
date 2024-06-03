using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Shopping_Management_System
{
    public partial class Profile_User : Form
    {
        public Profile_User()
        {
            InitializeComponent();

            update_user_info();

            fill_youradver_combobox();
            ////////////////////////////////////////
            SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=Shopping System;Integrated Security=True");
            con.Open();

            string strCmd1 = ("select ID from User_Info where  Name ='" + var.current_username + "'and Password='" + var.current_pass + "'");
            SqlCommand cmd1 = new SqlCommand(strCmd1, con);
            int id = Convert.ToInt32(cmd1.ExecuteScalar());

            string strCmd3 = ("select Product_ID from Buying_Product where  User_ID ='" + id + "'");
            SqlCommand cmd3 = new SqlCommand(strCmd3, con);
            SqlDataReader rdr1 = cmd3.ExecuteReader();

            DataTable purchased_products = new DataTable();
            purchased_products.Columns.Add("Product_ID");
            DataRow row2;
          
            while (rdr1.Read())
            {
                row2 = purchased_products.NewRow();
                row2["Product_ID"] = rdr1["Product_ID"];
                purchased_products.Rows.Add(row2);
            }
            rdr1.Close();
            purchased_combobox.Items.Clear();
            foreach (DataRow row3 in purchased_products.Rows)
            {
                purchased_combobox.Items.Add(row3[0].ToString());
            }
            con.Close();

        }

        private void button_back_Click(object sender, EventArgs e)
        {
            Cart cart = new Cart();
            cart.Show();
            this.Hide();
        }

        private void button2_editinfo_profile_Click(object sender, EventArgs e)
        {
            panel_add_product.Visible = false;
            panel_advertisement.Visible = false;
            panel_purchased.Visible = false;
            panel_edit_product.Visible = false;
            panel40_update_userinfo.Visible = true;

            SqlConnection con = new SqlConnection(@"Data Source =.; Initial Catalog = Shopping System; Integrated Security = True");
            con.Open();
            SqlCommand com = new SqlCommand("select Picture ,Name ,Email,Address,Gender,Password,Pocket_Money from User_Info where Password='" + var.current_pass + "'and Name='" + var.current_username + "'", con);
            SqlDataReader dr = com.ExecuteReader();
            bool gender = false;

            dr.Read();

            if (dr["Picture"].ToString() == string.Empty)
            {
                pictureBox6_update.Image = Properties.Resources.username_logo2;
            }
            else
            {
                pictureBox6_update.Image = Image.FromFile(dr["Picture"].ToString());
            }

            textBox_user_update.Text = dr["Name"].ToString();
            textBox_email_update.Text = dr["Email"].ToString();
            textBox_address_update.Text = dr["Address"].ToString();
            gender = Convert.ToBoolean(dr["Gender"]);
            textBox_pass_update.Text = dr["Password"].ToString();

            dr.Close();

            if (gender == false)
            {
                radioButton1_male_update.Checked = true;
            }
            else
            {
                radioButton2_female_update.Checked = true;
            }

            con.Close();

            usernametext = label_username.Text;
        }

        string usernametext;

        string image_location;
        bool pic_changed = false;
        bool pic_isremoved = false;

        private void button_update_userinfo_Click(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=Shopping System;Integrated Security=True");
            con.Open();

            bool user_exist = false;

            bool gender = false;
            if (radioButton1_male_update.Checked)
            {
                gender = false;
            }
            else
            {
                gender = true;
            }

            if (textBox_pass_update.Text == textBox_repass_update.Text)
            {
                SqlCommand com;


                if (usernametext != textBox_user_update.Text)
                {


                    com = new SqlCommand("select Count(*) from User_Info where Name='" + textBox_user_update.Text + "'", con);
                    int result = Convert.ToInt32(com.ExecuteScalar());
                    if (result > 0)
                    {
                        user_exist = true;
                    }
                    else
                    {
                        user_exist = false;
                    }

                    //////////////////////
                    if (user_exist == false)
                    {
                        if (pic_changed == true && pic_isremoved == false)
                        {
                            com = new SqlCommand("update User_Info set Picture='" + image_location + "', Name ='" + textBox_user_update.Text + "' ,Email='" + textBox_email_update.Text + "',Address='" + textBox_address_update.Text + "',Password='" + textBox_pass_update.Text + "',Gender='" + gender + "' where  Password='" + var.current_pass + "'and Name='" + var.current_username + "'", con);
                            com.ExecuteNonQuery();
                            MessageBox.Show("Update Information Done ");
                            var.current_pass = Convert.ToInt32(textBox_pass_update.Text);
                            textBox_repass_update.Text = null;
                            panel40_update_userinfo.Visible = false;
                        }
                        else if (pic_changed == false && pic_isremoved == false)
                        {
                            com = new SqlCommand("update User_Info set  Name ='" + textBox_user_update.Text + "' ,Email='" + textBox_email_update.Text + "',Address='" + textBox_address_update.Text + "',Password='" + textBox_pass_update.Text + "',Gender='" + gender + "' where  Password='" + var.current_pass + "'and Name='" + var.current_username + "'", con);
                            com.ExecuteNonQuery();
                            MessageBox.Show("Update Information Done ");
                            var.current_pass = Convert.ToInt32(textBox_pass_update.Text);
                            textBox_repass_update.Text = null;
                            panel40_update_userinfo.Visible = false;
                        }
                        else
                        {
                            com = new SqlCommand("update User_Info set Picture=NULL, Name ='" + textBox_user_update.Text + "' ,Email='" + textBox_email_update.Text + "',Address='" + textBox_address_update.Text + "',Password='" + textBox_pass_update.Text + "',Gender='" + gender + "' where  Password='" + var.current_pass + "'and Name='" + var.current_username + "'", con);
                            com.ExecuteNonQuery();
                            MessageBox.Show("Update Information Done ");
                            var.current_pass = Convert.ToInt32(textBox_pass_update.Text);
                            textBox_repass_update.Text = null;
                            panel40_update_userinfo.Visible = false;
                        }
                        
                        var.current_username = textBox_user_update.Text;
                    }
                    else
                    {
                        MessageBox.Show("Username Is Already Exist ,Please Choose Another One");
                    }
                    update_user_info();
                }
                else
                {
                    if (pic_changed == true && pic_isremoved == false)
                    {
                        com = new SqlCommand("update User_Info set Picture='" + image_location + "',Name ='" + textBox_user_update.Text + "' ,Email='" + textBox_email_update.Text + "',Address='" + textBox_address_update.Text + "',Password='" + textBox_pass_update.Text + "',Gender='" + gender + "' where  Password='" + var.current_pass + "'and Name='" + var.current_username + "'", con);
                        com.ExecuteNonQuery();
                        MessageBox.Show("Update Information Done ");
                        var.current_pass = Convert.ToInt32(textBox_pass_update.Text);
                        textBox_repass_update.Text = null;
                        panel40_update_userinfo.Visible = false;
                    }
                    else if (pic_changed == true && pic_isremoved == false)
                    {
                        com = new SqlCommand("update User_Info set Name ='" + textBox_user_update.Text + "' ,Email='" + textBox_email_update.Text + "',Address='" + textBox_address_update.Text + "',Password='" + textBox_pass_update.Text + "',Gender='" + gender + "' where  Password='" + var.current_pass + "'and Name='" + var.current_username + "'", con);
                        com.ExecuteNonQuery();
                        MessageBox.Show("Update Information Done ");
                        var.current_pass = Convert.ToInt32(textBox_pass_update.Text);
                        textBox_repass_update.Text = null;
                        panel40_update_userinfo.Visible = false;
                    }
                    else
                    {
                        com = new SqlCommand("update User_Info set Picture=NULL,Name ='" + textBox_user_update.Text + "' ,Email='" + textBox_email_update.Text + "',Address='" + textBox_address_update.Text + "',Password='" + textBox_pass_update.Text + "',Gender='" + gender + "' where  Password='" + var.current_pass + "'and Name='" + var.current_username + "'", con);
                        com.ExecuteNonQuery();
                        MessageBox.Show("Update Information Done ");
                        var.current_pass = Convert.ToInt32(textBox_pass_update.Text);
                        textBox_repass_update.Text = null;
                        panel40_update_userinfo.Visible = false;
                    }
                    update_user_info();
                }
                
            }
            else
            {
                MessageBox.Show("Please Enter Password Correctly");
            }
            con.Close();
        }
        private void update_user_info()
        {
            SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=Shopping System;Integrated Security=True");
            con.Open();

            SqlCommand com = new SqlCommand("select Picture ,Name ,Email,Address,Gender,Password,Pocket_Money from User_Info where Password='" + var.current_pass + "'and Name='" + var.current_username + "'", con);
            SqlDataReader dr = com.ExecuteReader();
            bool gender = false;
            dr.Read();

            if (dr["Picture"].ToString() == string.Empty)
            {
                pictureBox1_profile.Image = Properties.Resources.username_logo2;
            }
            else
            {
                pictureBox1_profile.Image = Image.FromFile(dr["Picture"].ToString());
            }

            label_username.Text = dr["Name"].ToString();
            label_email.Text = dr["Email"].ToString();
            label_address.Text = dr["Address"].ToString();
            gender = Convert.ToBoolean(dr["Gender"]);
            label_pass.Text = dr["Password"].ToString();
            label_pocket.Text = dr["Pocket_Money"].ToString();

            dr.Close();
            if (gender == false)
            {
                label_gender.Text = "Male";
            }
            else
            {
                label_gender.Text = "Female";
            }


            con.Close();

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

        private void button_addprod_Click(object sender, EventArgs e)
        {
            panel_add_product.Visible = true;
            panel_advertisement.Visible = false;
            panel_purchased.Visible = false;
            panel_edit_product.Visible = false;
            panel40_update_userinfo.Visible = false;

            SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=Shopping System;Integrated Security=True");
            con.Open();
            
            SqlDataAdapter ad = new SqlDataAdapter("select Name from Category", con);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            comboBox_add_product.Items.Clear();
            foreach (DataRow item in dt.Rows)
            {
                comboBox_add_product.Items.Add(item[0].ToString());
            }
        }

        bool date_ischanged = false;
        bool choose_category = false;
        bool pic_product_add = false;
        string img_location_add_product;

        private void button_add_product_Click(object sender, EventArgs e)
        {
            if (textBox_product_name.Text.Length == 0 || choose_category == false || textBox_amount_add_product.Text.Length == 0 || textBox_description_add_product.Text.Length == 0 || textBox_price_add_prod.Text.Length == 0)
            {
                MessageBox.Show("Please Enter A Valid Information");
            }
            else
            {
                SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=Shopping System;Integrated Security=True");
                con.Open();

                SqlCommand cmd = new SqlCommand("select Count(*) from Product where Name='" + textBox_product_name.Text + "'", con);
                int count = Convert.ToInt32(cmd.ExecuteScalar());

                if (count == 0)
                {
                    if (date_ischanged == true)
                    {
                       

                        SqlCommand com = new SqlCommand("add_newproduct", con);
                        com.CommandType = CommandType.StoredProcedure;

                        SqlParameter[] param = new SqlParameter[10];


                        param[0] = new SqlParameter("@name", SqlDbType.NVarChar, 250);
                        param[0].Value = textBox_product_name.Text;

                        param[1] = new SqlParameter("@category_name", SqlDbType.NVarChar, 250);
                        param[1].Value = comboBox_add_product.SelectedItem.ToString();


                        param[2] = new SqlParameter("@minimum_amount", SqlDbType.Int);
                        param[2].Value = textBox_amount_add_product.Text;

                        param[3] = new SqlParameter("@date_of_expire", SqlDbType.Date);
                        param[3].Value = dateTime_expire_panel_add.Value.ToShortDateString();

                        param[4] = new SqlParameter("@date_of_production", SqlDbType.Date);
                        param[4].Value = dateTime_production_panel_add.Value.ToShortDateString();

                        param[5] = new SqlParameter("@desciption", SqlDbType.NVarChar, 250);
                        param[5].Value = textBox_description_add_product.Text.ToString();

                        param[6] = new SqlParameter("@price", SqlDbType.Float);
                        param[6].Value = textBox_price_add_prod.Text;

                        param[7] = new SqlParameter("@status", SqlDbType.Bit, 1);
                        param[7].Value = 0;

                        param[8] = new SqlParameter("@publisher_name", SqlDbType.NVarChar, 250);
                        param[8].Value = var.current_username;

                        if (pic_product_add == true)
                        {
                            param[9] = new SqlParameter("@picture", SqlDbType.NVarChar, 250);
                            param[9].Value = img_location_add_product;
                        }
                        else
                        {
                            param[9] = new SqlParameter("@picture", SqlDbType.NVarChar, 250);
                            param[9].Value = "NULL";
                        }

                        com.Parameters.AddRange(param);

                        com.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Add Product Is Done, Wait Until Admin Approve It, If Admin reject it ,it will Automaticaly Deleted");
                    }
                    else
                    {
                      
                        SqlCommand com = new SqlCommand("add_newproduct", con);
                        com.CommandType = CommandType.StoredProcedure;

                        SqlParameter[] param = new SqlParameter[8];


                        param[0] = new SqlParameter("@name", SqlDbType.NVarChar, 250);
                        param[0].Value = textBox_product_name.Text;

                        param[1] = new SqlParameter("@category_name", SqlDbType.NVarChar, 250);
                        param[1].Value = comboBox_add_product.SelectedItem.ToString();


                        param[2] = new SqlParameter("@minimum_amount", SqlDbType.Int);
                        param[2].Value = textBox_amount_add_product.Text;

                        param[3] = new SqlParameter("@desciption", SqlDbType.NVarChar, 250);
                        param[3].Value = textBox_description_add_product.Text.ToString();

                        param[4] = new SqlParameter("@price", SqlDbType.Float);
                        param[4].Value = textBox_price_add_prod.Text;

                        param[5] = new SqlParameter("@status", SqlDbType.Bit, 1);
                        param[5].Value = 0;

                        param[6] = new SqlParameter("@publisher_name", SqlDbType.NVarChar, 250);
                        param[6].Value = var.current_username;

                        if (pic_product_add == true)
                        {
                            param[5] = new SqlParameter("@picture", SqlDbType.NVarChar, 250);
                            param[5].Value = img_location_add_product;
                        }
                        else
                        {
                            param[7] = new SqlParameter("@picture", SqlDbType.NVarChar, 250);
                            param[7].Value = "NULL";
                        }


                        com.Parameters.AddRange(param);

                        com.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Add Product Is Done, Wait Until Admin Approve It, If Admin reject it ,it will Automaticaly Deleted");
                    }

                    textBox_product_name.Text = null;
                    textBox_price_add_prod.Text = null;
                    comboBox_add_product.Text = null;
                    pictureBox_add_product.Image = Properties.Resources.images;
                    textBox_amount_add_product.Text = null;
                    date_ischanged = false;
                    textBox_description_add_product.Text = null;
                    panel_add_product.Visible = false;
                    fill_youradver_combobox();
                }
                else
                {
                    MessageBox.Show("Product Name Is Already Exist , Please Enter Another Name");
                }
                con.Close();
            }

                

        }

        private void comboBox_add_product_SelectedIndexChanged(object sender, EventArgs e)
        {
            choose_category = true;
        }

        private void button_upload_pic_add_product_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Images Files|*.JpG;*.JPEG;*.PNG";
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox_add_product.Image = Image.FromFile(openFileDialog.FileName);
                img_location_add_product = openFileDialog.FileName.ToString();
                pic_product_add = true;
            }
        }

        private void button_remove_add_product_Click(object sender, EventArgs e)
        {
            pictureBox_add_product.Image = Properties.Resources.images;
            img_location_add_product = default(string);
            pic_product_add = false;
        }

        private void Profile_User_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void dateTime_expire_panel_add_ValueChanged(object sender, EventArgs e)
        {
            date_ischanged = true;
        }

        private void dateTime_production_panel_add_ValueChanged(object sender, EventArgs e)
        {
            date_ischanged = true;
        }

        private void youradvertisements_combobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            panel_advertisement.Visible = true;
            panel_add_product.Visible = false;
            panel_purchased.Visible = false;
            panel_edit_product.Visible = false;
            panel40_update_userinfo.Visible = false;

            SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=Shopping System;Integrated Security=True");
            con.Open();

            SqlCommand  cmd = new SqlCommand("select ID from Product where Name='" + youradvertisements_combobox.SelectedItem.ToString() + "'", con);
            int ID_Product= Convert.ToInt32(cmd.ExecuteScalar());

            cmd = new SqlCommand("select Category_ID from Product where Name='" + youradvertisements_combobox.SelectedItem.ToString() + "'", con);
            int ID_Cat = Convert.ToInt32(cmd.ExecuteScalar());

            cmd = new SqlCommand("select ID from User_Info where Name='" + var.current_username+ "'and Password='"+var.current_pass+"'", con);
            int ID_User = Convert.ToInt32(cmd.ExecuteScalar());


            cmd = new SqlCommand("select Name from Category where ID='" + ID_Cat + "'", con);
            SqlDataReader reader = cmd.ExecuteReader();
            string cat_name;
            reader.Read();
            cat_name =reader["Name"].ToString();
            reader.Close();

            cmd = new SqlCommand("select Count(*) from Selling_Product where Product_ID='" + ID_Product + "'and User_ID='"+ ID_User + "'", con);
            int sales_amount = Convert.ToInt32(cmd.ExecuteScalar());


            cmd = new SqlCommand("select Name,Picture,Minimum_Amount,Date_Of_Expiring,Date_Of_Production,Description,Price,Status from Product where Name='" + youradvertisements_combobox.SelectedItem + "'", con);
            reader = cmd.ExecuteReader();

            reader.Read();
            label_adver_productname.Text = reader["Name"].ToString();

            if (reader["Picture"].ToString() != "NULL")
            {
                pictureBox_adver_product.Image = Image.FromFile(reader["Picture"].ToString());
            }
            else
            {
                pictureBox_adver_product.Image = Properties.Resources.images; 
            }
            
            label_adver_amount.Text = reader["Minimum_Amount"].ToString();

            if (reader["Date_Of_Expiring"].ToString() != string.Empty)
            {
                label_adver_exp.Text = reader["Date_Of_Expiring"].ToString();
            }
            else
            {
                label_adver_exp.Text = "-/-/-";
            }

            if (reader["Date_Of_Production"].ToString() != string.Empty)
            {
                label_adver_prod.Text = reader["Date_Of_Production"].ToString();
            }
            else
            {
                label_adver_prod.Text = "-/-/-";
            }

            label_adver_desc.Text = reader["Description"].ToString();
            label_price_adver.Text = reader["Price"].ToString();
            bool status = false;
            status =Convert.ToBoolean( reader["Status"].ToString());
            reader.Close();

            label_adver_salesamount.Text = sales_amount.ToString();
            label_catname_adver.Text = cat_name.ToString();
            if (status == false)
            {
                label_status_adver.Text = "Waiting";
            }
            else
            {
                label_status_adver.Text = "Approved";
            }
            con.Close();
        }

        private void purchased_combobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            panel_add_product.Visible = false;
            panel_advertisement.Visible = false;
            panel_purchased.Visible = true;
            panel_edit_product.Visible = false;
            panel40_update_userinfo.Visible = false;

            SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=Shopping System;Integrated Security=True");
            con.Open();

            int product_id = Convert.ToInt32(purchased_combobox.SelectedItem.ToString());
            SqlCommand cmd = new SqlCommand("select Category_ID from Product where ID ='" + product_id + "'", con); 
            int catID = Convert.ToInt32(cmd.ExecuteScalar());
            SqlCommand cmd1 = new SqlCommand("select Name from Category where ID ='" + catID + "'", con);
            string catName = (string)cmd1.ExecuteScalar();
            cmd1= new SqlCommand("select Name from Product where ID ='" + product_id + "'", con);
            string product_name = (string)cmd1.ExecuteScalar();
            SqlCommand cmd2 = new SqlCommand("select Price from Product where ID ='" + product_id + "'", con);
            int price = Convert.ToInt32(cmd2.ExecuteScalar());
            cmd2=new SqlCommand("select Picture from Product where ID ='" + product_id + "'", con);
            SqlDataReader reader = cmd2.ExecuteReader();
            reader.Read();
            if (reader["Picture"].ToString() != string.Empty)
            {
                pictureBox_purchased.Image = Image.FromFile(reader["Picture"].ToString());
            }
            else
            {
                pictureBox_purchased.Image = Properties.Resources.images;
            }
            reader.Close();
            SqlCommand cmd3 = new SqlCommand("select Amount_Of_Buying,Date_Of_Buying,Payment_Method from Buying_Product  where Product_ID='" + product_id + "'", con);
            SqlDataReader rdr = cmd3.ExecuteReader();
            rdr.Read();
                label_amount_purchased.Text = rdr["Amount_Of_Buying"].ToString();
                label_date_purchased.Text = rdr["Date_Of_Buying"].ToString();
                label_payment_purchased.Text = rdr["Payment_Method"].ToString();

            rdr.Close();
            label_productname_purchased.Text = product_name;
            label_cat_name_purchased.Text = catName;
            label_price_purchased.Text = price.ToString();
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel_purchased.Visible = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=Shopping System;Integrated Security=True");
            con.Open();

            SqlCommand cmd = new SqlCommand("select ID from Product where Name='" + youradvertisements_combobox.SelectedItem+ "'", con);
            int ID_Product = Convert.ToInt32(cmd.ExecuteScalar());

            cmd = new SqlCommand("deleteproduct", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter prm = new SqlParameter("@name_id", SqlDbType.Int);
            prm.Value = ID_Product;
            cmd.Parameters.Add(prm);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Deleted successfully");

            pictureBox_adver_product.Image = Properties.Resources.images;
            panel_advertisement.Visible = false;
            label_adver_productname.Text = null;
            label_catname_adver.Text = null;
            label_adver_amount.Text = null;
            label_adver_exp.Text = null;
            label_adver_prod.Text = null;
            label_adver_desc.Text = null;
            label_price_adver.Text = null;
            label_status_adver.Text = null;
            label_adver_salesamount.Text = null;
            fill_youradver_combobox();
            
        }
        private void fill_youradver_combobox()
        {
            SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=Shopping System;Integrated Security=True");
            con.Open();

            string strCmd1 = ("select ID from User_Info where  Name ='" + var.current_username + "'and Password='" + var.current_pass + "'");
            SqlCommand cmd1 = new SqlCommand(strCmd1, con);
            int id = Convert.ToInt32(cmd1.ExecuteScalar());

            string strCmd2 = ("select Name from Product where  Publisher_ID ='" + id + "'");
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

        bool pic_product_changed = false;
        string pic_product_location;
        bool date_ischanged_update = false;
        int id_product;

        private void button_updateproduct_Click(object sender, EventArgs e)
        {
            if (textBox_updateprod_user.Text.Length == 0 || textBox_amount_updateprod.Text.Length == 0 || textBox_des_updateprod.Text.Length == 0 || textBox_price_update_prod.Text.Length == 0)
            {
                MessageBox.Show("Please Enter A Valid Information");
            }
            else
            {
                if (date_ischanged_update == true)
                {
                    SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=Shopping System;Integrated Security=True");
                    con.Open();

                    SqlCommand cmd = new SqlCommand("select ID from Category where Name ='" + comboBox_catname_updateprod.Text+ "'", con); ;
                    int id_cat = Convert.ToInt32(cmd.ExecuteScalar());
                    
                        if(pic_product_changed == true)
                        {
                               SqlCommand com = new SqlCommand("update Product set Description ='" + textBox_des_updateprod.Text+"',Picture='"+ pic_product_location + "', Name ='" + textBox_updateprod_user.Text + "' ,Category_ID='" + id_cat + "',Minimum_Amount='" + textBox_amount_updateprod.Text + "',Date_Of_Expiring='" + dateTimePicker_expir_update.Value + "',Date_Of_Production='" + dateTimePicker_prod_update.Value+ "',Price='"+textBox_price_update_prod.Text+"' where ID='"+id_product+"'", con);
                                com.ExecuteNonQuery();
                        }
                    else
                    {
                        SqlCommand com = new SqlCommand("update Product set Picture='NULL', Description ='" + textBox_des_updateprod.Text + "', Name ='" + textBox_updateprod_user.Text + "' ,Category_ID='" + id_cat + "',Minimum_Amount='" + textBox_amount_updateprod.Text + "',Date_Of_Expiring='" + dateTimePicker_expir_update.Value + "',Date_Of_Production='" + dateTimePicker_prod_update.Value + "',Price='" + textBox_price_update_prod.Text + "' where ID='" + id_product + "'", con);
                        com.ExecuteNonQuery();
                    }
                    
                    con.Close();
                    MessageBox.Show("Update Product Is Done");
                }
                else
                {
                    SqlConnection con = new SqlConnection(@"Data Source=MANOR\MANARSQL;Initial Catalog=Shopping System;Integrated Security=True");
                    con.Open();

                    SqlCommand cmd = new SqlCommand("select ID from Category where Name ='" + comboBox_catname_updateprod.Text + "'", con); ;
                    int id_cat = Convert.ToInt32(cmd.ExecuteScalar());

                    if (pic_product_changed == true)
                    {
                        SqlCommand com = new SqlCommand("update Product set Description ='" + textBox_des_updateprod.Text + "', Picture='" + pic_product_location + "', Name ='" + textBox_updateprod_user.Text + "' ,Category_ID='" + id_cat + "',Minimum_Amount='" + textBox_amount_updateprod.Text + "',Price='" + textBox_price_update_prod.Text + "' where ID='" + id_product + "'", con);
                        com.ExecuteNonQuery();
                    }
                    else
                    {
                        SqlCommand com = new SqlCommand("update Product set Picture='NULL', Description ='" + textBox_des_updateprod.Text + "', Name ='" + textBox_updateprod_user.Text + "' ,Category_ID='" + id_cat + "',Minimum_Amount='" + textBox_amount_updateprod.Text + "',Price='" + textBox_price_update_prod.Text + "' where ID='" + id_product + "'", con);
                        com.ExecuteNonQuery();
                    }

                    con.Close();
                    MessageBox.Show("Update Product Is Done");

                }
                 fill_youradver_combobox();
                panel_edit_product.Visible = false;
              
               
            }
        }

        private void button2_upload_pic_updateproduct_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Images Files|*.JpG;*.JPEG;*.PNG";
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox_updateproduct.Image = Image.FromFile(openFileDialog.FileName);
                pic_product_location = openFileDialog.FileName.ToString();
                pic_product_changed = true;
        
            }
        }

        private void dateTimePicker_expir_update_ValueChanged(object sender, EventArgs e)
        {

            date_ischanged_update = true;
        }

        private void dateTimePicker_prod_update_ValueChanged(object sender, EventArgs e)
        {
            date_ischanged_update = true;
        }

        private void button_remove_updateproduct_Click(object sender, EventArgs e)
        {
            pictureBox_updateproduct.Image = Properties.Resources.images;
            pic_product_location = default(string);
            pic_product_changed = false;
        }

        private void button_edit_product_Click(object sender, EventArgs e) 
        {
            SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=Shopping System;Integrated Security=True");
            con.Open();

            SqlCommand cmd = new SqlCommand("select ID from Category where Name ='" + label_catname_adver.Text + "'", con); ;
           int id_cat = Convert.ToInt32(cmd.ExecuteScalar());

             cmd = new SqlCommand("select ID from Product where Category_ID ='" + id_cat + "'and Name='"+label_adver_productname.Text+"'", con); ;
             id_product = Convert.ToInt32(cmd.ExecuteScalar());


            //////////////////////////////////////////////
            string strCmd3 = ("select Name from Category");
            SqlCommand cmd3 = new SqlCommand(strCmd3, con);
            SqlDataReader rdr1 = cmd3.ExecuteReader();

            DataTable tb = new DataTable();
            tb.Columns.Add("Name");
            DataRow row2;

            while (rdr1.Read())
            {
                row2 = tb.NewRow();
                row2["Name"] = rdr1["Name"];
                tb.Rows.Add(row2);
            }
            rdr1.Close();
            comboBox_catname_updateprod.Items.Clear();
            foreach (DataRow row3 in tb.Rows)
            {
                comboBox_catname_updateprod.Items.Add(row3[0].ToString());
            }
            ///////////////////////////////////////
            panel_add_product.Visible = false;
            panel_advertisement.Visible = false;
            panel_purchased.Visible = false;
            panel_edit_product.Visible = true;
            panel40_update_userinfo.Visible = false;

            ////////////////////////////////////////
            ///
            cmd = new SqlCommand("select Name,Picture,Minimum_Amount,Date_Of_Expiring,Date_Of_Production,Description,Price from Product where ID='" + id_product+ "'", con);
           SqlDataReader reader = cmd.ExecuteReader();

            reader.Read();
            textBox_updateprod_user.Text = (string)reader["Name"];

            if (reader["Picture"].ToString() != "NULL")
            {
                pictureBox_updateproduct.Image = Image.FromFile(reader["Picture"].ToString());
            }
            else
            {
                pictureBox_updateproduct.Image = Properties.Resources.images;
            }

            textBox_amount_updateprod.Text = reader["Minimum_Amount"].ToString();

            if (reader["Date_Of_Expiring"].ToString() != string.Empty)
            {
                dateTimePicker_expir_update.Value =Convert.ToDateTime(reader["Date_Of_Expiring"]);
            }
            

            if (reader["Date_Of_Production"].ToString() != string.Empty)
            {
                dateTimePicker_prod_update.Value = Convert.ToDateTime(reader["Date_Of_Production"]);
            }


            textBox_des_updateprod.Text = reader["Description"].ToString();
            textBox_price_update_prod.Text = reader["Price"].ToString();
            
            reader.Close();

            comboBox_catname_updateprod.SelectedItem = label_catname_adver.Text;

            
            con.Close();

            /////////////////////////////////////////

           
        
        }

        private void button2_cancel_panel_updateprod_Click(object sender, EventArgs e)
        {
            panel_edit_product.Visible = false;
        }

        private void button2_cancel_userupdate_Click(object sender, EventArgs e)
        {
            panel40_update_userinfo.Visible = false;
        }

        private void button2_cancel_addprod_Click(object sender, EventArgs e)
        {
            panel_add_product.Visible = false;
        }
    }
}
