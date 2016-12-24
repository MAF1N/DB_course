using System;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Course_Work_DB
{
    public partial class ClientForm : Form
    {
        const string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='C:\LABS\Курс 2\Семестр 1\Course_Work_DB\Course_Work_DB\Bank.mdf';Integrated Security = True";
        private readonly int id;
        readonly bool edit;
        public ClientForm()
        {
            InitializeComponent();
            clientsTableAdapter.Fill(bankDataSet.Clients);
            edit = false;
        }
        public ClientForm(int id, string fN,string gender, string city, string address, string phone, DateTime birthday, string email):this()
        {
            this.id = id;
            textBox_FN_Client.Text = fN;
            switch (gender.ToLower())
            {
                case "мужской":
                    genderBox.SelectedIndex = 0;
                    break;
                case "женский":
                    genderBox.SelectedIndex = 1;
                    break;
                default:
                    genderBox.SelectedIndex = 0;
                    break;
            }
            cityBox.Text = city;
            addressBox.Text = address;
            phoneBox.Text = phone;
            datePicker_Birthday.Value = birthday;
            emailBox.Text = email;
            edit = true;
        }

        private void button_Exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            
            string gender = "";
            if (genderBox.SelectedIndex == 0)
            {
                gender = "мужской";
            }
            else
            {
                gender = "женский";
            }
            if (edit)
            {
                cmd.Parameters.Clear();
                DateTime date = datePicker_Birthday.Value;
                cmd.Parameters.AddWithValue("@date", date);
                cmd.CommandText = "Update clients SET "+
                    "[Full Name]= N'"+ textBox_FN_Client.Text.ToString() + "'," +
                    "[Gender]= N'"+gender + "'," +
                    "[City]= N'" + cityBox.Text.ToString() + "'," +
                    "[Address]= N'" + addressBox.Text.ToString() + "'," +
                    "[Phone]=" + phoneBox.Text + "," +
                    "[Birthday]=" + "@date, " +
                    "[E-mail]= '"+
                    emailBox.Text.ToString() + "' WHERE Id="+this.id;
                clientsTableAdapter.UpdateQuery(
                    textBox_FN_Client.Text,
                    gender,
                    cityBox.Text,
                    addressBox.Text,
                    phoneBox.Text,
                    datePicker_Birthday.Value.ToString(),
                    emailBox.Text,
                    this.id
                    );
            }
            else
            {
                cmd.Parameters.Clear();
                DateTime date = datePicker_Birthday.Value;
                cmd.Parameters.AddWithValue("@date", date);
                cmd.CommandText = "insert into clients ([Full Name],[Gender], [City], [Address], [Phone], [Birthday],[E-mail]) Values (N'" +
                    textBox_FN_Client.Text.ToString() + "', N'" +
                    gender + "', N'" +
                    cityBox.Text.ToString() + "', N'" +
                    addressBox.Text.ToString() + "'," +
                    phoneBox.Text + "," +
                     "@date, '" +
                    emailBox.Text.ToString()+"')";
                //Костыль
                clientsTableAdapter.InsertQuery(
                    textBox_FN_Client.Text,
                    gender,
                    cityBox.Text,
                    addressBox.Text,
                    phoneBox.Text,
                    datePicker_Birthday.Value.ToString(),
                    emailBox.Text
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
            clientsTableAdapter.Fill(bankDataSet.Clients);
            bankDataSet.AcceptChanges();
            if (!edit)
            {
                var af = new Account();
                af.ShowDialog();
            }
            Close();
        }

        private void ClientForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }
    }
}
