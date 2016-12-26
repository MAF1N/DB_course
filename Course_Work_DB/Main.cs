using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Net.Mail;
using System.Diagnostics;

namespace Course_Work_DB
{
    public partial class Main : Form
    {
        const string ConnectionString =@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='C:\LABS\Курс 2\Семестр 1\Course_Work_DB\Course_Work_DB\Bank.mdf';Integrated Security = True";     


        public Main()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'bankDataSet.Clients' table. You can move, or remove it, as needed.
            this.clientsTableAdapter.Fill(this.bankDataSet1.Clients);
            // TODO: This line of code loads data into the 'bankDataSet.Operations' table. You can move, or remove it, as needed.
            this.operationsTableAdapter.Fill(this.bankDataSet1.Operations);
            // TODO: This line of code loads data into the 'bankDataSet.Managers' table. You can move, or remove it, as needed.
            this.managersTableAdapter.Fill(this.bankDataSet1.Managers);
            // TODO: This line of code loads data into the 'bankDataSet.Branches' table. You can move, or remove it, as needed.
            this.branchesTableAdapter.Fill(this.bankDataSet1.Branches);
            // TODO: This line of code loads data into the 'bankDataSet.Agreement' table. You can move, or remove it, as needed.
            this.agreementTableAdapter.Fill(this.bankDataSet1.Agreement);
            // TODO: This line of code loads data into the 'bankDataSet.Accounts' table. You can move, or remove it, as needed.
            this.accountsTableAdapter.Fill(this.bankDataSet1.Accounts);

            comboBoxSearchBy.Items.AddRange(new String[] { "Full Name", "Address", "E-mail" });
            comboBoxSearchBy.SelectedIndex = 0;

            getClientCities();
            bindingNavigator1.BindingSource = clientsBindingSource;
            dataGridView1.DataSource = clientsBindingSource;
            label_Current_Base.Text = "Clients";
            dataGridView1.AutoGenerateColumns = true;
        }

        #region Work With DataSets and TableAdapters
        private void acceptChanges()
        {
            bankDataSet1.Clients.AcceptChanges();
            bankDataSet1.Branches.AcceptChanges();
            bankDataSet1.Managers.AcceptChanges();
            bankDataSet1.Accounts.AcceptChanges();
            bankDataSet1.Agreement.AcceptChanges();
            bankDataSet1.AcceptChanges();
        }

        private void updateTables()
        {
            accountsTableAdapter.Update(bankDataSet1.Accounts);
            clientsTableAdapter.Update(bankDataSet1.Clients);
            branchesTableAdapter.Update(bankDataSet1.Branches);
            managersTableAdapter.Update(bankDataSet1.Managers);
            agreementTableAdapter.Update(bankDataSet1.Agreement);
            
        }

        private void fillTables()
        {
            accountsTableAdapter.Fill(bankDataSet1.Accounts);
            clientsTableAdapter.Fill(bankDataSet1.Clients);
            branchesTableAdapter.Fill(bankDataSet1.Branches);
            managersTableAdapter.Fill(bankDataSet1.Managers);
            agreementTableAdapter.Fill(bankDataSet1.Agreement);
        }
        #endregion

        private void getClientCities()
        {
            comboBox_Filter.DataSource = bankDataSet1.Clients.Select(s => s.City).Distinct().ToList();
        }

        private DataTable getDataTable(string SQL)
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

        private void sendMessagesAboutStocksToAll()
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
            DataTable dt =getDataTable("SELECT [Full Name], [E-mail], [Type], Accounts.[Capitalisation], [Stock Starts], [Stock Ends], [Condition], [Result] FROM Clients, Accounts, Stocks Where [Clients].[Id]=Accounts.[UserId] AND Accounts.Type=Stocks.[Account Type] AND Accounts.[Capitalisation]=Stocks.[Capitalisation]");

