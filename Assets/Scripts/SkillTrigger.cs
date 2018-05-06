using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class SkillTrigger:MonoBehaviour
    {

        private Vector2 position; //存储trigger当前位置; 初始化时应该等于当前英雄位置
        public bool isRepeated; //trigger是否会按照步骤重复
        public int triggersDuration; //存储触发器的持续步骤数
        public int triggerdelay; //存储效果开始允许触发所需的延迟步骤数

        private Hero ownerHero; //存储技能效果归属的英雄 ；初始化trigger时需要被player指定
        private List<Hero> targetHero; //存储buff宿主列表  ;  在每次判断前都假设全部英雄都是目标对象，然后再进行筛选，留下的才是生效对象 ;初始化时需要被指定
        public GameMode controlMode; //存储Mode控制器

        public ParticleSystem specialEffect; //存储技能释放特效
        public List<Vector2> effectRange;  //存储技能触发器范围的二维向量数组 
        public List<Vector2> effectRange_ori;  //存储技能触发器范围的二维向量数组 

        //（存储的是主角面向的相对位置的值（比如如果包含主角身前1格范围则为0,1），以x为横坐标，y为纵坐标（正方向为角色朝向），角色为坐标原点）
        //这个range需要在trigger初始化的时候，由当时发出技能的player位置，以及施法的朝向，和初始trigger的range，重新累加和转置，保存为新的range
        //另外，这个range也需要根据trigger是否运动来进行更改位移

        public List<Buff> effectBuff; //存储技能效果携带的Buff数组

        public List<EnableEffectTypes> enbleEffectTypesList; //生效条件类型枚举以及其值的列表

        private void Awake()
        {

        }

        public void MoveOn()
        {
            if (isRepeated)
            {
                if (triggersDuration > 0)
                {
                    triggersDuration--;

                    if (triggerdelay > 0)
                    {
                        triggerdelay--;
                    }
                    else
                    {
                        if (EnableEffectCheck())
                        {
                            TakeEffect();
                        }
                    }
                }
                else
                {
                    DestroyBuff();
                    return;
                }
            }
            else  //如果不重复
            {
                if (triggerdelay > 0)
                    triggerdelay--;
                else
                {
                    if (EnableEffectCheck())
                    {
                        TakeEffect();
                        DestroyBuff();
                    }
                }
            }
        }

        //根据设置的初始range，playerPosi以及施法方向direction，得出最终最终的range
        public void InitializeTheTriggerRange(Vector2 heroPosi, Direction direction)   //初始化trigger range ，在Player释放技能时被调用
        {
            position = heroPosi;  //初始化trigger位置

            switch (direction)
            {
                case Direction.up:
                    break;

                case Direction.down:
                    for (int i = 0; i <= effectRange_ori.Count ; i++)
                    {
                        Vector2 v2Temp = new Vector2(effectRange_ori[i].x, effectRange_ori[i].y * -1);
                        effectRange[i] = v2Temp+position;
                    }
                    break;

                case Direction.left:
                    for (int i = 0; i <= effectRange_ori.Count; i++)
                    {
                        Vector2 v2Temp = new Vector2(effectRange_ori[i].y*-1, effectRange_ori[i].x * 1);
                        effectRange[i] = v2Temp + position;
                    }
                    break;

                case Direction.right:
                    for (int i = 0; i <= effectRange_ori.Count; i++)
                    {
                        Vector2 v2Temp = new Vector2(effectRange_ori[i].y, effectRange_ori[i].x * -1);
                        effectRange[i] = v2Temp + position;
                    }
                    break;

                default:
                    break;
            }
        }

        private void TakeEffect()  //在剩余的目标英雄身上产生buff，add list。至于触发了多少个英雄，则是在外部处理
        {
            int count = effectBuff.Count;
            int length = targetHero.Count;
            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j <= length; j++)      //需要对每一个目标添加buff
                {
                    targetHero[j].Buffs.Add(effectBuff[i]);
                }
                
            }
        }

        private void DestroyBuff() //从游戏模式中移除trigger
        {
            controlMode.skillTriggerList.Remove(this);
        }

        private bool EnableEffectCheck()
        {
            bool isEnable = true;

            //在此处添加触发范围决定是否触发成功的判断，需要检查所有在场英雄位置
            PositionCheck();

            //轮询列表中的每个枚举，一旦遇到不符合的条件都会自动跳出返回false
            for (int i = 0; i <= enbleEffectTypesList.Count; i++)
            {
                int j = 0;
                int targetHeroesNum = targetHero.Count;

                for ( j = 0; j < targetHeroesNum; j++)//轮询每一种枚举，每一个潜在目标英雄都是否满足条件
                {


                    switch (enbleEffectTypesList[i])
                    {
                        case EnableEffectTypes.camp_opponent:
                            isEnable = targetHero[j].OwnPlayer.GetCamp() != ownerHero.OwnPlayer.GetCamp();
                            break;

                        case EnableEffectTypes.camp_self:
                            isEnable = targetHero[j].OwnPlayer.GetCamp() == ownerHero.OwnPlayer.GetCamp();
                            break;

                        case EnableEffectTypes.isSlowed_false:
                            //如果当前移动力小于最大移动力，则不视为减速
                            isEnable = targetHero[j].Hero_Stats_Cur.movingPoint >= targetHero[j].Hero_Stats_Max.movingPoint;
                            break;

                        case EnableEffectTypes.isSlowed_true:
                            //如果当前移动力大于等于最大移动力，则不视为减速
                            isEnable = targetHero[j].Hero_Stats_Cur.movingPoint < targetHero[j].Hero_Stats_Max.movingPoint;
                            break;

                        case EnableEffectTypes.isStun_true:
                            isEnable = !targetHero[j].isStuned;
                            break;

                        case EnableEffectTypes.isStun_false:
                            isEnable = targetHero[j].isStuned;
                            break;

                        case EnableEffectTypes.isHPLowerThan4_true:
                            isEnable = targetHero[j].Hero_Stats_Cur.movingPoint < 4;
                            break;

                        case EnableEffectTypes.isHPLowerThan4_false:
                            isEnable = targetHero[j].Hero_Stats_Cur.movingPoint >= 4;
                            break;

                        default:
                            isEnable = true;
                            break;
                    }

                    if (!isEnable)
                    {
                        targetHero.RemoveAt(j);     //如果判断到该英雄并非合法目标，则直接将他从targetList中移除
                    }
                }

               
            }

            return isEnable;
        }

        //自动维护了所有英雄是否在trigger范围内的判断，如果某英雄不在范围内，则将他从trigger列表中移除
        private void PositionCheck()   
        {
            bool oneHeroIsInRange = false;

            int targetHeroCount = targetHero.Count;
            int triggerAreaCount= effectRange.Count;
            for (int i = 0; i <= targetHeroCount; i++)
            {

                for (int j = 0; j <= triggerAreaCount; j++)  //遍历该英雄在不在所有的格子里
                {
                    if (targetHero[i].position == effectRange[j])
                    {
                        oneHeroIsInRange = true;
                    }
                }

                if (!oneHeroIsInRange)  //如果在所有的范围格子中都不存在该英雄，则在列表中移除该英雄
                {
                    targetHero.RemoveAt(i);
                }
                else   //如果英雄在某一格子里，重置判断标志
                {
                    oneHeroIsInRange = false;   
                }

            }

        }
















    }


}
