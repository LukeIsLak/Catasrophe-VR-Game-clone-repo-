using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isInteractable : MonoBehaviour
{
public GameObject interaction;
public Vector3 rotationOffset;
public Collider col;

public void Update() {
  //Fixes mistakes on NotHand
  if (this.gameObject.transform.parent == interaction.GetComponent<NotHand>().leftHand) {
    transform.position = interaction.GetComponent<NotHand>().leftHand.transform.position;
    transform.eulerAngles = interaction.GetComponent<NotHand>().leftHand.transform.eulerAngles + rotationOffset;

    if (transform.position != interaction.GetComponent<NotHand>().leftHand.transform.position || transform.rotation != interaction.GetComponent<NotHand>().leftHand.transform.rotation) {
        transform.position = interaction.GetComponent<NotHand>().leftHand.transform.position;
        transform.eulerAngles = interaction.GetComponent<NotHand>().leftHand.transform.eulerAngles + rotationOffset;
    }
    }
    else if(interaction.GetComponent<NotHand>().leftComponent != null && this.gameObject.transform.parent != interaction.GetComponent<NotHand>().leftHand && interaction.GetComponent<NotHand>().leftComponent == this.gameObject) {
      interaction.GetComponent<NotHand>().leftComponent = null;
    }
  if (this.gameObject.transform.parent == interaction.GetComponent<NotHand>().rightHand) {
      transform.position = interaction.GetComponent<NotHand>().rightHand.transform.position;
      transform.eulerAngles = interaction.GetComponent<NotHand>().rightHand.transform.eulerAngles + rotationOffset;

      if (transform.position != interaction.GetComponent<NotHand>().rightHand.transform.position || transform.rotation != interaction.GetComponent<NotHand>().rightHand.transform.rotation) {
        transform.position = interaction.GetComponent<NotHand>().rightHand.transform.position;
        transform.eulerAngles = interaction.GetComponent<NotHand>().rightHand.transform.eulerAngles + rotationOffset;
      }
    }
  }
}
