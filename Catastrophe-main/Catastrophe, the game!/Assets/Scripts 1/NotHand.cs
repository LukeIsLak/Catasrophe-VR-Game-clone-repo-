using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class NotHand : MonoBehaviour {

public GameObject cameraRig;
public GameObject Camera;

public GameObject leftHand;
public GameObject leftComponent;
public GameObject rightHand;
public GameObject rightComponent;

public float teleportRange;
public float snapTurn;
public float grabRange;

public Material hovering;
public Material baseMat;

public List<GameObject> objInRange = new List<GameObject>();

public bool hasPump;
public GameObject pump;

  void Update() {

    //Cast raycats from hands
    RaycastHit hitLeft;
    RaycastHit hitRight;
    Ray raycastLeft = new Ray(leftHand.transform.position, leftHand.transform.forward);
    Ray raycastRight = new Ray(rightHand.transform.position, rightHand.transform.forward);

    //If the raycast hits something, teleport to that something
    if (Physics.Raycast(leftHand.transform.position, leftHand.transform.TransformDirection(Vector3.forward), out hitLeft, grabRange)) {
      if (hitLeft.transform.gameObject.GetComponent<isInteractable>() != null) {
      GameObject inRange = hitLeft.transform.gameObject;
      if (!objInRange.Contains(inRange)) {
        objInRange.Add(inRange);
      }

      //Highlight object children
      foreach(Transform child in inRange.transform) {
        if (child.gameObject.name != "Nozzle") {
          child.gameObject.GetComponent<MeshRenderer>().material = hovering;
          }
        }

      //Highlight current inRange object
      if (inRange.GetComponent<MeshRenderer>() != null) {
        inRange.gameObject.GetComponent<MeshRenderer>().material = hovering;
        }
      }
    }

    //Determines Object in range
    if (Physics.Raycast(rightHand.transform.position, rightHand.transform.TransformDirection(Vector3.forward), out hitRight, grabRange)) {
      if (hitRight.transform.gameObject.GetComponent<isInteractable>() != null) {
      GameObject inRange = hitRight.transform.gameObject;
      if (!objInRange.Contains(inRange)) {
        objInRange.Add(inRange);
      }
      foreach(Transform child in inRange.transform) {
        if (child.gameObject.name != "Nozzle") {
          child.gameObject.GetComponent<MeshRenderer>().material = hovering;
            }
          }
      if (inRange.GetComponent<MeshRenderer>() != null) {
        inRange.gameObject.GetComponent<MeshRenderer>().material = hovering;
          }
        }
    }
      //If player if inputing manual turning
      if(SteamVR_Actions.default_SnapTurnLeft.GetStateDown(SteamVR_Input_Sources.RightHand)) {
            Camera.transform.eulerAngles -= new Vector3(0, snapTurn, 0);
        }
      if(SteamVR_Actions.default_SnapTurnRight.GetStateDown(SteamVR_Input_Sources.RightHand)) {
            Camera.transform.eulerAngles += new Vector3(0, snapTurn, 0);
        }
      if(SteamVR_Actions.default_SnapTurnLeft.GetStateDown(SteamVR_Input_Sources.LeftHand)) {
            Camera.transform.eulerAngles -= new Vector3(0, snapTurn, 0);
        }
      if(SteamVR_Actions.default_SnapTurnRight.GetStateDown(SteamVR_Input_Sources.LeftHand)) {
            Camera.transform.eulerAngles += new Vector3(0, snapTurn, 0);
        }

      //When the player teleports
      if(SteamVR_Actions.default_Teleport.GetStateDown(SteamVR_Input_Sources.Any)) {
        Debug.Log("Teleport");
        if(SteamVR_Actions.default_Teleport.GetStateDown(SteamVR_Input_Sources.LeftHand)) {
            teleport(leftHand);
        }
        if(SteamVR_Actions.default_Teleport.GetStateDown(SteamVR_Input_Sources.RightHand)) {
            teleport(rightHand);
        }
      }

      //When the player is trying to grab something
      if(SteamVR_Actions.default_GrabPinch.GetStateDown(SteamVR_Input_Sources.Any)) {
        Debug.Log("Throwing");
        if(SteamVR_Actions.default_GrabPinch.GetStateDown(SteamVR_Input_Sources.LeftHand) && leftComponent == null) {
            grabObject(leftHand);
        }
        if(SteamVR_Actions.default_GrabPinch.GetStateDown(SteamVR_Input_Sources.RightHand) && rightComponent == null) {
            grabObject(rightHand);
        }
      }

      //when the player is trying to throw something
      if(SteamVR_Actions.default_GrabGrip.GetStateDown(SteamVR_Input_Sources.Any)) {
        Debug.Log("Grabbing Object");
        if(SteamVR_Actions.default_GrabGrip.GetStateDown(SteamVR_Input_Sources.LeftHand)) {
            throwObject(leftHand);
        }
        if(SteamVR_Actions.default_GrabGrip.GetStateDown(SteamVR_Input_Sources.RightHand)) {
            throwObject(rightHand);
        }
      }
  }
  public void LateUpdate() {
    if (hasPump) {
      float pumpRes = pump.transform.parent.GetComponent<GunPrefab>().pumpRes;
      Vector3 pumpRest = pump.transform.parent.GetComponent<GunPrefab>().pumpRest;
      if (pump == leftComponent) {
        pump = leftComponent;
      }
      if (pump == rightComponent) {
        pump = rightComponent;
      }
      if (pump.transform.localPosition.x >= pumpRest.x + pumpRes || pump.transform.localPosition.x <= pumpRest.x - pumpRes || pump.transform.localPosition.y >= 0.1f || pump.transform.localPosition.y <= -0.1f || pump.transform.localPosition.z >=0.1f || pump.transform.localPosition.z <= -0.1f) {
        pump.transform.localPosition = new Vector3((pump.transform.localPosition.x >= pumpRest.x + pumpRes)? pumpRest.x + pumpRes: pumpRest.x - pumpRes, 0, 0);
        pump.transform.localEulerAngles = new Vector3(-90,0,0);
        pump.transform.localPosition = new Vector3((pump.transform.localPosition.x >= pumpRest.x + pumpRes)? pumpRest.x + pumpRes: pumpRest.x - pumpRes, 0.1993969f, 0.2f);
        pump.transform.localEulerAngles = new Vector3(-90,0,0);
        }
      }
    foreach(GameObject g in objInRange) {
      Debug.Log("Test");
      RaycastHit hitLeft;
      RaycastHit hitRight;
      Ray raycastLeft = new Ray(leftHand.transform.position, leftHand.transform.forward);
      Ray raycastRight = new Ray(rightHand.transform.position, rightHand.transform.forward);

      //If the raycast hits something, teleport to that something
      if (Physics.Raycast(leftHand.transform.position, leftHand.transform.TransformDirection(Vector3.forward), out hitLeft, grabRange)) {
        if (hitLeft.transform.gameObject.GetComponent<isInteractable>() != null) {
          if (hitLeft.transform.gameObject != g) {
        GameObject inRange = g;
        objInRange.Remove(g);
        foreach(Transform child in g.transform) {
          if (child.gameObject.name != "Nozzle") {
            child.gameObject.GetComponent<MeshRenderer>().material = baseMat;
              }
            }
          }
        }
      }
      if (Physics.Raycast(rightHand.transform.position, rightHand.transform.TransformDirection(Vector3.forward), out hitRight, grabRange)) {
        if (hitRight.transform.gameObject.GetComponent<isInteractable>() != null) {
          if (hitRight.transform.gameObject != g) {
        GameObject inRange = g;
        objInRange.Remove(g);
        foreach(Transform child in g.transform) {
          if (child.gameObject.name != "Nozzle") {
            child.gameObject.GetComponent<MeshRenderer>().material = baseMat;
                }
              }
            }
          }
      }
    }
  }

  //On teleport
  public void teleport(GameObject inputHand) {

    //Cast a raycast
    RaycastHit hit;
    Ray raycast = new Ray(inputHand.transform.position, inputHand.transform.forward);

    //If the raycast hits something, teleport to that something
    if (Physics.Raycast(inputHand.transform.position, inputHand.transform.TransformDirection(Vector3.forward), out hit, teleportRange)) {
      cameraRig.transform.position = hit.point;
    }
  }

  //On throw
  public void throwObject(GameObject inputHand) {

    //When the object is not null, remove it from the components
    if (inputHand == leftHand && leftComponent != null) {

      //unlink component
      leftComponent.transform.parent = null;

      //apply removed components
      Rigidbody rb = leftComponent.GetComponent<Rigidbody>();
      leftComponent.GetComponent<isInteractable>().col.enabled = true;

      //remove component
      leftComponent = null;

      //apply rigidbody
      rb.useGravity = true;
      rb.velocity += leftHand.GetComponent<Rigidbody>().velocity;
    }

    //When the object is not null, remove it from the components
    if (inputHand == rightHand && rightComponent != null) {

      //unlink component
      rightComponent.transform.parent = null;

      //apply removed components
      Rigidbody rb = rightComponent.GetComponent<Rigidbody>();
      rightComponent.GetComponent<isInteractable>().col.enabled = true;

      //remove component
      rightComponent = null;

      //apply rigidbody
      rb.useGravity = true;
      rb.velocity += rightHand.GetComponent<Rigidbody>().velocity;
    }
  }

  //On Grab
  public void grabObject(GameObject inputHand) {

    //Cast raycast
    RaycastHit hit;
    Ray raycast = new Ray(inputHand.transform.position, inputHand.transform.forward);
    Debug.DrawRay(inputHand.transform.position, inputHand.transform.forward);

    //If it hits a interactable gameobject && is not a magazine
    if (Physics.Raycast(inputHand.transform.position, inputHand.transform.TransformDirection(Vector3.forward), out hit, grabRange) && hit.transform.gameObject.GetComponent<isInteractable>() != null) {
      Debug.Log("Object in Range");
      if (hit.transform.gameObject.name != "pump") {

      GameObject grabObj = hit.transform.gameObject;

      //Apply it to the hand
      if (inputHand == leftHand) {
        leftComponent = grabObj;
        grabObj.transform.parent = leftHand.transform;
        grabObj.transform.localPosition = new Vector3(0,0,0);
        grabObj.transform.localEulerAngles = new Vector3(0,0,0) + grabObj.GetComponent<isInteractable>().rotationOffset;

        grabObj.GetComponent<isInteractable>().col.enabled = false;

        Rigidbody rb = grabObj.GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.velocity = new Vector3(0,0,0);
        }

      if (inputHand == rightHand) {
        rightComponent = grabObj;
        grabObj.transform.parent = rightHand.transform;
        grabObj.transform.localPosition = new Vector3(0,0,0);
        grabObj.transform.localEulerAngles = new Vector3(0,0,0) + grabObj.GetComponent<isInteractable>().rotationOffset;

        grabObj.GetComponent<isInteractable>().col.enabled = false;

        Rigidbody rb = grabObj.GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.velocity = new Vector3(0,0,0);
        }
      //If it is a magazine, apply the unReload function
      }
      else {
        if (hit.transform.gameObject.name == "pump") {
        hasPump = true;
        if (inputHand == leftHand) {
          leftComponent = hit.transform.gameObject;
          }

        if (inputHand == rightHand) {
          rightComponent = hit.transform.gameObject;
          }
        }
      }
    }
  }
}
