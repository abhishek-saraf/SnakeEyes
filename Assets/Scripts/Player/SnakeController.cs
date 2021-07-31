using UnityEngine;

namespace com.abhishek.saraf.SnakeEyes
{
    /**
     * This script will be reponsible for controlling the snake movement.
     */
    public class SnakeController : MonoBehaviour
    {
        #region Private Attributes

        [SerializeField] private float _speed = 0.1f; // variable to store the snake's speed.

        [SerializeField] private float _turnSpeed = 10.0f; // variable to store the snake's turn speed.

        #endregion

        #region Private Methods

        private void FixedUpdate()
        {
            MoveSnake(); // move the snake with constant speed.
            TurnSnake(); // turn the snake based on player's input from the UI.
        }

        /// <summary>
        /// Method called for moving the snake with a constant speed.
        /// </summary>
        private void MoveSnake()
        {
            transform.position += _speed * Time.deltaTime * transform.forward; // update the snake's position with a constant speed.
        }

        /// <summary>
        /// Turn the snake based on the player's input from the UI.
        /// </summary>
        private void TurnSnake()
        {
            #region MyComment
            // Please uncomment the below code snip in case we want to move snake using keyboard keys instead of the UI slider.
            /*
            if (Input.GetAxis("Horizontal") != 0)
            {
                float turnAmount = Input.GetAxis("Horizontal");

                gameObject.transform.Rotate(0.0f, turnAmount * _turnSpeed, 0.0f);
            }
            */
            #endregion

            SliderController sliderController = GameManager.instance.Slider.GetComponent<SliderController>(); // storing reference for the UI Slider/Joystick.

            bool isSliderHeld = sliderController.SliderHeld; // variable to store boolean value if the slider is currently being held by the player.

            float sliderOutput = sliderController.output; // get the calculated output based on slider's turn.

            if (isSliderHeld)
            {
                gameObject.transform.Rotate(0.0f, sliderOutput * _turnSpeed, 0.0f); // update the snake's rotation based on the input.
            }
        }

        /// <summary>
        /// Event called when the snake collides with the grid's wall(s).
        /// </summary>
        /// <param name="collision">The Collision data associated with this collision event.</param>
        private void OnCollisionEnter(Collision collision)
        {
            // Store new direction - we need to update the snake's rotation to simulate a mirror's reflection.
            Vector3 newDirection = Vector3.Reflect(transform.forward, collision.contacts[0].normal);
            //Rotate to new direction.
            transform.rotation = Quaternion.LookRotation(newDirection);
        }

        #endregion
    }
}
