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

        private Rigidbody _myRigidBody;

        [SerializeField] private float _speed = 0.01f;

        [SerializeField] private float _turnSpeed = 10.0f;

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
            _myRigidBody = GetComponent<Rigidbody>();

            Debug.Log(GetComponent<Collider>().isTrigger);
        }

        // Update is called once per frame
        void Update()
        {
            
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
            if (Input.GetAxis("Horizontal") != 0)
            {
                float _turnAmount = Input.GetAxis("Horizontal");
                gameObject.transform.Rotate(0.0f, _turnAmount * _turnSpeed, 0.0f);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("Collided with something");
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Triggered with something");
        }

        #endregion

        #region Public Methods



        #endregion

        #region Public Overriden Methods



        #endregion
    }
}
