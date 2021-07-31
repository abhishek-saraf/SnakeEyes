using UnityEngine;
using UnityEngine.EventSystems;

namespace com.abhishek.saraf.SnakeEyes
{
    /**
     * This script will be responsible for controlling the UI slider in the In-Game menu - for controlling the snake movement.
     */
    public class SliderController : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        #region Private Attributes

        public bool _sliderHeld = false;

        [SerializeField] private RectTransform _slider;

        private float _sliderAngle = 0.0f;

        private float _lastSliderAngle = 0.0f;

        private Vector2 _center;

        [SerializeField] private float _maxSliderSteerAngle;

        [SerializeField] private float _releaseSpeed = 300.0f;

        #endregion

        #region Public Attributes

        public float output;

        #endregion

        #region Properties

        public bool SliderHeld
        {
            get { return _sliderHeld; }
        }

        #endregion

        #region Private Methods

        // Update is called once per frame
        void Update()
        {
            if (!_sliderHeld && _sliderAngle != 0.0f)
            {
                float deltaAngle = _releaseSpeed * Time.deltaTime;
                if (Mathf.Abs(deltaAngle) > Mathf.Abs(_sliderAngle))
                {
                    _sliderAngle = 0.0f;
                }
                else if (_sliderAngle > 0.0f)
                {
                    _sliderAngle -= deltaAngle;
                }
                else
                {
                    _sliderAngle += deltaAngle;
                }
            }
            _slider.localEulerAngles = new Vector3(0.0f, 0.0f, -_sliderAngle);
            output = _sliderAngle / _maxSliderSteerAngle;
        }

        #endregion

        #region Public Methods

        public void OnDrag(PointerEventData eventData)
        {
            float newAngle = Vector2.Angle(Vector2.up, eventData.position - _center);
            if ((eventData.position - _center).sqrMagnitude >= 400)
            {
                if (eventData.position.x > _center.x)
                {
                    _sliderAngle += newAngle - _lastSliderAngle;
                }
                else
                {
                    _sliderAngle -= newAngle - _lastSliderAngle;
                }
            }
            _sliderAngle = Mathf.Clamp(_sliderAngle, -_maxSliderSteerAngle, _maxSliderSteerAngle);
            _lastSliderAngle = newAngle;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _sliderHeld = true;
            _center = RectTransformUtility.WorldToScreenPoint(eventData.pressEventCamera, _slider.position);
            _lastSliderAngle = Vector2.Angle(Vector2.up, eventData.position - _center);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            OnDrag(eventData);
            _sliderHeld = false;
        }

        #endregion
    }
}
