using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace WindowsFormsApp1
{
    public class Character
    {
        public string Name { get; set; }
        public double MaxHP;
        public double CurrentHP;
        public double Attack;
        public double MaxMana;
        public double CurrentMana;
        public double Armor;
        public int Level;
        public int Experience;
        public int NextLevelExp;
        public double TempDefenseBonus { get; set; }
        public int DefenseTurnsRemaining { get; set; }

        public Character(string name, double hp, double attack, double armor, double mana)
        {
            Name = name;
            MaxHP = hp;
            CurrentHP = hp;
            Attack = attack;
            Level = 1;
            Experience = 0;
            NextLevelExp = 100;
            MaxMana = mana;
            CurrentMana = mana;
            Armor = armor;
        }
        public void GainExp(int exp)
        {
            Experience += exp;
            while (Experience >= NextLevelExp)
            {
                Experience -= NextLevelExp;
                LevelUp();
            }
        }
        private void LevelUp()
        {
            Level++;
            double levrise = 1 + Level * 0.25;
            MaxHP += 10 + levrise;
            MaxMana += 10 + levrise;
            Attack += 5;
            CurrentHP = MaxHP;
            CurrentMana = MaxMana;
            NextLevelExp = (int)(NextLevelExp * 1.5);

            MessageBox.Show($"Conratilations! {Name} reached Level: {Level}!");
        }
        public void TakeDamage(double dmg)
        {
            double damageTaken = dmg - Armor;
            if (damageTaken < 0)
                damageTaken = 0;
            CurrentHP -= damageTaken;
            if (CurrentHP < 0) CurrentHP = 0;
        }
        public bool UseMana(double amount)
        {
            if (CurrentMana >= amount)
            {
                CurrentMana -= amount;
                Console.WriteLine($"Mana used: {amount}, current mana: {CurrentMana}");
                return true;
            }
            return false;
        }

        public double Heal(double baseAmount, double manaCost)
        {
            if (!UseMana(manaCost))
                return 0;

            double healAmount = baseAmount + (Level * 1.2);
            double missingHP = MaxHP - CurrentHP;

            double actualHealed = Math.Min(healAmount, missingHP);
            CurrentHP += actualHealed;

            return actualHealed;
        }
    }
}

