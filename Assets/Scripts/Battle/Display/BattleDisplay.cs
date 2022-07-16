using UnityEngine;

namespace CardGame.Battle.Display
{
    public class BattleDisplay : MonoBehaviour
    {
        [SerializeField] private StateDisplay stateDisplay;
        [SerializeField] private GameObject skipAttacksButton;
        [SerializeField] private CardInfoDisplay cardInfoDisplay;
        private BattleManager battleManager;

        public void Setup(BattleManager battleManager)
        {
            this.battleManager = battleManager;

            stateDisplay.Setup(battleManager.GameState, battleManager.Hero);

            battleManager.GameState.OnStateChanged.AddListener(() =>
            {
                skipAttacksButton.SetActive(battleManager.GameState.Phase == Phase.BattleStart);
            });
        }

        public void AdvancePhase()
        {
            battleManager.GameState.AdvancePhase();
        }

        public void SkipAttacks()
        {
            battleManager.GameState.SetPhase(Phase.End);
        }

        public void ShowCardInfo(Card card)
        {
            cardInfoDisplay.Show(card);
        }

        public void HideCardInfo()
        {
            cardInfoDisplay.Hide();
        }
    }
}