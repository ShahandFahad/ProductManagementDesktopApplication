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

namespace VsMajor
{
    public partial class Home : Form
    {
        string connectionString = "server=localhost; database=product_management; user=root; password=;";

        MySqlConnection connection;
        public Home()
        {
            InitializeComponent();
            
            connection = new MySqlConnection(connectionString);
            showSale();

        }

        private void showSale()
        {
            connection.Open();

            string Query = "SELECT * FROM sale";


            if (connection.State == ConnectionState.Open)
            {
                //MessageBox.Show("DB Connected");
                MySqlCommand sqlSaleCommand = new MySqlCommand(Query, connection);
                //MySqlDataReader priductDataReader = sqlProductCommand.ExecuteReader();

                MySqlDataAdapter sda = new MySqlDataAdapter(sqlSaleCommand);
                DataTable dt = new DataTable();

                sda.Fill(dt);

                dataGridView1.DataSource = dt;

                connection.Close();
            }
            else
            {
                //MessageBox.Show("DB Connection Failed");
                connection.Close();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            StockForm stock = new StockForm();
            stock.ShowDialog();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnAddSales_Click(object sender, EventArgs e)
        {

            string pId = productID.Text;
            string pSolds = soldItems.Text;


            if (String.IsNullOrEmpty(pId) ||
                String.IsNullOrEmpty(pSolds))
            {
                MessageBox.Show("Empty Fields");
            }
            else
            {

                int productId = Convert.ToInt32(pId);
                int productSales = Convert.ToInt32(pSolds);


                //string Query1  = $"SELECT product.Name FROM product WHERE product.Id";


                connection = new MySqlConnection(connectionString);
                connection.Open();

                //string Query = $"INSERT INTO sale (item_sold, profit) VALUES(${productSales}, ";
                string Query = $"SELECT product.Name, product.Price FROM product WHERE product.Id = {productId}";
                string productNmae = "";
                int productPrice = 0;
                if (connection.State == ConnectionState.Open)
                {
                  //  MessageBox.Show("DB Connected Sales add");
                    MySqlCommand sqlSaleCommand = new MySqlCommand(Query, connection);
                    MySqlCommand saleInsert = new MySqlCommand(Query, connection);
                    MySqlDataReader reader =  saleInsert.ExecuteReader();

                     while (reader.Read())
                    {

                        MessageBox.Show(reader.GetString(0) + " " + reader.GetInt32(1));
                        productNmae = reader.GetString(0);
                        productPrice = reader.GetInt32(1);
                    }

                    reader.Close();

                    if (String.IsNullOrEmpty(productNmae))
                    {
                        MessageBox.Show("Item Not Found");
                    }
                    else
                    {
                        int profit = productPrice * productSales;
                        string Query1 = $"INSERT INTO sale (product_name, product_sold, profit) VALUES ('{productNmae}', {productSales}, {profit});";
                        MySqlCommand sqlSaleCommand2 = new MySqlCommand(Query1, connection);
                        MySqlCommand saleInser2t = new MySqlCommand(Query1, connection);
                        saleInser2t.ExecuteReader();
                    }

                    


                    connection.Close();
                    showSale();
                }

               
            }
        }

        int key = 0;
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.CurrentRow.Selected = true;
            productID.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            soldItems.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            

            if (productID.Text == "")
            {
                MessageBox.Show("Select a row");
            }
            else
            {
                key = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                //MessageBox.Show(Convert.ToString(key));
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(key == 0)
            {
                MessageBox.Show("Select a row");
            }
            else
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("DELETE from sale WHERE Id = @key", connection);
                cmd.Parameters.AddWithValue("@key", key);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Delted");
                connection.Close();
                showSale();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
