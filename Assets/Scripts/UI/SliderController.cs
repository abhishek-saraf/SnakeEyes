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

        [SerializeField] private RectTransform _slider; // UI slider/joystick's RectTransform reference.

        private float _sliderAngle = 0.0f; // variable to store the slider angle.

        private float _lastSliderAngle = 0.0f; // variable to store the last slider angle.

        private Vector2 _center; // variable to store the center of slider's position (center of the joystick).

        [SerializeField] private float _maxSliderSteerAngle = 360.0f; // variable to clamp the slider to a certain angle (if-required).

        // variable to store the release speed of the slider - describes the speed at which slider comes back to it's initial position.
        [SerializeField] private float _releaseSpeed = 300.0f;

        [SerializeField] private bool _clampSlider = false; // variable to decide whether to clamp the slider in the UI.

        #endregion

        #region Public Attributes

        public bool _sliderHeld = false; // variable to store the boolean flag - if the slider if currently held down by the player.

        public float output; // store the calculated output based on slider's turn - useful while turning the snake.

        #endregion

        #region Properties

        public bool SliderHeld
        {
            get { return _sliderHeld; } // reference - useful for other scripts - to know if the slider is currently held down by the player.
        }

        #endregion

        #region Private Methods

        // Update is called once per frame
        void Update()
        {
            // We need to reverse the wheel to it's original position.
            if (!_sliderHeld && _sliderAngle != 0.0f) // proceed only if the slider is not being held down & the slider angle is not in it's initial position.
            {
                // store the delta angle based on the release speed (release speed can be set in the inspector).
                float deltaAngle = _releaseSpeed * Time.deltaTime;
                // we need to bring the slider back to its original position - in both cases - if its rotated either left/right by the user.
                if (Mathf.Abs(deltaAngle) > Mathf.Abs(_sliderAngle))
                {
                    _sliderAngle = 0.0f; // if the delta angle is greater than the slider's angle - bring the value to 0.
                }
                else if (_sliderAngle > 0.0f)
                {
                    _sliderAngle -= deltaAngle; // decrement the slider angle with the delta angle if the slider angle is greater than 0.
                }
                else
                {
                    _sliderAngle += deltaAngle; // increment the slider angle with the delta angle if the slider angle is less than 0.
                }
            }
            
            _slider.localEulerAngles = new Vector3(0.0f, 0.0f, -_sliderAngle); // rotate the slider/joystick in the 'Z-axis' based on the slider angle.
            
            // calculate the output to know to which degree we need to rotate the snake, based on the slider angle & max slider steer angle.
            output = _sliderAngle / _maxSliderSteerAngle;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Called by the EventSystem every time the pointer is moved during dragging.
        /// </summary>
        /// <param name="eventData">Current event data.</param>
        public void OnDrag(PointerEventData eventData)
        {
            // calculate and store the new angle similar to calculation of last slider angle based on the event data's position & slider's center position.
            float newAngle = Vector2.Angle(Vector2.up, eventData.position - _center);
            // calculate and store the slider angle based on the new angle calculated above and the last slider angle.
            // Proceed only if the square magnitude of the difference b/w the event data's position & center >= 400.
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
            // Clamp the slider based on the boolean "clampSlider", i.e. clamp the slider only if required.
            if (_clampSlider)
            {
                // clamp the slider angle based on the max slider steer angle (negative to positive range, ex. -360 to +360 degrees).
                _sliderAngle = Mathf.Clamp(_sliderAngle, -_maxSliderSteerAngle, _maxSliderSteerAngle);
            }
            
            _lastSliderAngle = newAngle; // update the last slider angle to the new angle calculated above.
        }

        /// <summary>
        /// Called by the EventSystem when a PointerDown event occurs.
        /// </summary>
        /// <param name="eventData">Current event data.</param>
        public void OnPointerDown(PointerEventData eventData)
        {
            _sliderHeld = true; // update 'sliderHeld' to 'true' if the slider is held down.
            _center = RectTransformUtility.WorldToScreenPoint(eventData.pressEventCamera, _slider.position); // update the center of the slider.
            // calculate and store the last slider angle based on the event data's position & slider's center position.
            _lastSliderAngle = Vector2.Angle(Vector2.up, eventData.position - _center);
        }

        /// <summary>
        /// Called by the EventSystem when a PointerUp event occurs.
        /// </summary>
        /// <param name="eventData">Current event data.</param>
        public void OnPointerUp(PointerEventData eventData)
        {
            OnDrag(eventData); // call the 'OnDrag' event manually.
            _sliderHeld = false; // update 'sliderHeld' to 'false' if the slider is not held down/pointer is lifted up.
        }

        #endregion
    }
}
