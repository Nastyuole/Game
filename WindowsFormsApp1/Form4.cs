using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using static WindowsFormsApp1.Form2;

namespace WindowsFormsApp1
{
    public partial class Form4 : Form
    {
       
            private Character player;
            private Character enemy;
            private Action updateUI;
            Random rand = new Random();
        public void Lightning()
            {
                double manaCost = 50;
                double spellDamage = 35 + player.Level * 5;

                if (player.CurrentMana < manaCost)
                {
                    MessageBox.Show("Not enough mana for Tsundara!");
                    return;
                }
                player.CurrentMana -= manaCost;
                enemy.TakeDamage(spellDamage);
            showMessage10?.Invoke($"{SessionData.Username} attacked {enemy.Name} by tsundara and caused {spellDamage}");
            double chance = rand.Next(1, 11);
       
            {
                if (enemy.Name == "Goblin" || enemy.Name == "Skelet" || enemy.Name == "Ghost" || enemy.Name == "Wywern")
                {
                    if (chance == 7 && enemy.CurrentHP < enemy.MaxHP * 0.9)
                    {
                        double hheal = 0;
                        if (enemy.Name == "Wywern")
                        {
                            hheal = 40 + player.Level * 2;
                            enemy.CurrentHP = Math.Min(enemy.CurrentHP + hheal, enemy.MaxHP);
                        }
                        else
                        {
                            hheal = 20 + player.Level * 2;
                            enemy.CurrentHP = Math.Min(enemy.CurrentHP + hheal, enemy.MaxHP);
                        }
                        showMessage?.Invoke($"{enemy.Name} is healed on {hheal}");
                    }
                    else if (chance == 4 || enemy.Name == "Ghost" && chance != 7)
                    {
                        double LightDMG = 0;
                        if (enemy.Name == "Wywern")
                        {
                            LightDMG = 30 + player.Level * 2;
                            player.TakeDamage(40 + player.Level * 1.5);
                        }
                        else
                        {
                            player.TakeDamage(25 + player.Level * 1.5);
                            LightDMG = 30 + player.Level * 2;
                        }
                        showMessage?.Invoke($"{enemy.Name} attacking tsundara and cause {LightDMG} to {SessionData.Username}");
                    }
                    else
                    {
                        player.TakeDamage(enemy.Attack);
                        showMessage?.Invoke($"{enemy.Name} caused {enemy.Attack:F2} damage to {SessionData.Username}!");
                    }
                }
                else
                {
                    player.TakeDamage(enemy.Attack);
                    showMessage?.Invoke($"{enemy.Name} attack and causes {enemy.Attack:F1} of damage!");

                }
            }

            updateUI(); 
            }
        void EarhArmor()
        {
            double manaCost = 60;
            double armoRR = player.Armor + player.Level * 1.5 - 1;
            if (player.CurrentMana < manaCost)
            {
                MessageBox.Show("Not enough mana for Earth Armor!");
                return;
            }
            updateUI();
            double defenseBoost = 5 * player.Level;
            int duration = 5;

            player.CurrentMana -= manaCost;
            player.Armor += defenseBoost;
            player.TempDefenseBonus = defenseBoost;
            player.DefenseTurnsRemaining = duration;

            showMessage10?.Invoke($"🛡️ Defence is boosted by {defenseBoost:F1} on {duration} moves!");

            updateUI?.Invoke();
            this.Close();
        }

        public Form4(Character player, Character enemy, Action updateUI, Action<string> showMessage, Action<string> showMessage10)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.TopMost = true;
            InitializeComponent();
            this.player = player;
            this.enemy = enemy;
            this.updateUI = updateUI;
            this.showMessage = showMessage;
            this.showMessage10 = showMessage10;
            this.AcceptButton = button1;
        }
        private Action<string> showMessage;
        private Action<string> showMessage10;

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                Lightning(); 
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                EarhArmor();
            }
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }
    }
}
