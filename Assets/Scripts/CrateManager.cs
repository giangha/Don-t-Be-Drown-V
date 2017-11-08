using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateManager : MonoBehaviour {

	public int time;       // game time
	public GameObject woodCrate;                // The wood crate
	public GameObject metalCrate;                // The metal crate
	public float woodSpawnTime;
	public float metalSpawnTime;              // How long between each spawn.
	public Transform spawnPoint;         // An array of the spawn points this enemy can spawn from.


	void Start ()
	{
		// Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
		InvokeRepeating ("woodSpawn", woodSpawnTime, woodSpawnTime);
		InvokeRepeating ("metalSpawn", metalSpawnTime,metalSpawnTime);
	}


	void woodSpawn ()
	{
		// If the player has no health left...
		if(time <= 0f)
		{
			// ... exit the function.
			return;
		}
		// Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
		Instantiate (woodCrate, spawnPoint.position, spawnPoint.rotation);
	}

	void metalSpawn ()
	{
		// If the player has no health left...
		if(time <= 0f)
		{
			// ... exit the function.
			return;
		}
		// Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
		Instantiate (metalCrate, spawnPoint.position, spawnPoint.rotation);
	}
}
