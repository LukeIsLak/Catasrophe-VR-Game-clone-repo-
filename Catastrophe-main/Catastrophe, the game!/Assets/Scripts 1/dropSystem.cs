using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dropSystem : MonoBehaviour
{
    public List<GameObject> possibleDrops;

    //When Destroyed
    public void OnDestroy() {
      //Give a range of posible oebjcts
      int i = Random.Range(0, 100);

      //Spawn one at random
      if (possibleDrops[i] != null) {
        GameObject drop = Instantiate(possibleDrops[i], transform.position, Quaternion.identity);
      }
    }
}
