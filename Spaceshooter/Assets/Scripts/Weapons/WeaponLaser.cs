using System;
using UnityEngine;

public class WeaponLaser : Weapon
{
    public GameObject laser;
    private Camera mainCamera;
    private Vector3 playerToMouseOld = Vector3.zero;
    
    private void Awake()
    {
        mainCamera = Camera.main;
        laser = Instantiate(laser);

    }

    public override void DoCleanUp()
    {
        Destroy(laser);
    }

    public override void Shoot()
    {
        //calculate offset from player
        //float r = laser.transform.localScale.y;
        float r = .05f;
        Vector3 mousePos = Input.mousePosition;
        mousePos = mainCamera.ScreenToWorldPoint(mousePos);
        Vector3 weaponPosition = weaponTransform.position;
        weaponPosition.z = 0;
        mousePos.z = 0;
        Vector3 playerToMouse = mousePos - weaponPosition;
        playerToMouse = playerToMouse.normalized * r;
        playerToMouse.z = 0;

        //calculate angle
        float angleOfLaser = Vector3.SignedAngle(Vector3.up, playerToMouse, Vector3.forward);
        if (Mathf.Abs(angleOfLaser) <= 60)
        {
            //set rotation of laser
            laser.transform.rotation = Quaternion.Euler(-90 + angleOfLaser,-90, 0);
            
            //set position of laser
            laser.transform.position = weaponPosition + playerToMouse;
            playerToMouseOld = playerToMouse;

        }
        else
        {
            if (playerToMouseOld == Vector3.zero)
                playerToMouseOld = Vector3.up * r;
            laser.transform.position = weaponPosition + playerToMouseOld;
        }
            

    }
}