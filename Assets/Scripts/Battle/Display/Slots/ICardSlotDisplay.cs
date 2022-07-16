namespace CardGame.Battle.Display.Slots
{
    public interface ICardSlotDisplay
    {
        public bool CanAddCard(BattleContext slotInteractContext, Card card);
        public void AddCard(BattleContext slotInteractContext, Card card);
        public void RemoveCard(BattleContext slotInteractContext, Card card = null);
        public bool CanRemoveCard(BattleContext slotInteractContext, Card card = null);
        public void OnHover();
        public void OnUnhover();
        public void Click(BattleContext context);
    }
}