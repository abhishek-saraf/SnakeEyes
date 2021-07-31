using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace com.abhishek.saraf.SnakeEyes
{
    /**
     * This script is responsible for managing the grid within which the snake will move.
     */
    public class GridController : MonoBehaviour
    {
        #region Private Attributes

        // X-axis = height, Z-axis = width.
        public int gridHeight = 5, gridWidth = 10; // Modifiable via Unity Inspector.

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
            InitalizeGrid();
        }

        // Update is called once per frame
        void Update()
        {

        }

        /// <summary>
        /// Initialize the grid.
        /// </summary>
        private void InitalizeGrid()
        {
            gameObject.transform.localScale = new Vector3(gridHeight, 1, gridWidth);
        }

        #endregion

        #region Public Methods



        #endregion

        #region Public Overriden Methods



        #endregion
    }
}
