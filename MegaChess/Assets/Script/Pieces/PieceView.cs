using Board;
using Core;
using UnityEngine;
using UnityEngine.UI;

namespace Pieces
{
    public class PieceView : View
    {
        [SerializeField] private Image image;
        [SerializeField] private Button ClickDetector;
        public Button GetClickDetector => ClickDetector;

        public void Configure(PieceModel model,PlayerSide side)
        {
            switch (side)
            {
                case PlayerSide.player1:
                    image.sprite = model.WhiteSpirte;
                    break;
                case PlayerSide.player2:
                    image.sprite = model.BlackSprite;
                    break;
            }
        }
    }
}
