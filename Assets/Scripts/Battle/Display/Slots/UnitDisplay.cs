using CardGame.Battle.Card_Slots;
using UnityEngine;
using UnityEngine.Serialization;

namespace CardGame.Battle.Display.Slots
{
    public class UnitDisplay : CardSlotDisplay<UnitSlot>
    {
        [SerializeField] private float setAngle = 90;
        private CardDisplay _cardDisplay;

        public override void Setup(UnitSlot slot)
        {
            base.Setup(slot);

            _cardDisplay = CardDisplayFactory.Create(transform);
            _cardDisplay.OnClick.AddListener(Click);

            slot.OnChangeCard.AddListener(ChangeHandler);
            slot.OnToggle.AddListener(() =>
            {
                _cardDisplay.transform.localRotation = Quaternion.Euler(0, 0, Slot.Set ? setAngle : 0);
            });
            ChangeHandler(slot.Card);
        }

        private void ChangeHandler(Card card)
        {
            if (card == null)
                RemoveCard();
            else
                AddCard(card);
        }

        private void AddCard(Card card)
        {
            _cardDisplay.Show(card);
        }

        private void RemoveCard()
        {
            _cardDisplay.Clear();
        }

        public override void Click(BattleContext context)
        {
            if (Slot.CanChangeStance(context))
                if (Slot.Set)
                {
                    Slot.StandSlot();
                    context.State.Attack.RemoveUnit(Slot);
                }
                else if (!Slot.Set && context.State.Attack.CanAddUnit(Slot))
                {
                    Slot.SetSlot();
                    context.State.Attack.AddUnit(Slot);
                }

            // _cardDisplay.transform.localRotation = Quaternion.Euler(0, 0, Slot.Set ? setAngle : 0);
            Debug.Log(Slot.Set);
            base.Click(context);
        }
    }
}