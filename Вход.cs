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
using System.Threading;

namespace Fitnes_Gump
{
    public partial class Вход : MetroFramework.Forms.MetroForm
    {
        public Вход()
        {
            InitializeComponent();
        }
        SqlConnection sql = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Xiacom\\OneDrive\\Рабочий стол\\Project\\Fitnes Gump\\Fitnes Gump\\База_Данных.mdf\";Integrated Security=True");        
        
        private async void gunaButton1_Click(object sender, EventArgs e)
        {
            if (label3.Visible == false)
            {                
                label3.Visible = true;
                await Task.Delay(1000);
            }
            sql.Open();            
                SqlCommand command = new SqlCommand("select Фамилия,Имя,Отчество, Город Login,Password from Users Where Login='" + textBox1.Text + "'and Password='" +textBox2.Text + "'",sql);
                SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                if (!string.IsNullOrWhiteSpace(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox1.Text) &&
                   !string.IsNullOrWhiteSpace(textBox2.Text) && !string.IsNullOrWhiteSpace(textBox2.Text))
                {
                    Главная_Форма_Пользователя reg = new Главная_Форма_Пользователя();
                    reg.label3.Text = reader[0].ToString() + " " + reader[1].ToString() + " " + reader[2].ToString();
                    reg.label4.Text = reader[3].ToString();
                    reg.Show();
                    Popup.Popup_pop();
                    Hide();
                }
            }
            else 
                MessageBox.Show("Ошибка","Уведомление",MessageBoxButtons.OK,MessageBoxIcon.Error);
                sql.Close();
        }

        private void Вход_Load(object sender, EventArgs e)
        {            
        }

        private void gunaCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(gunaCheckBox1.Checked)            
                textBox2.UseSystemPasswordChar = true;            
            else
                textBox2.UseSystemPasswordChar = false;
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
        
    }
}
