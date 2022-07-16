using System.Collections.Generic;
using CardGame.Battle.Card_Slots;
using UnityEngine;

namespace CardGame.Battle.Display.Slots
{
    public class HandDisplay : CardSlotDisplay<HandSlot>
    {
        [SerializeField] private Transform content;
        [SerializeField] private float horizontalOffset = 4;
        private readonly List<CardDisplay> cards = new List<CardDisplay>();

        public override void Setup(HandSlot slot)
        {
            base.Setup(slot);

            slot.OnCardAdded.AddListener(AddCard);
            slot.OnCardRemoved.AddListener(RemoveCard);

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
            size.x = CardDisplay.CardSize.x * cards.Count;
            BoxCollider.size = size;

            var offset = (cards.Count - 1) * horizontalOffset / 2;
            for (var i = 0; i < cards.Count; i++)
            {
                var display = cards[i];
                display.transform.localPosition = new Vector3(horizontalOffset * i - offset, 0, 0);
            }
        }
    }
}