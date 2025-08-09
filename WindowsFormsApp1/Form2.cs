using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        public string PlayerName { get; private set; }
        public Form2()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.TopMost = true;
            InitializeComponent();
            this.AcceptButton = button1;
        }

        public void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;

            SessionData.Username = username;


            Form3 form = new Form3();   
            form.ShowDialog();
            this.Close();
        }


        public void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }
        public static class SessionData
        {
            public static string Username { get; set; }
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
