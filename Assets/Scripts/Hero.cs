using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Hero :MonoBehaviour
    {
        private int heroID; //角色（英雄）ID

        public Player OwnPlayer; //存储控制的Player

        public Hero_Stats Hero_Stats_Ori; //英雄属性的初始值
        public Hero_Stats Hero_Stats_Max; //英雄属性的最大值
        public Hero_Stats Hero_Stats_Cur; //英雄属性的当前值

        public bool isStuned; //英雄是否被眩晕
        public bool isSilenced; //英雄是否被沉默

        public Vector2 position; //存储角色当前位置
        public Vector2 position_Planning; //存储英雄被玩家部署时的当前位置，予以显示，方便玩家从视觉上规划行动

        public List<Skill> heroSkillList; //存储角色的技能列表
        public List<Buff> Buffs; //存储英雄携带的buff列表
        private List<PlayerAction> actionList; //存储本英雄在本回合中由玩家设置的行动序列

        public Sprite heroTexture; //英雄贴图：初期版本就一个贴图搞定  不需要转面


        public void Awake()
        {
            Hero_Stats_Cur = new Hero_Stats(Hero_Stats_Ori); //将英雄属性初始化
            Hero_Stats_Max = new Hero_Stats(Hero_Stats_Ori);
        }

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