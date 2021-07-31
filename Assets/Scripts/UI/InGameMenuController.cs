using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using TMPro;


namespace com.abhishek.saraf.SnakeEyes
{
    /**
     * This script will be responsible for managing the Main Menu functionalities.
     */
    public class InGameMenuController : MonoBehaviour
    {
        #region Private Attributes

        [SerializeField] private int _mainMenuSceneIndex = 0;

        [SerializeField] private GameObject _inGameMenu, _pauseMenu;

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

        #endregion

        #region Public Methods

        public void LoadPauseMenu()
        {
            _inGameMenu.SetActive(false);
            _pauseMenu.SetActive(true);
        }

        public void LoadInGameMenu()
        {
            _pauseMenu.SetActive(false);
            _inGameMenu.SetActive(true);
        }

        public void LoadMainMenu()
        {
            SceneManager.LoadScene(_mainMenuSceneIndex);
        }

        public void Pause()
        {
            Time.timeScale = 0;
        }

        public void Resume()
        {
            Time.timeScale = 1;
        }

        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        #endregion

        #region Public Overriden Methods



        #endregion
    }
}
