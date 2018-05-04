using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class SkillTrigger:MonoBehaviour
    {
        private Vector2 position; //存储trigger当前位置
        public bool isRepeatedBySteps; //trigger是否会按照步骤重复
        public int triggerExistingTime; //记录触发器已经存在了多久
        public int triggersDurationSteps; //存储触发器的持续步骤数
        public int delaySteps; //存储效果开始允许触发所需的延迟步骤数

        private Hero ownerHero; //存储技能效果归属的英雄
        private Hero targetHero; //存储buff宿主   记得得更新上述两个对象

        public ParticleSystem specialEffect; //存储技能释放特效
        public Vector2[] effectRange;  //存储技能触发器范围的二维向量数组 （存储的是主角面向的相对位置的值（比如如果包含主角身前1格范围则为0,1），以x为横坐标，y为纵坐标（正方向为角色朝向），角色为坐标原点）
        public List<Buff> effectBuff; //存储技能效果携带的Buff数组

        public List<EnableEffectTypes> enbleEffectTypesList; //生效条件类型枚举以及其值的列表


        public void StepsMoveOn()
        {

        }

        public void TurnsMoveOn()
        {

        }

        private void TakeEffect()
        {

        }

        private bool EnableEffectCheck()
        {
            bool isEnable = true;

            //轮询列表中的每个枚举，一旦遇到不符合的条件都会自动跳出返回false
            for (int i = 0; i < enbleEffectTypesList.Count; i++)
            {
                switch (enbleEffectTypesList[i])
                {
                    case EnableEffectTypes.camp_opponent:
                        isEnable = targetHero.OwnPlayer.GetCamp() != ownerHero.OwnPlayer.GetCamp();
                        break;

                    case EnableEffectTypes.camp_self:
                        isEnable = targetHero.OwnPlayer.GetCamp() == ownerHero.OwnPlayer.GetCamp();
                        break;

                    case EnableEffectTypes.isSlowed_false:
                        //如果当前移动力小于最大移动力，则不视为减速
                        isEnable = targetHero.Hero_Stats_Cur.movingPoint >= targetHero.Hero_Stats_Max.movingPoint;
                        break;

                    case EnableEffectTypes.isSlowed_true:
                        //如果当前移动力大于等于最大移动力，则不视为减速
                        isEnable = targetHero.Hero_Stats_Cur.movingPoint < targetHero.Hero_Stats_Max.movingPoint;
                        break;

                    case EnableEffectTypes.isStun_true:
                        isEnable = !targetHero.isStuned;
                        break;

                    case EnableEffectTypes.isStun_false:
                        isEnable = targetHero.isStuned;
                        break;

                    case EnableEffectTypes.isHPLowerThan4_true:
                        isEnable = targetHero.Hero_Stats_Cur.movingPoint < 4;
                        break;

                    case EnableEffectTypes.isHPLowerThan4_false:
                        isEnable = targetHero.Hero_Stats_Cur.movingPoint >= 4;
                        break;

                    default:
                        isEnable = true;
                        break;
                }
            }

            return isEnable;
        }

    }
}