using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using TMPro;

namespace com.abhishek.saraf.SnakeEyes
{
    /**
     * This script is responsible for management of whole game.
     */
    public class GameManager : MonoBehaviour
    {
        #region Private Attributes

        [SerializeField] private Camera _camera;

        [SerializeField] private GameObject _grid;

        [SerializeField] private GameObject _slider;

        [SerializeField] private GameObject _uiController;

        #endregion

        #region Public Attributes

        public static GameManager instance;

        [HideInInspector] public int cameraHeight, cameraWidth;

        [HideInInspector] public bool isGameOver;

        #endregion

        #region Properties

        public GameObject Slider
        {
            get { return _slider; }
        }

        #endregion

        #region Private Methods

        // Awake is called when the script instance is being loaded.
        private void Awake()
        {
            instance = this;
            cameraHeight = _grid.GetComponent<GridController>().gridHeight;
            cameraWidth = _grid.GetComponent<GridController>().gridWidth;

            int cameraSize = cameraHeight >= cameraWidth ? cameraHeight : cameraWidth;
            _camera.orthographicSize = 0.125f * cameraSize;
        }

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        #endregion

        #region Public Methods



        public void GameOver()
        {
            isGameOver = true;

            _uiController.GetComponent<InGameMenuController>().ShowGameOverMenu();

            Time.timeScale = 0;
        }

        #endregion

        #region Public Overriden Methods



        #endregion
    }
}
