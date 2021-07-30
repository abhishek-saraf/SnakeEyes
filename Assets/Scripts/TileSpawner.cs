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

        private int _totalTiles;

        private Vector3 _tileScale;

        private int cols, rows;

        [SerializeField] private float _tileSize = 0.237f;

        #endregion

        #region Public Attributes

        public List<GameObject> tiles;

        #endregion

        #region Private Methods

        // Awake is called when the script instance is being loaded.
        private void Awake()
        {
            _tileScale = new Vector3(0.237f, 0.001f, 0.237f);
            tiles = new List<GameObject>();
        }

        // Start is called before the first frame update
        void Start()
        {
            rows = GameManager.instance.cameraHeight;
            cols = GameManager.instance.cameraWidth;
            _totalTiles = rows * cols;
            InstantiateTiles();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void InstantiateTiles()
        {
            GameObject referenceTile = Instantiate(Resources.Load<GameObject>(Path.Combine("Prefabs", "Tile")));
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    Debug.Log("Hey!");
                    // GameObject tile = Instantiate(Resources.Load<GameObject>(Path.Combine("Prefabs", "Tile")), new Vector3(), Quaternion.identity, transform);

                    GameObject tile = Instantiate(referenceTile, transform);

                    float posX = col * _tileSize;
                    float posZ = row * -_tileSize;

                    tile.transform.position = new Vector3(posX, 0.0f, posZ);
                }
            }

            Destroy(referenceTile);

            // float gridWidth = cols * _tileSize;
            // float gridHeight = rows * _tileSize;
            // transform.position = new Vector3(-gridWidth / 2 + _tileSize / 2, gridHeight / 2 - _tileSize /2);
        }

        #endregion

        #region Public Methods



        #endregion

        #region Public Overriden Methods



        #endregion
    }
}
