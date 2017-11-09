using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_control_test : MonoBehaviour {
    public Rigidbody2D rb;
    public float speed;
    public bool scare;
    private Animator amin;

	public GameObject net;
	public Transform netDropPoint;

	private bool toTheRight;

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
			toTheRight = true;
		}
		else if(Input.GetKey("left")){
			transform.localScale = new Vector3(1,1,1);	
			toTheRight = false;
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

	private float shootTime = 0;
	private float shootRate = 0.5f;

	void FixedUpdate(){
		if (Input.GetKey("up"))
		{

			if (Time.time > shootTime)
			{
				shootTime = Time.time + shootRate;
				Rigidbody2D netRid;
				var Clone = Instantiate(net, netDropPoint.position, netDropPoint.rotation);
				netRid = Clone.GetComponent<Rigidbody2D> ();
				if (toTheRight == true || Input.GetKey("right")) {
					netRid.AddForce (transform.right * 300);
				} else {
					netRid.AddForce (transform.right * -300);
				}
				netRid.AddForce (transform.up * 300);
				//net_drop = true;
				//nets--;
				//netUpdate();
				return;
			}
		}
	}

}
