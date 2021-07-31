using UnityEngine;

namespace com.abhishek.saraf.SnakeEyes
{
    /**
     * This script manages a single tile.
     */
    public class Tile : MonoBehaviour
    {
        #region Private Attributes

        [SerializeField] private Material _greenMat;

        #endregion

        #region Private Methods

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag.Equals("Snake"))
            {
                gameObject.GetComponent<MeshRenderer>().material = _greenMat;
                if (TileSpawner.instance.CheckForGameOver())
                {
                    GameManager.instance.GameOver();
                }
            }
        }

        #endregion
    }
}
