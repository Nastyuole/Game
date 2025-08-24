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
            InitializeComponent();
        }

        public void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;

            SessionData.Username = username;
            switch (comboBox1.SelectedItem)
            {
                case "Easy":
                    SessionData.GameDifficulty = Difficulty.Easy;
                    break;
                case "Normal":
                    SessionData.GameDifficulty = Difficulty.Normal;
                    break;
                case "Hard":
                    SessionData.GameDifficulty = Difficulty.Hard;
                    break;
                default:
                    SessionData.GameDifficulty = Difficulty.Normal;
                    break;
            }

            Form3 form = new Form3();   
            form.ShowDialog();
            this.Close();
        }
        public enum Difficulty
        {
            Easy,
            Normal,
            Hard
        }


        public void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }
        public static class SessionData
        {
            public static string Username { get; set; }
            public static Difficulty GameDifficulty = Difficulty.Normal;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
