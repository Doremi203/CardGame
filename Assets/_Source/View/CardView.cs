using Models;
using UnityEngine;

namespace View
{
    public class CardView : MonoBehaviour
    {
        private CardInstance _cardInstance;
        
        public void Init(CardInstance cardInstance)
        {
            _cardInstance = cardInstance;
        }
    }
}