using UnityEngine;
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

        [SerializeField] private GameObject _inGameMenu, _pauseMenu, _gameOverMenu;

        [SerializeField] private TextMeshProUGUI _scoreText, _highScoreText;

        [SerializeField] private AudioClip _deathClip;

        #endregion

        #region Private Methods

        // Start is called before the first frame update
        void Start()
        {
            _pauseMenu.SetActive(false);
            _inGameMenu.SetActive(true);
        }

        private void Pause()
        {
            PauseAudio();
            Time.timeScale = 0;
        }

        private void Resume()
        {
            Time.timeScale = 1;
        }

        private void PauseAudio()
        {
            AudioController.instance.GameAudioSource.Pause();
        }

        private void ResumeAudio()
        {
            AudioController.instance.GameAudioSource.Play();
        }

        private void Reset()
        {
            GameManager.instance.isGameOver = false;
            _gameOverMenu.SetActive(false);
            _inGameMenu.SetActive(true);
        }

        private void CheckAndSaveHighScore()
        {
            int currentScore = ScoreManager.instance.score;
            int highScore = PlayerPrefs.GetInt("HighScore", 0);

            if (currentScore > highScore)
            {
                PlayerPrefs.SetInt("HighScore", currentScore);
            }
        }

        #endregion

        #region Public Methods

        public void LoadPauseMenu()
        {
            _inGameMenu.SetActive(false);
            _pauseMenu.SetActive(true);
            Pause();
        }

        public void LoadInGameMenu()
        {
            ResumeAudio();
            Resume();
            _pauseMenu.SetActive(false);
            _inGameMenu.SetActive(true);
        }

        public void LoadMainMenu()
        {
            Resume();
            Reset();
            SceneManager.LoadScene(_mainMenuSceneIndex);
        }

        public void Restart()
        {
            ResumeAudio();
            Resume();
            Reset();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        public void ShowGameOverMenu()
        {
            AudioController.instance.GetComponent<AudioSource>().Stop();
            AudioController.instance.GetComponent<AudioSource>().PlayOneShot(_deathClip);
            _inGameMenu.SetActive(false);
            _gameOverMenu.SetActive(true);
            _scoreText.text = "Score: " + ScoreManager.instance.score.ToString();
            CheckAndSaveHighScore();
            _highScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0);
        }

        #endregion
    }
}
