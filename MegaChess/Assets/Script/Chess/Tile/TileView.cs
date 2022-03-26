using Core;
using Pieces;
using UnityEngine;
using UnityEngine.UI;

namespace Tile
{
    public class TileView : View
    {
        [SerializeField] private Image image;

        [SerializeField] private Button clickDetector;
        public Button ClickDetector => clickDetector;

        public void Configure(TileModel model)
        {
            image.sprite = model.sprite;
            gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(GlobalParameters.TILE_LENGTH, GlobalParameters.TILE_LENGTH);
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
                case MoveType.CurrentPosition:
                    image.color = Color.yellow;
                    break;
                case MoveType.NULL:
                    image.color = Color.white;
                    break;
            }
        }
    }
}