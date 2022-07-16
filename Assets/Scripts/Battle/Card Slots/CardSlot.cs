namespace CardGame.Battle.Card_Slots
{
    public abstract class CardSlot
    {
        public virtual bool CanAddCard(BattleContext context, Card card)
        {
            return false;
        }

        public virtual void AddCard(BattleContext context, Card card)
        {
        }

        public virtual bool CanRemoveCard(BattleContext context, Card card)
        {
            return false;
        }

        public virtual void RemoveCard(BattleContext context, Card card)
        {
        }
    }
}