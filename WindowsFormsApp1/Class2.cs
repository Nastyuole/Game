using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            double levelMultiplier = 1 + (playerLevel - 1)  * 0.32; 

            double scaledHP = baseHP * levelMultiplier;
            double scaledAttack = baseAttack * (1 + (playerLevel - 1) * 0.5);
            var enemy = new Character(Name, scaledHP, scaledAttack, 0, 0);
            enemy.Level = playerLevel;
            return enemy;
        }
    }
}
