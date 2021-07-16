using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Resources.Scripts.TrackedObject
{
    public class ObjetoAtributos
    {
        private int hp;

        public ObjetoAtributos(int hp, int defense, int attackPower)
        {
            Hp = hp;
            Defense = defense;
            AttackPower = attackPower;
        }

        public int Hp { get => hp; set => hp = value; }
        public int Defense { get; set; }
        public int AttackPower { get; set; }
    }
}
