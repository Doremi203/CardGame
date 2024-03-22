using Core;
using ScriptableObjects;

namespace Models
{
    public class CardInstance
    {
        public CardAsset CardAsset { get; private set; }
        public int LayoutNumber { get; private set; } = -1;
        public int CardPosition { get; set; }

        public CardInstance(CardAsset cardAsset)
        {
            CardAsset = cardAsset;
        }

        public void MoveToLayout(int layoutId)
        {
            var previousLayout = LayoutNumber;
            LayoutNumber = layoutId;
            CardGame.Instance.RecalculateLayout(LayoutNumber);
            CardGame.Instance.RecalculateLayout(previousLayout);
        }
    }
}