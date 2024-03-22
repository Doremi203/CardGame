using ScriptableObjects;

namespace Models
{
    public class CardInstance
    {
        public CardAsset CardAsset { get; private set; }
        public int LayoutNumber { get; private set; }
        public int CardPosition { get; private set; }
        
        public CardInstance(CardAsset cardAsset)
        {
            CardAsset = cardAsset;
        }

        public void MoveToLayout(int layoutNumber)
        {
            LayoutNumber = layoutNumber;
            CardPosition = 0;
        }
    }
}