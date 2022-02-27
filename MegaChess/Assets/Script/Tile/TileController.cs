using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tile
{
    public class TileController : Controller<TileModel,TileView>
    {
        private Vector2 gridPosition;
        public void Configure(Vector2 pos)
        {
            View.Configure(Model);
            gridPosition = pos;
        }
    }
}