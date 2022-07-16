using UnityEngine;

namespace CardGame.Battle.Display
{
    public class BattleDisplay : MonoBehaviour
    {
        [SerializeField] private StateDisplay stateDisplay;
        [SerializeField] private CardInfoDisplay cardInfoDisplay;
        private BattleManager _battleManager;

        public void Setup(BattleManager battleManager)
        {
            _battleManager = battleManager;

            stateDisplay.Setup(battleManager.GameState, battleManager.Hero);
        }

        public void AdvancePhase()
        {
            _battleManager.GameState.AdvancePhase(_battleManager.GetContext(true));
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