using System;
using CardGame.Battle.Display;
using UnityEngine;

namespace CardGame.Battle
{
    public class BattleManager : MonoBehaviour
    {
        [SerializeField] private StructuredDeck heroDeck;
        [SerializeField] private StructuredDeck enemyDeck;
        [SerializeField] private BoardDisplay heroBoard;
        [SerializeField] private BoardDisplay enemyBoard;
        [SerializeField] private BattleDisplay stateDisplay;
        [SerializeField] private CardData testingCard;
        private bool heroTurn = true;

        private Player Enemy { get; set; }
        public GameState GameState { get; private set; }
        public Player Hero { get; private set; }

        private void Start()
        {
            StartBattle(heroDeck.ConvertToDeck(), enemyDeck.ConvertToDeck());
        }

        public void StartBattle(Deck heroDeck, Deck enemyDeck)
        {
            Hero = new Player(heroDeck);
            Enemy = new Player(enemyDeck);


            GameState = new GameState();
            GameState.SwitchTurn(Hero);
            GameState.OnTurnEnd.AddListener(() =>
            {
                heroTurn = !heroTurn;
                GameState.SwitchTurn(heroTurn ? Hero : Enemy);
            });
            GameState.OnStateChanged.AddListener(() =>
            {
                switch (GameState.Phase)
                {
                    case Phase.Draw:
                        GameState.Player.DrawCard();
                        GameState.AdvancePhase();
                        break;
                    case Phase.Stand:
                        GameState.Player.StandAll();
                        GameState.AdvancePhase();
                        break;
                }
            });

            stateDisplay.Setup(this);

            heroBoard.Setup(Hero);
            enemyBoard.Setup(Enemy);

            Hero.championSlot.OnChangeCard.AddListener(card => GameState.AdvancePhase());
            Enemy.championSlot.OnChangeCard.AddListener(card => GameState.AdvancePhase());
        }

        private void Update()
        {
            // Testing purposes
            Debug.Log(Hero.handSlot.Cards.Count);
        }

        private Player GetPlayer(bool hero)
        {
            return hero ? Hero : Enemy;
        }

        public BattleContext GetContext(bool hero)
        {
            return new BattleContext(GameState, GetPlayer(hero), GetPlayer(!hero));
        }
    }
}