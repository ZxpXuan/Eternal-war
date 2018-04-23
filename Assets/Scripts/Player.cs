using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Player
    {
        private int playerID;  //玩家ID，初版功能只需要用序号代替玩家就行了
        private Sprite playerIcon;  //玩家ICON
        private int actionPoint;  //当前AP点数 
        public int actionPoint_Max; //总AP点数 
        private List<Hero> heroes; //用于存储单个玩家操控的英雄对象列表，当英雄死亡时需要重新维护
        public int Camp; //存储玩家的阵营

    }
}