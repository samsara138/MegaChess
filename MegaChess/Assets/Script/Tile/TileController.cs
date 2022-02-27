using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tile
{
    public class TileController : Controller<TileModel,TileView>
    {
        public void Configure()
        {
            View.Configure(Model);
        }
    }
}