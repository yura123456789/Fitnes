using Guna.UI2.WinForms.Suite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fitnes_Gump
{
    public partial class Admin : MetroFramework.Forms.MetroForm
    {
        public Admin()
        {
            InitializeComponent();
            gunaLabel18.Text = DateTime.Now.Day + "."+ DateTime.Now.Month + "." +DateTime.Now.DayOfYear + " " + DateTime.Now.ToLongTimeString();
            timer1.Start();
        }
        
        private void metroTabPage1_Click(object sender, EventArgs e)
        {

        }
        string im = "";
        private void gunaAdvenceButton2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {                
                im = openFileDialog.FileName.ToString();
                gunaPictureBox1.ImageLocation = im;
            }
        }
       SqlConnection sql =new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Xiacom\\OneDrive\\Рабочий стол\\Project\\Fitnes Gump\\Fitnes Gump\\База_Данных.mdf\";Integrated Security=True");
        public void Obnova()
        {
            SqlCommand command = sql.CreateCommand();
            command.CommandText = "select *from Расписание";
            command.ExecuteNonQuery();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            gunaDataGridView3.DataSource = dataTable;
            Popup.Popup_pop();
        }
        public void Obnova_novosti()
        {
            SqlCommand command = sql.CreateCommand();
            command.CommandText = "select *from Novosti";
            command.ExecuteNonQuery();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            gunaDataGridView3.DataSource = dataTable;
            Popup.Popup_pop();
        }
        public void Add_trener()
        {
            byte[] img = null;
            FileStream fileStream = new FileStream(im, FileMode.Open, FileAccess.Read);
            BinaryReader reader = new BinaryReader(fileStream);
            img = reader.ReadBytes((int)fileStream.Length);
            sql.Open();
            SqlCommand command = new SqlCommand("insert into [Тренера] (Foto,Name,Otchestvo,Familya,Profil) values(@Foto,@Name,@Otchestvo,@Familya,@Profil)", sql);
            command.Parameters.AddWithValue("Foto", img);
            command.Parameters.AddWithValue("Name", metroTextBox1.Text);
            command.Parameters.AddWithValue("Otchestvo", metroTextBox2.Text);
            command.Parameters.AddWithValue("Familya", metroTextBox3.Text);
            command.Parameters.AddWithValue("Profil", metroTextBox4.Text);
            command.ExecuteNonQuery();
            Popup.Popup_pop();
            sql.Close();
        }
        private void gunaAdvenceButton1_Click(object sender, EventArgs e)
        {
            Add_trener();
        }

        private void Admin_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "база_ДанныхDataSet6.Карты". При необходимости она может быть перемещена или удалена.
            this.картыTableAdapter.Fill(this.база_ДанныхDataSet6.Карты);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "база_ДанныхDataSet4.Novosti". При необходимости она может быть перемещена или удалена.
            this.novostiTableAdapter.Fill(this.база_ДанныхDataSet4.Novosti);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "база_ДанныхDataSet5.Тренера". При необходимости она может быть перемещена или удалена.
            this.тренераTableAdapter.Fill(this.база_ДанныхDataSet5.Тренера);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "база_ДанныхDataSet2.Расписание". При необходимости она может быть перемещена или удалена.
            this.расписаниеTableAdapter.Fill(this.база_ДанныхDataSet2.Расписание);
        }
        public void Update_News()
        {
            sql.Open();
            SqlCommand command = new SqlCommand("update Расписание set [Понедельник]=@Понедельник,[Вторник]=@Вторник,[Среда]=@Среда,[Четверг]=@Четверг,[Пятница]=@Пятница,[Суббота]=@Суббота,[Воскресенье]=@Воскресенье Where [Id]=@Id", sql);
            command.Parameters.AddWithValue("Id", gunaTextBox1.Text);
            command.Parameters.AddWithValue("Понедельник", gunaTextBox2.Text);
            command.Parameters.AddWithValue("Вторник", gunaTextBox3.Text);
            command.Parameters.AddWithValue("Среда", gunaTextBox4.Text);
            command.Parameters.AddWithValue("Четверг", gunaTextBox5.Text);
            command.Parameters.AddWithValue("Пятница", gunaTextBox6.Text);
            command.Parameters.AddWithValue("Суббота", gunaTextBox7.Text);
            command.Parameters.AddWithValue("Воскресенье", gunaTextBox8.Text);
            command.ExecuteNonQuery();
            Obnova();
            sql.Close();
        }
        private void gunaButton1_Click(object sender, EventArgs e)
        {
            Update_News();
        }

        private void bindingNavigatorPositionItem_Click(object sender, EventArgs e)
        {
        }
        public void Delete_trener()
        {
            sql.Open();
            SqlCommand command = new SqlCommand("delete from Тренера where Id like N'%" + metroTextBox5.Text + "%'", sql);
            command.ExecuteNonQuery();
            Popup.Popup_pop();
            sql.Close();
        }
        private void gunaAdvenceButton3_Click(object sender, EventArgs e)
        {
            Delete_trener();
        }
        public void  Search_Trener()
        {
            sql.Open();
            SqlCommand command = new SqlCommand("select *from Тренера where Familya like N'%" + metroTextBox2.Text + "%'", sql);
            command.ExecuteNonQuery();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);
            gunaDataGridView2.DataSource = dt;
            sql.Close();
        }
        private void gunaAdvenceButton4_Click(object sender, EventArgs e)
        {
            Search_Trener();
        }

        private void gunaAdvenceButton5_Click(object sender, EventArgs e)
        {
            sql.Open();
            SqlCommand comm = new SqlCommand("select * from Тренера",sql);
            comm.ExecuteNonQuery();
            SqlDataAdapter adapter = new SqlDataAdapter(comm);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            gunaDataGridView2.DataSource = dt;
            sql.Close();
        }

        private void gunaLabel12_Click(object sender, EventArgs e)
        {

        }

        private void gunaAdvenceButton10_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                im = openFileDialog.FileName.ToString();
                gunaPictureBox3.ImageLocation = im;
            }
        }
        public void Add_news()
        {
            byte[] img = null;
            FileStream fileStream = new FileStream(im, FileMode.Open, FileAccess.Read);
            BinaryReader reader = new BinaryReader(fileStream);
            img = reader.ReadBytes((int)fileStream.Length);
            if (!string.IsNullOrEmpty(gunaTextBox9.Text) && !string.IsNullOrWhiteSpace(gunaTextBox9.Text) &&
                gunaPictureBox3.Image != null &&
                !string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox1.Text))
            {
                sql.Open();
                SqlCommand command = new SqlCommand("insert into [Novosti] (Foto_novosti,Title,Novosti,Date_publication) values(@Foto_novosti,@Title,@Novosti,@Date_publication)", sql);
                command.Parameters.AddWithValue("Foto_novosti", img);
                command.Parameters.AddWithValue("Title", gunaTextBox9.Text);
                command.Parameters.AddWithValue("Novosti", textBox1.Text);
                command.Parameters.AddWithValue("Date_publication", gunaLabel18.Text);
                command.ExecuteNonQuery();
                MessageBox.Show("Успешно добавили", "уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Obnova_novosti();
                sql.Close();
            }
        }
        private void gunaAdvenceButton6_Click(object sender, EventArgs e)
        {
            Add_news();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            gunaLabel18.Text = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString();
        }

        public void Search_Id()
        {
            try
            {
                sql.Open();
                SqlCommand command = new SqlCommand($"select * from Novosti where Id={gunaTextBox13.Text}", sql);
                command.ExecuteNonQuery();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                gunaDataGridView3.DataSource = dt;
                sql.Close();
            }
            catch
            {
                Popup.Popup_error();
            }
        }
        private void gunaAdvenceButton9_Click(object sender, EventArgs e)
        {
            Search_Id();
        }
        public void Delete_news()
        {
            sql.Open();
            SqlCommand command = new SqlCommand("delete from Novosti where Id like N'%" + gunaTextBox13.Text + "%'", sql);
            command.ExecuteNonQuery();
            Obnova_novosti();
            MessageBox.Show("Успешно удалили новость");
            sql.Close();
        }
        private void gunaAdvenceButton8_Click(object sender, EventArgs e)
        {
            Delete_news();
        }

        public void Revue ()
        {
            sql.Open();
            DateTime a = dateTimePicker1.Value.Date;
            DateTime b = dateTimePicker2.Value.Date;
            SqlCommand command = new SqlCommand($"select Sum(Цена)  from Карты where Дата_покупки between @Startdate and @endDate", sql);
            command.Parameters.AddWithValue("@Startdate", a);
            command.Parameters.AddWithValue("@endDate", b);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read() && !reader.IsDBNull(0))
            {
                textBox2.Text = Convert.ToDecimal(reader[0]).ToString();
            }
            else
                textBox2.Text = "Такой даты нет ";
            sql.Close();
        }
        private void gunaButton2_Click(object sender, EventArgs e)
        {
            try
            {
                Revue();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}