using System.Collections.Generic;
using CardGame.Battle.Card_Slots;
using UnityEngine;

namespace CardGame.Battle.Display.Slots
{
    public class GuardDisplay : CardSlotDisplay<GuardSlot>
    {
        [SerializeField] private Transform content;
        [SerializeField] private float horizontalOffset = 2;
        [SerializeField] private float heightOffset = -0.1f;
        private readonly List<CardDisplay> cards = new List<CardDisplay>();

        public override void Setup(GuardSlot slot)
        {
            base.Setup(slot);

            slot.OnGuardRemoved.AddListener(AddCard);
            slot.OnGuardRemoved.AddListener(RemoveCard);

            foreach (var card in slot.Cards) AddCard(card);
        }

        private void AddCard(Card card)
        {
            var newDisplay = CardDisplayFactory.Create(content);
            newDisplay.Show(card);
            cards.Add(newDisplay);
            UpdatePositions();
        }

        private void RemoveCard(Card card)
        {
            for (var i = 0; i < cards.Count; i++)
            {
                var cardDisplay = cards[i];
                if (cardDisplay.Card == card)
                {
                    Destroy(cardDisplay.gameObject);
                    cards.RemoveAt(i);
                    UpdatePositions();
                    break;
                }
            }
        }

        private void UpdatePositions()
        {
            var size = BoxCollider.size;
            size.x = CardDisplay.CardSize.x * Slot.Cards.Count;
            BoxCollider.size = size;

            var offset = (cards.Count - 1) * horizontalOffset / 2;
            for (var i = 0; i < cards.Count; i++)
            {
                var display = cards[i];
                display.transform.localPosition = new Vector3(0, horizontalOffset * i - offset, i * heightOffset);
            }
        }
    }
}