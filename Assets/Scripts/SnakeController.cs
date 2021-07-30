using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.abhishek.saraf.SnakeEyes
{
    /**
     * This script will be reponsible for controlling the snake movement.
     */
    public class SnakeController : MonoBehaviour
    {
        #region Private Attributes

        [SerializeField] private float _speed = 0.01f;

        [SerializeField] private float _turnSpeed = 10.0f;

        [SerializeField] private GameObject _parentSnakeGameObject;

        private Rigidbody _snakeRigidBody;

        private Vector3 _lastVelocity;

        #endregion

        #region Public Attributes



        #endregion

        #region Private Methods

        // Awake is called when the script instance is being loaded.
        private void Awake()
        {

        }

        // Start is called before the first frame update
        void Start()
        {
            _snakeRigidBody = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            _lastVelocity = _snakeRigidBody.velocity;
        }


        private void FixedUpdate()
        {
            MoveSnake();
            TurnSnake();
        }

        private void MoveSnake()
        {
            _parentSnakeGameObject.transform.position += _speed * Time.deltaTime * _parentSnakeGameObject.transform.forward;
            // transform.position += _speed * Time.deltaTime * transform.forward;
        }

        private void TurnSnake()
        {
            if (Input.GetAxis("Horizontal") != 0)
            {
                float _turnAmount = Input.GetAxis("Horizontal");
                _parentSnakeGameObject.transform.Rotate(0.0f, _turnAmount * _turnSpeed, 0.0f);
                // gameObject.transform.Rotate(0.0f, _turnAmount * _turnSpeed, 0.0f);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("Snake Collided with something");
            /*
            var speed = _lastVelocity.magnitude;
            Debug.Log("Speed: " + speed);
            var direction = Vector3.Reflect(_lastVelocity.normalized, collision.GetContact(0).normal);
            Debug.Log("Direction: " + direction);
            _snakeRigidBody.velocity = direction * Mathf.Max(speed, 0f);
            Debug.Log("New velocity: " + _snakeRigidBody.velocity);
            */
            // Debug.Log("Old position: " + _parentSnakeGameObject.transform.position);
            // _parentSnakeGameObject.transform.position = Vector3.Reflect(_parentSnakeGameObject.transform.position, collision.GetContact(0).normal);
            // Debug.Log("New position: " + _parentSnakeGameObject.transform.position);

            Vector3 magnitude = Vector3.Reflect(_parentSnakeGameObject.transform.rotation.eulerAngles, collision.GetContact(0).normal);
            Debug.Log("Magnitude: " + Vector3.Reflect(_parentSnakeGameObject.transform.rotation.eulerAngles, collision.GetContact(0).normal));
            _parentSnakeGameObject.transform.Rotate(magnitude);
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Snake Triggered with something");
        }

        #endregion

        #region Public Methods

        public void Reflect()
        {
            Debug.Log("Reflect Me!");
        }

        #endregion

        #region Public Overriden Methods



        #endregion
    }
}
