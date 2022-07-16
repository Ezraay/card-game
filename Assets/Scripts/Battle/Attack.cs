using CardGame.Battle.Card_Slots;

namespace CardGame.Battle
{
    public class Attack
    {
        public readonly Player Player;
        public UnitSlot AttackingUnit;
        public int Critical = 1;
        public int DamageChecks;
        public int DriveChecks;
        public int ExtraPower;
        public UnitSlot SupportingUnit;

        public Attack(Player player)
        {
            Player = player;
        }

        public int Power => ExtraPower +
                            (AttackingUnit?.Card?.Power ?? 0) +
                            (SupportingUnit?.Card?.Power ?? 0);

        public bool CanAttack()
        {
            return AttackingUnit != null;
        }

        public bool InvalidAttack()
        {
            return AttackingUnit == null && SupportingUnit != null;
        }

        public bool CanAddUnit(UnitSlot slot)
        {
            if (AttackingUnit != null)
            {
                return slot.Column == AttackingUnit.Column;
            } else if (SupportingUnit != null)
            {
                return slot.Column == SupportingUnit.Column;
            }

            return true;
        }

        public void AddUnit(UnitSlot slot)
        {
            if (slot.Row == UnitRow.Front)
                AttackingUnit = slot;
            else
                SupportingUnit = slot;
        }

        public void RemoveUnit(UnitSlot slot)
        {
            if (slot == AttackingUnit)
                AttackingUnit = null;
            else
                SupportingUnit = null;
        }

        public void PostPhase()
        {
            AttackingUnit.Attacked = true;
            if (SupportingUnit != null)
                SupportingUnit.Attacked = true;
        }

        public void Undo(BattleContext context)
        {
            if (SupportingUnit != null && SupportingUnit.CanChangeStance(context))
            {
                SupportingUnit.StandSlot();
                SupportingUnit = null;
            }

            if (AttackingUnit != null && AttackingUnit.CanChangeStance(context))
            {
                AttackingUnit.StandSlot();
                AttackingUnit = null;
            }
        }

        public int Damage(BattleContext context)
        {
            return context.Enemy.guardSlot.Guard() <= Power ? Critical : 0;
        }
    }
} 