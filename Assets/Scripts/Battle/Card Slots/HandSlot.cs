using System.Collections.Generic;
using UnityEngine.Events;

namespace CardGame.Battle.Card_Slots
{
    public class HandSlot : CardSlot
    {
        public readonly UnityEvent<Card> OnCardAdded = new UnityEvent<Card>();
        public readonly UnityEvent<Card> OnCardRemoved = new UnityEvent<Card>();
        public List<Card> Cards { get; } = new List<Card>();

        public override bool CanAddCard(BattleContext context, Card card)
        {
            return !context.HeroTurn() &&
                   context.State.Phase == Phase.BattleGuard; // Allows removal of guards from guard zone
        }

        public override void AddCard(BattleContext context, Card card)
        {
            Cards.Add(card);
            OnCardAdded.Invoke(card);
        }

        public override void RemoveCard(BattleContext context, Card card)
        {
            if (!Cards.Contains(card)) return;

            Cards.Remove(card);
            OnCardRemoved.Invoke(card);
        }

        public override bool CanRemoveCard(BattleContext context, Card card)
        {
            return context.HeroTurn() &&
                   (context.State.Phase == Phase.Ascend ||
                    context.State.Phase == Phase.Main)
                   || !context.HeroTurn() && context.State.Phase == Phase.BattleGuard;
        }
    }
}