using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts
{
    public class Buff : MonoBehaviour
    {

        public BuffEffectAndValue buffEffectAndValue; //生效种类以及值，每个buff只带一种效果
        public List<EnableEffectTypes> enbleEffectTypesList; //生效条件类型枚举以及其值的列表

        private Hero ownerHero; //存储技能效果归属的英雄
        private Hero targetHero; //存储buff宿主

        public bool isExistByTurn; //buff是否以回合计其效果，如果是0，则代表回合
        public bool isRepeated; //buff是否重复生效。按照回合或者是按照steps重复生效；如果不重复，则是单次效果，生效后Buff就可以被销毁了
        public int buffDelay; //buff延迟生效时间（按照回合或步骤结算效果）
        public int buffDuration; //buff持续时间（如果=0则为立即效果，按照回合或步骤结算效果）

        public ParticleSystem  BuffSpecialEffect; //存储buff携带特效，需要在Buff效果消失同时销毁buff效果

        public void MoveOn()   //无论是不是按回合调用，都有这个效果，只不过是当是按回合/按步骤时，是在一回合结束/步骤结束时才会调用这个buff
        {
            //尤其是，眩晕、减速、沉默、AP_Max这类效果的buff必定是回合结算，其余按照步骤结算

            //if (buffDelay > 0)
            //{
            //    buffDelay--;
            //    return;
            //}

            //if (EnableEffectCheck())
            //{
            //    TakeEffect();
            //}

            //if (!isRepeated)
            //{
            //    DestroyBuff();
            //    return;
            //}
            
            //if (buffDuration > 0)
            //{
            //    buffDuration--;
            //}
            //else
            //{
            //    DestroyBuff();
            //}

            //return;


            if (isRepeated)
            {
                if (buffDuration > 0)
                {
                    buffDuration--;

                    if (buffDelay > 0)
                    {
                        buffDelay--;
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
                if (buffDelay > 0)
                    buffDelay--;
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

        private void DestroyBuff()
        {
            RecoverStatus();
            targetHero.Buffs.Remove(this);
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

        private void TakeEffect()
        {
            bool tempbool;

            switch (buffEffectAndValue.skillEffectTypes)
            {
                case SkillEffectTypes.HP:
                    targetHero.Hero_Stats_Cur.HP += buffEffectAndValue.affectValue;
                    if (targetHero.Hero_Stats_Cur.HP < 0)
                        targetHero.Hero_Stats_Cur.movingPoint = 0;    //避免HP小于零
                    if (targetHero.Hero_Stats_Cur.HP > targetHero.Hero_Stats_Max.HP)
                        targetHero.Hero_Stats_Cur.HP = targetHero.Hero_Stats_Max.HP;   //避免HP大于Max值
                    break;

                case SkillEffectTypes.HP_Max:
                    targetHero.Hero_Stats_Max.HP += buffEffectAndValue.affectValue;
                    if (targetHero.Hero_Stats_Max.HP < 1)
                        targetHero.Hero_Stats_Max.HP = 1;    //避免HP_Max小于1
                    if (targetHero.Hero_Stats_Max.HP < targetHero.Hero_Stats_Cur.HP)
                    {
                        targetHero.Hero_Stats_Cur.HP = targetHero.Hero_Stats_Max.HP;   //避免玩家当前HP大于HP_Max
                    }
                    break;

                case SkillEffectTypes.AP:
                    targetHero.OwnPlayer.actionPoint += buffEffectAndValue.affectValue;
                    if (targetHero.OwnPlayer.actionPoint < 0)
                        targetHero.OwnPlayer.actionPoint = 0;    //避免AP小于零
                    if (targetHero.OwnPlayer.actionPoint > targetHero.OwnPlayer.actionPoint_Max)
                        targetHero.OwnPlayer.actionPoint_Max = targetHero.OwnPlayer.actionPoint;   //避免AP大于Max值
                    break;

                case SkillEffectTypes.AP_Max:
                    targetHero.OwnPlayer.actionPoint_Max += buffEffectAndValue.affectValue;
                    if (targetHero.OwnPlayer.actionPoint_Max < 1)
                        targetHero.OwnPlayer.actionPoint_Max = 1;    //避免AP_Max小于1
                    if (targetHero.OwnPlayer.actionPoint_Max < targetHero.OwnPlayer.actionPoint)
                    {
                        targetHero.OwnPlayer.actionPoint = targetHero.OwnPlayer.actionPoint_Max;   //避免玩家当前HP大于HP_Max
                    }
                    break;

                case SkillEffectTypes.stun:
                    if (buffEffectAndValue.affectValue > 0)
                    {
                        tempbool = true;
                    }
                    else { tempbool = false; }
                    targetHero.isStuned = tempbool;
                    break;

                case SkillEffectTypes.silence:
                    if (buffEffectAndValue.affectValue > 0)
                    {
                        tempbool = true;
                    }
                    else { tempbool = false; }
                    targetHero.isSilenced = tempbool;
                    break;

                case SkillEffectTypes.movingPoint:
                    targetHero.Hero_Stats_Cur.movingPoint += buffEffectAndValue.affectValue;
                    if (targetHero.Hero_Stats_Cur.movingPoint < 0)
                        targetHero.Hero_Stats_Cur.movingPoint = 0;    //避免移动力小于零
                    if (targetHero.Hero_Stats_Cur.movingPoint > targetHero.Hero_Stats_Max.movingPoint)
                        targetHero.Hero_Stats_Cur.movingPoint = targetHero.Hero_Stats_Max.movingPoint;   //避免移动力大于Max值
                    break;

                case SkillEffectTypes.movingPoing_Max:
                    targetHero.Hero_Stats_Max.movingPoint += buffEffectAndValue.affectValue;
                    if (targetHero.Hero_Stats_Max.movingPoint < 1)
                        targetHero.Hero_Stats_Max.movingPoint = 1;    //避免MP_Max小于1
                    if (targetHero.Hero_Stats_Max.movingPoint < targetHero.Hero_Stats_Cur.movingPoint)
                    {
                        targetHero.Hero_Stats_Cur.movingPoint = targetHero.Hero_Stats_Max.movingPoint;  //避免玩家当前MP大于MP_Max
                    }
                    break;

                case SkillEffectTypes.distance:    //之后再制作
                    break;

                case SkillEffectTypes.createBuffOnTarget:  //之后再制作
                    break;

                default:
                    break;
            }

        }

        private void RecoverStatus()        //在buff失效后恢复英雄或者palyer状态
        {
            switch (buffEffectAndValue.skillEffectTypes)
            {
                case SkillEffectTypes.HP_Max:
                    targetHero.Hero_Stats_Max.HP-= buffEffectAndValue.affectValue;
                    break;

                case SkillEffectTypes.AP_Max:
                    targetHero.OwnPlayer.actionPoint_Max -= buffEffectAndValue.affectValue;
                    break;

                case SkillEffectTypes.stun:
                    targetHero.isStuned = false;
                    break;

                case SkillEffectTypes.silence:
                    targetHero.isSilenced = false;
                    break;

                case SkillEffectTypes.movingPoing_Max:
                    targetHero.Hero_Stats_Max.movingPoint -= buffEffectAndValue.affectValue;
                    break;

                case SkillEffectTypes.distance:    //之后再制作
                    break;

                case SkillEffectTypes.createBuffOnTarget:  //之后再制作
                    break;

                default:
                    return;
            }

        }

    }

}
