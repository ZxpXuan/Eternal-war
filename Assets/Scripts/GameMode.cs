using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameMode:MonoBehaviour
    {
        private int gamemodeID; // 游戏模式ID
        public string gamemodename;  //游戏模式名称
        public int numberOfPlayers; //所需游戏玩家数
        private List<Player> playerList;  //场内的玩家列表，就是一场游戏最多多少人参与，有哪些人参与。初版功能只要有几个玩家序号就行了，便于扩展同局参与人数
        private int stepsInMode=0; //游戏当前步骤数
        private int turnsInMode=0; //游戏当前进行的回合数
        private BattleField BF; //存储游戏当前的战场地图
        private List<SkillTrigger> skillTriggerList;//存储独立于英雄存在的技能触发器

        private void Awake()   //初始化函数
        {
            //加载玩家
            for (int i = 0; i <= numberOfPlayers; i++)
            {
                Player p=new Player();
                playerList.Add(p);
            }

            //实例化载地图
            BattleField Map_01;

        }

        private void Start()
        {
            GamemodeProcedure();
        }


        void GamemodeProcedure()  //游戏模式运行的主流程，这玩意根据不同的游戏模式进行重写
        {
            GameStart();
        }

        void GameStart() //游戏初始化，创建玩家，创建英雄，创建战场地图，创建基地
        {

            Debug.Log("GameStart()");
        }

        void StepsMoveOn()//步骤数增加，在其中维护技能、维护buff效果
        {
            stepsInMode++;
        }

        void TurnMoveOn()//总回合数增加，在其中维护技能、维护buff效果
        {
            turnsInMode++;
        }

        bool WhetherGameFinished() //判断游戏是否达到结束条件
        {
            bool isFinished = false;
            return isFinished;
        }









        void GameEnd()//游戏结束之后的操作，输出游戏结果，清空数据
        {

        }

        void GameModeInitial() //游戏模式运行初始化，需要重写
        {

        }

        List<Hero> ChooseHerosList()//玩家选取英雄，返回英雄选择列表 
        {
            List<Hero> herolist = new List<Hero>();

            return herolist; ;
        }

        List<int> GameFinished() //游戏结束的逻辑，返回胜利者的玩家ID，需要重写
        {
            List<int> winner = new List<int>();
            winner.Add(1);
            return winner;
        }

        private void LoadBattleField(BattleField BF)//加载战场地图，以及战场上的特殊地形（技能）到SkillList中
        {

        }

        private void MaintainSkillTriggerList_refreshSkill()  //维护TriggerList刷新技能trigger的剩余持续步骤数，清空无效了的技能trigger；在此判断trigger是否能够合法触发某单位使其附带buff效果，为英雄的BuffList新增数据
        {

        }

        private void MaintainActionList_refreshSkill() //维护各个英雄的行动列表，英雄的行动在这里被判断，立即效果在这里结算；先结算位移，再结算技能；在此判断技能的trigger是否合法产生，为TriggerList新增数据
        {

        }

        private void MaintainBuffList_refreshBuff() //维护各角色身上的buff列表，buff效果在这里结算；刷新buff的剩余持续步骤数，清空到期的buff，为英雄的BuffList减少数据
        {

        }
    }
}
