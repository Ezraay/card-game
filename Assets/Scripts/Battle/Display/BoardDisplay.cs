using CardGame.Battle.Display.Slots;
using UnityEngine;

namespace CardGame.Battle.Display
{
    public class BoardDisplay : MonoBehaviour
    {
        [SerializeField] private UnitDisplay championSlot;

        [SerializeField] private UnitDisplay leftUnit;
        [SerializeField] private UnitDisplay leftSupport;
        [SerializeField] private UnitDisplay rightUnit;
        [SerializeField] private UnitDisplay rightSupport;
        [SerializeField] private UnitDisplay championSupport;
        [SerializeField] private HandDisplay handDisplay;
        [SerializeField] private GraveyardDisplay graveyardDisplay;
        [SerializeField] private DamageDisplay damageDisplay;
        [SerializeField] private GuardDisplay guardSlot;

        public Player Player { get; private set; }

        public void Setup(Player player)
        {
            Player = player;

            championSlot.Setup(player.championSlot);
            championSupport.Setup(player.championSupport);
            leftUnit.Setup(player.leftUnit);
            leftSupport.Setup(player.leftSupport);
            rightUnit.Setup(player.rightUnit);
            rightSupport.Setup(player.rightSupport);

            handDisplay?.Setup(player.handSlot);
            graveyardDisplay.Setup(player.graveyard);
            damageDisplay.Setup(player.damageSlot);
            guardSlot.Setup(player.guardSlot);
        }
    }
}