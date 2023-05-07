using JawadMajor;
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

namespace VsMajor
{
    public partial class AvailableStock : Form
    {
        string connectionString = "server=localhost; database=product_management; user=root; password=;";



        MySqlConnection connection;
        string Query = "SELECT * FROM product";
        public AvailableStock()
        {
            InitializeComponent();

            connection = new MySqlConnection(connectionString);
            connection.Open();

            if (connection.State == ConnectionState.Open)
            {
                //MessageBox.Show("Stock Connection");


                MySqlCommand sqlProductCommand = new MySqlCommand(Query, connection);
                //MySqlDataReader priductDataReader = sqlProductCommand.ExecuteReader();

                MySqlDataAdapter sda = new MySqlDataAdapter(sqlProductCommand);
                DataTable dt = new DataTable();

                sda.Fill(dt);

                dataGridView1.DataSource = dt;

                connection.Close();
            }
            else
            {
               // MessageBox.Show("DB Connection Failed");
                connection.Close();
            }

        }

        private void AvailableStock_Load(object sender, EventArgs e)
        {

        }

        private void logInBtn_Click(object sender, EventArgs e)
        {

            string UserID = userID.Text.ToString();
            string passwrod = userPass.Text.ToString();

            if(string.IsNullOrEmpty(userID.Text) || string.IsNullOrEmpty(passwrod))
            {
                MessageBox.Show("Please Insert Id or Password!");
            }
            else
            {
                int id = int.Parse(UserID);
                //MessageBox.Show(id + passwrod);

                connection = new MySqlConnection(connectionString);
                connection.Open();

                if (connection.State == ConnectionState.Open)
                {
                    //MessageBox.Show("Stock Connection");


                    MySqlCommand sqlProductCommand = new MySqlCommand("SELECT * FROM users", connection);
                    MySqlDataReader userReader = sqlProductCommand.ExecuteReader();
                    int tempUserID = 0;
                    string tempPasswrod = "";
                    while(userReader.Read())
                    {
                        tempUserID = int.Parse(userReader.GetString(0));
                        tempPasswrod = userReader.GetString(1);

                       // MessageBox.Show(userReader.GetString(1));
                        //MessageBox.Show(userReader.GetString(1));

                        break;
                    }

                    if (tempUserID == id && tempPasswrod == passwrod)
                    {
                        Home home = new Home();
                        home.Show();
                    }
                    else
                    {
                        MessageBox.Show("Invalid User or Passowrd");
                    }

                    connection.Close();
                }
                else
                {
                    // MessageBox.Show("DB Connection Failed");
                    connection.Close();
                }


            }
        }

        private void userID_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void regBtn_Click(object sender, EventArgs e)
        {
            Register register = new Register();
            register.Show();
        }
    }
}
