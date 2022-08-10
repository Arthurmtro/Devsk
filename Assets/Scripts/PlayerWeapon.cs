using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    private GameObject playerWeapon;
    private GameObject playerWeaponPivotPoint;

    private float weaponOffset = 0.8f;

    void Start()
    {
        playerWeapon = GameObject.FindGameObjectWithTag("Player Weapon");
        playerWeaponPivotPoint = GameObject.FindGameObjectWithTag("Player Weapon Pivot Point");

        if (playerWeapon == null)
        {
            Debug.Log("playerWeapon is not defined");
            return;
        }


        if (playerWeaponPivotPoint == null)
        {
            Debug.Log("playerWeapon is not defined");
            return;
        }

        playerWeapon.transform.position = new Vector3(transform.position.x, transform.position.y - weaponOffset, transform.position.z );
    }

    void Update()
    {
        rotateWeapon();


        if (Input.GetButton("Fire1"))
        {
            fire();
        }
    }

    private void rotateWeapon()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - playerWeaponPivotPoint.transform.position;
        float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg + 90f;
        playerWeaponPivotPoint.transform.rotation = Quaternion.Euler(0f, 0f, rotation_z + weaponOffset);
    }

    private void fire()
    {
        Debug.Log("FIre");
    }
}
