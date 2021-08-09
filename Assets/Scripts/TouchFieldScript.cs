using UnityEngine;
using UnityEngine.EventSystems;

namespace InputTouchField
{

    public class TouchFieldScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private Vector2 _touchDist;
        private Vector2 _pointerOld;
        private int _pointerId;
        private bool _pressed;
        public Vector2 TouchDist => _touchDist;
        void Update()
        {
            if (_pressed)
            {
                if (_pointerId >= 0 && _pointerId < Input.touches.Length)
                {
                    _touchDist = Input.touches[_pointerId].position - _pointerOld;
                    _pointerOld = Input.touches[_pointerId].position;
                }
                else
                {
                    _touchDist = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - _pointerOld;
                    _pointerOld = Input.mousePosition;
                }
            }
            else
            {
                _touchDist = new Vector2();
            }
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            _pressed = true;
            _pointerId = eventData.pointerId;
            _pointerOld = eventData.position;
        }
        public void OnPointerUp(PointerEventData eventData)
        {
            _pressed = false;
        }
    }
}