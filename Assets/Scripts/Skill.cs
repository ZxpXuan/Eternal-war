using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts
{
    public class Skill
    {
        public int skillID; //记录技能ID
        public string skillName; //技能名称
        public Sprite skillIcon; //技能ICON
        public List<SkillTrigger> skillTriggers; //技能触发器列表
        private Hero thisHero; //存储技能归属的英雄
        public int skillAPCost; //技能的AP消耗
        public  Direction direction; //技能释放朝向
        public bool cdByTurn; //技能发动CD，以回合计，为0则代表可以在一回合内使用多次

    }
}

