using Core;
using Pieces;
using UnityEngine;
using UnityEngine.UI;

namespace Tile
{
    public class TileView : View
    {
        [SerializeField]
        private Image image;
        private Color defaultColor;

        public void Configure(TileModel model)
        {
            defaultColor = model.tileColor;
            image.color = defaultColor;
        }

        public void ShowStepEffect(MoveType type)
        {
            switch (type)
            {
                case MoveType.NormalMove:
                    image.color = Color.green;
                    break;
                case MoveType.KillMove:
                    image.color = Color.red;
                    break;
                case MoveType.NULL:
                    image.color = defaultColor;
                    break;
            }
        }
    }
}