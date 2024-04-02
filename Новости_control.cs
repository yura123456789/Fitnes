using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fitnes_Gump
{
    public partial class Новости_control : UserControl
    {
        public Новости_control()
        {
            InitializeComponent();
        }
        public Image Image
        {
            get { return gunaPictureBox1.Image; }
            set { gunaPictureBox1.Image = value; }
        }
        public string Title { get { return label1.Text; } set { label1.Text = value; } }        
        public string Date_Time { get { return label2.Text; } set { label2.Text = value; label2.Text = label2.Text.ToString(); } }
        public string Novosti_text { get { return textBox1.Text; } set { textBox1.Text = value; } }
    }
}
