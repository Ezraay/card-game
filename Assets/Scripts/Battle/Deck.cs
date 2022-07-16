namespace CardGame.Battle
{
    public class Deck
    {
        public readonly CardData[] Cards;
        public readonly CardData Champion;

        public Deck(CardData[] cards, CardData champion)
        {
            Cards = cards;
            Champion = champion;
        }
    }
}