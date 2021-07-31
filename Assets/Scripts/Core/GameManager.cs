using UnityEngine;

namespace com.abhishek.saraf.SnakeEyes
{
    /**
     * This script is responsible for management of whole game.
     */
    public class GameManager : MonoBehaviour
    {
        #region Private Attributes

        [SerializeField] private Camera _camera; // camera reference.

        [SerializeField] private GameObject _grid; // grid reference.

        [SerializeField] private GameObject _slider; // reference for the snake controller - slider in the UI.

        [SerializeField] private GameObject _uiController; // UI controller reference.

        #endregion

        #region Public Attributes

        public static GameManager instance; // declaring the singleton instance.

        [HideInInspector] public int cameraHeight, cameraWidth; // variables to store the camera's height & width based on grid values.

        [HideInInspector] public bool isGameOver; // variable to check if the game is over.

        #endregion

        #region Properties

        public GameObject Slider
        {
            get { return _slider; } // slider reference - useful for other scripts.
        }

        #endregion

        #region Private Methods

        // Awake is called when the script instance is being loaded.
        private void Awake()
        {
            instance = this; // initialize the singleton instance.
            cameraHeight = _grid.GetComponent<GridController>().gridHeight; // initializing the camera height based on the grid's height.
            cameraWidth = _grid.GetComponent<GridController>().gridWidth; // initializing the camera width based on the grid's width.
            // calculate the orthographic camera size based on the camera's height & width.
            int cameraSize = cameraHeight >= cameraWidth ? cameraHeight : cameraWidth;
            _camera.orthographicSize = 0.125f * cameraSize;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Method called when game is over - when no free tiles are left.
        /// </summary>
        public void GameOver()
        {
            isGameOver = true; // update the game over boolean variable.

            _uiController.GetComponent<InGameMenuController>().ShowGameOverMenu(); // display the game over screen.

            Time.timeScale = 0; // update the time scale.
        }

        #endregion
    }
}
