namespace CardGame.Battle
{
    public class Attack
    {
        public readonly Player Player;
        public Card AttackingUnit;
        public int Critical = 1;
        public int DamageChecks;
        public int DriveChecks;
        public int ExtraPower;
        public Card SupportingUnit;

        public Attack(Player player)
        {
            Player = player;
        }

        public int Power => ExtraPower +
                            (AttackingUnit?.Power ?? 0) +
                            (SupportingUnit?.Power ?? 0);
    }
} 