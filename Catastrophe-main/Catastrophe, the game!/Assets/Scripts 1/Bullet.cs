using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime;
    public float speed;

    void Update() {
        //Decrease Lifetime
        lifeTime --;

        //Update position
        transform.position += transform.forward * speed * Time.deltaTime;

        //If position is less than 0
        if (lifeTime <= 0) {

          //Destroy the gameObject
          Destroy(this.gameObject);
        }
    }
    public void OnCollisionEnter(Collision other) {
      Destroy(this.gameObject);
    }
}
