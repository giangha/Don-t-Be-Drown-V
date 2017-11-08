using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // Crates
    //public GameObject woodCrate;
	//public GameObject metalCrate;

    public GameObject boat;

    public GameObject net;
	public Transform netPoint;
    //public Transform droppingPoint;
    public int crateCount;
    //public float dropRateWood;
	//public float dropRateMetal;
    bool net_drop;
    public AudioSource scoresound;
    public AudioSource dropcrate;
    //AudioSource audioSource;


    // Time
    public float startingTime;
    public GUIText theText;

    // Score
    public GUIText scoreText;
    public int score;

    public GUIText netText;
    public int nets;

    // Hint
    public GUIText hintText;

    // Total crates on boat
    GameObject[] totalCrates;

    public int boat_health = 100;
    public Slider playerHealthSlider;


    void Start()
    {
        score = 0;
        //StartCoroutine(SpawnWave());
        //net_drop = false;
        scoreUpdate();
        //audioSource = GetComponent<AudioSource>();
        nets = 5;
        netUpdate();
        boat_health = 100;
        playerHealthSlider.maxValue = boat_health;
        playerHealthSlider.value = boat_health;

		//InvokeRepeating ("SpawnWoodCrate", dropRateWood, dropRateWood);
		//InvokeRepeating ("SpawnMetalCrate", dropRateMetal, dropRateMetal);
    }

	/*
    // Create crates from above
    IEnumerator SpawnWave()
    {
        // Wait for few second before start dropping crates
        yield return new WaitForSeconds(dropRate);

        // Thuan, you can add sound of dropping crate here
        for (int i = 0; i < crateCount; i++)
        {
            // Create crate and drop it
            dropcrate.Play();
            float x = Random.Range(-3, 3);
            float y = 4.96f;
            float z = 0;
            Vector3 pos = new Vector3(x, y, z);
            droppingPoint.position = pos;
            Instantiate(crate, droppingPoint.position, droppingPoint.rotation);
            yield return new WaitForSeconds(dropRate);
        }
    }
	*/

	/*
	void SpawnWoodCrate ()
	{
		// If the player has no health left...
		if(startingTime <= 0f)
		{
			// ... exit the function.
			return;
		}
		// Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
		Instantiate (woodCrate, droppingPoint.position, droppingPoint.rotation);
	}

	void SpawnMetalCrate ()
	{
		// If the player has no health left...
		if(startingTime <= 0f)
		{
			// ... exit the function.
			return;
		}
		// Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
		Instantiate (metalCrate, droppingPoint.position, droppingPoint.rotation);
	}

	*/
    // Add score to every catched crate
    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        scoreUpdate();
        
    }

    // Add crates to the list to destroy later when unload them at shelter station
    /*public void AddCrate(GameObject newCrate){
		totalCrates.Add (newCrate);
	}*/
    public void Alligator_Damage()
    {
        boat_health = boat_health - 5;
        if(boat_health<=0)
        {
           // SceneManager.LoadScene(3);
            //end game
        }
        playerHealthSlider.value=boat_health;
       // playerHealthSlider.
    }
    void scoreUpdate()
    {
        scoreText.text = "Coins:" + score;
        scoresound.Play();
    }

    void netUpdate()
    {
        netText.text = "Nets:" + nets;
        
    }

    void Update()
    {
        // Time counting
        startingTime -= Time.deltaTime;
        theText.text = Mathf.Round(startingTime).ToString();


        // Unload crates
        if (Input.GetKey(KeyCode.Z))
        {
            if (boat.transform.position.x < -7.5)
            {
                /*for (int i = 0; i <= totalCrates.Count; i++) {
					Destroy (totalCrates [i]);
				}*/
                totalCrates = GameObject.FindGameObjectsWithTag("Crate");
                for (int i = 0; i < totalCrates.Length; i++)
                {
                    AddScore(10);
                    Destroy(totalCrates[i]);
                }
                //hintText.text = "Thank you very muchhhhhhhhhhhhh";
                nets = 5;
                netUpdate();
            }
            else
            {
                hintText.text = "please go to shelter to unload crates";
            }
            return;
        }

		/*
        if (Input.GetKey(KeyCode.X))
        {
            
            if (nets >= 1)
            {
				Rigidbody2D netRid;
				var Clone = Instantiate(net, netPoint.position, netPoint.rotation);
				netRid = Clone.GetComponent<Rigidbody2D> ();
				netRid.AddForce (transform.right * 1000);
				netRid.AddForce (transform.up * 10000);
                net_drop = true;
                nets--;
                netUpdate();
                return;
            }
        }
        */

        if (startingTime <= 0)
        {
            if (score >= 50)
            {
                hintText.text = " You Win";
                SceneManager.LoadScene(3);
            }
            else
            {
                SceneManager.LoadScene(3);
            }
            // Detroy crate when go out boundary
        }
    }


	private float shootTime = 0;
	private float shootRate = 0.5f;
	void FixedUpdate(){
		if (Input.GetKey("up"))
		{

			if (nets >= 1 && Time.time > shootTime)
			{
				shootTime = Time.time + shootRate;
				Rigidbody2D netRid;
				var Clone = Instantiate(net, netPoint.position, netPoint.rotation);
				netRid = Clone.GetComponent<Rigidbody2D> ();
				netRid.AddForce (transform.right * 100);
				netRid.AddForce (transform.up * 100);
				net_drop = true;
				nets--;
				netUpdate();
				return;
			}
		}
	}
 
}
