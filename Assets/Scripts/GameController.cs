using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // Crates
    public GameObject crate;
    public GameObject boat;
    public GameObject net;
    public Transform droppingPoint;
    public int crateCount;
    public float dropRate;
    bool net_drop;
    public AudioSource scoresound;
    public AudioSource dropcrate;
    AudioSource audioSource;


    // Time
    public float startingTime;
    public GUIText theText;

    // Score
    public GUIText scoreText;
    public int score;

    // Hint
    public GUIText hintText;

    // Total crates on boat
    GameObject[] totalCrates;

    void Start()
    {
        score = 0;
        StartCoroutine(SpawnWave());
        net_drop = false;
        scoreUpdate();
        audioSource = GetComponent<AudioSource>();
    }

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

    void scoreUpdate()
    {
        scoreText.text = "Coins :" + score;
        scoresound.Play();
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
                net_drop = false;
            }
            else
            {
                hintText.text = "please go to shelter to unload crates";
            }
            return;
        }

        if (Input.GetKey(KeyCode.X))
        {
            if (!net_drop)
            {
                float x = 0;
                float y = 4.96f;
                float z = 0;
                Vector3 pos = new Vector3(x, y, z);
                droppingPoint.position = pos;
                Instantiate(net, droppingPoint.position, droppingPoint.rotation);
                net_drop = true;
                return;
            }
        }

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

 
}
