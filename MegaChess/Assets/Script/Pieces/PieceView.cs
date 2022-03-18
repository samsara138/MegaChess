using Board;
using Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Pieces
{
    public class PieceView : View
    {
        [SerializeField] private Image image;
        [SerializeField] private Button clickDetector;
        [SerializeField] private AnimationCurve movementCurve;

        public Button ClickDetector => clickDetector;

        public void Configure(PieceModel model, PlayerSide side)
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
            gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(GlobalParameters.TILE_LENGTH, GlobalParameters.TILE_LENGTH);
        }

        public void OnKill()
        {
            Destroy(gameObject);
        }

        public void MoveToPosition(Transform tileTransform)
        {
            transform.SetParent(transform.parent.parent.parent, true);
            transform.SetAsLastSibling();

            Vector3 diff = tileTransform.position - transform.position;

            IEnumerator corountine = Transition(diff, tileTransform);
            StartCoroutine(corountine);
        }

        private IEnumerator Transition(Vector3 moveVector, Transform tileTransform)
        {
            float[] steps = CreateAnimationSteps(movementCurve, GlobalParameters.PIECE_MOVE_STEPS);
            for (short i = 0; i < GlobalParameters.PIECE_MOVE_STEPS; i++)
            {
                Debug.Log(steps[i]);
                transform.Translate(moveVector * steps[i]);
                yield return new WaitForEndOfFrame();
            }
            transform.SetParent(tileTransform, true);
        }

        private float[] CreateAnimationSteps(AnimationCurve curve, int steps)
        {
            float[] samples = new float[steps];
            float stepSize = (curve.keys[curve.keys.Length - 1].time) / steps;
            float sum = 0;
            float currStep = 0;

            for (short i = 0; i < steps; i++)
            {
                samples[i] = curve.Evaluate(currStep);
                sum += samples[i];
                currStep += stepSize;
            }
            for (short i = 0; i < steps; i++)
            {
                samples[i] /= sum;
            }
            return samples;
        }
    }
}
