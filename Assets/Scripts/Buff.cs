using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts
{
    public class Buff : MonoBehaviour
    {

     private Hero ownerHero; //存储技能效果归属的英雄
     private Hero targetHero; //存储buff宿主
     public int buffDelaySteps; //buff延迟生效步骤数
     public int buffDurationSteps; //buff持续步骤数（如果=0则为立即效果）
     public List<EnableEffectTypes> enableEffectTypesList; //生效条件类型枚举列表
     public BuffEffectAndValue buffEffectAndValue;   //生效种类以及值，每个buff只带一种效果

     public ParticleSystem  BuffSpecialEffect; //存储buff携带特效，需要在Buff效果消失同时销毁buff效果
    }
}
