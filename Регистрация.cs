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
using Tulpep.NotificationWindow;
using System.Net.Mail;
using Fitnes_Gump.Properties;
using System.Threading;

namespace Fitnes_Gump
{
    public partial class Регистрация : MetroFramework.Forms.MetroForm
    {
        public Регистрация()
        {
            InitializeComponent();
            gunaTextBox7.UseSystemPasswordChar = true;
        }

        private void gunaTextBox1_Leave(object sender, EventArgs e)
        {
            if(gunaTextBox1.Text =="")
            {
                gunaTextBox1.Text = "Имя";
            }
        }

        private void gunaTextBox2_Leave(object sender, EventArgs e)
        {
            if (gunaTextBox2.Text == "")            
                gunaTextBox2.Text = "Фамилия";            
            else if (gunaTextBox3.Text == "")
                gunaTextBox3.Text = "Отчество";
            else if (gunaTextBox5.Text == "")            
                gunaTextBox5.Text = "Email";
            else if (gunaTextBox6.Text == "")           
                gunaTextBox6.Text = "Login";
            if (gunaTextBox7.Text == "")
                gunaTextBox7.Text = "Pasword";            
        }
        SqlConnection sql = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Xiacom\\OneDrive\\Рабочий стол\\Project\\Fitnes Gump\\Fitnes Gump\\База_Данных.mdf\";Integrated Security=True");        
        private async void gunaButton1_Click(object sender, EventArgs e)
        {
            label2.Visible = true;
            await Task.Delay(5000);
            PopupNotifier popup = new PopupNotifier();
            popup.TitleText = "Уведовелние";
            popup.ContentText = "Успешно зарегестрировались";
            popup.Image = Resources.Галка;
            sql.Open();
            if (!string.IsNullOrWhiteSpace(gunaTextBox1.Text) && !string.IsNullOrEmpty(gunaTextBox1.Text) &&
                !string.IsNullOrWhiteSpace(gunaTextBox2.Text) && !string.IsNullOrEmpty(gunaTextBox2.Text) &&
                !string.IsNullOrWhiteSpace(gunaTextBox3.Text) && !string.IsNullOrEmpty(gunaTextBox3.Text) &&
                !string.IsNullOrWhiteSpace(maskedTextBox1.Text) && !string.IsNullOrEmpty(maskedTextBox1.Text) &&
                !string.IsNullOrWhiteSpace(gunaTextBox5.Text) && !string.IsNullOrEmpty(gunaTextBox5.Text) &&
                !string.IsNullOrWhiteSpace(gunaTextBox6.Text) && !string.IsNullOrEmpty(gunaTextBox6.Text) &&
                !string.IsNullOrWhiteSpace(gunaTextBox7.Text) && !string.IsNullOrEmpty(gunaTextBox7.Text))
            {
                SqlCommand command = new SqlCommand("Insert into [Users] (Имя,Фамилия,Отчество,Дата_рождения,Телефон,Email,Login,Password,Sale,Город) Values(@Имя,@Фамилия,@Отчество,@Дата_рождения,@Телефон,@Email,@Login,@Password,@Sale,@Город)", sql);
                SqlCommand Login = new SqlCommand("select Имя,Фамилия,Отчество,Дата_рождения,Телефон,Email,Login,Password from Users where Login=N'" + gunaTextBox6.Text + "'", sql);
                object poisk = Login.ExecuteScalar();
                if (gunaTextBox7.TextLength > 5)
                {
                    if (poisk == null)
                    {
                        command.Parameters.AddWithValue("Имя", gunaTextBox1.Text);
                        command.Parameters.AddWithValue("Фамилия", gunaTextBox2.Text);
                        command.Parameters.AddWithValue("Отчество", gunaTextBox3.Text);
                        command.Parameters.AddWithValue("Дата_рождения", gunaDateTimePicker1.Value);
                        command.Parameters.AddWithValue("Телефон", maskedTextBox1.Text);
                        try
                        {
                            MailAddress mail = new MailAddress(gunaTextBox5.Text);
                            command.Parameters.AddWithValue("Email", mail.Address);
                            command.Parameters.AddWithValue("Login", gunaTextBox6.Text);
                            command.Parameters.AddWithValue("Password", gunaTextBox7.Text);
                            command.Parameters.AddWithValue("Sale", 15);
                            command.Parameters.AddWithValue("Город", gunaTextBox4.Text);
                            command.ExecuteNonQuery();
                            popup.Popup();
                            Главная_Форма_Пользователя a = new Главная_Форма_Пользователя();                           
                            a.Show();
                            MessageBox.Show("При первой регистрации вам даеться скидка в 15 %");
                            Hide();
                        }
                        catch
                        {
                            MessageBox.Show("нет");
                          
                        }
                    }
                    else
                        MessageBox.Show("Такой логин  уже существует");
                }
                else
                    MessageBox.Show("Пароль должен быть больше 5 символов");
            }
            sql.Close();
        }

        private async void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            label2.Visible = true;
            await Task.Delay(3000);
            Вход a = new Вход();                        
            a.Show();
            Hide();
        }
        public string Generatet_password(int[] mas)
        {
            string str = "";
            Random rnd = new Random();
            for (int i = 0; i < mas.Length; i++)
            {
                mas[i] += rnd.Next(45, 100);
                str += (char)mas[i];
            }
            return str;
        }
        private void gunaCheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (gunaCheckBox2.Checked)
                gunaTextBox7.Text = Generatet_password(new int[10]);
            else
                gunaTextBox7.Clear();
        }

        private void gunaCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (gunaCheckBox1.Checked)
                gunaTextBox7.UseSystemPasswordChar = true;
            else
                gunaTextBox7.UseSystemPasswordChar = false;
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }
    }
}
