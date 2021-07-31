using UnityEngine;

namespace com.abhishek.saraf.SnakeEyes
{
    /**
     * This script manages a single tile.
     */
    public class Tile : MonoBehaviour
    {
        #region Private Attributes

        // Green material - stored since we need to change the material of the tile as soon as the snake touches one.
        [SerializeField] private Material _greenMat; 

        #endregion

        #region Private Methods

        /// <summary>
        /// Event triggered as soon as the snake touches a tile.
        /// </summary>
        /// <param name="other">the other collider involved in this event. Our focus will be the snake here.</param>
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag.Equals("Snake")) // Only proceed if the 'Snake' touches the tile(s).
            {
                gameObject.GetComponent<MeshRenderer>().material = _greenMat; // Change the tile material on trigger.
                if (TileSpawner.instance.CheckForGameOver()) // check for free tiles.
                {
                    GameManager.instance.GameOver(); // Trigger game over event if no free tiles are left.
                }
            }
        }

        #endregion
    }
}
