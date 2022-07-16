using CardGame.Battle.Card_Slots;
using UnityEngine;

namespace CardGame.Battle.Display.Slots
{
    public class UnitDisplay : CardSlotDisplay<UnitSlot>
    {
        [SerializeField] private float exhaustedAngle = 90;
        private CardDisplay _cardDisplay;

        public override void Setup(UnitSlot slot)
        {
            base.Setup(slot);

            _cardDisplay = CardDisplayFactory.Create(transform);

            slot.OnChangeCard.AddListener(ChangeHandler);
            ChangeHandler(slot.Card);
        }

        private void ChangeHandler(Card card)
        {
            if (card == null)
                RemoveCardHandler();
            else
                AddCard(card);
        }

        private void AddCard(Card card)
        {
            _cardDisplay.Show(card);
            _cardDisplay.transform.localRotation = Quaternion.Euler(0, 0, Slot.Exhausted ? exhaustedAngle : 0);
        }

        private void RemoveCardHandler()
        {
            _cardDisplay.Clear();
        }

        public override void Click(BattleContext context)
        {
            if (Slot.CanSet(context))
                Slot.Toggle();
            base.Click(context);
        }
    }
}