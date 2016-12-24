using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Course_Work_DB
{
    public partial class Operation : Form
    {
        const string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='C:\LABS\Курс 2\Семестр 1\Course_Work_DB\Course_Work_DB\Bank.mdf';Integrated Security = True";
        public Operation()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                Close();
            }
            else
            {
                try
                {
                    SqlConnection sqlconn = new SqlConnection(ConnectionString);
                    SqlCommand cmd = new SqlCommand();
                    bool temp = true;
                    SqlCommand cmd2 = new SqlCommand();
                    cmd2.Connection = sqlconn;
                    cmd2.Parameters.Clear();
                    cmd2.Parameters.AddWithValue("@bal", Convert.ToDecimal(textBox1.Text));
                    if (typeCombo.Text.Contains("Снятие"))
                    {
                        temp = false;
                        cmd2.CommandText = "Update Accounts Set Balance=Balance- @bal Where Id=" + Convert.ToInt32(accIdCombo.Text);
                        accountsTableAdapter.UpdateBalanceMinus(Convert.ToDecimal(textBox1.Text), Convert.ToInt32(accIdCombo.Text));
                    }
                    else
                    {
                        cmd2.CommandText = "Update Accounts Set Balance=Balance+ @bal Where Id=" + Convert.ToInt32(accIdCombo.Text);
                        accountsTableAdapter.UpdateBalanceAdd(Convert.ToDecimal(textBox1.Text), Convert.ToInt32(accIdCombo.Text));
                    }
                    cmd.CommandText = "Insert INTO Operations (Type,Amount,Account_Id,Income) Values(N'" +
                        typeCombo.Text + "'," +
                        Convert.ToDouble(textBox1.Text) + "," +
                        accIdCombo.SelectedValue + ",'" +
                        temp + "')";
                    cmd.Connection = sqlconn;
                    sqlconn.Open();
                    cmd.ExecuteNonQuery();
                    cmd2.ExecuteNonQuery();
                    sqlconn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(@"ERROR: " + ex.Message);
                }
                Close();
            }
        }

        private void Operation_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'bankDataSet.Clients' table. You can move, or remove it, as needed.
            this.clientsTableAdapter.Fill(this.bankDataSet.Clients);
            // TODO: This line of code loads data into the 'bankDataSet.Accounts' table. You can move, or remove it, as needed.
            this.accountsTableAdapter.Fill(this.bankDataSet.Accounts);
            accIdCombo.SelectedValue = 0;
            typeCombo.SelectedValue = 0;
           
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            accountsBindingSource.DataSource = bankDataSet.Accounts;
            accountsBindingSource.Filter = accIdCombo.Text;
        }
    }
}
