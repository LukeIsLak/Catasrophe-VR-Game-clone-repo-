using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class reloadGun : MonoBehaviour
{

  public bool Rifle, Pistol, Shotgun;
  public GunPrefab gun;
  public GameObject magazineGun;
  public GameObject magazinePrefab;
  public GameObject shotgunSlot;

  public bool isRange;
  public GameObject closeMagazine;
  public NotHand hand;

  // Testing at Home
  public void Update() {
    if(SteamVR_Actions.default_Reload.GetStateDown(SteamVR_Input_Sources.Any)) {
      unReload();
    }
  }
  public void LateUpdate() {
    if(SteamVR_Actions.default_GrabGrip.GetStateDown(SteamVR_Input_Sources.Any) && isRange == true) {
      Reload();
    }
  }

  //on unload
  public void unReload() {
    Debug.Log("Unloading");
    // For guns that require a magazine
    if (Rifle == true || Pistol == true) {

      //If the visual effect of the magazine is active
      if (magazineGun.activeSelf) {

        // deactivate it
        magazineGun.SetActive(false);

        //make a magazine copy, with the remaining ammo in the gun left
        GameObject magazineCopy = Instantiate(magazinePrefab, magazineGun.transform.position, magazineGun.transform.rotation);
        magazineCopy.GetComponent<magazine>().ammoReserve = gun.currentAmmoNum;

        if (SteamVR_Actions.default_GrabPinch.GetStateDown(SteamVR_Input_Sources.RightHand) && hand.leftComponent == null) {
          hand.leftComponent = magazineCopy;
          magazineCopy.transform.parent = hand.leftHand.transform;
          magazineCopy.transform.localPosition = new Vector3(0,0,0);
          magazineCopy.transform.localEulerAngles = new Vector3(0,0,0) + magazineCopy.GetComponent<isInteractable>().rotationOffset;

          magazineCopy.GetComponent<isInteractable>().col.enabled = false;

          Rigidbody rb = magazineCopy.GetComponent<Rigidbody>();
          rb.useGravity = false;
          rb.velocity = new Vector3(0,0,0);
          }

        if (SteamVR_Actions.default_GrabPinch.GetStateDown(SteamVR_Input_Sources.LeftHand) && hand.rightComponent == null) {
          hand.rightComponent = magazineCopy;
          magazineCopy.transform.parent = hand.rightHand.transform;
          magazineCopy.transform.localPosition = new Vector3(0,0,0);
          magazineCopy.transform.localEulerAngles = new Vector3(0,0,0) + magazineCopy.GetComponent<isInteractable>().rotationOffset;

          magazineCopy.GetComponent<isInteractable>().col.enabled = false;

          Rigidbody rb = magazineCopy.GetComponent<Rigidbody>();
          rb.useGravity = false;
          rb.velocity = new Vector3(0,0,0);
          }
        //make the gun empty
        gun.currentAmmoNum = 0;
      }
    }
  }

  //on realod
  public void Reload() {
    //for guns that require magazines
      if (Rifle == true || Pistol == true) {

        //if the magazine is not active
        if (!magazineGun.activeSelf) {

          //add ammo to the gun
          gun.currentAmmoNum += closeMagazine.GetComponent<magazine>().ammoReserve;

          //prevents the gun from holding more that it should
          if (gun.currentAmmoNum > gun.ammoNum) {
            gun.currentAmmoNum = gun.ammoNum;
          }

          //Destroy the copy magazine
          Destroy(closeMagazine);

          //make the magazine active
          magazineGun.SetActive(true);
        }
      }
      if (Shotgun) {

        //if the magazine is not active
        if (!shotgunSlot.activeSelf) {

          //add ammo to the gun
          gun.currentAmmoNum += closeMagazine.GetComponent<magazine>().ammoReserve;

          //prevents the gun from holding more that it should
          if (gun.currentAmmoNum > gun.ammoNum) {
            gun.currentAmmoNum = gun.ammoNum;
          }
          if (gun.shotgunShotNum == 0) {
            gun.currentAmmoNum -= closeMagazine.GetComponent<magazine>().ammoReserve;
            gun.shotgunShotNum += closeMagazine.GetComponent<magazine>().ammoReserve;
          }

          //Destroy the copy magazine
          Destroy(closeMagazine);

          //make the magazine active
          shotgunSlot.SetActive(true);
        }
      }
  }

  //when mag in range
  public void OnTriggerEnter (Collider other) {
    //Check the mag component
    magazine mag = other.GetComponent<magazine>();

    //If there is a mag nearby
    if(mag != null) {

      //Set ocmponent true
      if (Rifle && mag.Rifle) {
      isRange = true;

      //get the magazine component
      closeMagazine = other.gameObject;
      }
      //Set ocmponent true
      if (Pistol && mag.Pistol) {
      isRange = true;

      //get the magazine component
      closeMagazine = other.gameObject;
      }
      //Set ocmponent true
      if (Shotgun && mag.Shotgun && gun.shotgunCoc) {
      isRange = true;

      //get the magazine component
      closeMagazine = other.gameObject;
      }

      //opens shotgun
      if (Shotgun && mag.Shotgun == true) {
        shotgunSlot.SetActive(false);
      }
    }

    //If there isnt a mag nearby
    else {

      //Set componetent false
      isRange = false;

      //remove any magazine components
      closeMagazine = null;

      //closes shotgun
      if (Shotgun) {
        shotgunSlot.SetActive(true);
      }
    }
  }

  // Closes everything on exit
  public void OnTriggerExit (Collider other) {
    isRange = false;
    closeMagazine = null;
    if (Shotgun) {
      shotgunSlot.SetActive(true);
    }
  }
}
