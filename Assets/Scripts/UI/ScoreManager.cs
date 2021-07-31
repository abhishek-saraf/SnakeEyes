using UnityEngine;

using TMPro;

namespace com.abhishek.saraf.SnakeEyes
{
    /**
     * This script is responsible for managing the player's score.
     */
    public class ScoreManager : MonoBehaviour
    {
        #region Private Attributes

        // Text element to store the player's score & display in the UI.
        [SerializeField] private TextMeshProUGUI _scoreText;

        // Audio clip to be player as soon as the 'Snake' eats a slice of pizza.
        [SerializeField] private AudioClip _pizzaEatingClip;

        #endregion

        #region Public Attributes

        public static ScoreManager instance; // Singleton instance of this class.

        [HideInInspector] public int score = 0; // player score container.

        #endregion

        #region Private Methods

        // Awake is called when the script instance is being loaded.
        private void Awake()
        {
            instance = this; // initializing the singleton.
        }

        // Update is called once per frame
        void Update()
        {
            _scoreText.text = "Score: " + score; // keep the player's score in the UI always up-to-date. This will be done every frame.
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Increment player's score as soon as the snake eats a slice of pizza.
        /// </summary>
        public void AddScore()
        {
            AudioController.instance.GetComponent<AudioSource>().PlayOneShot(_pizzaEatingClip); // play the audio clip.
            score += 1; // increment the score.
        }

        #endregion
    }
}
