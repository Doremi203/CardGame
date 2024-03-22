using Core;
using ScriptableObjects;

namespace Models
{
    public class CardInstance
    {
        public CardAsset CardAsset { get; private set; }
        public int LayoutNumber { get; private set; } = -1;
        public int CardPosition { get; private set; }
        
        public CardInstance(CardAsset cardAsset)
        {
            CardAsset = cardAsset;
        }

        public void MoveToLayout(int layoutId)
        {
            var lastPos = CardGame.Instance.GetCardsInLayout(layoutId).Count;
            LayoutNumber = layoutId;
            CardPosition = lastPos;
        }
    }
}