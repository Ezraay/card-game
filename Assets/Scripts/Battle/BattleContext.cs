namespace CardGame.Battle
{
    public class BattleContext
    {
        public readonly Player Enemy;
        public readonly Player Hero;
        public readonly GameState State;

        public BattleContext(GameState state, Player hero, Player enemy)
        {
            State = state;
            Hero = hero;
            Enemy = enemy;
        }

        public bool HeroTurn()
        {
            return Hero == State.Player;
        }
    }
}