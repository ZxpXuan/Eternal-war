using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameProcedure: MonoBehaviour
    {
        private BattleField BF;
        private GameMode GM;

        private void Awake()
        {
            //LoadGameMode()   加载游戏模式  目前先写成固定加载第一个模式
        }


        private void GameMainProcedure()// 游戏运行的主流程
        {

        }
            
        private void LoadGameMode(GameMode gm)                  // 加载游戏模式
        {
            GM = gm;
        }


    }
}
