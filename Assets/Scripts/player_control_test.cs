using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_control_test : MonoBehaviour {
    public Rigidbody2D rb;
    public float speed;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {

        float moveHorizontal = Input.GetAxis("Horizontal");

        Vector2 move = new Vector2(moveHorizontal, 0.0f);
        rb.velocity = move * speed;
    }
}
