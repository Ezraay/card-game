using System;
using UnityEngine;

namespace CardGame.Battle.Display
{
    public class CardDisplayFactory : MonoBehaviour
    {
        [SerializeField] private CardDisplay cardDisplay;

        private static CardDisplayFactory instance;

        private void Awake()
        {
            instance = this;
        }

        public static CardDisplay Create(Transform parent)
        {
            CardDisplay newCardDisplay = Instantiate(instance.cardDisplay, parent);
            return newCardDisplay;
        }
    }
}