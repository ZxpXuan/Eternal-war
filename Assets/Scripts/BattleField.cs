using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class BattleField:MonoBehaviour
    {
        private int battleFieldID;  //战场ID

        //[System.Serializable]
        //private class SpriteWithCoordinate
        //{
        //    public Sprite tile;
        //    public Vector2 coordinate;
        //}
        //public List<SpriteWithCoordinate> battleFieldTextureList;  //存储战场的贴图表现的二维列表
        private List<Skill> specialTerrainList; //存储的特殊地形（技能）数组
        public Sprite tile;
        public int length;
        public int width;
        private void Start()
        {

            for (int i=0; i<= width; i++)
            {
                
                for (int j= 0; j<= length; j++)
                {
                    string tileName = "tile_" + i.ToString()+ j.ToString();
                    var tile_cur = new GameObject(tileName).AddComponent<SpriteRenderer>();
                    tile_cur.sprite = tile;
                    tile_cur.transform.localPosition = new Vector3(i,j, 0);
                }
                
            }
        }
    }
}