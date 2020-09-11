using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platform : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //checks if the object is hitting collider from the top or bottom 
        // if the collided object velocity < 0 that means it is falling 
        if(collision.relativeVelocity.y <= 0f)
        {
            //do cool stuff here
        }
        //gameObject.GetComponent<EdgeCollider2D>().transform.position.y
        if(collision.transform.position.y<= gameObject.GetComponent<EdgeCollider2D>().transform.position.y/*transform.position.y*/)
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(),this.GetComponent<Collider2D>());
        }
    }

   
}
