using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateManager : MonoBehaviour {

	public int time;       // Reference to the player's heatlh.
	public GameObject crate;                // The enemy prefab to be spawned.
	public float spawnTime = 3f;            // How long between each spawn.
	public Transform spawnPoint;         // An array of the spawn points this enemy can spawn from.


	void Start ()
	{
		// Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
		InvokeRepeating ("Spawn", spawnTime, spawnTime);
	}


	void Spawn ()
	{
		// If the player has no health left...
		if(time <= 0f)
		{
			// ... exit the function.
			return;
		}
		// Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
		Instantiate (crate, spawnPoint.position, spawnPoint.rotation);
	}
}
