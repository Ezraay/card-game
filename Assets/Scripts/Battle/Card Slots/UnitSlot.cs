using UnityEngine.Events;

namespace CardGame.Battle.Card_Slots
{
    public class UnitSlot : CardSlot
    {
        public readonly UnityEvent<Card> OnChangeCard = new UnityEvent<Card>();
        public readonly UnityEvent OnToggle = new UnityEvent();
        public Card Card { get; protected set; }
        public bool Set { get; private set; }
        public readonly UnitColumn Column;
        public readonly UnitRow Row;
        public bool Attacked;

        public UnitSlot(UnitColumn column, UnitRow row)
        {
            Column = column;
            Row = row;
        }

        public override bool CanAddCard(BattleContext context, Card card)
        {
            return card.Tier <= context.Hero.championSlot.Card.Tier &&
                   context.State.Phase == Phase.Main &&
                   context.HeroTurn();
        }

        public override void AddCard(BattleContext context, Card card)
        {
            if (Card != null) context.Hero.graveyard.AddCard(context, Card);

            Card = card;
            OnChangeCard.Invoke(card);
        }

        public bool CanChangeStance(BattleContext context)
        {
            return context.State.Phase == Phase.BattleStart &&
                   context.HeroTurn() && 
                   context.State.Attack.CanAddUnit(this) && 
                   !Attacked;
        }

        public void StandSlot()
        {
            Set = false;
            OnToggle.Invoke();
            Attacked = false;
        }

        public void SetSlot()
        {
            Set = true;
            OnToggle.Invoke();
        }
    }
}