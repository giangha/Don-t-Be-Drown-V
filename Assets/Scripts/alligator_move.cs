using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alligator_move : MonoBehaviour
{
    public GameObject aligator;
    public Transform target;//set target from inspector instead of looking in Update
    public float speed;
    private GameController gameController;
    // Use this for initialization
    private Animator amin;
    public bool caught;
    public bool angry;
    public bool sleep;
    public float TimeInNet;
    private Rigidbody2D rb2d;
    private float pushforce = 250f;

    void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();

        GameObject gameControllerObject = GameObject.FindGameObjectWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        amin = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        amin.SetBool("angry", angry);
        amin.SetBool("sleep", sleep);
        amin.SetBool("caught", caught);
         target = GameObject.FindWithTag("Boat").transform;

         float step = speed * Time.deltaTime;
         Vector3 offset = new Vector3(0, -.5f, 0);
         Vector3 targetHeading = target.position + offset;
         Vector3 targetDirection = targetHeading.normalized;
         float old_x_location = transform.position.x;
         transform.position = Vector3.MoveTowards(transform.position, targetHeading, step);
         float new_x_location = targetHeading.x;


         float difference_between_locations = old_x_location - new_x_location;
         if (difference_between_locations < 0) difference_between_locations = difference_between_locations * -1;
         if (difference_between_locations < .01)
         {
             pushBack();
             angry = true;
             gameController.Alligator_Damage();
             
            // aligator.gameObject.SetActive(false);
         }

        //   Vector3 position = new Vector3(Random.Range(-1, 1), 0, 0);
        //  transform.Translate(Vector3.MoveTowards(Vector3.curr* Time.deltaTime, Camera.main.transform);
    }

    
    void OnTriggerEnter2D(Collider2D other)
    {
       if (other.gameObject.CompareTag("Net"))
       {
            //Invoke("Reappear", 15);
           // aligator.gameObject.SetActive(false);
            other.gameObject.SetActive(false);
            caught = true;
            speed = .1f;
            Invoke("unCaught", 5);
        }
        if (other.gameObject.CompareTag("Boat")) {
            angry = true; }
       
      //  if (other.gameObject.CompareTag("boat_area"))
      //  {
            
           // other.gameObject.SetActive(false);
       // }
       
    }


    void unCaught()
    {
        caught = false;
        speed = 5f;
    }

    void pushBack()
    {
        
       
        rb2d.AddForce(Vector2.right * pushforce);
        Invoke("Reappear", 15);
        
    }


    void Reappear()
        {
        CancelInvoke();
        gameObject.SetActive(true); 
        rb2d.Sleep();
        angry = false;
        
        Vector3 position = new Vector3(5.29f,-2.81f,0);
        transform.position = position;
        
        }
        
}
