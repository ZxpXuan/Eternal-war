using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    class SkillTrigger
    {
        private Vector2 position; //存储trigger当前位置
        public int trigger existingTime; //记录触发器已经存在了多久
        public int triggersDurationSteps; //存储触发器的持续步骤数
        public int delaySteps; //存储效果开始允许触发所需的延迟步骤数
        private Hero ownerHero; //存储技能效果归属的英雄
        public ParticleSystem specialEffect; //存储技能释放特效
        private effectRange Vector2[];  //存储技能触发器范围的二维向量数组 （存储的是主角面向的相对位置的值（比如如果包含主角身前1格范围则为0,1），以x为横坐标，y为纵坐标（正方向为角色朝向），角色为坐标原点）
        private Buff effectBuff[]; //存储技能效果携带的Buff数组

    }
}