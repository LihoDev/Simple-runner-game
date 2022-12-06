using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Player
{
    public class SwipeDetector : MonoBehaviour, IDragHandler, IEndDragHandler
    {
        [SerializeField] private SideMovement _sideMovement;
        [SerializeField] private JumpMovement _jumpMovement;

        private enum DraggedDirection
        {
            Up,
            Down,
            Right,
            Left
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (_sideMovement == null)
            {
                Debug.LogError("EvasionsMovement missing!");
                return;
            }
            if (_jumpMovement == null)
            {
                Debug.LogError("JumpMovement missing!");
                return;
            }
            Vector3 dragVectorDirection = (eventData.position - eventData.pressPosition).normalized;
            switch (GetDragDirection(dragVectorDirection))
            {
                case DraggedDirection.Up:
                    _jumpMovement.Jump();
                    break;
                case DraggedDirection.Down:
                    _jumpMovement.Down();
                    break;
                case DraggedDirection.Right:
                    _sideMovement.MoveRight();
                    break;
                case DraggedDirection.Left:
                    _sideMovement.MoveLeft();
                    break;
            }
        }

        public void OnDrag(PointerEventData eventData) { }

        private DraggedDirection GetDragDirection(Vector3 dragVector)
        {
            float positiveX = Mathf.Abs(dragVector.x);
            float positiveY = Mathf.Abs(dragVector.y);
            DraggedDirection draggedDir;
            if (positiveX > positiveY)
            {
                draggedDir = (dragVector.x > 0) ? DraggedDirection.Right : DraggedDirection.Left;
            }
            else
            {
                draggedDir = (dragVector.y > 0) ? DraggedDirection.Up : DraggedDirection.Down;
            }
            Debug.Log(draggedDir);
            return draggedDir;
        }
    }
}