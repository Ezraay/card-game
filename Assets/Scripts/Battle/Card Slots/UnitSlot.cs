using UnityEngine.Events;

namespace CardGame.Battle.Card_Slots
{
    public class UnitSlot : CardSlot
    {
        public readonly UnityEvent<Card> OnChangeCard = new UnityEvent<Card>();
        public readonly UnityEvent OnToggle = new UnityEvent();
        public Card Card { get; protected set; }
        public bool Exhausted { get; private set; }

        public override bool CanAddCard(BattleContext context, Card card)
        {
            return card.Tier <= context.Hero.championSlot.Card.Tier &&
                   context.State.Phase == Phase.Main &&
                   context.HeroTurn();
        }

        public override void AddCard(BattleContext context, Card card)
        {
            if (Card != null) context.Hero.graveyard.AddCard(context, card);

            Card = card;
            OnChangeCard.Invoke(card);
        }

        public bool CanSet(BattleContext context)
        {
            return context.State.Phase == Phase.BattleStart &&
                   context.HeroTurn();
        }

        public void Stand()
        {
            Exhausted = false;
        }

        public void Toggle()
        {
            Exhausted = !Exhausted;
            OnToggle.Invoke();
        }
    }
}