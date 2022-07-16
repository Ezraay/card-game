using System.Collections.Generic;
using CardGame.Battle.Card_Slots;
using UnityEngine;

namespace CardGame.Battle.Display.Slots
{
    public class DamageDisplay : CardSlotDisplay<DamageSlot>
    {
        [SerializeField] private Transform content;
        [SerializeField] private float verticalOffset = 1.5f;
        [SerializeField] private float heightOffset = -0.1f;
        private readonly List<CardDisplay> _cardDisplays = new List<CardDisplay>();

        public override void Setup(DamageSlot slot)
        {
            base.Setup(slot);

            slot.OnDamageAdded.AddListener(AddDamage);
            slot.OnDamageRemoved.AddListener(RemoveDamage);

            foreach (var damage in slot.Cards) AddDamage(damage.card);
        }

        private void AddDamage(Card card)
        {
            var newDisplay = CardDisplayFactory.Create(content);
            newDisplay.Show(card);
            _cardDisplays.Add(newDisplay);
            UpdatePositions();
        }

        private void RemoveDamage(Card card)
        {
            for (var i = 0; i < _cardDisplays.Count; i++)
            {
                var cardDisplay = _cardDisplays[i];
                if (cardDisplay.Card == card)
                {
                    Destroy(cardDisplay.gameObject);
                    _cardDisplays.RemoveAt(i);
                    UpdatePositions();
                    break;
                }
            }
        }

        private void UpdatePositions()
        {
            var size = BoxCollider.size;
            size.x = CardDisplay.CardSize.x * _cardDisplays.Count;
            BoxCollider.size = size;

            var offset = (_cardDisplays.Count - 1) * verticalOffset / 2;
            for (var i = 0; i < _cardDisplays.Count; i++)
            {
                var display = _cardDisplays[i];
                display.transform.localPosition = new Vector3(0, verticalOffset * i - offset, i * heightOffset);
                display.transform.localRotation = Quaternion.Euler(0, 0, 90);
                display.SetCanvasOrder(2 + i);
            }
        }
    }
}