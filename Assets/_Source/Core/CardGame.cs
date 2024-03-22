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

        private void Awake()
        {
            Instance = this;
        }
        
        private void StartGame()
        {
            foreach (var player in _players)
            {
                foreach (var cardAsset in CardAssets)
                {
                    var cardInstance = new CardInstance(cardAsset); 
                    CreateCardView(cardInstance);
                    player.Cards.Add(cardInstance);
                }
            }
        }

        private void CreateCardView(CardInstance cardInstance)
        {
            var cardView = Instantiate(_cardViewPrefab, transform).GetComponent<CardView>();
            cardView.Init(cardInstance);
            _cardViews.Add(cardInstance, cardView);
        }

        public void AddPlayer(Player player)
        {
            _players.Add(player);
        }
    }
}