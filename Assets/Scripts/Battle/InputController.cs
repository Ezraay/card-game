using CardGame.Battle;
using CardGame.Battle.Display;
using CardGame.Battle.Display.Slots;
using UnityEngine;

namespace CardGame
{
    public class InputController : MonoBehaviour
    {
        [SerializeField] private LayerMask cardDisplayMask;
        [SerializeField] private LayerMask cardSlotMask;
        [SerializeField] private BattleManager battleManager;
        [SerializeField] private BattleDisplay battleDisplay;
        [SerializeField] private float clickThreshold = 0.1f;
        private Vector3? _clickPosition;
        private bool _dragging;
        private CardDisplay _hoveredCard;
        private ICardSlotDisplay _hoveredCardSlot;

        private void Update()
        {
            var context = battleManager.GetContext(true);
            var currentCard = RaycastCardDisplay();
            var currentSlot = RaycastCardSlot();

            if (currentCard != _hoveredCard && !_dragging)
            {
                if (_hoveredCard != null)
                    _hoveredCard.OnUnhover();
                if (currentCard != null)
                {
                    currentCard.OnHover();
                    if (currentCard.Card != null)
                        battleDisplay.ShowCardInfo(currentCard.Card);
                }
                else
                {
                    battleDisplay.HideCardInfo();
                }

                _hoveredCard = currentCard;
            }

            // Start drag
            if (Input.GetMouseButtonDown(0) && currentSlot != null)
            {
                _clickPosition = Utility.RaycastMousePoint();
                _hoveredCardSlot = currentSlot;
            }

            // During mouse held
            if (_clickPosition != null)
            {
                if (_dragging)
                {
                    _hoveredCard.Drag();
                }
                else
                {
                    if (Vector3.Distance(Utility.RaycastMousePoint(), _clickPosition.Value) >= clickThreshold)
                    {
                        if (_hoveredCardSlot.CanRemoveCard(context))
                        {
                            // Start dragging
                            _dragging = true;
                            _hoveredCard.StartDragging();
                        }
                        else
                        {
                            _clickPosition = null;
                        }
                    }
                }
            }

            // End drag
            if (Input.GetMouseButtonUp(0) && _clickPosition != null)
            {
                if (!_dragging)
                {
                    // Click
                    if (_hoveredCard?.Card != null)
                        _hoveredCard.Click(context);
                    else
                        _hoveredCardSlot.Click(context);
                }
                else
                {
                    // Try perform drag action
                    _hoveredCard.StopDragging();
                    if (currentSlot != null && _hoveredCardSlot != currentSlot &&
                        currentSlot.CanAddCard(context, _hoveredCard.Card))
                    {
                        currentSlot.AddCard(context, _hoveredCard.Card);
                        _hoveredCardSlot.RemoveCard(context, _hoveredCard.Card);
                    }
                }

                _clickPosition = null;
                _dragging = false;
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Gizmos.DrawRay(ray.origin, ray.direction * 1000);
        }

        private CardDisplay RaycastCardDisplay()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit, 1000, cardDisplayMask))
            {
                var display = hit.collider.GetComponent<CardDisplay>();
                if (display != null)
                    return display;
            }

            return null;
        }

        private ICardSlotDisplay RaycastCardSlot()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit, 1000, cardSlotMask))
            {
                var slot = hit.collider.GetComponent<ICardSlotDisplay>();
                if (slot != null)
                    return slot;
            }

            return null;
        }
    }
}