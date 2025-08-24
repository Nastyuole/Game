using Game;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using static WindowsFormsApp1.Form2;

namespace WindowsFormsApp1
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            label5.Visible = false;
            label6.Visible = false;
            button4.Enabled = false;
            double playerAttack = rand.Next(10, 16);
            double playerMana = rand.Next(140, 150);
            int playerArmor = rand.Next(4,7);
            player = new Character(SessionData.Username, 100.0, playerAttack, playerArmor, playerMana);

        }
        Character player;
        Character enemy;
        Random rand = new Random();

        private void Form3_Load(object sender, EventArgs e)
        {
            SpawnEnemy();
        }
        void DefenceDuration()
        {
            if (player.DefenseTurnsRemaining > 0)
            {
                player.DefenseTurnsRemaining--;

                if (player.DefenseTurnsRemaining == 0)
                {
                    player.Armor -= player.TempDefenseBonus;
                    player.TempDefenseBonus = 0;
                    MessageBox.Show("⏳ Defence effect is over!");
                }
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            ButtonVisible();

            double healBase = rand.Next(15, 26);
            double manaCost = 15;

            if (player.CurrentMana < manaCost)
            {
                label6.Text = "Not enough Mana!";
                return;
            }
            if (player.CurrentHP == player.MaxHP) 
            {
                label6.Text = "Hp is 100%!";
                return;
            }
            double healed = player.Heal(healBase, manaCost);

            if (healed == 0)
            {
                label6.Text = "Not enough Mana!";
            }
            else
            {
                label6.Text = $"{SessionData.Username} healed for {healed:F1} HP.";
                if (player.CurrentHP <= 0)
                {
                    MessageBox.Show("You lost...");
                    ButtonFalse();
                    button4.Enabled = false;
                    this.Close();
                    return;
                }
            }
            DefenceDuration();
            UpdateUI();
        }
        void CritChance()
        {
            double damage = player.Attack * 2;
            enemy.TakeDamage(damage);
            label6.Text = "Critical hit!";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ButtonVisible();
            EnAtk();
            RegenerateMana(5.0);
            DefenceDuration();
            UpdateUI();
        }
        List<EnemyTemplate> enemyTemplates = new List<EnemyTemplate>
        {
             new EnemyTemplate("Goblin", 50, 61, 9, 14, 1),
             new EnemyTemplate("Ork", 70, 81, 10, 18, 1),
             new EnemyTemplate("Skelet", 60, 69, 6, 15, 1),
             new EnemyTemplate("Ghost", 40, 51, 5, 7, 1),
             new EnemyTemplate("Wolf", 50, 56, 10, 15, 1)
        };

        void SpawnEnemy()
        {
            int heal = 10;
            player.CurrentHP = Math.Min(player.CurrentHP + heal, player.MaxHP);
            if (player.Level % 5 == 0) Boss();
           
            else { 
                EnemyTemplate template = enemyTemplates[rand.Next(enemyTemplates.Count)];
            enemy = template.CreateInstance(rand, player.Level);

            MessageBox.Show($"Appeared {enemy.Name} with {enemy.CurrentHP:F2} hp and attack {enemy.Attack:F2}!");
            label2.Text = enemy.Name;
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = false;
            }
            Pictures();
            UpdateUI();
        }
        void Boss()
        {
            var bossTemplate = new EnemyTemplate("Wywern", 115, 131, 21, 26, player.Level);
            enemy = bossTemplate.CreateInstance(rand, player.Level);

            MessageBox.Show($"!!! Boss {enemy.Name} Appeared with {enemy.CurrentHP:F2} hp and {enemy.Attack:F2} attack!");
            label2.Text = enemy.Name;
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = false;

            UpdateUI();
        }
        public void RegenerateMana(double amount)
        {
            double epsilon = 0.0001;
            if (player.CurrentMana + epsilon >= player.MaxMana)
            {
                player.CurrentMana = player.MaxMana; 
                return;
            }
            player.CurrentMana = Math.Min(player.CurrentMana + amount, player.MaxMana);
        }
        public void UpdateUI()
        {
            label1.Text = $"{SessionData.Username} HP: {player.CurrentHP:F2}/{player.MaxHP:F2} and Armor: {player.Armor}, Difficulty: {SessionData.GameDifficulty}";
            label2.Text = $"{enemy.Name} HP: {enemy.CurrentHP:F2}/{enemy.MaxHP:F2}";
            label7.Text = $" Level {player.Level} |  Exp: {player.Experience}/{player.NextLevelExp}";
            label8.Text = $"MP: {player.CurrentMana:F1}/{player.MaxMana}";

            progressBar1.Maximum = (int)Math.Ceiling(player.MaxHP);
            progressBar1.Value = (int)Math.Min(progressBar1.Maximum, Math.Max(progressBar1.Minimum, player.CurrentHP));

            progressBar2.Maximum = (int)enemy.MaxHP;
            progressBar2.Value = Math.Max(0, (int)enemy.CurrentHP);

            progressBar3.Maximum = (int)Math.Ceiling(player.MaxMana);
            progressBar3.Value = Math.Min(progressBar3.Maximum, Math.Max(progressBar3.Minimum, (int)player.CurrentMana));

        }

        public void ShowMessage(string text)
        {
            label5.Text = text;
        }
        public void ShowMessage10(string text)
        {
            label6.Text = text;
        }
        public void EnAtk()
        {
            double chance = rand.Next(1, 11);
            if (chance == 3)
            {
                CritChance();
            }
            else
            {
                enemy.TakeDamage(player.Attack);
                label6.Text = $"{SessionData.Username} caused {player.Attack:F2} damage to the {enemy.Name}!";
            }

            if (enemy.CurrentHP <= 0)
            {
                if (enemy.Name == "Wywern")
                {
                    player.Level++;
                    MessageBox.Show($"{SessionData.Username} defeated the Boss! Level increaces by 1!");
                    player.CurrentHP = player.MaxHP;
                    player.CurrentMana = player.MaxMana;
                    ButtonFalse();
                    button4.Enabled = true;
                }
                else
                {
                    EXPGain();
                }
            }
            else
            {
                if (enemy.Name == "Goblin" || enemy.Name == "Skelet" || enemy.Name == "Ghost" || enemy.Name == "Wywern")
                {
                    if (chance == 7 && enemy.CurrentHP != enemy.MaxHP)
                    {
                        double hheal = 0;
                        if (enemy.Name == "Wywern")
                        {
                            hheal = 45 + player.Level * 2;
                            enemy.CurrentHP = Math.Min(enemy.CurrentHP + hheal, enemy.MaxHP);
                        }
                        else
                        {
                            hheal = 20 + player.Level * 2;
                            enemy.CurrentHP = Math.Min(enemy.CurrentHP + hheal, enemy.MaxHP);
                        }
                        label5.Text = $"{enemy.Name} is healed on {hheal}";
                    }
                    else if (chance == 4 || enemy.Name == "Ghost" && chance != 7)
                    {
                        double LightDMG = 0;
                        if (enemy.Name == "Wywern")
                        {
                            LightDMG = 35 + player.Level * 2;
                            player.TakeDamage(40 + player.Level * 1.5);
                        }
                        else
                        {
                            player.TakeDamage(25 + player.Level * 1.5);
                            LightDMG = 30 + player.Level * 2;
                        }
                        label5.Text = $"{enemy.Name} attacking tsundara and cause {LightDMG} to {SessionData.Username}";
                    }
                    else
                    {
                        player.TakeDamage(enemy.Attack);
                        label5.Text = $"{enemy.Name} caused {enemy.Attack:F2} damage to {SessionData.Username}!";
                    }
                }
                else
                {
                    player.TakeDamage(enemy.Attack);
                    label5.Text = $"{enemy.Name} attack and causes {enemy.Attack:F1} of damage!";

                }
                if (player.CurrentHP <= 0)
                {
                    MessageBox.Show("You lost...");
                    ButtonFalse();
                    this.Close();
                }
            }
        }
        void ButtonVisible()
        {
            label5.Visible = true;
            label6.Visible = true;
        }
        void ButtonFalse()
        {
            button1.Enabled = false;
            button2.Enabled = false;
            button4.Enabled = false;
        }
        private void label1_Click(object sender, EventArgs e)
        {
          
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SpawnEnemy();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form4 spellForm = new Form4(player, enemy, UpdateUI, ShowMessage, ShowMessage10);
            spellForm.ShowDialog();
            if (enemy.CurrentHP <= 0)
            {
                EXPGain();
            }
            ButtonVisible();


        }
        void EXPGain()
        {
            int rewardExp = rand.Next(30, 70);
            MessageBox.Show($"{enemy.Name} is defeated! You gain {rewardExp} experience.");

            player.GainExp(rewardExp);
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = true;
        }
        private void progressBar2_Click(object sender, EventArgs e)
        {
        }

        public void label5_Click(object sender, EventArgs e)
        {
           
        }

        public void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {
            
        }

        private void progressBar3_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }
        void Pictures()
        {
            if(enemy.Name == "Skelet")
            {
               pictureBox1.Image = Game.Properties.Resources.Skelet;
            }
            else if(enemy.Name == "Goblin")
            {
                pictureBox1.Image  = Game.Properties.Resources.Goblin;
            }
            else if (enemy.Name == "Wolf")
            {
                pictureBox1.Image = Game.Properties.Resources.Wolf;
            }
            else if (enemy.Name == "Ork")
            {
                pictureBox1.Image = Game.Properties.Resources.Ork;
            }
            else if (enemy.Name == "Ghost")
            {
                pictureBox1.Image = Game.Properties.Resources.Ghost;
            }
            else if (enemy.Name == "Wywern")
            {
                pictureBox1.Image = Game.Properties.Resources.Wywern;
            }
        }
    }
}
