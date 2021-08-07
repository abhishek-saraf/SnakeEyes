using System.Collections.Generic;
using System.IO;

using UnityEngine;

namespace com.abhishek.saraf.SnakeEyes
{
    /**
     * This script is responsible for spawning the tiles.
     */
    public class TileSpawner : MonoBehaviour
    {
        #region Private Attributes

        private int cols, rows; // variable to store the columns & rows - will be used to instantiate the tiles dynamically.

        [SerializeField] private float _tileSize = 0.237f; // default tile size.

        [SerializeField] private Material _initialMat; // reference for the initial tile material.

        #endregion

        #region Public Attributes

        public List<GameObject> tiles; // list to hold all the tiles.

        public static TileSpawner instance; // declaring the singleton instance.

        #endregion

        #region Private Methods

        // Awake is called when the script instance is being loaded.
        private void Awake()
        {
            instance = this; // initializing the singleton instance.

            tiles = new List<GameObject>(); // initializing the list for storing all the tiles.
        }

        // Start is called before the first frame update
        void Start()
        {
            rows = GameManager.instance.cameraHeight; // initialize the rows.
            cols = GameManager.instance.cameraWidth; // initialize the columns.
            InstantiateTiles(); // Instantiate the tiles dynamically.

            Pickups.instance.SpawnPizzas(); // Spawn the pizza slice pickup items.
        }

        /// <summary>
        /// Instantiate the tiles dynamically.
        /// </summary>
        private void InstantiateTiles()
        {
            GameObject referenceTile = Instantiate(Resources.Load<GameObject>(Path.Combine("Prefabs", "Tile"))); // store the reference tile from prefabs.
            referenceTile.GetComponent<MeshRenderer>().material = _initialMat; // update the material for the reference tile.

            // Generate the tiles dynamically & store them in the "tiles" list.
            for (int row = 0; row < rows; row++) // iterate through the available rows.
            {
                for (int col = 0; col < cols; col++) // iterate through the available columns.
                {
                    GameObject tile = Instantiate(referenceTile, transform); // Instantiate the tile.

                    float posX = row * _tileSize; // calculate and store the value for the 'X' position of the tile.
                    float posZ = col * -_tileSize; // calculate and store the value for the 'Z' position of the tile.

                    // update the position of the tile's transform based on above calculated values.
                    tile.transform.position = new Vector3(posX, 0.0f, posZ);

                    tiles.Add(tile); // add the tile to the list.
                }
            }

            Destroy(referenceTile); // once all the tiles are added, destroy the reference tile.

            float gridWidth = cols * _tileSize; // calculate and store the value for the new gridWidth.
            float gridHeight = rows * _tileSize; // calculate and store the value for the new gridHeight.

            // update the grid's position based on above calculated values.
            transform.position = new Vector3(-gridHeight / 2 + _tileSize / 2, 0.0f, gridWidth / 2 - _tileSize / 2);
        }

        /// <summary>
        /// Get the list of free tiles.
        /// free tiles = tiles whose color = initial color(white; not green/yet touched by the snake).
        /// </summary>
        /// <returns> return the list of free tiles - used for spawning the pizza slices.</returns>
        private List<GameObject> GetFreeTiles()
        {
            List<GameObject> freeTiles = new List<GameObject>();

            foreach (GameObject tile in tiles)
            {
                if (tile.GetComponent<MeshRenderer>().material.color.Equals(_initialMat.color))
                {
                    freeTiles.Add(tile);
                }
            }

            return freeTiles;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Based on the list of free tiles, get the random pizza slice spawn location.
        /// free tiles = tiles whose color = initial color(white; not green/yet touched by the snake).
        /// </summary>
        /// <returns>return a random pizza slice spawn location.</returns>
        public GameObject GetPizzaSpawnLocation()
        {
            List<GameObject> freeTiles = GetFreeTiles();

            if (freeTiles.Count == 0)
            {
                return null;
            }

            int randomTileIndex = Random.Range(0, freeTiles.Count); // get the random index from the list of free tiles.

            return freeTiles[randomTileIndex]; // return the random tile from list of free tiles.
        }

        /// <summary>
        /// Check if the game is over.
        /// </summary>
        /// <returns>returns true if no free tile is left.</returns>
        public bool CheckForGameOver()
        {
            List<GameObject> freeTiles = GetFreeTiles();

            if (freeTiles.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion
    }
}
