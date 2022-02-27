using Core;
using UnityEngine;
using UnityEngine.UI;

namespace Tile
{
    public class TileView : View
    {
        [SerializeField]
        private Image image;

        public void Configure(TileModel model)
        {
            image.color = model.tileColor;

        }
    }
}