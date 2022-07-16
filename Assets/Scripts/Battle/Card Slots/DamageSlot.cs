using System.Collections.Generic;
using UnityEngine.Events;

namespace CardGame.Battle.Card_Slots
{
    public class DamageSlot : CardSlot
    {
        private const int MaxDamage = 6;
        public readonly List<Damage> Cards = new List<Damage>();
        public readonly UnityEvent<Card> OnDamageAdded = new UnityEvent<Card>();
        public readonly UnityEvent<Card> OnDamageRemoved = new UnityEvent<Card>();

        public void AddCard(Card card)
        {
            Cards.Add(new Damage(card));
            OnDamageAdded.Invoke(card);
        }

        public void RemoveCard(Card card)
        {
            foreach (var damage in Cards)
                if (damage.card == card)
                {
                    Cards.Remove(damage);
                    OnDamageRemoved.Invoke(card);
                }
        }

        public class Damage
        {
            public Card card;

            public Damage(Card card)
            {
                this.card = card;
            }
        }
    }
}