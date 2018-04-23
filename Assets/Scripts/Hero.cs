using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Hero :MonoBehaviour
    {
        private int heroID; //角色（英雄）ID
        public int ATK;
        public int movingPoint;
        private Vector2 position; //存储角色当前位置
        private Vector2 position_Planning; //存储英雄被玩家部署时的当前位置，予以显示，方便玩家从视觉上规划行动

        private List<Skill> heroSkillList; //存储角色的技能列表
        private List<Buff> Buffs; //存储英雄携带的buff列表
        public List<PlayerAction> actionList; //存储本英雄在本回合中由玩家设置的行动序列

        public Sprite heroTexture; //英雄贴图：初期版本就一个贴图搞定  不需要转面

        public void AddAction(PlayerAction act) //添加英雄在本回合本玩家所做出的行动序列
        {

        }

        public void MoveAction(int index) //将英雄的行动序列从索引位置后移
        {

        }

        public void DeleteAction(int index)//将英雄的行动序列从索引位置删除，然后剩余行动序列前移
        {

        }

    }
}