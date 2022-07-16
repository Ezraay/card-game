using System.Collections.Generic;
using UnityEngine.Events;

namespace CardGame.Battle.Card_Slots
{
    public class GraveyardSlot : CardSlot
    {
        public readonly UnityEvent<Card> OnCardAdded = new UnityEvent<Card>();
        public readonly UnityEvent<Card> OnCardRemoved = new UnityEvent<Card>();
        public List<Card> Cards { get; } = new List<Card>();

        public override void AddCard(BattleContext context, Card card)
        {
            Cards.Add(card);
            OnCardAdded.Invoke(card);
        }

        public override void RemoveCard(BattleContext context, Card card)
        {
            Cards.Remove(card);
            OnCardRemoved.Invoke(card);
        }
    }
}