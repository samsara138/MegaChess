using Board;
using Core;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Pieces
{
    public class PieceView : View
    {
        [SerializeField] private Image image;
        [SerializeField] private Button clickDetector;
        public Button ClickDetector => clickDetector;

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

        public void OnKill()
        {
            Destroy(gameObject);
        }

        internal void MoveToPosition(Transform tileTransform)
        {
            transform.parent = tileTransform;
            transform.localPosition = Vector3.zero;
        }
    }
}