            //Sending message
            foreach (DataRow dr in dt.Rows) {
                if (DateTime.Now <= Convert.ToDateTime(dr[5]))
                {
                    MailMessage mm = new MailMessage("do@not.reply", dr[1].ToString(), "Акция", "Ув." + dr[0] + "\r\n" +
                        "В нашем банке есть акция, которая может вас заинтересовать.\r\nОна продлится с: "+dr[4].ToString()+
                        " до "+dr[5]+"\r\n"+
                        "Условия таковы: "+dr[6]+"\r\n"+
                        "В результате вы: "+dr[7]+ "\r\nЖдем Вас в ближайшем филиале нашего банка! \r\nС Уважением, Ваш It-Bank");
                    mm.BodyEncoding = UTF8Encoding.UTF8;
                    mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                    client.Send(mm);
                }
            }
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*SqlConnection sqlconn = new SqlConnection(ConnectionString);
            sqlconn.Open();
            SqlDataAdapter dA =new SqlDataAdapter("Select * FROM Clients",sqlconn);
            dA.Update(bankDataSet.Clients);
            sqlconn.Close();*/
            //bankDataSet.AcceptChanges();
            //fillTables();
            //updateTables();
            //bankDataSet.Dispose();
        }

        #region Changing DataGridView DataSource
        private void managersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            updateTables();
            bindingNavigator1.BindingSource = managersBindingSource;
            dataGridView1.DataSource = managersBindingSource;
            label_Current_Base.Text = "Managers";
            comboBoxSearchBy.Items.Clear();
            comboBoxSearchBy.Items.AddRange(new String[] { "Full Name", "Phone" });
            comboBoxSearchBy.SelectedIndex = 0;
            
        }

        private void clientsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            updateTables();
            getClientCities();
            bindingNavigator1.BindingSource = clientsBindingSource;
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = clientsBindingSource;
            label_Current_Base.Text = "Clients";
            comboBoxSearchBy.Items.Clear();
            comboBoxSearchBy.Items.AddRange(new String[] { "Full Name", "Address", "E-mail" });
            comboBoxSearchBy.SelectedIndex = 0;
        }

        private void branchesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            updateTables();
            bindingNavigator1.BindingSource = branchesBindingSource;
            dataGridView1.DataSource = branchesBindingSource;
            label_Current_Base.Text = "Branches";
        }

        private void accountsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            updateTables();
            bindingNavigator1.BindingSource = accountsBindingSource;
            dataGridView1.DataSource = accountsBindingSource;
            label_Current_Base.Text = "Accounts";
        }

        private void agreementsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bindingNavigator1.BindingSource = agreementBindingSource;
            dataGridView1.DataSource = agreementBindingSource;
            label_Current_Base.Text = "Agreements";
            //fillTables();
            updateTables();
        }
        #endregion

        private void queryEditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //updateDB();
            updateTables();
            var qe = new QueryEdit();
            qe.Show();
        }

        #region Work With Clients
        private void registrationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var cl = new ClientForm();
            cl.ShowDialog();
            updateTables();
            clientsTableAdapter.Fill(bankDataSet1.Clients);
            getClientCities();

        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.DataSource != clientsBindingSource)
            {
                MessageBox.Show("Не выбрана таблица клиентов.", "Ошибка");
                return;
            }
            if (dataGridView1.SelectedRows.Count==0) {
                MessageBox.Show("Не выбрана строка");
                    return;
                    }
            var dtCl = new BankDataSet.ClientsDataTable();
            var clT = clientsTableAdapter.FillBy(dtCl,
                Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value));
            object[] row = dtCl.Rows[0].ItemArray;
            var clForm = new ClientForm(
                Convert.ToInt32(row[0]),
                row[1].ToString(),
                row[2].ToString(),
                row[3].ToString(),
                row[4].ToString(),
                row[5].ToString(),
                Convert.ToDateTime(row[6]),
                row[7].ToString()
                );
            clForm.ShowDialog();
            fillTables();
            updateTables();
            getClientCities();
        }

        private void deleteCurrentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SqlConnection sqlConn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand();
            if (dataGridView1.DataSource != clientsBindingSource)
            {
                MessageBox.Show("Не выбрана таблица клиентов.", "Ошибка");
                return;
            }
            sqlConn.Open();
            cmd.CommandText="Delete From Clients WHERE Id="+Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            cmd.Connection = sqlConn;
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
            sqlConn.Close();
            clientsTableAdapter.DeleteQuery(
                Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value)
                );
            clientsTableAdapter.Fill(bankDataSet1.Clients);
            //fillTables();
            bankDataSet1.AcceptChanges();
            getClientCities();
        }

        private void registrationToolStripMenuItem_Paint(object sender, PaintEventArgs e)
        {

        }
        #endregion

        #region Work With Managers
        private void addNewManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var mf = new ManagerForm();
            mf.ShowDialog();
            fillTables();
            updateTables();
            bankDataSet1.AcceptChanges();
        }

        private void updateToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.DataSource != managersBindingSource)
            {
                MessageBox.Show("Не выбрана таблица менеджеров.", "Ошибка");
                return;
            }
            var dtemp = new BankDataSet.ManagersDataTable();
            var mT = managersTableAdapter.FillBy(dtemp,
                Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value));
            object[] row = dtemp.Rows[0].ItemArray;
            var mf = new ManagerForm(
                Convert.ToInt32(row[0]),
                row[1].ToString(),
                Convert.ToInt16(row[2]),
                row[3].ToString(),
                Convert.ToDateTime(row[4]),
                Convert.ToInt32(row[5])
                );
            mf.ShowDialog();
            fillTables();
            updateTables();
            bankDataSet1.AcceptChanges();
            //
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SqlConnection sqlConn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand();
            if (dataGridView1.DataSource != managersBindingSource)
            {
                MessageBox.Show("Не выбрана таблица менеджеров.", "Ошибка");
                return;
            }
            sqlConn.Open();
            cmd.CommandText = "Delete From Managers WHERE Id=" + Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            cmd.Connection = sqlConn;
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
            sqlConn.Close();
            managersTableAdapter.DeleteQuery(
                Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value)
                );
            updateTables();
            fillTables();
            acceptChanges();
        }
        #endregion

        #region Work With Accounts
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var af = new Account();
            af.ShowDialog();
            updateTables();
            fillTables();
            acceptChanges();
        }

        private void updateInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.DataSource != accountsBindingSource)
            {
                MessageBox.Show("Не выбрана таблица счетов.", "Ошибка");
                return;
            }
            var dtemp = new BankDataSet.AccountsDataTable();
            var mT = accountsTableAdapter.FillBy(dtemp,
                Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value));
            object[] row = dtemp.Rows[0].ItemArray;
            var mf = new Account(
                Convert.ToInt32(row[0]),            //int id,
                Convert.ToDecimal(row[1]),          //decimal balance,
                row[2].ToString(),                  //string type,
                Convert.ToInt16(row[3]),            //int capital,
                Convert.ToInt32(row[4])             // int UserID
                );
            mf.ShowDialog();
            fillTables();
            bankDataSet1.AcceptChanges();
            updateTables();
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SqlConnection sqlConn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand();

            if (dataGridView1.DataSource != accountsBindingSource)
            {
                MessageBox.Show("Не выбрана таблица счетов.", "Ошибка");
                return;
            }
            sqlConn.Open();
            cmd.CommandText = "Delete From Accounts WHERE Id=" + Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            cmd.Connection = sqlConn;
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
            sqlConn.Close();
            accountsTableAdapter.DeleteQuery(
                Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value)
                );
            accountsTableAdapter.Fill(bankDataSet1.Accounts);
            //bankDataSet.AcceptChanges();
            updateTables();
            bankDataSet1.AcceptChanges();
        }
        #endregion

        private void fillBy1ToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.clientsTableAdapter.FillBy1(this.bankDataSet1.Clients);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        #region Searching In DataGridView
        private void searchInDataGrid(int column)
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Selected = false;
                    if (dataGridView1.Rows[i].Cells[column].Value != null)
                        if (dataGridView1.Rows[i].Cells[column].Value.ToString().Contains(textBox1.Text))
                        {
                            dataGridView1.Rows[i].Selected = true;
                        }
            }
        }

        private void clearDataGridSelection()
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Selected = false;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "") clearDataGridSelection();
            else
            {
                if (dataGridView1.DataSource == clientsBindingSource)
                {
                    switch (comboBoxSearchBy.SelectedIndex)
                    {
                        case 0:
                            searchInDataGrid(1);
                            break;
                        case 1:
                            searchInDataGrid(4);
                            break;
                        case 2:
                            searchInDataGrid(7);
                            break;
                    }
                }
                else if (dataGridView1.DataSource == managersBindingSource)
                {
                    switch (comboBoxSearchBy.SelectedIndex)
                    {
                        case 0:
                            searchInDataGrid(1);
                            break;
                        case 1:
                            searchInDataGrid(3);
                            break;
                    }
                }
            }
        }
        #endregion

        #region SORT IN DATAGRIDVIEW
        private void dataGridView1_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            if (e.Column.Index == 1)
            {
                e.SortResult = int.Parse(e.CellValue1.ToString()).CompareTo(int.Parse(e.CellValue2.ToString()));
                e.Handled = true;
            }
            else
            {
                #region clientsSort
                if (dataGridView1.DataSource == clientsBindingSource) //clients Sort
                {
                    if (e.Column.Index == 8)
                    {
                        e.SortResult = DateTime.Parse(e.CellValue1.ToString()).CompareTo(DateTime.Parse(e.CellValue2.ToString()));
                        e.Handled = true;
                    }
                    else
                    {
                        e.SortResult = e.CellValue1.ToString().CompareTo(e.CellValue2.ToString());
                        e.Handled = true;
                    }
                }
                #endregion
                #region accountSort
                else if (dataGridView1.DataSource == accountsBindingSource) //accounts sort
                {
                    if (e.Column.Index == 4 || e.Column.Index == 7)
                    {
                        e.SortResult = int.Parse(e.CellValue1.ToString()).CompareTo(int.Parse(e.CellValue2.ToString()));
                        e.Handled = true;
                    }
                    else if (e.Column.Index == 2)
                    {
                        e.SortResult = Decimal.Parse(e.CellValue1.ToString()).CompareTo(Decimal.Parse(e.CellValue2.ToString()));
                        e.Handled = true;
                    }
                    else
                    {
                        e.SortResult = e.CellValue1.ToString().CompareTo(e.CellValue2.ToString());
                        e.Handled = true;
                    }
                }
                #endregion
                #region agreementSort
                else if (dataGridView1.DataSource == agreementBindingSource) //agreement
                {
                    if (e.Column.Index==2||e.Column.Index==3||e.Column.Index==4)
                    {
                        e.SortResult = int.Parse(e.CellValue1.ToString()).CompareTo(int.Parse(e.CellValue2.ToString()));
                        e.Handled = true;
                    }
                    else
                    {
                        e.SortResult = DateTime.Parse(e.CellValue1.ToString()).CompareTo(DateTime.Parse(e.CellValue2.ToString()));
                        e.Handled = true;
                    }
                }
                #endregion
                #region managersSort
                else if (dataGridView1.DataSource == managersBindingSource) //managers
                {
                    if (e.Column.Index==3||e.Column.Index==6)
                    {
                        e.SortResult = int.Parse(e.CellValue1.ToString()).CompareTo(int.Parse(e.CellValue2.ToString()));
                        e.Handled = true;
                    }
                    else if (e.Column.Index == 5)
                    {
                        e.SortResult= DateTime.Parse(e.CellValue1.ToString()).CompareTo(DateTime.Parse(e.CellValue2.ToString()));
                        e.Handled = true;
                    }
                    else
                    {
                        e.SortResult = e.CellValue1.ToString().CompareTo(e.CellValue2.ToString());
                        e.Handled = true;
                    }
                }
                #endregion
                else
                {
                    e.SortResult = e.CellValue1.ToString().CompareTo(e.CellValue2.ToString());
                    e.Handled = true;
                }
            }
        }
        #endregion

        private void comboBox_Filter_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.DataSource == clientsBindingSource)
            {
                clientsTableAdapter.FilterByCity(bankDataSet1.Clients, comboBox_Filter.SelectedItem.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.DataSource == clientsBindingSource)
            {
                clientsTableAdapter.Fill(bankDataSet1.Clients);
            }
        }

        private void statisticsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var St = new Statistic();
            St.ShowDialog();
        }

        private void reportToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        //posible way to save data to db, but still doesn`t work;c
        private void updateDB()
        {
            SqlConnection sqlconn = new SqlConnection(ConnectionString);
            sqlconn.Open();
            SqlDataAdapter dAdapter = clientsTableAdapter.Adapter;
            dAdapter.Update(bankDataSet1.Clients);
            sqlconn.Close();
        }

        private void dataGridView1_DataSourceChanged(object sender, EventArgs e)
        {
            updateTables();
            fillTables();
            acceptChanges();
            if (dataGridView1.DataSource == clientsBindingSource) groupBox1.Visible = true;
            else groupBox1.Visible = false;
            if (dataGridView1.DataSource == clientsBindingSource || dataGridView1.DataSource == managersBindingSource) groupBox2.Visible = true;
            else groupBox2.Visible = false;
        }

        private void userRequestToolStripMenuItem_Click(object sender, EventArgs e)
        { 
            var doc1 = new Document();
            string fileString = "C:\\LABS\\Курс 2\\Семестр 1\\Course_Work_DB\\Course_Work_DB\\UserRequests.pdf";
            BaseFont baseFont = BaseFont.CreateFont("C:\\LABS\\Курс 2\\Семестр 1\\Course_Work_DB\\Course_Work_DB\\arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, iTextSharp.text.Font.DEFAULTSIZE, iTextSharp.text.Font.NORMAL);
            
            //checking doc
            if (File.Exists(fileString))
                File.Delete(fileString);

            PdfWriter.GetInstance(doc1, new FileStream(fileString, FileMode.OpenOrCreate));
            
            PdfPTable table = new PdfPTable(4);
            table.SetWidthPercentage(new float[] { 120,120,120,120 }, doc1.PageSize);
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                string query = "SELECT [Full Name], [Birthday], [City], [Address] FROM Clients";
                SqlCommand cmd = new SqlCommand(query, conn);
                try
                {
                    conn.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        table.AddCell("Full Name");
                        table.AddCell("Birthday");
                        table.AddCell("City");
                        table.AddCell("Address");
                        while (rdr.Read())
                        {
                            table.AddCell(new Phrase(rdr[0].ToString(),font));
                            table.AddCell(new Phrase(rdr[1].ToString(),font));
                            table.AddCell(new Phrase(rdr[2].ToString(),font));
                            table.AddCell(new Phrase(rdr[3].ToString(),font));
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ex: " + ex.Message);
                }
                doc1.Open();
                Paragraph p = new Paragraph("Отчет за все время работы банка", font);
                p.Alignment = Element.ALIGN_CENTER;
                doc1.Add(p);
                p = new Paragraph("Список зарегистрированных пользователей", font);
                p.Alignment = Element.ALIGN_CENTER;
                doc1.Add(p);
                p = new Paragraph(" ", font);
                p.Alignment = Element.ALIGN_CENTER;
                doc1.Add(p);
                doc1.Add(table);
                p = new Paragraph("По состоянию на " + DateTime.Now.ToString(), font);
                p.Alignment = Element.ALIGN_RIGHT;
                doc1.Add(p);
                doc1.Close();
            }
            MessageBox.Show("Документ успешно создан ");
            Process.Start(fileString);
        }

        private void managerRequestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var doc1 = new Document();
            string fileString = "C:\\LABS\\Курс 2\\Семестр 1\\Course_Work_DB\\Course_Work_DB\\ManagerRequest.pdf";
            BaseFont baseFont = BaseFont.CreateFont("C:\\LABS\\Курс 2\\Семестр 1\\Course_Work_DB\\Course_Work_DB\\arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, iTextSharp.text.Font.DEFAULTSIZE, iTextSharp.text.Font.NORMAL);
            
            
            //checking the doc
            if (File.Exists(fileString))
                File.Delete(fileString);
            PdfWriter.GetInstance(doc1, new FileStream(fileString, FileMode.Create));
            PdfPTable table = new PdfPTable(4);
            PdfPTable table2 = new PdfPTable(2);
            table.SetWidthPercentage(new float[] { 120, 120, 120, 120 }, doc1.PageSize);
            table2.SetWidthPercentage(new float[] { 240, 240 }, doc1.PageSize);

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                string query = "SELECT [FullName], [Phone], Branches.[City], Branches.[Address] FROM Managers,Branches WHERE Managers.[Branche_Id]=Branches.[Id]";
                SqlCommand cmd = new SqlCommand(query, conn);
                try
                {
                    conn.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        table.AddCell("Full Name");
                        table.AddCell("Phone");
                        table.AddCell("City");
                        table.AddCell("Address");
                        while (rdr.Read())
                        {
                            table.AddCell(new Phrase(rdr[0].ToString(), font));
                            table.AddCell(new Phrase(rdr[1].ToString(), font));
                            table.AddCell(new Phrase(rdr[2].ToString(), font));
                            table.AddCell(new Phrase(rdr[3].ToString(), font));
                        }
                        
                    }
                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ex: " + ex.Message);
                }
                doc1.Open();
                doc1.AddHeader("h1", "IT-Bank");
                Paragraph p = new Paragraph("Отчет за все время работы банка", font);
                p.Alignment = Element.ALIGN_CENTER;
                doc1.Add(p);
                p = new Paragraph("Список зарегистрированных менеджеров", font);
                p.Alignment = Element.ALIGN_CENTER;
                doc1.Add(p);
                p = new Paragraph(" ", font);
                p.Alignment = Element.ALIGN_CENTER;
                doc1.Add(p);
                doc1.Add(table);
                using (SqlConnection conn1 = new SqlConnection(ConnectionString))
                {
                    string query1 = "Select FullName As 'Full Manager Name', Count(Agreement.Id) As 'Number of Agreements'From Agreement, Managers Where Agreement.Manager_Id=Managers.Id Group By Managers.FullName";
                    SqlCommand cmd1 = new SqlCommand(query1, conn1);
                    try
                    {
                        conn1.Open();
                        using (SqlDataReader rdr1 = cmd1.ExecuteReader())
                        {
                            table2.AddCell("Full Manager Name");
                            table2.AddCell("Number of Agreements");
                            while (rdr1.Read())
                            {
                                table2.AddCell(new Phrase(rdr1[0].ToString(), font));
                                table2.AddCell(new Phrase(rdr1[1].ToString(), font));
                            }
                        }
                        conn1.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ex: " + ex.Message);
                    }
                }
                p = new Paragraph(" ", font);
                p.Alignment = Element.ALIGN_CENTER;
                doc1.Add(p);
                p = new Paragraph("Успешность менеджеров", font);
                p.Alignment = Element.ALIGN_CENTER;
                doc1.Add(p);
                p = new Paragraph(" ", font);
                p.Alignment = Element.ALIGN_CENTER;
                doc1.Add(p);
                doc1.Add(table2);
                p = new Paragraph(" ", font);
                p.Alignment = Element.ALIGN_CENTER;
                doc1.Add(p);
                p = new Paragraph("По состоянию на " + DateTime.Now.ToString(), font);
                p.Alignment = Element.ALIGN_RIGHT;
                doc1.Add(p);
                doc1.Close();
            }
            MessageBox.Show("Отчет менеджеров успешно создан.");
            Process.Start(fileString);
        }

        private void operationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operation o = new Operation();
            o.ShowDialog();
            updateTables();
            accountsTableAdapter.Fill(bankDataSet1.Accounts);
            updateTables();
        }
    }
}
