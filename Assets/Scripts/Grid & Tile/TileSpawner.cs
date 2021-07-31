using System.Collections;
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

        private int cols, rows;

        [SerializeField] private float _tileSize = 0.237f;

        [SerializeField] private Material _initialMat;

        #endregion

        #region Public Attributes

        public List<GameObject> tiles;

        public static TileSpawner instance;

        #endregion

        #region Private Methods

        // Awake is called when the script instance is being loaded.
        private void Awake()
        {
            instance = this;

            tiles = new List<GameObject>();
        }

        // Start is called before the first frame update
        void Start()
        {
            rows = GameManager.instance.cameraHeight;
            cols = GameManager.instance.cameraWidth;
            InstantiateTiles();

            Pickups.instance.SpawnPizzas();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void InstantiateTiles()
        {
            GameObject referenceTile = Instantiate(Resources.Load<GameObject>(Path.Combine("Prefabs", "Tile")));
            referenceTile.GetComponent<MeshRenderer>().material = _initialMat;

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    GameObject tile = Instantiate(referenceTile, transform);

                    float posX = row * _tileSize;
                    float posZ = col * -_tileSize;

                    tile.transform.position = new Vector3(posX, 0.0f, posZ);

                    tiles.Add(tile);
                }
            }

            Destroy(referenceTile);

            float gridWidth = cols * _tileSize;
            float gridHeight = rows * _tileSize;
            transform.position = new Vector3(-gridHeight / 2 + _tileSize / 2, 0.0f, gridWidth / 2 - _tileSize / 2);
        }

        #endregion

        #region Public Methods

        public GameObject GetPizzaSpawnLocation()
        {
            List<GameObject> freeTiles = new List<GameObject>();

            foreach (GameObject tile in tiles)
            {
                if (tile.GetComponent<MeshRenderer>().material.color.Equals(_initialMat.color))
                {
                    freeTiles.Add(tile);
                }
            }

            if (freeTiles.Count == 0)
            {
                GameManager.instance.GameOver();
            }

            int randomTileIndex = Random.Range(0, freeTiles.Count);

            return freeTiles[randomTileIndex];
        }

        #endregion

        #region Public Overriden Methods



        #endregion
    }
}
