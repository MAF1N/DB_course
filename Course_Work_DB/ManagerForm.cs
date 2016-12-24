using System;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Course_Work_DB
{
    public partial class ManagerForm : Form
    {
        const string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='C:\LABS\Курс 2\Семестр 1\Course_Work_DB\Course_Work_DB\Bank.mdf';Integrated Security = True";
        private readonly bool edit;
        private readonly int id;
        public ManagerForm()
        {
            InitializeComponent();
            branchesTableAdapter.Fill(bankDataSet.Branches);
            managersTableAdapter.Fill(bankDataSet.Managers);
            brancheComboBox.SelectedIndex = 0;
            edit = false;
        }
        public ManagerForm(int id, string fio, int exp, string phone,DateTime apDate, int branceId):this()
        {
            this.id = id;
            FullNameBox.Text = fio;
            ExpBox.Text = exp.ToString();
            PhoneNumberBox.Text = phone;
            dateOfAppoinment.Value = apDate;
            brancheComboBox.SelectedValue = branceId;
            edit = true;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@date", dateOfAppoinment.Value);
            if (edit)
            {
                
                cmd.CommandText = "Update Managers SET" +
                    "[FullName]=N'" + FullNameBox.Text + "'," +
                    "[Experience]=" + Convert.ToInt16(ExpBox.Text) + "," +
                    "[Phone]='" + PhoneNumberBox.Text + "'," +
                    "[Appointment Date]=" +"@date,"+
                    "[Branche_Id]=" + Convert.ToInt32(brancheComboBox.SelectedValue) +
                    "WHERE Id=" + this.id;
                managersTableAdapter.UpdateQuery(
                    FullNameBox.Text,
                    Convert.ToInt16(ExpBox.Text),
                    PhoneNumberBox.Text,
                    dateOfAppoinment.Value.ToString(),
                    Convert.ToInt32(brancheComboBox.SelectedValue),
                    this.id
                    );
            }
            else
            {
                cmd.CommandText = "INSERT INTO [Managers] ([FullName], [Experience], [Phone], [Appointment Date], [Branche_Id]) VALUES ( N'" +
                    FullNameBox.Text + "'," +
                    Convert.ToInt16(ExpBox.Text) + ", '" +
                    PhoneNumberBox.Text + "'," +
                    "@date," +
                    Convert.ToInt32(brancheComboBox.SelectedValue) +
                    ")";
                managersTableAdapter.InsertQuery(
                    FullNameBox.Text,
                    Convert.ToInt16(ExpBox.Text),
                    PhoneNumberBox.Text,
                    dateOfAppoinment.Value.ToString(),
                    Convert.ToInt32(brancheComboBox.SelectedValue)
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
            managersTableAdapter.Update(bankDataSet.Managers);
            bankDataSet.AcceptChanges();
            Close();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
