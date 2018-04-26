using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts
{
    public class TerrainSkill:MonoBehaviour
    {
        public int terrainID;  //地形ID
        public Sprite terrainTexture; //地形格材质图
        public Skill Skill; //特殊地形中存储的材质
    }
}
