using System;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Net.Mail;

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

        public Account(int id,decimal balance,string type,int capital, int UserID):this()
        {
            this.id = id;
            balanceBox.Text = balance.ToString();
            comboBox_Type.SelectedIndex = (type == "Кредитный") ? 1 : 0;
            textBox_Capitalisation.Text = capital.ToString();
            comboBox_UserId.SelectedValue = UserID;
            edit = true;
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@bal", Convert.ToDecimal(balanceBox.Text));
            SqlCommand cmd2 = new SqlCommand();
            cmd2.Parameters.Clear();

            // if update or edit form 
            if (edit)
            {
                
                cmd.CommandText = "Update Accounts SET" +
                    "[Balance]= @bal," +
                    "[Type]= N'" + comboBox_Type.Text + "'," +
                    "[Capitalisation]=" + Convert.ToInt16(textBox_Capitalisation.Text) + "," +
                    "[UserId]="+Convert.ToInt32(comboBox_UserId.SelectedValue)+
                    "Where Id=" + this.id;

                accountsTableAdapter.UpdateQuery(
                    Convert.ToDecimal(balanceBox.Text),
                    comboBox_Type.Text,
                    Convert.ToInt16(textBox_Capitalisation.Text),
                    Convert.ToInt32(comboBox_UserId.SelectedValue.ToString()),
                    this.id
                    );
            }
            else
            {
                cmd.CommandText = "INSERT INTO [Accounts] ([Balance], [Type], [Capitalisation],"+ 
                    "[UserId]) VALUES (" +
                    "@bal, N'" +
                    comboBox_Type.Text.ToString() + "'," +
                    Convert.ToInt16(textBox_Capitalisation.Text) + "," +
                    Convert.ToInt32(comboBox_UserId.SelectedValue) + ")";

                accountsTableAdapter.InsertQuery(
                    Convert.ToDecimal(balanceBox.Text),
                    comboBox_Type.Text.ToString(),
                    Convert.ToInt16(textBox_Capitalisation.Text),
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
            createAgreement();
            agreementTableAdapter.Update(bankDataSet.Agreement);
            clientsTableAdapter.Update(bankDataSet.Clients);
            bankDataSet.AcceptChanges();
            
            sendStockMessage();
            Close();
        }

        private void button_Exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Account_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'bankDataSet.Managers' table. You can move, or remove it, as needed.
            this.managersTableAdapter.Fill(this.bankDataSet.Managers);
            comboBox_Type.SelectedValue = 0;
            //comboBox_UserId.SelectedValue = 0;
            comboBox1.SelectedValue = 3;
        }

        private void comboBox_UserId_TextChanged(object sender, EventArgs e)
        {
            //clientsBindingSource.DataSource = bankDataSet.Clients;
            //if (comboBox_UserId.Text != "")
            //    clientsBindingSource.Filter = "[Full Name] like '%" + comboBox_UserId.Text.ToString() + "%'";
            //else clientsBindingSource.RemoveFilter();
        }

        private void createAgreement()
        {
            if (!edit)
            {
                SqlConnection sqlcon = new SqlConnection(ConnectionString);
                DataTable dt = GetDataTable("SELECT TOP 1 Id" +
                  " FROM Accounts Order By Id Desc"
                  );
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = sqlcon;
                cmd.CommandText = "Insert INTO Agreement (Account_Id, Manager_Id) Values(" +
                    Convert.ToInt32(dt.Rows[0][0].ToString()) + "," +
                    Convert.ToInt32(comboBox1.SelectedValue) + ")";
                sqlcon.Open();
                cmd.ExecuteNonQuery();
                sqlcon.Close();
                agreementTableAdapter.InsertQuery(Convert.ToInt32(dt.Rows[0][0].ToString()), Convert.ToInt32(comboBox1.SelectedValue));
            }
        }
        private DataTable GetDataTable(string SQL)
        {
            SqlCommand command = new SqlCommand(SQL);
            SqlConnection conn = new SqlConnection(ConnectionString);
            command.Connection = conn;
            conn.Open();
            using (SqlDataReader dr = command.ExecuteReader())
            {
                var tb = new DataTable();
                tb.Load(dr);
                return tb;
            }
        }

        private void sendStockMessage()
        {
            //creating smtp client
            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.Timeout = 10000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("nikita.ghost.yar@gmail.com", "1n2i3k4i5t6a");

            //getting emails
            DataTable dt = GetDataTable("SELECT [Full Name], [E-mail], [Type], Accounts.[Capitalisation], [Stock Starts], [Stock Ends], [Condition], [Result]"+
                " FROM Clients, Accounts, Stocks Where [Clients].[Id]=Accounts.[UserId] AND [Clients].[Id]="+Convert.ToInt32(comboBox_UserId.SelectedValue)+
                " AND Accounts.Type=Stocks.[Account Type]"+
                " AND Accounts.[Capitalisation]=Stocks.[Capitalisation]");

            //Sending message
            foreach (DataRow dr in dt.Rows)
            {
                if (DateTime.Now <= Convert.ToDateTime(dr[5]))
                {
                    MailMessage mm = new MailMessage("do@not.reply", dr[1].ToString(), "Акция", "Ув." + dr[0] + "\r\n" +
                        "В нашем банке есть акция, которая может вас заинтересовать.\r\nОна продлится с: " + dr[4].ToString() +
                        " до " + dr[5] + "\r\n" +
                        "Условия таковы: " + dr[6] + "\r\n" +
                        "В результате вы: " + dr[7] + "\r\nЖдем Вас в ближайшем филиале нашего банка! \r\nС Уважением, Ваш It-Bank");
                    mm.BodyEncoding = UTF8Encoding.UTF8;
                    mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                    client.Send(mm);
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = GetDataTable("SELECT [FullName], [City], [Address]" +
               " FROM Branches, Managers" +
               " Where [Branches].[Id]=[Managers].[Branche_Id] AND"+
               " [Managers].[Id]="+Convert.ToInt32(comboBox1.SelectedValue)
               );
            managerNameLabel.Text = dt.Rows[0][0].ToString().Split(' ')[0] + "\r\n" + dt.Rows[0][0].ToString().Split(' ')[1];
            managerWorkAddressLabel.Text = dt.Rows[0][1].ToString() +"\r\n"+ dt.Rows[0][2].ToString() ;
        }
    }
}
