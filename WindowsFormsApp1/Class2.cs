using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WindowsFormsApp1.Form2;

namespace WindowsFormsApp1
{
    public class EnemyTemplate
    {
        public string Name { get; set; }
        public double MinHP { get; set; }
        public double MaxHP { get; set; }
        public double MinAttack { get; set; }
        public double MaxAttack { get; set; }
        public int Level { get; set; }
        public EnemyTemplate(string name, double minHP, double maxHP, double minAtk, double maxAtk, int level)
        {
            Name = name;
            MinHP = minHP;
            MaxHP = maxHP;
            MinAttack = minAtk;
            MaxAttack = maxAtk;
            Level = level;
        }

        public Character CreateInstance(Random rand, int playerLevel)
        {
            double baseHP = rand.Next((int)MinHP, (int)MaxHP + 1);
            double baseAttack = rand.Next((int)MinAttack, (int)MaxAttack + 1);

            double scaledHP = baseHP + (playerLevel * 7);
            double scaledAttack = baseAttack + (playerLevel * 2);

            double difficultyMultiplier = 1.0;
            switch (SessionData.GameDifficulty)
            {
                case Difficulty.Easy:
                    difficultyMultiplier = 0.8;
                    break;
                case Difficulty.Normal:
                    difficultyMultiplier = 1.0;
                    break;
                case Difficulty.Hard:
                    difficultyMultiplier = 1.6;
                    break;
            }

            scaledHP *= difficultyMultiplier;
            var enemy = new Character(Name, scaledHP, scaledAttack, 0, 0);
            enemy.Level = playerLevel;
            return enemy;
        }
    }
}
