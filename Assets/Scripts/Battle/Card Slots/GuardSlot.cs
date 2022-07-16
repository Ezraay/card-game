using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;

namespace CardGame.Battle.Card_Slots
{
    public class GuardSlot : CardSlot
    {
        public readonly UnityEvent<Card> OnGuardAdded = new UnityEvent<Card>();
        public readonly UnityEvent<Card> OnGuardRemoved = new UnityEvent<Card>();
        public List<Card> Cards { get; } = new List<Card>();

        public override bool CanAddCard(BattleContext context, Card card)
        {
            return !context.HeroTurn() && // If not our turn
                   context.State.Phase == Phase.BattleGuard &&
                   card.Tier <= context.Hero.championSlot.Card.Tier; // And our turn to guard
        }

        public override void AddCard(BattleContext context, Card card)
        {
            Cards.Add(card);
            OnGuardAdded.Invoke(card);
        }

        public override bool CanRemoveCard(BattleContext context, Card card)
        {
            return !context.HeroTurn() && // If not our turn
                   context.State.Phase == Phase.BattleGuard; // And our turn to guard
        }

        public override void RemoveCard(BattleContext context, Card card)
        {
            Cards.Remove(card);
            OnGuardRemoved.Invoke(card);
        }

        public int Guard()
        {
            return Cards.Sum(card => card.Guard);
        }
    }
}