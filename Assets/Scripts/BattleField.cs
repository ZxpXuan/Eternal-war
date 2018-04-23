using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class BattleField
    {
        private int battleFieldID;  //战场ID
        private Sprite[,] battleFieldTexture = new Sprite[5,7];  //存储战场的贴图表现的二维列表
        private List<Skill> specialTerrainList; //存储的特殊地形（技能）数组

    }
}