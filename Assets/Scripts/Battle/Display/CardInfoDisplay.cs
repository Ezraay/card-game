using System.Collections;
using System.Collections.Generic;
using CardGame.Battle;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CardGame
{
    public class CardInfoDisplay : MonoBehaviour
    {
        [SerializeField] private GameObject content;
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private TMP_Text powerText;
        [SerializeField] private TMP_Text guardText;
        [SerializeField] private TMP_Text tierText;
        [SerializeField] private Image spriteImage;
        
        public void Show(Card card)
        {
            content.SetActive(true);
            titleText.text = card.Name;
            powerText.text = card.Power.ToString();
            guardText.text = card.Guard.ToString();
            tierText.text = $"Tier {Utility.GetRomanNumerals(card.Tier)}";
            spriteImage.sprite = card.Sprite;
        }

        public void Hide()
        {
            content.SetActive(false);
        }
    }
}
