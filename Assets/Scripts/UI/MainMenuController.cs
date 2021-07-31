using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using TMPro;

namespace com.abhishek.saraf.SnakeEyes
{
    /**
     * This script will be responsible for managing the Main Menu functionalities.
     */
    public class MainMenuController : MonoBehaviour
    {
        #region Private Attributes

        [SerializeField] private int _gameSceneIndex = 1; // store the Game scene's index value.

        [SerializeField] private GameObject _mainMenuPanel, _settingsPanel; // variables to store the main menu & settings menu panel(s).

        [SerializeField] private Slider _volumeSlider; // the volume slider.

        [SerializeField] private GameObject _audioOn, _audioOff; // container for the audio control buttons in the UI.

        [SerializeField] private TextMeshProUGUI _playerName, _highScoreText; // UI text reference for player's name & high score.

        [SerializeField] private TMP_InputField _playerNameInputField; // input field element for the player's name input.

        [SerializeField] private AudioClip _mainMenuAudioClip, _inGameAudioClip; // audio clip references - main menu audio clip & in-game audio clip.

        private float _volume; // variable to store audio volume in the game.

        private bool _isMuted = false; // variable to store whether our game audio is muted.

        #endregion

        #region Private Methods

        // Start is called before the first frame update
        void Start()
        {
            _playerName.text = PlayerPrefs.GetString("PlayerName", "Player"); // get & store the player's name from PlayerPrefs. default value = "Player".
            _volumeSlider.value = PlayerPrefs.GetFloat("GameAudioVolume", 1.0f); // get & store the game audio volume from PlayerPrefs. default value = 1.0

            _highScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0); // get & store the game's high score from PlayerPrefs. default value = 0

            AudioController.instance.GetComponent<AudioSource>().clip = _mainMenuAudioClip; // initialize the main menu audio clip for the audio source.
            AudioController.instance.GetComponent<AudioSource>().Play(); // play the clip via the audio source.
        }

        /// <summary>
        /// Method to change the audio volume.
        /// </summary>
        /// <param name="value">by how much value we need to change the volume.</param>
        private void ChangeAudioVolume(float value)
        {
            _volumeSlider.value = value; // update the volume slider's value.
            _volume = value; // update the volume.
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Start the game - load the "Game" scene.
        /// </summary>
        public void StartGame()
        {
            AudioController.instance.GetComponent<AudioSource>().Stop(); // stop the previous clip.
            AudioController.instance.GetComponent<AudioSource>().clip = _inGameAudioClip; // initialize the in-game audio clip.
            AudioController.instance.GetComponent<AudioSource>().Play(); // play the in-game audio clip via the audio source.
            SceneManager.LoadScene(_gameSceneIndex); // Load the "Game" scene.
        }

        /// <summary>
        /// Load Settings Menu.
        /// </summary>
        public void LoadSettingsMenu()
        {
            _mainMenuPanel.SetActive(false);
            _settingsPanel.SetActive(true);
        }

        /// <summary>
        /// Load Main Menu.
        /// </summary>
        public void LoadMainMenu()
        {
            _settingsPanel.SetActive(false);
            _mainMenuPanel.SetActive(true);
        }

        /// <summary>
        /// Quit the game.
        /// </summary>
        public void QuitGame()
        {
            Application.Quit();
        }

        /// <summary>
        /// Method/Event called when we change the volume from the slider.
        /// </summary>
        public void OnVolumeChanged()
        {
            _volume = _volumeSlider.value; // update the game's audio volume.

            AudioController.instance.GameAudioSource.volume = _volume; // update the audio source's volume value.

            PlayerPrefs.SetFloat("GameAudioVolume", _volume); // store the volume in PlayerPrefs.
        }

        /// <summary>
        /// Mute the game audio.
        /// </summary>
        public void Mute()
        {
            _isMuted = true;
            _volumeSlider.interactable = !_isMuted; // disable the volume slider if game is muted.
            _audioOn.GetComponent<Button>().interactable = true; // enable/make interactable the audioOn button if muted.
            _audioOff.GetComponent<Button>().interactable = false; // disable/make non-interactable the audioOff button if muted.
            PlayerPrefs.SetFloat("LastGameAudioVolume", _volume); // store the last game audio volume to be used later if required.
            ChangeAudioVolume(0.0f); // call the change audio volume method.
        }

        /// <summary>
        /// Unmute the game audio.
        /// </summary>
        public void UnMute()
        {
            _isMuted = false;
            _volumeSlider.interactable = !_isMuted; // enable the volume slider if game is unmuted.
            _audioOff.GetComponent<Button>().interactable = true; // enable/make interactable the audioOff button if unmuted.
            _audioOn.GetComponent<Button>().interactable = false; // disable/make non-interactable the audioOn button if unmuted.
            ChangeAudioVolume(PlayerPrefs.GetFloat("LastGameAudioVolume", 1.0f)); // change audio volume based on last game audio volume.
        }

        /// <summary>
        /// Event called when the player finishes editing the player name input field.
        /// </summary>
        public void OnPlayerNameEndEdit()
        {
            string value = _playerNameInputField.text; // store the input value for player's name.
            PlayerPrefs.SetString("PlayerName", value); // store the player's name in the PlayerPrefs.
            _playerName.text = value; // update the player's name in the UI.
        }

        #endregion
    }
}
