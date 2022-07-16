using UnityEngine;

namespace CardGame.Battle
{
    [CreateAssetMenu(menuName = "Create Card", fileName = "New Card", order = 0)]
    public class CardData : ScriptableObject
    {
        [field: SerializeField] public string ID { get; } = "new_card";
        [field: SerializeField] public Sprite Sprite { get; private set; }
        [field: SerializeField] public int Power { get; private set; }
        [field: SerializeField] public int Guard { get; private set; }
        [field: SerializeField] public int Tier { get; private set; } = 1;
    }
}