using UnityEngine;

namespace com.abhishek.saraf.SnakeEyes
{
    /**
     * This script will be responsible for managing the audio in the game.
     */
    public class AudioController : MonoBehaviour
    {
        #region Private Attributes

        [SerializeField] private AudioSource _gameAudioSource; // reference for the game audio source component.

        #endregion

        #region Public Attributes

        public static AudioController instance; // declaring the singleton instance.

        #endregion

        #region Properties

        public AudioSource GameAudioSource
        {
            get { return _gameAudioSource; } // provide reference of game audio source - make it available for other scripts to use it.
        }

        #endregion

        #region Private Methods

        // Awake is called when the script instance is being loaded.
        private void Awake()
        {
            // Initialize/Setting up the singleton variable.

            // If instance is null, set it to 'this' - current instance of the script.
            if (instance == null)
            {
                instance = this;
            }
            else // if instance is not null, and already having some value.
            {
                if (instance != this) // meaning the instance refers to an another instance of the current script, and we want to destroy the older version of our singleton.
                {
                    Destroy(instance.gameObject);
                    instance = this; // set the singleton to the new instance of current script.
                }
            }
            // Do not destory the target object when loading a new scene.
            DontDestroyOnLoad(gameObject); // to make sure this gameobject persists in the next scene.
        }

        // Start is called before the first frame update
        void Start()
        {
            _gameAudioSource.volume = PlayerPrefs.GetFloat("GameAudioVolume", 1.0f); // update the game's audio source based on value stored in PlayerPrefs.
        }

        #endregion
    }
}
