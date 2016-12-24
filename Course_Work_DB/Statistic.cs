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
    public partial class Statistic : Form
    {
        const string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='C:\LABS\Курс 2\Семестр 1\Course_Work_DB\Course_Work_DB\Bank.mdf';Integrated Security = True";
        public Statistic()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            chart1.Visible = true;
            try
            {
                SqlConnection sqlconn = new SqlConnection(ConnectionString);
                sqlconn.Open();
                SqlDataAdapter oda= new SqlDataAdapter();
                DataTable dt = new DataTable();
                #region ComboBox Value
                if (comboBox1.SelectedIndex == 0)
                {
                    oda = new SqlDataAdapter("Select * FROM [ClientCityStatistic]", sqlconn);
                    chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
                    chart1.Titles[0].Text = comboBox1.SelectedText;
                    chart1.Series[0].Name = "Клиентов в городе";
                    chart1.Series[0].XValueMember = "City";
                    chart1.Series[0].YValueMembers = "Number";
                }
                if (comboBox1.SelectedIndex == 1)
                {
                    oda = new SqlDataAdapter("SELECT (DATEDIFF(YEAR, Birthday, GETDATE())+(SIGN(DATEDIFF(DAY,Birthday,DATEADD(YEAR,YEAR(Birthday)-YEAR(GETDATE()),GETDATE())))-1)/2) AS Age, Count(Id) As Number From Clients Group By (DATEDIFF(YEAR, Birthday, GETDATE())+(SIGN(DATEDIFF(DAY,Birthday,DATEADD(YEAR,YEAR(Birthday)-YEAR(GETDATE()),GETDATE())))-1)/2) ", sqlconn);
                    chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar;
                    chart1.Titles[0].Text = comboBox1.SelectedText;
                    chart1.Series[0].Name = "Возраст клиентов";
                    chart1.Series[0].XValueMember = "Age";
                    chart1.Series[0].YValueMembers = "Number";
                }
                if (comboBox1.SelectedIndex == 2)
                {
                    oda = new SqlDataAdapter("Select Type, Count(UserId) As Number From Accounts Group By Type", sqlconn);
                    chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
                    chart1.Titles[0].Text = comboBox1.SelectedText;
                    chart1.Series[0].Name = "Аккаунты";
                    chart1.Series[0].XValueMember = "Type";
                    chart1.Series[0].YValueMembers = "Number";
                }
                if (comboBox1.SelectedIndex == 3)
                {
                    oda = new SqlDataAdapter("Select FullName As 'Full Manager Name', Count(Agreement.Id) As 'Number of Agreements'From Agreement, Managers Where Agreement.Manager_Id=Managers.Id Group By Managers.FullName", sqlconn);
                    chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
                    chart1.Titles[0].Text = comboBox1.SelectedText;
                    chart1.Series[0].Name = "Количество счетов";
                    chart1.Series[0].XValueMember = "Full Manager Name";
                    chart1.Series[0].YValueMembers = "Number of Agreements";
                }
                if (comboBox1.SelectedIndex == 4)
                {
                    oda = new SqlDataAdapter("Select Type, Sum(Amount) as Amount From Operations Group By Type", sqlconn);
                    chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar;
                    chart1.Titles[0].Text = comboBox1.SelectedText;
                    chart1.Series[0].Name = "Money";
                    chart1.Series[0].XValueMember = "Type";
                    chart1.Series[0].YValueMembers = "Amount";
                }
                #endregion
                oda.Fill(dt);
                
                dataGridView1.DataSource = dt;
                sqlconn.Close();
                chart1.DataSource = dt;
                chart1.DataBind();
                chart1.Update();
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"ERROR: " + ex.Message);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Statistic_Load(object sender, EventArgs e)
        {
            chart1.Visible = false;
        }
    }
}
