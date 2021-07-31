using System.IO;

using UnityEngine;

namespace com.abhishek.saraf.SnakeEyes
{
    /**
     * This script is responsible for spawning the pizza slices, and managing player pickups.
     */
    public class Pickups : MonoBehaviour
    {
        #region Private Attributes

        [SerializeField] private int _pizzaSpawnTime = 5;

        [SerializeField] private int _pizzaStayTime = 5;

        #endregion

        #region Public Attributes

        public static Pickups instance;

        #endregion

        #region Private Methods

        // Awake is called when the script instance is being loaded.
        private void Awake()
        {
            instance = this;
        }

        private void SpawnPizza()
        {
            GameObject tile = TileSpawner.instance.GetPizzaSpawnLocation();
            if (tile != null)
            {
                Vector3 posToSpawn = tile.transform.position;
                GameObject pizza = Instantiate(Resources.Load<GameObject>(Path.Combine("Prefabs", "PizzaSlice")), posToSpawn, Quaternion.identity, transform);
                Destroy(pizza, _pizzaStayTime);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag.Equals("Snake"))
            {
                ScoreManager.instance.AddScore();
                Destroy(gameObject);
            }
        }

        #endregion

        #region Public Methods

        public void SpawnPizzas()
        {
            InvokeRepeating(nameof(SpawnPizza), 2.0f, _pizzaSpawnTime);
        }

        #endregion
    }
}
