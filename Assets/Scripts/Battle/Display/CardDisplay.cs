using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CardGame.Battle.Display
{
    public class CardDisplay : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private GameObject content;
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private Image flatSprite;
        [SerializeField] private Animator animator;
        [SerializeField] private float dragHeight = 3;
        private Vector3 _preDragPosition;
        private Quaternion _preDragRotation;
        public static Vector2 CardSize { get; } = new Vector2(4, 5);
        public readonly UnityEvent<BattleContext> OnClick = new UnityEvent<BattleContext>();
        public Card Card { get; private set; }

        private void Start()
        {
            canvas.worldCamera = Camera.main;
            _preDragPosition = transform.position;
            _preDragRotation = transform.rotation;
        }

        public void SetCanvasOrder(int order)
        {
            canvas.sortingOrder = order;
        }

        public void Show(Card card)
        {
            if (card == null)
            {
                Clear();
                return;
            }

            Card = card;
            content.SetActive(true);

            flatSprite.sprite = card.Sprite;
            titleText.text = card.Name;
        }

        public void Clear()
        {
            Card = null;
            content.SetActive(false);
        }

        public void OnHover()
        {
            animator.SetBool("hovered", true);
        }

        public void OnUnhover()
        {
            animator.SetBool("hovered", false);
        }

        public void StartDragging()
        {
            _preDragPosition = transform.position;
            _preDragRotation = transform.rotation;
        }

        public void Click(BattleContext context)
        {
            OnClick.Invoke(context);
            Debug.Log("Card clicked");
        }

        public void Drag()
        {
            var position = Utility.RaycastMousePoint();
            position.y = dragHeight;
            transform.position = position;
            transform.rotation = Quaternion.LookRotation(Vector3.down);
        }

        public void StopDragging()
        {
            transform.position = _preDragPosition;
            transform.rotation = _preDragRotation;
        }
    }
}