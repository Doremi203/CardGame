using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Models;
using ScriptableObjects;
using UnityEngine;
using View;

namespace Core
{
    public class CardGame : MonoBehaviour
    {
        [field: SerializeField] 
        public List<CardAsset> CardAssets { get; private set; }
        [field: SerializeField]
        public List<DeckPart> InitialDeck { get; private set; }
        [field: SerializeField] 
        public int HandSize { get; private set; }
        [field: SerializeField]
        public int DeckLayoutId { get; private set; }
        [field: SerializeField]
        public int DiscardLayoutId { get; private set; }
        [field: SerializeField]
        public int FieldLayoutId { get; private set; }
        [SerializeField] private GameObject _cardViewPrefab;
        [SerializeField] private List<int> _playerLayouts = new();

        private static CardGame _instance;
        private readonly Dictionary<CardInstance, CardView> _cardViews = new();

        public static CardGame Instance
        {
            get => _instance;
            private set
            {
                if (_instance != null)
                {
                    Debug.LogError("There should never be more than one CardGame object.");
                }
                else
                {
                    _instance = value;
                }
            }
        }
        
        public CardView GetCardView(CardInstance card)
        {
            return _cardViews[card];
        }

        public List<CardInstance> GetCardsInLayout(int layoutId)
        {
            var cards = new List<CardInstance>();
            foreach (var kvp in _cardViews)
            {
                if (kvp.Key.LayoutNumber == layoutId)
                {
                    cards.Add(kvp.Key);
                }
            }

            return cards;
        }

        public void RecalculateLayout(int layoutId)
        {
            var cards = GetCardsInLayout(layoutId);
            for (int i = 0; i < cards.Count; i++)
            {
                cards[i].CardPosition = i;
            }
        }

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            StartGame();
        }

        private void StartGame()
        {
            foreach (var layoutId in _playerLayouts)
            {
                foreach (var cardAsset in CardAssets)
                {
                    CreateCard(cardAsset, layoutId);
                }
            }
            InitDeck();
        }

        private void InitDeck()
        {
            foreach (var deckPart in InitialDeck)
            {
                for (int i = 0; i < deckPart.CardCount; i++)
                {
                    CreateCard(deckPart.CardAsset, DeckLayoutId);
                }
            }
        }
        
        public void ShuffleDeck()
        {
            var deck = GetCardsInLayout(DeckLayoutId);
            
            for (int i = 0; i < deck.Count; i++)
            {
                var temp = deck[i].CardPosition;
                var randomIndex = UnityEngine.Random.Range(i, deck.Count);
                deck[i].CardPosition = deck[randomIndex].CardPosition;
                deck[randomIndex].CardPosition = temp;
            }
        }

        private void CreateCard(CardAsset cardAsset, int layoutId)
        {
            var cardInstance = new CardInstance(cardAsset);
            CreateCardView(cardInstance);
            cardInstance.MoveToLayout(layoutId);
        }

        private void CreateCardView(CardInstance cardInstance)
        {
            var cardView = Instantiate(_cardViewPrefab, transform).GetComponent<CardView>();
            cardView.Init(cardInstance);
            _cardViews.Add(cardInstance, cardView);
        }
        
        public void StartTurn()
        {
            foreach (var layoutId in _playerLayouts)
            {
                DrawCards(layoutId);
            }
        }

        private void DrawCards(int playerLayoutId)
        {
            var playerCards = GetCardsInLayout(playerLayoutId); 
            for (int i = playerCards.Count; i < HandSize; i++)
            {
                var deck = GetCardsInLayout(DeckLayoutId);
                if (deck.Count == 0)
                    return;
                deck[^1].MoveToLayout(playerLayoutId);
            }
        }
    }

    [Serializable]
    public class DeckPart
    {
        [field: SerializeField]
        public CardAsset CardAsset { get; set; }
        [field: SerializeField]
        public int CardCount { get; set; }
    }
}