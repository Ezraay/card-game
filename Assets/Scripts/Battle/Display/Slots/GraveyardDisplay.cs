using CardGame.Battle.Card_Slots;

namespace CardGame.Battle.Display.Slots
{
    public class GraveyardDisplay : CardSlotDisplay<GraveyardSlot>
    {
        private CardDisplay _cardDisplay;

        public override void Setup(GraveyardSlot slot)
        {
            base.Setup(slot);

            _cardDisplay = CardDisplayFactory.Create(transform);
            _cardDisplay.Clear();
            slot.OnCardAdded.AddListener(AddCard);
            slot.OnCardRemoved.AddListener(RemoveCard);
        }

        private void AddCard(Card card)
        {
            _cardDisplay.Show(card);
        }

        private void RemoveCard(Card card)
        {
            if (Slot.Cards.Count == 0)
            {
                _cardDisplay.gameObject.SetActive(false);
            }
            else
            {
                _cardDisplay.gameObject.SetActive(true);
                _cardDisplay.Show(Slot.Cards[Slot.Cards.Count - 1]);
            }
        }
    }
}