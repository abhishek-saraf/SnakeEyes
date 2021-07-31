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
    public class MainMenuController : MonoBehaviour
    {
        #region Private Attributes

        [SerializeField] private int _gameSceneIndex = 1;

        [SerializeField] private GameObject _mainMenuPanel, _settingsPanel;

        [SerializeField] private Slider _volumeSlider;

        [SerializeField] private GameObject _audioOn, _audioOff;

        [SerializeField] private TextMeshProUGUI _playerName;

        [SerializeField] private TMP_InputField _playerNameInputField;

        private float _volume;

        private bool _isMuted = false;

        #endregion

        #region Public Attributes



        #endregion

        #region Properties

        public float Volume
        {
            get { return _volume; }
        }

        public bool IsMuted
        {
            get { return _isMuted; }
        }

        #endregion

        #region Private Methods

        // Awake is called when the script instance is being loaded.
        private void Awake()
        {

        }

        // Start is called before the first frame update
        void Start()
        {
            _playerName.text = PlayerPrefs.GetString("PlayerName", "Player");
            _volumeSlider.value = PlayerPrefs.GetFloat("GameAudioVolume", 1.0f);
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        private void ChangeAudioVolume(float value)
        {
            _volumeSlider.value = value;
            _volume = value;
        }

        #endregion

        #region Public Methods

        public void StartGame()
        {
            SceneManager.LoadScene(_gameSceneIndex);
        }

        public void LoadSettingsMenu()
        {
            _mainMenuPanel.SetActive(false);
            _settingsPanel.SetActive(true);
        }

        public void LoadMainMenu()
        {
            _settingsPanel.SetActive(false);
            _mainMenuPanel.SetActive(true);
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        public void OnVolumeChanged()
        {
            _volume = _volumeSlider.value;

            AudioController.instance.GameAudioSource.volume = _volume;

            PlayerPrefs.SetFloat("GameAudioVolume", _volume);
        }

        public void Mute()
        {
            _isMuted = true;
            _volumeSlider.interactable = !_isMuted;
            _audioOn.GetComponent<Button>().interactable = true;
            _audioOff.GetComponent<Button>().interactable = false;
            PlayerPrefs.SetFloat("LastGameAudioVolume", _volume);
            ChangeAudioVolume(0.0f);
        }

        public void UnMute()
        {
            _isMuted = false;
            _volumeSlider.interactable = !_isMuted;
            _audioOff.GetComponent<Button>().interactable = true;
            _audioOn.GetComponent<Button>().interactable = false;
            ChangeAudioVolume(PlayerPrefs.GetFloat("LastGameAudioVolume", 1.0f));
        }

        public void OnPlayerNameEndEdit()
        {
            string value = _playerNameInputField.text;
            PlayerPrefs.SetString("PlayerName", value);
            _playerName.text = value;
        }

        #endregion

        #region Public Overriden Methods



        #endregion
    }
}
