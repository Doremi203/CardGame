using System;
using Core;
using Models;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace View
{
    public class CardView : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private Image _image;
        [SerializeField] private GameObject _cardFront;
        [SerializeField] private GameObject _cardBack;
        [SerializeField] private Outline _outline;
        [SerializeField] private TextMeshProUGUI _cardName;
        private bool _isFaceUp;
        public RectTransform RectTransform { get; private set; }
        private CardInstance _cardInstance;

        private void Awake()
        {
            RectTransform = transform as RectTransform;
            if (RectTransform is null)
                Debug.LogError("CardView must be placed on gameobject with a RectTransform.");
        }

        private void PlayCard()
        {
            _cardInstance.MoveToLayout(CardGame.Instance.FieldLayoutId);
        }

        public void Init(CardInstance cardInstance)
        {
            _cardInstance = cardInstance;
            SetCardSprite();
            SetCardOutlineColor();
            SetCardName();
        }
        
        public void Flip(bool isFaceUp)
        {
            _isFaceUp = isFaceUp;
            _cardFront.SetActive(_isFaceUp);
            _cardBack.SetActive(!_isFaceUp);
        }

        private void SetCardName()
        {
            gameObject.name = _cardInstance.CardAsset.name;
            _cardName.text = _cardInstance.CardAsset.name;
        }

        private void SetCardOutlineColor()
        {
            _outline.effectColor = _cardInstance.CardAsset.Color;
        }

        private void SetCardSprite()
        {
            var sprite = _cardInstance.CardAsset.Sprite;
            if (sprite is null)
            {
                Debug.LogError("CardAsset must have a sprite.");
                return;
            }

            _image.sprite = sprite;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log($"Clicked on card: {_cardInstance.CardAsset.name}");
            PlayCard();
        }
    }
}