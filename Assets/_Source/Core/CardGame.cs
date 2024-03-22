using System.Collections;
using System.Collections.Generic;
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
        [SerializeField]
        private GameObject _cardViewPrefab;
        
        private static CardGame _instance;
        private readonly List<Player> _players = new();
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
        
        public void AddPlayer(Player player)
        {
            _players.Add(player);
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
            foreach (var player in _players)
            {
                foreach (var cardAsset in CardAssets)
                {
                    var cardInstance = CreateCard(cardAsset, player.Layout.LayoutId);
                    player.Cards.Add(cardInstance);
                }
            }
        }

        private CardInstance CreateCard(CardAsset cardAsset, int layoutId)
        {
            var cardInstance = new CardInstance(cardAsset);
            CreateCardView(cardInstance);
            cardInstance.MoveToLayout(layoutId);
            return cardInstance;
        }

        private void CreateCardView(CardInstance cardInstance)
        {
            var cardView = Instantiate(_cardViewPrefab, transform).GetComponent<CardView>();
            cardView.Init(cardInstance);
            _cardViews.Add(cardInstance, cardView);
        }

        public CardView GetCardView(CardInstance card)
        {
            return _cardViews[card];
        }
    }
}