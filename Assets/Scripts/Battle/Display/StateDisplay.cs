using System.Collections;
using System.Collections.Generic;
using CardGame.Battle;
using TMPro;
using UnityEngine;

namespace CardGame
{
    public class StateDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text stateText;
        [SerializeField] private TMP_Text descriptionText;
        [SerializeField] private TMP_Text turnText;
        
        public void Setup(GameState state, Player hero)
        {
            state.OnStateChanged.AddListener(() =>
            {
                UpdateText(state);
                turnText.text = state.Player == hero ? "Your turn" : "Enemy's turn";
            });
        }

        private void UpdateText(GameState state)
        {
            stateText.text = state.Formatted();
            descriptionText.text = state.Description();
        }
    }
}
