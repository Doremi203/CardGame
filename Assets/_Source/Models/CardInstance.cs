using ScriptableObjects;

namespace Models
{
    public class CardInstance
    {
        public CardAsset CardAsset { get; private set; }
        
        public CardInstance(CardAsset cardAsset)
        {
            CardAsset = cardAsset;
        }
    }
}