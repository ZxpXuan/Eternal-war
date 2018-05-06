using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Player:MonoBehaviour
    {
        private int playerID;  //玩家ID，这里的ID只是代表着玩家在一局游戏中的序号，需要初始化
        private Sprite playerIcon;  //玩家ICON
        public int actionPoint;  //当前AP点数 
        public int actionPoint_Max; //总AP点数 

        //用于存储单个玩家操控的英雄对象列表(所以这是个prefab)，当英雄死亡时需要重新维护; 需要上层进行初始化，加载玩家英雄池
        private List<GameObject> heroes; 
        private List<GameObject> heroes_Dead; //英雄死亡池，需要在回合moveOn时维护
        private int Camp;//存储玩家的阵营
        private Castle castle; //存储玩家对应的基地
        public GameMode controlMode; //存储Mode控制器 ;需要初始化


        public void InitialLize(int playerIndex,int apM, Castle cas, int camp, GameMode gameMode)  //上层驱动的某些值的初始化
        {
            playerID = playerIndex;
            actionPoint = actionPoint_Max = apM;
            castle = cas;
            Camp = camp;
            controlMode = gameMode;
        }




        public void AddHero(GameObject h)
        {
            heroes.Add(h);
        }
        public List<GameObject> GetHeroes()
        {
            return heroes;
        }

        public Castle GetCastle()
        {
            return castle;
        }

        public int GetPlayerID()
        {
            return playerID;
        }

        public int GetAP_Max()
        {
            return actionPoint_Max;
        }
        public void SetAP_Max(int AP)
        {
            actionPoint_Max = AP;
        }
        public int GetAP_Cur()
        {
            return actionPoint;
        }
        public void SetAP_Cur(int AP)
        {
            actionPoint = AP;
        }

        public int GetCamp()
        {
            return Camp;
        }

        public void SetICON(Sprite icon)
        {
            playerIcon = icon;
        }
        public Sprite GetICON()
        {
            return playerIcon;
        }

        public void SubtractCastleHP()
        {
            castle.SetCSHP_Curr (castle.GetCSHP_Cur() - castle.basicDMG);
        }
    }
}