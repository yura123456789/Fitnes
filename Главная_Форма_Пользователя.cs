using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Channels;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tulpep.NotificationWindow;

namespace Fitnes_Gump
{
    public partial class Главная_Форма_Пользователя : MetroFramework.Forms.MetroForm
    {
        public Главная_Форма_Пользователя()
        {
            InitializeComponent();
           // WindowState = FormWindowState.Maximized;            
            metroLabel1.Text = "Время " + DateTime.Now.ToLongTimeString();
            timer1.Start();

        }
        public void Pop()
        {
            PopupNotifier popup = new PopupNotifier();
            popup.TitleText = "Уведомление";
            popup.ContentText = Temperatur(label4.Text);
            popup.Popup();
        }
        
        public string Temperatur(string gorod)
        {
            string url = $"https://api.openweathermap.org/data/2.5/weather?q={gorod}&lang=ru&units=metric&appid=ea82c0bce4788001307cef5c170bc9e8";

            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);

            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            string response;
            using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
            {
                response = streamReader.ReadToEnd();
            }
            TEmpResponse emp = JsonConvert.DeserializeObject<TEmpResponse>(response);
            return $" Температура в городе {emp.Name} {emp.Main.Temp} C {Environment.NewLine} Минимальная температура {emp.Main.Temp_min} C {Environment.NewLine} Максимальная температура {emp.Main.Temp_max} C";
        }


        public void Trener(Image a, string nam, string ot, string fam, string prof)
        {
            flowLayoutPanel1.Controls.Add(new Тренера()
            {
                image = a,
                name = nam,
                Otchestvo = ot,
                Family = fam,
                Profil = prof
            });
        }

