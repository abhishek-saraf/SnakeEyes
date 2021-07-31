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

        [SerializeField] private int _pizzaSpawnTime = 5; // time over which the pizza slices will spawn.

        [SerializeField] private int _pizzaStayTime = 5; // time till which a pizza slice pickup will stay in the screen.

        #endregion

        #region Public Attributes

        public static Pickups instance; // declaring singleton instance for this class.

        #endregion

        #region Private Methods

        // Awake is called when the script instance is being loaded.
        private void Awake()
        {
            instance = this; // initializing the singleton instance.
        }

        /// <summary>
        /// Spawn Pizza slice pickups.
        /// </summary>
        private void SpawnPizza()
        {
            GameObject tile = TileSpawner.instance.GetPizzaSpawnLocation(); // get the spawn location over which we will instantiate the pickup.
            if (tile != null) // only instantiate the pickup if the tile is not null.
            {
                Vector3 posToSpawn = tile.transform.position; // store the position to spawn the pickup to.
                // Instantiate the pickup.
                GameObject pizza = Instantiate(Resources.Load<GameObject>(Path.Combine("Prefabs", "PizzaSlice")), posToSpawn, Quaternion.identity, transform);
                Destroy(pizza, _pizzaStayTime); // destroy the pickup after a designated time period, i.e. pizzaStayTime in our case.
            }
        }

        /// <summary>
        /// Event called when the player/snake touches the pickup.
        /// </summary>
        /// <param name="other">the other collider involved in this event. We are concerned about the snake in this case.</param>
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag.Equals("Snake")) // check if the collider's tag = "Snake" & proceed in this case.
            {
                ScoreManager.instance.AddScore(); // increment the player's score.
                Destroy(gameObject); // destroy the pickup after snake touches it.
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Spawn Pizzas on the grid, over a specific time period.
        /// </summary>
        public void SpawnPizzas()
        {
            // This method will be invoked to spawn pizza(s) pickup's every "pizzaSpawnTime" seconds, and will be first invoked in 2.0 seconds.
            InvokeRepeating(nameof(SpawnPizza), 2.0f, _pizzaSpawnTime);
        }

        #endregion
    }
}
