using System.Collections;
using System.Collections.Generic;

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

        [SerializeField] private TextMeshProUGUI _scoreText;

        [SerializeField] private AudioClip _pizzaEatingClip;

        #endregion

        #region Public Attributes

        public static ScoreManager instance;

        [HideInInspector] public int score = 0;

        #endregion

        #region Private Methods

        // Awake is called when the script instance is being loaded.
        private void Awake()
        {
            instance = this;
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
            AudioController.instance.GetComponent<AudioSource>().PlayOneShot(_pizzaEatingClip);
            score += 1;
        }

        #endregion

        #region Public Overriden Methods



        #endregion
    }
}
