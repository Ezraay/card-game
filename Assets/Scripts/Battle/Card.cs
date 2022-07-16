using UnityEngine;

namespace CardGame.Battle
{
    public class Card
    {
        private readonly CardData data;

        public Card(CardData cardData)
        {
            data = cardData;
        }

        public int Power => data.Power;
        public int Guard => data.Guard;
        public int Tier => data.Tier;
        public Sprite Sprite => data.Sprite;
        public string Name => data.name;
    }
}