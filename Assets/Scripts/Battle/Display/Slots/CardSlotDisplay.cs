using CardGame.Battle.Card_Slots;
using UnityEngine;
using UnityEngine.Events;

namespace CardGame.Battle.Display.Slots
{
    [RequireComponent(typeof(BoxCollider))]
    public abstract class CardSlotDisplay<T> : MonoBehaviour, ICardSlotDisplay where T : CardSlot
    {
        public readonly UnityEvent OnClick = new UnityEvent();
        private Animator _animator;
        protected BoxCollider BoxCollider;
        protected T Slot;

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        public bool CanAddCard(BattleContext slotInteractContext, Card card)
        {
            return Slot.CanAddCard(slotInteractContext, card);
        }

        public void AddCard(BattleContext slotInteractContext, Card card)
        {
            Slot.AddCard(slotInteractContext, card);
        }

        public void RemoveCard(BattleContext slotInteractContext, Card card = null)
        {
            Slot.RemoveCard(slotInteractContext, card);
        }

        public bool CanRemoveCard(BattleContext slotInteractContext, Card card = null)
        {
            return Slot.CanRemoveCard(slotInteractContext, card);
        }

        public void OnHover()
        {
            _animator.SetBool("hovered", true);
        }

        public void OnUnhover()
        {
            _animator.SetBool("hovered", false);
        }

        public virtual void Click(BattleContext context)
        {
            OnClick.Invoke();
            Debug.Log("Slot clicked");
        }

        public virtual void Setup(T slot)
        {
            BoxCollider = GetComponent<BoxCollider>();
            Slot = slot;
        }
    }
}