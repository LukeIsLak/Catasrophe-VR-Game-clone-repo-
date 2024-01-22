using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryableObject : MonoBehaviour
{
    public float velThreshold;
    public bool weakenObject;


    //On any collisions
    public void OnCollisionEnter (Collision other) {

      //Get the colliding objects Rigidbody
      Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();

      // if the magnitude of the velocity is greater than the threshold,
      if(rb.velocity.magnitude > velThreshold && rb != null && other.gameObject.tag == "bullet") {

        //Destroy this object
        Destroy(this.gameObject);

      }
      //If it does not meet the threshold but does weaken
      else if (weakenObject && rb != null && other.gameObject.tag == "bullet") {

        //Decrease the threshold by the amount it was hit with
        velThreshold -= rb.velocity.magnitude;

        Debug.Log(other);
      }
    }
}
