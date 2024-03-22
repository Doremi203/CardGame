using System;
using Models;
using UnityEngine;

namespace Core
{
    public class CardLayout : MonoBehaviour
    {
        [field: SerializeField]
        public int LayoutId { get; set; }
        [field: SerializeField]
        public Vector2 Offset { get; set; }
        [SerializeField] private bool _isInitiallyFaceUp;
        private RectTransform _rectTransform;

        private void Awake()
        {
            _rectTransform = transform as RectTransform;
            if (_rectTransform is null)
                Debug.LogError("CardLayout must be placed on gameobject with a RectTransform.");
        }

        private void Update()
        {
            foreach (var card in CardGame.Instance.GetCardsInLayout(LayoutId))
            {
                PositionCardView(card);
            }
        }

        private void PositionCardView(CardInstance card)
        {
            var cardView = CardGame.Instance.GetCardView(card);
            cardView.RectTransform.SetParent(_rectTransform, false);
            cardView.RectTransform.anchoredPosition = Offset * card.CardPosition;
            cardView.RectTransform.SetSiblingIndex(card.CardPosition);
            cardView.Flip(_isInitiallyFaceUp);
        }
    }
}