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

        [SerializeField] private TextMeshProUGUI _scoreText;

        #endregion

        #region Public Attributes

        public static GameManager instance;

        public int cameraHeight, cameraWidth;

        public int score = 0;

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
            _scoreText.text = "Score: " + score;
        }

        #endregion

        #region Public Methods

        public void AddScore()
        {
            score += 1;
        }

        public void GameOver()
        {
            Time.timeScale = 0;
        }

        #endregion

        #region Public Overriden Methods



        #endregion
    }
}
