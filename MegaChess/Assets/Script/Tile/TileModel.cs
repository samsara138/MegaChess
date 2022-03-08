using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tile
{
    [CreateAssetMenu(fileName = "TileModel", menuName = "Mega Chess/Tile Model")]
    public class TileModel : Model
    {
        public static int tileLength = 100;

        public Sprite sprite;

        public TileType tileType;
    }

    public enum TileType
    {
        Null = 0,
        Black,
        White
    }
}