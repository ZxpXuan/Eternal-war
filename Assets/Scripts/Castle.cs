using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Castle:MonoBehaviour
    {

        private int CSHP_Max=12;  //基地总HP
        private int CSHP_Cur=12;  //基地当前HP
        private Player ownerPlayer;//belongPlayer目前归属的玩家
        private Player ownerPlayer_Origin;//belongPlayer原本归属的玩家

        public Sprite CSTexture; //基地材质图

        public int GetCSHP_Cur()
        {
            return CSHP_Cur;
        }
        public void SetCSHP_Curr(int HP)
        {
            CSHP_Cur = HP;
        }
        public int GetCSHP_Max_Cur()
        {
            return CSHP_Max;
        }
        public void SetCSHP_Max(int HP)
        {
            CSHP_Max = HP;
        }

        public Player GetownerPlayer()
        {
            return ownerPlayer;
        }
        public void SetownerPlayer(Player player)   //重新设置英雄的归属玩家   某些控制技能
        {
            ownerPlayer = player;
        }

        public Player GetownerPlayer_Origin()
        {
            return ownerPlayer_Origin;
        }
        public void ReSetownerPlayer_Origin()   //重置英雄的归属玩家   某些控制技能
        {
            ownerPlayer = ownerPlayer_Origin;
        }
    }
}
