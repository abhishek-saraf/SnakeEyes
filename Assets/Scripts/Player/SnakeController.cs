using UnityEngine;

namespace com.abhishek.saraf.SnakeEyes
{
    /**
     * This script will be reponsible for controlling the snake movement.
     */
    public class SnakeController : MonoBehaviour
    {
        #region Private Attributes

        [SerializeField] private float _speed = 0.1f;

        [SerializeField] private float _turnSpeed = 10.0f;

        [SerializeField] private GameObject _parentSnakeGameObject;

        private Rigidbody _snakeRigidBody;

        #endregion

        #region Private Methods

        // Start is called before the first frame update
        void Start()
        {
            _snakeRigidBody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            MoveSnake();
            TurnSnake();
        }

        private void MoveSnake()
        {
            transform.position += _speed * Time.deltaTime * transform.forward;
        }

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

            SliderController sliderController = GameManager.instance.Slider.GetComponent<SliderController>();

            bool isSliderHeld = sliderController.SliderHeld;

            float sliderOutput = sliderController.output;

            if (isSliderHeld)
            {
                gameObject.transform.Rotate(0.0f, sliderOutput * _turnSpeed, 0.0f);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            //Store new direction
            Vector3 newDirection = Vector3.Reflect(transform.forward, collision.contacts[0].normal);
            //Rotate to new direction
            transform.rotation = Quaternion.LookRotation(newDirection);
        }

        #endregion
    }
}
