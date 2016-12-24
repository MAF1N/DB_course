using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Course_Work_DB
{
    public partial class QueryEdit : Form
    {
        const string ConnectionString= @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='C:\LABS\Курс 2\Семестр 1\Course_Work_DB\Course_Work_DB\Bank.mdf';Integrated Security = True";
        public QueryEdit()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection sqlconn = new SqlConnection(ConnectionString);
                sqlconn.Open();
                SqlDataAdapter oda = new SqlDataAdapter(TestInput.Text, sqlconn);
                DataTable dt = new DataTable();
                oda.Fill(dt);
                dataGridView1.DataSource = dt;
                sqlconn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"ERROR: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TestInput.Text = "Select";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void QueryEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void TestInput_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
