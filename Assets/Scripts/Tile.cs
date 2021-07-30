using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace com.abhishek.saraf.SnakeEyes
{
    /**
     * This script manages a single tile.
     */
    public class Tile : MonoBehaviour
    {
        #region Private Attributes

        [SerializeField] private Material _greenMat;

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

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag.Equals("Snake"))
            {
                gameObject.GetComponent<MeshRenderer>().material = _greenMat;
            }
        }

        #endregion

        #region Public Methods



        #endregion

        #region Public Overriden Methods



        #endregion
    }
}
