using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using Valve.VR;

public class GunPrefab : MonoBehaviour
{
  public bool Rifle;
  public bool Shotgun;
  public bool Pistol;

  public int ammoNum;
  public int currentAmmoNum;
  public int bulletNum;

  public int shotgunShotNum;
  public int shotgunRef;
  public bool shotgunCoc;

  public GameObject bulletPrefab;

  public float vertSpread;
  public float horzSpread;

  public float bulletLife;
  public float bulletSpeed;

  public bool isAutomatic;
  public bool isReloading;
  public float shotInbetween;
  public GameObject nozzle;

  public GameObject shotgunPump;
  public float pumpRes;
  public Vector3 pumpRest;

  public SteamVR_Input_Sources hand;
  public NotHand it;


  void Update() {
    //Determine when player wants to reload
    if (!isReloading && currentAmmoNum > 0 && SteamVR_Actions.default_GrabPinch.GetStateDown(SteamVR_Input_Sources.Any)) {
      if(it.leftComponent == this.gameObject || it.rightComponent == this.gameObject) {
        if (Pistol || Rifle) {
          Shoot();
        }
        if (Shotgun && shotgunCoc == false) {
          Shoot();
        }
      }
    }

    //When shotgunpump is not in correct position
    if (shotgunPump.transform.localPosition.x == pumpRest.x + pumpRes && !shotgunCoc) {
      currentAmmoNum -= shotgunRef;
      shotgunShotNum += shotgunRef;
      shotgunCoc = true;
    }
    if (shotgunPump.transform.localPosition.x == pumpRest.x - pumpRes && shotgunCoc) {
      shotgunCoc = false;
    }
  }

  public void Shoot() {
    int shotNum = 0;

    if (Rifle || Pistol) {
    for (int bulletSpawn = 0; bulletSpawn < bulletNum; bulletSpawn++) {
          //If there are no more ammo left
          shotNum ++;
          if (currentAmmoNum <= 0) {
            //Prevent from shooting
            break;
            //Stop automatic weapons if applicable
            StopCoroutine(AutomaticRealoader());
          }
        }
      }
    if (Shotgun) {
        for (int bulletSpawn = 0; bulletSpawn < shotgunShotNum; bulletSpawn++) {
              //If there are no more ammo left
              shotNum ++;
              if (currentAmmoNum <= 0 || shotgunShotNum <= 0) {
                //Prevent from shooting
                break;
                //Stop automatic weapons if applicable
                StopCoroutine(AutomaticRealoader());
        }
      }
    }
    for (int i = 0; i < shotNum; i++) {

          //Determine BulletSpread
          Vector3 bulletRot = bulletTransform(nozzle, horzSpread, vertSpread);

          //Instantiate Object and Debug its rotation (stupid quanternions)
          GameObject bullet = Instantiate(bulletPrefab, nozzle.transform.position, Quaternion.identity);
          bullet.transform.eulerAngles = bulletRot;

          //Set Bullets Values based on gun prefrences
          bullet.GetComponent<Bullet>().lifeTime = bulletLife;
          bullet.GetComponent<Bullet>().speed = bulletSpeed;

          //decrease ammo
          shotgunShotNum --;
    }
    //if the gun is automatic and the player is pressing the shoot button
    if (isAutomatic && SteamVR_Actions.default_GrabPinch.GetState(SteamVR_Input_Sources.Any)) {

      //Repeat shooting
      StartCoroutine(AutomaticRealoader());
    }
  }

  IEnumerator AutomaticRealoader() {

    //wait a specific amount of time
    yield return new WaitForSeconds(shotInbetween);

    //Check if player wants to shooting
    if (isAutomatic && SteamVR_Actions.default_GrabPinch.GetState(SteamVR_Input_Sources.Any)) {
    Shoot();
    }
  }

  public Vector3 bulletTransform(GameObject nozzle, float x, float y) {
    //create a new vector3
    Vector3 currentTransform = new Vector3(0,0,0);

    //add spread to the bullets based in nozzle's angle
    currentTransform += new Vector3(Random.Range(-x, x), Random.Range(-y, y), 0);
    currentTransform += nozzle.transform.eulerAngles;

    //return value
    return currentTransform;
  }

}
