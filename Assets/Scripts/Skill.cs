using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts
{
    public class Skill:MonoBehaviour
    {
        public int skillID; //记录技能ID
        public string skillName; //技能名称
        public Sprite skillIcon; //技能ICON
        public List<SkillTrigger> skillTriggers; //技能触发器列表

        private Vector2 position; //存储trigger当前位置; 初始化时应该等于当前英雄位置
        private GameMode controlMode; //存储Mode控制器 ;需要初始化
        private Hero ownerHero; //存储技能归属的英雄

        public int skillAPCost; //技能的AP消耗
        public Direction direction; //技能在释放时的朝向 （所有技能都是默认向上的）
        public bool cdByTurn; //技能发动CD，以回合计，为false则代表可以在一回合内使用多次
        public int CD_Cur;  //技能当前剩余CD；
        public int CD_Ori;

        public void InitialLize(Vector2 p, Hero ownHero, GameMode gameMode)  //上层驱动的某些值的初始化
        {
            position = p;
            ownerHero = ownHero;
            controlMode = gameMode;
        }

        public void MoveOn()
        {
            TakeEffect();
        }

        private void TakeEffect() //此处的生效就是将trugger加入到mode中的trigger列表中,技能计算是开始生效了
        {
            if (ValidCheck())
            {
                int length = skillTriggers.Count;
                for (int i = 0; i <= length; i++)
                {
                    controlMode.skillTriggerList.Add(skillTriggers[i]);
                }
                CD_Cur = CD_Ori;  //成功使用出来，CD
            }
        }

        private bool ValidCheck()  //游戏回合内技能释放是否合法的检验
        {
            bool flag = false;

            if ( !ownerHero.isSilenced)   //是否被沉默
            {
                flag = true;
            }

            return flag;
        }

        public void SetControlMode(GameMode mode)
        {
            controlMode = mode;
        }
















    }
}

