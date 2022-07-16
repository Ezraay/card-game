using System;
using System.Collections.Generic;
using UnityEngine;

namespace CardGame.Battle
{
    [CreateAssetMenu(menuName = "Create Structured Deck", fileName = "New Structured Deck", order = 0)]
    public class StructuredDeck : ScriptableObject
    {
        [field: SerializeField] public CardData Champion { get; private set; }
        [field: SerializeField] public DeckItem[] DeckItems { get; private set; }

        public Deck ConvertToDeck()
        {
            var cards = new List<CardData>();
            foreach (var deckItem in DeckItems)
                for (var i = 0; i < deckItem.count; i++)
                    cards.Add(deckItem.cardData);

            var deck = new Deck(cards.ToArray(), Champion);
            return deck;
        }

        [Serializable]
        public struct DeckItem
        {
            public int count;
            public CardData cardData;
        }
    }
}