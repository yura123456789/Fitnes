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

namespace Fitnes_Gump
{
    public partial class Преобрести : MetroFramework.Forms.MetroForm
    {
        public Преобрести()
        {
            InitializeComponent();
            label13.Text = DateTime.Now.ToLongDateString();
            timer1.Start();
        }
        SqlConnection sql = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Xiacom\\OneDrive\\Рабочий стол\\Project\\Fitnes Gump\\Fitnes Gump\\База_Данных.mdf\";Integrated Security=True");
        public void Delete_skidku()
        {
            int sale = 0;
            sql.Open();
            SqlCommand cmd = new SqlCommand("update Users set [Sale]=@Sale Where [Фамилия]=@Фамилия",sql);
            cmd.Parameters.AddWithValue("Фамилия", label1.Text.Split(' ')[0]);
            cmd.Parameters.AddWithValue("Sale",label3.Text = sale.ToString());
            cmd.ExecuteNonQuery();
            sql.Close();
        }
        private void gunaButton3_Click(object sender, EventArgs e)
        {
            Add_Card();
            Delete_skidku();
        }
        public void Rascet_skidki()
        {
            double price = Convert.ToInt32(label2.Text);
            double procent = Convert.ToInt32(label3.Text);
            double skidka = procent / 100;
            double pr = price * skidka;
            double result = price - pr;
            label8.Text = result.ToString();
        }

        public void Add_Card()
        {
            sql.Open();
            SqlCommand cmd = new SqlCommand("insert into [Карты] (ФИО,Цена,Карта,Дата_покупки) values(@ФИО,@Цена,@Карта,@Дата_покупки)", sql);
            cmd.Parameters.AddWithValue("ФИО",label1.Text);
            cmd.Parameters.AddWithValue("Цена",label8.Text);
            cmd.Parameters.AddWithValue("Карта", label10.Text);
            cmd.Parameters.AddWithValue("Дата_покупки", DateTime.Parse(label13.Text));
            cmd.ExecuteNonQuery();
            MessageBox.Show("Успешно прошла оплата");
            sql.Close();
        }
        private void Преобрести_Load(object sender, EventArgs e)
        {
            Rascet_skidki();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label13.Text = DateTime.Now.ToLongDateString();
        }
    }
}
