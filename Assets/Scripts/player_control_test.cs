using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_control_test : MonoBehaviour {
    public Rigidbody2D rb;
    public float speed;
    public bool scare;
    private Animator amin;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        amin = gameObject.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        amin.SetFloat("speed", Mathf.Abs(Input.GetAxis("Horizontal")));
        amin.SetBool("scare", scare);
        float moveHorizontal = Input.GetAxis("Horizontal");
		if(Input.GetKey("right")) {
			transform.localScale = new Vector3(-1,1,1);
		}
		else if(Input.GetKey("left")){
			transform.localScale = new Vector3(1,1,1);	
		}

        Vector2 move = new Vector2(moveHorizontal, 0.0f);
        rb.velocity = move * speed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Alligator"))
        {
            scare = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Alligator"))
        {
            scare = true;
        }
    }
}
