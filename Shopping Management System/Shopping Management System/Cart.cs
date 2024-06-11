using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Shopping_Management_System
{

    public partial class Cart : Form
    {
        hello_window Hello = new hello_window();

        public Cart()
        {
            InitializeComponent();

            SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=Shopping System;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("select Name from Category", con);
            SqlDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("Name");
            DataRow dr;
            while (reader.Read())
            {
                dr = dt.NewRow();
                dr["Name"] = reader["Name"];
                dt.Rows.Add(dr);
            }

            reader.Close();
            foreach (DataRow row in dt.Rows)
            {
                comboBox_categories.Items.Add(row[0].ToString());
            }
            con.Close();
        }



        private void Button1_Buy_Click(object sender, EventArgs e)
        {
            panel_buy.Visible = true;
        }

        private void Button2_Buy_Click(object sender, EventArgs e)
        {
            if (textBox_amount.TextLength != 0 && textBox_paymentway.TextLength != 0 && Convert.ToInt32(textBox_amount.Text) != 0)
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=Shopping System;Integrated Security=True"))
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand("SELECT UserID FROM User_Info WHERE Name = @sellerName", con);
                    cmd.Parameters.AddWithValue("@sellerName", label1_pub_name.Text);
                    int ID_seller = Convert.ToInt32(cmd.ExecuteScalar());

                    cmd = new SqlCommand("SELECT UserID FROM User_Info WHERE Name = @buyerName", con);
                    cmd.Parameters.AddWithValue("@buyerName", var.current_username);
                    int ID_buyer = Convert.ToInt32(cmd.ExecuteScalar());

                    cmd = new SqlCommand("UPDATE Product SET BuyerID = @buyerID, PurchaseDate = @purchaseDate, PaymentWay = @paymentWay, Status = @status WHERE Name = @productName", con);
                    cmd.Parameters.AddWithValue("@buyerID", ID_buyer);
                    cmd.Parameters.AddWithValue("@purchaseDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@paymentWay", textBox_paymentway.Text);
                    cmd.Parameters.AddWithValue("@status", "Sold"); // Assuming 'Sold' is the status for purchased products
                    cmd.Parameters.AddWithValue("@productName", label1_product_name.Text);
                    cmd.ExecuteNonQuery();

                    con.Close();
                    panel_done.Visible = true;
                    panel_buy.Visible = false;
                }
            }
            else
            {
                MessageBox.Show("Please Enter A Valid Values");
            }
        }



        private void button_Cancel_Click(object sender, EventArgs e)
        {
            panel_buy.Visible = false;
        }

        private void listBox_search_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox_search.SelectedItem != null)
            {
                pictureBox_products.Visible = true;
                label_productname.Visible = true;
                label1_product_name.Visible = true;
                panel3.Visible = true;

                label_catname.Visible = true;
                label1_cat_name.Visible = true;
                panel4.Visible = true;

                label_pubname.Visible = true;
                label1_pub_name.Visible = true;
                panel5.Visible = true;

                label_minamount.Visible = true;
                label1_min_amount.Visible = true;
                panel6.Visible = true;

                label_dateexpir.Visible = true;
                label1_dateexpir.Visible = true;
                panel8.Visible = true;

                label_dateprod.Visible = true;
                label1_dateprod.Visible = true;
                panel17.Visible = true;

                label_description.Visible = true;
                label1_description.Visible = true;

                label_price.Visible = true;
                label1_price.Visible = true;
                panel16.Visible = true;

                Button1_Buy.Visible = true;

                SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=Shopping System;Integrated Security=True");
                con.Open();

                SqlCommand cmd = new SqlCommand("select User_Type from User_Info where Name='" + var.current_username + "'", con);
                bool usertype = false;
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                usertype = Convert.ToBoolean(reader["User_Type"]);
                reader.Close();
                if (usertype == true)
                {
                    button_delete.Visible = true;
                }

                //////////////////////////////
                cmd = new SqlCommand("select Category_ID from Product where Name='" + listBox_search.SelectedItem + "'", con);
                int ID_cat = Convert.ToInt32(cmd.ExecuteScalar());

                cmd = new SqlCommand("select Publisher_ID from Product where Name='" + listBox_search.SelectedItem + "'", con);
                int ID_publisher = Convert.ToInt32(cmd.ExecuteScalar());

                cmd = new SqlCommand("select Name from Category where ID='" + ID_cat + "'", con);
                reader = cmd.ExecuteReader();
                string cat_name = string.Empty;
                if (reader.Read())
                {
                    cat_name = reader["Name"].ToString();
                }
                reader.Close();

                cmd = new SqlCommand("select Name from User_Info where UserID='" + ID_publisher + "'", con);
                reader = cmd.ExecuteReader();
                string publisher_name = string.Empty;
                if (reader.Read())
                {
                    publisher_name = reader["Name"].ToString();
                }
                reader.Close();

                cmd = new SqlCommand("select Name,Picture,Minimum_Amount,Date_Of_Expiring,Date_Of_Production,Description,Price from Product where Name='" + listBox_search.SelectedItem + "'", con);
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    label1_product_name.Text = reader["Name"].ToString();

                    if (reader["Picture"] != DBNull.Value)
                    {
                        
                        //pictureBox_products.Image = Image.FromFile(reader["Picture"].ToString());
                        pictureBox_products.Image = Image.FromFile("C:\\Users\\pc\\Downloads\\Shopping-Management-System-master\\Shopping-Management-System-master\\Shopping Management System/"+ reader["Picture"].ToString());
                    }
                    else
                    {
                        pictureBox_products.Image = Properties.Resources.images;

                    }

                    label1_min_amount.Text = reader["Minimum_Amount"].ToString();
                    label1_dateexpir.Text = reader["Date_Of_Expiring"].ToString() != string.Empty ? reader["Date_Of_Expiring"].ToString() : "-/-/-";
                    label1_dateprod.Text = reader["Date_Of_Production"].ToString() != string.Empty ? reader["Date_Of_Production"].ToString() : "-/-/-";
                    label1_description.Text = reader["Description"].ToString();
                    label1_price.Text = reader["Price"].ToString();
                }
                reader.Close();

                label1_cat_name.Text = cat_name;
                label1_pub_name.Text = publisher_name;

                con.Close();
            }
        }


        private void comboBox_categories_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=Shopping System;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("select ID from Category where Name='" + comboBox_categories.Text + "' ", con);
            int ID = Convert.ToInt32(cmd.ExecuteScalar());

            /*****************************/
            if (checkBox_price.Checked)
            {
                cmd = new SqlCommand("select Name From Product where Category_ID='" + ID + "'and Status='1' order by Price", con);
                SqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Columns.Add("Name");
                DataRow dr;
                while (reader.Read())
                {
                    dr = dt.NewRow();
                    dr["Name"] = reader["Name"];
                    dt.Rows.Add(dr);

                }
                reader.Close();
                listBox_search.Items.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    listBox_search.Items.Add(row[0].ToString());
                }
                con.Close();
            }
            else if (checkBox_alpha.Checked)
            {
                cmd = new SqlCommand("select Name From Product where Category_ID='" + ID + "'and  order by Name", con);
                SqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Columns.Add("Name");
                DataRow dr;
                while (reader.Read())
                {
                    dr = dt.NewRow();
                    dr["Name"] = reader["Name"];
                    dt.Rows.Add(dr);

                }
                reader.Close();
                listBox_search.Items.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    listBox_search.Items.Add(row[0].ToString());
                }
                con.Close();
            }
            else if (checkBox_price.Checked && checkBox_alpha.Checked)
            {
                cmd = new SqlCommand("select Name From Product where Category_ID='" + ID + "'and Status='1' order by Price and Name ", con);
                SqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Columns.Add("Name");
                DataRow dr;
                while (reader.Read())
                {
                    dr = dt.NewRow();
                    dr["Name"] = reader["Name"];
                    dt.Rows.Add(dr);

                }
                reader.Close();
                listBox_search.Items.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    listBox_search.Items.Add(row[0].ToString());
                }
                con.Close();
            }
            else
            {
                cmd = new SqlCommand("select Name From Product where Category_ID='" + ID + "'and Status='1'  ", con);
                SqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Columns.Add("Name");
                DataRow dr;
                while (reader.Read())
                {
                    dr = dt.NewRow();
                    dr["Name"] = reader["Name"];
                    dt.Rows.Add(dr);

                }
                reader.Close();
                listBox_search.Items.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    listBox_search.Items.Add(row[0].ToString());
                }
                con.Close();
            }
        }

        private void button_cance_done_Click(object sender, EventArgs e)
        {
            panel_done.Visible = false;
            panel_buy.Visible = false;
        }

        private void pictureBox_profile_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=Shopping System;Integrated Security=True");
            con.Open();

            SqlCommand cmd = new SqlCommand("select User_Type From User_Info where Name='" + var.current_username + "'and Password='"+var.current_pass+"'  ", con);
            SqlDataReader reader = cmd.ExecuteReader();
            bool user_type = false;
            reader.Read();
            user_type = Convert.ToBoolean(reader["User_Type"]);
            if (user_type==false)
            {
            Profile_User profile_User = new Profile_User();
            profile_User.Show();
            this.Hide();
            }
            else
            {
                Profile_Admin profile_Admin = new Profile_Admin();
                profile_Admin.Show();
                this.Hide();
            }
            
        }

        private void Cart_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=Shopping System;Integrated Security=True");
            con.Open();

            SqlCommand cmd = new SqlCommand("delete Product where Name='" + label1_product_name.Text + "'", con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Done");
            con.Close();

            Cart cart = new Cart();
            cart.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            hello_window hello_Window = new hello_window();
            hello_Window.Show();
            this.Hide();
        }

        private void button_search_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=Shopping System;Integrated Security=True"))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("SELECT Name FROM Product WHERE Name LIKE '%' + @name_of_product + '%'", con);
                cmd.Parameters.AddWithValue("@name_of_product", textBox_search.Text);

                SqlDataReader reader = cmd.ExecuteReader();
                DataTable tbl_product = new DataTable();
                tbl_product.Columns.Add("Name");
                DataRow row;
                while (reader.Read())
                {
                    row = tbl_product.NewRow();
                    row["Name"] = reader["Name"];
                    tbl_product.Rows.Add(row);
                }
                reader.Close();

                listBox_search.Items.Clear();
                foreach (DataRow Row in tbl_product.Rows)
                {
                    listBox_search.Items.Add(Row[0].ToString());
                }
            }
        }

    }
}
