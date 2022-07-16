using System;
using System.Collections.Generic;
using System.Linq;
using CardGame.Battle.Card_Slots;

namespace CardGame.Battle
{
    public class Player
    {
        private readonly List<CardData> _deckPile;
        public ChampionSlot championSlot;
        public UnitSlot championSupport = new UnitSlot();

        public DamageSlot damageSlot = new DamageSlot();
        public GraveyardSlot graveyard = new GraveyardSlot();
        public GuardSlot guardSlot = new GuardSlot();
        public HandSlot handSlot = new HandSlot();
        public UnitSlot leftSupport = new UnitSlot();
        public UnitSlot leftUnit = new UnitSlot();
        public UnitSlot rightSupport = new UnitSlot();
        public UnitSlot rightUnit = new UnitSlot();

        public Player(Deck deck)
        {
            championSlot = new ChampionSlot(new Card(deck.Champion));

            _deckPile = deck.Cards.ToList();
            _deckPile = Shuffle(_deckPile);

            DrawCard();
            DrawCard();
            DrawCard();
        }

        private List<T> Shuffle<T>(List<T> target)
        {
            var rng = new Random();
            var n = target.Count;
            while (n > 1)
            {
                var k = rng.Next(n--);
                (target[n], target[k]) = (target[k], target[n]);
            }

            return target;
        }

        public void StandAll()
        {
            championSlot.Stand();
            championSupport.Stand();
            leftUnit.Stand();
            leftSupport.Stand();
            rightUnit.Stand();
            rightSupport.Stand();
        }

        public Card DrawCard()
        {
            if (_deckPile.Count == 0)
                return null;
            var card = new Card(_deckPile[_deckPile.Count - 1]);
            handSlot.AddCard(null, card);
            _deckPile.RemoveAt(_deckPile.Count - 1);

            return card;
        }
    }
}