using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Hero_Stats
    {
        public int HP;
        public int ATK;
        public int movingPoint;

        public Hero_Stats() { }
        public Hero_Stats(Hero_Stats stat)
        {
            HP = stat.HP;
            ATK = stat.ATK;
            movingPoint = stat.movingPoint;
        }
    }
}
