using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Collections;

namespace VsMajor
{
    public partial class StockForm : Form
    {

        MySqlConnector connector = new MySqlConnector();

        public StockForm()
        {
            InitializeComponent();
            showStock();
            connector.CloseConnection();
        }
        
        private void showStock()
        {
            connector.connection.Open();
            string Query = "SELECT * FROM product";
            
            //MySqlDataReader priductDataReader = sqlProductCommand.ExecuteReader();

            MySqlDataAdapter sda = new MySqlDataAdapter(Query,connector.connection);
            MySqlCommandBuilder builder= new MySqlCommandBuilder(sda);
            var ds = new DataSet();

            sda.Fill(ds);

            stockDGV.DataSource = ds.Tables[0];
            connector.connection.Close();

        }

        private void clear()
        {
            textProductName.Text = "";
            textPrice.Text = "";
            textSale.Text = "";
            textStock.Text = "";
        }


        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textProductName_TextChanged(object sender, EventArgs e)
        {

        }

        private void AddProduct_Click(object sender, EventArgs e)
        {


            connector.OpenConnection();

            string productName = textProductName.Text.ToString();
            string pName = textPrice.Text;
            string pSale = textSale.Text;
            string pStock = textStock.Text;

            if (String.IsNullOrEmpty(productName) ||
                String.IsNullOrEmpty(pName) ||
                String.IsNullOrEmpty(pSale) ||
                String.IsNullOrEmpty(pStock)
                )
            {
                MessageBox.Show("Empty Fields");
            }
            else 
            { 


                int productPrice = Convert.ToInt32(textPrice.Text);
                int prodcutSale = Convert.ToInt32(textSale.Text);
                int productStock = Convert.ToInt32(textStock.Text);
                //int productPrice = Convert.ToInt32(textPrice);
                //int prodcutSale = Convert.ToInt32(textSale);
                //int productStock = Convert.ToInt32(textStock);

                //productName.Replace("\\", "\\'");

                MessageBox.Show(productName + " " + productPrice + " " + prodcutSale + " " + productStock);
                string insetQuery = $"INSERT INTO product(Name, Price, Sale, Stock) VALUES('{productName}', {productPrice}, {prodcutSale}, {productStock});";



                MySqlCommand mySqlCommandInset = new MySqlCommand(insetQuery, connector.connection);
                 mySqlCommandInset.ExecuteReader();
                

                // 

               //string 

                MessageBox.Show("Data  Added");

                connector.CloseConnection();
                showStock();
                clear();
            }

            //string 
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {


           /* string connectionString = "server=localhost; database=profitmanagement; user=root; password=;";



            MySqlConnection connection = new MySqlConnection(connectionString);
            string Query = "SELECT * FROM product";

          

            connection.Open();

    


            if (connection.State == ConnectionState.Open)
            {
                MessageBox.Show("New Connection");


                MySqlCommand sqlProductCommand = new MySqlCommand(Query, connection);
                //MySqlDataReader priductDataReader = sqlProductCommand.ExecuteReader();

                MySqlDataAdapter sda = new MySqlDataAdapter(sqlProductCommand);
                DataTable dt = new DataTable();

                sda.Fill(dt);

                dataGridView1.DataSource = dt;


                // while(priductDataReader.Read())
                //{

                // MessageBox.Show(priductDataReader.GetString(0) + priductDataReader.GetString(1));
                //}
                

                connection.Close();


            }
            else
            {
                MessageBox.Show("DB Connection Failed");
                connection.Close();
            }
           */

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            
        }
        int key = 0;

        private void stockDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            stockDGV.CurrentRow.Selected = true;
            textProductName.Text = stockDGV.CurrentRow.Cells[1].Value.ToString();
            textPrice.Text = stockDGV.CurrentRow.Cells[2].Value.ToString();
            textSale.Text = stockDGV.CurrentRow.Cells[3].Value.ToString();
            textStock.Text = stockDGV.CurrentRow.Cells[4].Value.ToString();

            if (textProductName.Text == "")
            {
                MessageBox.Show("Select a row");
            }
            else
            {
                key = Convert.ToInt32(stockDGV.CurrentRow.Cells[0].Value.ToString());
                //MessageBox.Show(Convert.ToString(key));
            }
        }
        

        private void button3_Click(object sender, EventArgs e)
        {
            AvailableStock availableStock = new AvailableStock();
            availableStock.ShowDialog();
        }

        private void StockForm_Load(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if(key == 0)
            {
                MessageBox.Show("Select a row");
            }
            else
            {
                
                connector.OpenConnection();
                
                MySqlCommand cmd  = new MySqlCommand("UPDATE product SET Name = @pn, Price = @pp , Sale = @ps , Stock = @pstock WHERE Id = @k", connector.connection);
                cmd.Parameters.AddWithValue("@pn", textProductName.Text);
                cmd.Parameters.AddWithValue("@pp", textPrice.Text);
                cmd.Parameters.AddWithValue("@ps", textSale.Text);
                cmd.Parameters.AddWithValue("@pstock", textStock.Text);
                cmd.Parameters.AddWithValue("@k", key);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Updated Succesfully");
                connector.CloseConnection();
                showStock();
                clear();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Select a row");
            }
            else
            {
                connector.OpenConnection();
                MySqlCommand cmd = new MySqlCommand("DELETE from product WHERE Id = @key", connector.connection);
                cmd.Parameters.AddWithValue("@key", key);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Deleted");
                connector.CloseConnection();
                showStock();
                clear();
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
