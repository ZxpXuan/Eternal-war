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
        private int numberOfHeroesEachPlayer = 3; //存储每个玩家控制的英雄数
        public List<Player> playerList;  //场内的玩家列表，就是一场游戏最多多少人参与，有哪些人参与。初版功能只要有几个玩家序号就行了，便于扩展同局参与人数
        private int stepsInMode=0; //游戏当前步骤数
        private int turnsInMode=0; //游戏当前进行的回合数
        private BattleField BF; //存储游戏当前的战场地图
        public List<SkillTrigger> skillTriggerList;//存储独立于英雄存在的技能触发器

        private void Awake()   //初始化函数 游戏初始化，创建玩家，创建英雄，创建战场地图
        {
            //加载玩家
            for (int i = 0; i <= numberOfPlayers; i++)
            {
                Player p=new Player();
                //p.SetCamp(i);
                //playerList.Add(p);
            }

            //实例化载地图
            LoadBattleField();
            

            //实例化英雄加载英雄
            GameObject Players = new GameObject("Players");
            GameObject Hero_temp = (GameObject)Resources.Load("Prefabs/Heroes/Hero_001");   //实例化必须要用gameobject，使用时需要调用其中的component
            for (int i = 0; i <= numberOfPlayers; i++)
            {
                for (int j = 0; j <= numberOfHeroesEachPlayer; j++)
                {
                    playerList[i].AddHeros(Hero_temp);
                    var heroes_List = playerList[i].GetHeroes();
                    Instantiate(heroes_List[j]).transform.parent=GameObject.Find("Players").transform;  //把它预设到场景中的Players节点下
                }
            }

            //加载地形技能实例到skillTriggerList中



        }

        private void Start()
        {
            GamemodeProcedure();
        }


        void GamemodeProcedure()  //游戏模式运行的主流程，根据不同的游戏模式进行重写
        {
            GameStart();
        }

        void GameStart() 
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

        public void LoadBattleField()//加载战场地图，以及战场上的特殊地形（技能）到SkillList中
        {
            GameObject BF = (GameObject)Resources.Load("Prefabs/Heroes/Map_001");
            Instantiate(BF);
            BF.AddComponent<Transform>().position = new Vector3(0, 0, 0);   //在添加组件的同时设置组件，这样写更简洁

            //暂时先不写特殊地形
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
