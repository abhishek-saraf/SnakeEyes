using UnityEngine;
using UnityEngine.SceneManagement;

using TMPro;

namespace com.abhishek.saraf.SnakeEyes
{
    /**
     * This script will be responsible for managing the In-Game Menu functionalities.
     */
    public class InGameMenuController : MonoBehaviour
    {
        #region Private Attributes

        [SerializeField] private int _mainMenuSceneIndex = 0; // variable to store the main menu scene index.

        [SerializeField] private GameObject _inGameMenu, _pauseMenu, _gameOverMenu; // references for the in-game, pause & game over menu.

        [SerializeField] private TextMeshProUGUI _scoreText, _highScoreText; // references for the score & high score text in the UI.

        [SerializeField] private AudioClip _deathClip; // death clip reference - to be played once the game is over.

        #endregion

        #region Private Methods

        // Start is called before the first frame update
        void Start()
        {
            // hide the pause menu & display the in game menu.
            _pauseMenu.SetActive(false);
            _inGameMenu.SetActive(true);
        }

        /// <summary>
        /// Pause the game.
        /// </summary>
        private void Pause()
        {
            PauseAudio(); // pause the game's audio.
            Time.timeScale = 0; // update the time scale on pause.
        }

        /// <summary>
        /// Resume the game.
        /// </summary>
        private void Resume()
        {
            Time.timeScale = 1; // update the time scale on resume.
        }

        /// <summary>
        /// Pause the game audio.
        /// </summary>
        private void PauseAudio()
        {
            AudioController.instance.GameAudioSource.Pause(); // pause the game audio source.
        }

        /// <summary>
        /// Resume the game audio.
        /// </summary>
        private void ResumeAudio()
        {
            AudioController.instance.GameAudioSource.Play(); // resume/play the game audio source.
        }

        /// <summary>
        /// Reset the game during main menu load & restart.
        /// </summary>
        private void Reset()
        {
            GameManager.instance.isGameOver = false; // update the game over boolean variable.
            _gameOverMenu.SetActive(false); // hide the game over menu.
            _inGameMenu.SetActive(true); // display the in game menu.
        }

        /// <summary>
        /// Check the save player's high score - update the high score if current score is greater than it.
        /// </summary>
        private void CheckAndSaveHighScore()
        {
            int currentScore = ScoreManager.instance.score; // store the current score.
            int highScore = PlayerPrefs.GetInt("HighScore", 0); // get the high score from PlayerPrefs. default value = 0

            if (currentScore > highScore)
            {
                PlayerPrefs.SetInt("HighScore", currentScore); // update the player's high score only if the current high score is greater than it.
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Load the pause menu.
        /// </summary>
        public void LoadPauseMenu()
        {
            _inGameMenu.SetActive(false); // hide the in game menu.
            _pauseMenu.SetActive(true); // display the pause menu.
            Pause(); // call the pause method to update the audio & time scale.
        }

        /// <summary>
        /// Load the in-game menu.
        /// </summary>
        public void LoadInGameMenu()
        {
            ResumeAudio(); // resume the game audio.
            Resume(); // resume the game - update the time scale on resume.
            _pauseMenu.SetActive(false); // hide the pause menu.
            _inGameMenu.SetActive(true); // display the in game menu.
        }

        /// <summary>
        /// Load the main menu.
        /// </summary>
        public void LoadMainMenu()
        {
            Resume(); // resume the game - update the time scale on resume.
            Reset(); // reset the game while loading the main menu.
            SceneManager.LoadScene(_mainMenuSceneIndex); // Load the main menu scene.
        }

        /// <summary>
        /// Restart the game.
        /// </summary>
        public void Restart()
        {
            ResumeAudio(); // resume the game audio.
            Resume(); // resume the game - update the time scale on resume.
            Reset(); // reset the game while restarting.
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Load/Restart the current scene.
        }

        /// <summary>
        /// Quit the game.
        /// </summary>
        public void QuitGame()
        {
            Application.Quit();
        }

        /// <summary>
        /// Display the game over menu.
        /// </summary>
        public void ShowGameOverMenu()
        {
            AudioController.instance.GetComponent<AudioSource>().Stop(); // stop the already playing audio clip.
            AudioController.instance.GetComponent<AudioSource>().PlayOneShot(_deathClip); // play the death clip.
            _inGameMenu.SetActive(false); // hide the in game menu.
            _gameOverMenu.SetActive(true); // show the game over menu.
            _scoreText.text = "Score: " + ScoreManager.instance.score.ToString(); // display the score in the game over screen.
            CheckAndSaveHighScore(); // check if the player's score is the high score.
            _highScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0); // display the high score in the game over screen.
        }

        #endregion
    }
}