        public void Preobretenie(Преобрести a, object sender)
        {
            Guna.UI.WinForms.GunaButton button = (Guna.UI.WinForms.GunaButton)sender;
            sql.Open();
            SqlCommand command = new SqlCommand("select *from Users where Фамилия='" + label3.Text.Split(' ')[0] + "'", sql);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                a.label3.Text = reader.GetInt32(9).ToString();
            }
            sql.Close();
            a.label10.Visible = true;
            if (button.Name == "gunaButton3")
            {
                a.label1.Text = label3.Text;
                a.label2.Text = gunaLabel33.Text;
                a.label10.Text = gunaGroupBox3.Text;
            }
            else if(button.Name == "gunaButton4")
            {
                a.label1.Text = label3.Text;
                a.label2.Text = gunaLabel32.Text;
                a.label10.Text = gunaGroupBox2.Text;
            }
            else if(button.Name == "gunaButton1")
            {
                a.label1.Text = label3.Text;
                a.label2.Text = gunaLabel26.Text;
                a.label10.Text = gunaGroupBox1.Text;
            }
            a.Show();
        }
        private void label1_Click(object sender, EventArgs e)
        {            
        }
        
        SqlConnection sql = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Xiacom\\OneDrive\\Рабочий стол\\Project\\Fitnes Gump\\Fitnes Gump\\База_Данных.mdf\";Integrated Security=True");
        private void AddNovosti(Image image, string title, string dateTime, string novosti)
        {
            flowLayoutPanel2.Controls.Add(new Новости_control()
            {
                Image = image,
                Title = title,
                Date_Time = dateTime,
                Novosti_text = novosti
            });
        }        
        public void Novosti()
        {
            sql.Open();
            SqlCommand sqlCommand = new SqlCommand("select Foto_novosti,Title,Novosti,Date_publication from Novosti", sql);
            SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            Новости_control[] control = new Новости_control[dt.Rows.Count];
            for(int i =0; i<1;i++)
            {
                foreach(DataRow row in dt.Rows)
                {
                    control[i] = new Новости_control();
                    MemoryStream memory = new MemoryStream((byte[])row["Foto_novosti"]);
                    AddNovosti(
                    control[i].Image = new Bitmap(memory),
                    control[i].Title = row["Title"].ToString(),
                    control[i].Date_Time = row["Date_publication"].ToString(),
                    control[i].Novosti_text = row["Novosti"].ToString());
                }
            }          
            sql.Close();
        }
        private void Главная_Форма_Пользователя_Shown(object sender, EventArgs e)
        {            
            sql.Open();
            SqlCommand command = new SqlCommand("select Foto,Name,Otchestvo,Familya,Profil from Тренера", sql);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Тренера q = new Тренера();
                ToolTip t = new ToolTip();
                MemoryStream memory = new MemoryStream((byte[])reader["Foto"]);
                q.pictureBox1.Image = Image.FromStream(memory);
                //q.label5.Text = reader.GetString(1);
                q.label8.Text = reader.GetString(1);                
                q.label7.Text = reader.GetString(2);
                q.label6.Text = reader.GetString(3);
                t.SetToolTip(q.label4, reader.GetString(4));
                t.SetToolTip(q.gunaLinkLabel1, "Запись на индивидуальные тренировке звоните \r по телефону указанный в контактной вкладке");
                flowLayoutPanel1.Controls.Add(q);                
            }
            sql.Close();
            Novosti();
            ////ниже рабочий!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            //SqlDataAdapter adapter = new SqlDataAdapter(command);
            //DataTable dt = new DataTable();
            //adapter.Fill(dt);
            //Тренера[] tre = new Тренера[dt.Rows.Count];
            //for (int i = 0; i <1; i++)
            //{
            //    foreach (DataRow row in dt.Rows)
            //    {
            //        tre[i] = new Тренера();
            //        MemoryStream memory = new MemoryStream((byte[])row["Foto"]);
            //        tre[i].image = new Bitmap(memory);
            //        tre[i].name = row["Name"].ToString();
            //        tre[i].Otchestvo = row["Otchestvo"].ToString();
            //        tre[i].Family = row["Familya"].ToString();
            //        tre[i].Profil = row["Profil"].ToString();
            //        flowLayoutPanel1.Controls.Add(tre[i]);
            //или ниже
            //Trener(tre[i].image = new Bitmap(memory),
            //tre[i].name = row["Name"].ToString(),
            //tre[i].Family = row["Familya"].ToString(),
            //tre[i].Otchestvo = row["Otchestvo"].ToString(),
            //tre[i].Profil = row["Profil"].ToString());
            //    }
            //}
            sql.Close();
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            metroLabel1.Text = "Время " + DateTime.Now.ToLongTimeString();
        }

        private void Главная_Форма_Пользователя_Load(object sender, EventArgs e)
        {
            Popup a = new Popup();
            Pop();
            // TODO: данная строка кода позволяет загрузить данные в таблицу "база_ДанныхDataSet.Расписание". При необходимости она может быть перемещена или удалена.
            this.расписаниеTableAdapter.Fill(this.база_ДанныхDataSet.Расписание);        
        }

        private void metroTabPage2_Click(object sender, EventArgs e)
        {
            
        }

        private void gunaLabel1_Click(object sender, EventArgs e)
        {

        }
       
        private void расписаниеBindingSource_CurrentChanged(object sender, EventArgs e)
        {          
        }
        private void Главная_Форма_Пользователя_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Maximized)
            {
                gunaGroupBox1.Location = new Point(25, 0);
                gunaGroupBox1.Width = gunaGroupBox1.Width + 75;
                gunaGroupBox2.Location = new Point(425, 0);
                gunaGroupBox2.Width = gunaGroupBox2.Width + 100;
                gunaGroupBox3.Location = new Point(850, 0);
                gunaGroupBox3.Width = gunaGroupBox3.Width + 100;
                gunaGroupBox1.TextLocation = new Point(90, 4);
                gunaGroupBox2.TextLocation = new Point(160, 4);
                gunaGroupBox3.TextLocation = new Point(140, 4);
                gunaButton4.Dock = DockStyle.Bottom;
                gunaButton1.Dock = DockStyle.Bottom;
                gunaButton3.Dock = DockStyle.Bottom;
                gunaLabel30.Dock = DockStyle.Bottom;
                gunaLabel26.Dock = DockStyle.Bottom;
                gunaLabel28.Dock = DockStyle.Bottom;
                gunaLabel31.Dock = DockStyle.Bottom;
                gunaLabel32.Dock = DockStyle.Bottom;                                
                gunaLabel33.Dock = DockStyle.Bottom;
                Новости_control a = new Новости_control();
                a.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top; 
            }
            else
            {
                gunaGroupBox1.Location = new Point(0, 0);
                gunaGroupBox1.Size = new Size(261, 322);
                gunaGroupBox2.Location = new Point(280, 0);
                gunaGroupBox2.Size = new Size(261, 322);
                gunaGroupBox3.Location = new Point(558, 0);
                gunaGroupBox3.Size = new Size(261, 325);
                gunaGroupBox1.TextLocation = new Point(50, 4);
                gunaGroupBox2.TextLocation = new Point(110, 4);
                gunaGroupBox3.TextLocation = new Point(90, 4);
                gunaButton4.Dock = DockStyle.None;
                gunaButton1.Dock = DockStyle.None;
                gunaButton3.Dock = DockStyle.None;
                gunaLabel30.Dock = DockStyle.None;
                gunaLabel26.Dock = DockStyle.None;
                gunaLabel28.Dock = DockStyle.None;                
                gunaLabel32.Dock = DockStyle.None;
                gunaLabel33.Dock = DockStyle.None;                                
                gunaLabel31.Dock = DockStyle.None;
            }
        }
        private void gunaButton4_Click(object sender, EventArgs e)
        {
            Преобрести a = new Преобрести();
            Preobretenie(a, sender);
        }
        private void gunaButton1_Click(object sender, EventArgs e)
        {            
            Преобрести a = new Преобрести();
            Preobretenie(a, sender);
        }
        private void gunaButton3_Click(object sender, EventArgs e)
        {
            Преобрести a = new Преобрести(); 
            Preobretenie(a,sender);
        }
    }
}
