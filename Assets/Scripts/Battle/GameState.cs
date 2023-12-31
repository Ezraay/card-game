﻿using UnityEngine.Events;

namespace CardGame.Battle
{
    public class GameState
    {
        public Attack Attack;
        public Phase Phase;
        public Player Player;

        public UnityEvent OnTurnEnd { get; } = new UnityEvent();
        public UnityEvent OnStateChanged { get; } = new UnityEvent();

        public void SwitchTurn(Player player)
        {
            Player = player;
            Attack = new Attack(Player);
        }

        public void AdvancePhase(BattleContext context)
        {
            switch (Phase)
            {
                case Phase.Main:
                    Phase += 1;
                    Attack = new Attack(Player);
                    break;
                case Phase.BattleStart:
                    if (Attack.InvalidAttack())
                        Attack.Undo(context);
                    else if (!Attack.CanAttack())
                        Phase = Phase.End;
                    else
                        Phase += 1;

                    break;
                case Phase.BattleDamage:
                    for (int i = 0; i < Attack.Damage(context); i++)
                    {
                        context.Enemy.TakeDamage();
                    }

                    Phase += 1;
                    AdvancePhase(context);
                    break;
                case Phase.BattleEnd:
                    Phase = Phase.BattleStart;
                    Attack.PostPhase();
                    Attack = new Attack(Player);
                    break;
                case Phase.End:
                    Phase = 0;
                    OnTurnEnd.Invoke();
                    Attack = new Attack(Player);
                    break;
                default:
                    Phase += 1;
                    break;
            }

            OnStateChanged.Invoke();
        }

        public void SetPhase(Phase phase)
        {
            Phase = phase;

            OnStateChanged.Invoke();
        }

        public string Formatted()
        {
            switch (Phase)
            {
                case Phase.Stand:
                    return "Stand Phase";
                case Phase.Draw:
                    return "Draw Phase";
                case Phase.Ascend:
                    return "Ascend Phase";
                case Phase.Main:
                    return "Main Phase";
                case Phase.BattleStart:
                    return "Battle Phase";
                case Phase.BattleGuard:
                    return "Guard Step";
                case Phase.BattleDrive:
                    return "Drive Check";
                case Phase.BattleDamage:
                    return "Damage Check";
                case Phase.BattleEnd:
                    return "Battle End";
                case Phase.End:
                    return "End Phase";
            }

            return "Error!";
        }

        public string Description()
        {
            switch (Phase)
            {
                case Phase.Stand:
                    return "Stand all your units.";
                case Phase.Draw:
                    return "Draw your card for your turn.";
                case Phase.Ascend:
                    return "Choose a card to ascend your champion.";
                case Phase.Main:
                    return "Activate abilities and call supporting units.";
                case Phase.BattleStart:
                    return "The start of battle.";
                case Phase.BattleGuard:
                    return "Choose units to protect your champion.";
                case Phase.BattleDrive:
                    return "Check your attacking special effects";
                case Phase.BattleDamage:
                    return "Check your defending special effects.";
                case Phase.BattleEnd:
                    return "The end of your current attack";
                case Phase.End:
                    return "End of your turn.";
            }

            return "Error!";
        }
    }

    public enum Phase
    {
        Stand,
        Draw,
        Ascend,
        Main,
        BattleStart,
        BattleGuard,
        BattleDrive,
        BattleDamage,
        BattleEnd,
        End
    }
}