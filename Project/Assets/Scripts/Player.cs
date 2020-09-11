using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    public float jumpforce = 10f;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayerAlive();
    }

    private void FixedUpdate()
    {
       
    }


    public void Jump(Vector3 destination)
    {
        
        //Debug.Log("jumped to "+destination);
        Vector2 velocity = rb.velocity;
        //calculate how much to jump up (velocity.y)
        float diff_y = destination.y - transform.position.y;
        //the value to multiply by so that you get a proportional force applied 
        //higher numbers if the platform is closer vertically 
        //lower but not small numbers if the platform is higher
        //float mult = (-3.7f * Mathf.Sqrt(diff_y+0f)) + 10;
        float mult = 0;
        if (diff_y < 4)
            mult = -7.3f * Mathf.Pow(diff_y - 0.5f, (1 / 3.8f)) + 12.7f;
            //mult = -7f * Mathf.Pow(diff_y-0.2f, (1 / 3.8f)) + 12.7f;
        else if (diff_y >= 4)
            mult = -Mathf.Sqrt(diff_y + 5) + 5.6f;

        /*
        Debug.Log(diff_y);
        Debug.Log(mult);
        Debug.Log(diff_y * mult);
        */


        //calc how much to jump to the size left or right (velocity.x)
        float diff_x = destination.x - transform.position.x;
        //Debug.Log(diff_x);


        velocity.y = diff_y * mult;
        velocity.x = diff_x/1.35f;
        rb.velocity = velocity;
        
    }

    void CheckPlayerAlive()
    {
        if(transform.position.y<Camera.main.transform.position.y - Camera.main.orthographicSize)
        {
            GameObject.Find("game master").GetComponent<MasterScript>().GameOver();
        }
    }
}
