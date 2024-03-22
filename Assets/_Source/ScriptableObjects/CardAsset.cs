using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Card", menuName = "Card")]
    public class CardAsset : ScriptableObject
    {
        [field: SerializeField] 
        public string CardName { get; private set; }
        [field: SerializeField]
        public Color Color { get; private set; }
        [field: SerializeField]
        public Sprite Sprite { get; private set; }
    }
}