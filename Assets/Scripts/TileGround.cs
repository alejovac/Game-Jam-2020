using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileGround : Tile
{
    int luminosity;
    int humidity;
    int wind;

    bool recovered;

    //Sprite 


    public override void RefreshTile (Vector3Int location, ITilemap tilemap)
    {

    }

    public override void GetTileData(Vector3Int location, ITilemap tilemap, ref TileData tileData)
    {


    }



}
