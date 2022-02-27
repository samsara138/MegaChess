using Core;
using UnityEngine;
using UnityEngine.UI;

namespace Pieces
{
    public class PieceView : View
    {
        [SerializeField] Image image;

        public void Configure(PieceModel model)
        {
            image.sprite = model.Sprite;
        }
    }
}
