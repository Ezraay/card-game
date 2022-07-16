using System.Collections.Generic;

namespace CardGame.Battle.Card_Slots
{
    public class ChampionSlot : UnitSlot
    {
        public ChampionSlot(Card card) : base(UnitColumn.Champion, UnitRow.Front)
        {
            Card = card;
        }

        public List<Card> Soul { get; } = new List<Card>();

        public override bool CanAddCard(BattleContext context, Card card)
        {
            return (card.Tier == Card.Tier ||
                    card.Tier == Card.Tier + 1) &&
                   context.HeroTurn() &&
                   context.State.Phase == Phase.Ascend;
        }

        public override void AddCard(BattleContext slotInteractContext, Card card)
        {
            Soul.Add(Card);
            Card = card;

            OnChangeCard.Invoke(Card);
        }
    }
}