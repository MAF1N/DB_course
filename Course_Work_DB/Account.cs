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

namespace Course_Work_DB
{
    public partial class Account : Form
    {
        const string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='C:\LABS\Курс 2\Семестр 1\Course_Work_DB\Course_Work_DB\Bank.mdf';Integrated Security = True";
        private readonly int id;
        readonly bool edit;
        public Account()
        {
            InitializeComponent();
            clientsTableAdapter.Fill(bankDataSet.Clients);
            accountsTableAdapter.Fill(bankDataSet.Accounts);
            edit = false;
        }
        public Account(int id,decimal balance,string type,int capital, DateTime lastw,DateTime lastR, int UserID):this()
        {
            this.id = id;
            balanceBox.Text = balance.ToString();
            comboBox_Type.SelectedIndex = (type == "Кредитный") ? 1 : 0;
            textBox_Capitalisation.Text = capital.ToString();
            dateTimePicker_withdrawal.Value = lastw;
            dateTimePicker_refill.Value = lastR;
            comboBox_UserId.Text = UserID.ToString();
            edit = true;
        }
        private void button_OK_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@date1", dateTimePicker_withdrawal.Value);
            cmd.Parameters.AddWithValue("@date2", dateTimePicker_refill.Value);
            cmd.Parameters.AddWithValue("@bal", Convert.ToDecimal(balanceBox.Text));
            if (edit)
            {
                
                cmd.CommandText = "Update Accounts SET" +
                    "[Balance]= @bal," +
                    "[Type]= N'" + comboBox_Type.Text + "'," +
                    "[Capitalisation]=" + Convert.ToInt16(textBox_Capitalisation.Text) + "," +
    //                "[Last withdrawal]= @date1," +
    //                "[Last refill]= @date2, "+
                    "[UserId]="+Convert.ToInt32(comboBox_UserId.Text)+
                    "Where Id=" + this.id;

                accountsTableAdapter.UpdateQuery(
                    Convert.ToDecimal(balanceBox.Text),
                    comboBox_Type.Text,
                    Convert.ToInt16(textBox_Capitalisation.Text),
                    dateTimePicker_withdrawal.Value.ToString(),
                    dateTimePicker_refill.Value.ToString(),
                    Convert.ToInt32(comboBox_UserId.SelectedValue.ToString()),
                    this.id
                    );
            }
            else
            {
                cmd.CommandText = "INSERT INTO [Accounts] ([Balance], [Type], [Capitalisation],"+ 
                    "[Last withdrawal], [Last refill],"+
                    "[UserId]) VALUES (" +
                    "@bal, N'" +
                    comboBox_Type.Text.ToString() + "'," +
                    Convert.ToInt16(textBox_Capitalisation.Text) + "," +
                    "@date1, @date2," +
                    Convert.ToInt32(comboBox_UserId.SelectedValue) + ")";

                accountsTableAdapter.InsertQuery(
                    Convert.ToDecimal(balanceBox.Text),
                    comboBox_Type.Text.ToString(),
                    Convert.ToInt16(textBox_Capitalisation.Text),
                    dateTimePicker_withdrawal.Value.ToString(),
                    dateTimePicker_refill.Value.ToString(),
                    Convert.ToInt32(comboBox_UserId.SelectedValue)
                    );
            }
            try
            {
                SqlConnection sqlconn = new SqlConnection(ConnectionString);
                sqlconn.Open();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = sqlconn;
                cmd.ExecuteNonQuery();
                sqlconn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"ERROR: " + ex.Message);
            }
            clientsTableAdapter.Update(bankDataSet.Clients);
            bankDataSet.AcceptChanges();
            Close();
        }

        private void button_Exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Account_Load(object sender, EventArgs e)
        {
            comboBox_Type.SelectedValue = 0;
            //comboBox_UserId.SelectedValue = 0;
            MessageBox.Show("Selected " + comboBox_UserId.SelectedValue.ToString());
        }

        private void comboBox_UserId_TextChanged(object sender, EventArgs e)
        {
            clientsBindingSource.DataSource = bankDataSet.Clients;
            clientsBindingSource.Filter = "[Full Name] like '%" + comboBox_UserId.Text.ToString() + "%'";
        }

        private void comboBox_UserId_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
