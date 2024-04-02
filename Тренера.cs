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

namespace Fitnes_Gump
{
    public partial class Тренера : UserControl
    {
        public Тренера()
        {
            InitializeComponent();
        }
        public Image image { get => pictureBox1.Image; set => pictureBox1.Image = value; }
        public string name { get => label8.Text;  set=> label8.Text = value; } 
        public string Family { get=> label7.Text;  set=>label7.Text = value; }
        public string Otchestvo { get => label6.Text;  set => label6.Text = value; } 
        public string Profil { get => label5.Text;  set => label5.Text = value; }
    }
}
