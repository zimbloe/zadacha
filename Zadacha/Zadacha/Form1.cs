using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace Zadacha
{
    public partial class Form1 : Form
    {
        private string connectionString = "Data Source = local; Initial Catalog=AdventureWorks;Integrated Security=True";
        private SqlConnection connection;
        private SqlCommand command;
        private SqlDataReader reader;
        private int currentRowIndex = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                connection  = new SqlConnection(connectionString);
                connection.Open();
                command = new SqlCommand("Select CustomerID, FirstName, MiddleName, LastName, CompanyName FROM SalesLT.Customer ORDER BY CompanyName", connection);
                reader = command.ExecuteReader();

                if(reader.HasRows)
                {
                    reader.Read();
                    DisplayCustomerData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greshka pri svurzvaneto s bazata danni" + ex.Message);
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if(currentRowIndex > 0 )
            {
                currentRowIndex--;
                DisplayCustomerData();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(reader != null && reader.HasRows && currentRowIndex < reader.FieldCount - 1)
            {
                currentRowIndex++;
                DisplayCustomerData();
            }
        }
        
        private void DisplayCustomerData()
        {
            txtCustomerID.Text = reader.GetInt32(0).ToString();
            txtFirstName.Text = reader.GetString(1);
            txtMiddleName.Text = reader.GetString(2);
            txtLastName.Text = reader.GetString(3);
            txtCompanyName.Text = reader.GetString(4);
        }

        private void Form1_Load_Closing(object sender, FormClosedEventArgs e)
        {
            if(reader != null)
            {
                reader.Close();
            }

            if(connection != null && connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
    }
}
