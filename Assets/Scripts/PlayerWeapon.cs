using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerWeapon : MonoBehaviour
{
    public GameObject bulletPrefab;

    private GameObject playerWeapon;
    private GameObject playerWeaponPivotPoint;

    private float weaponOffset = 0.8f;
    private bool isInCooldown = false;

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
        fire();
    }

    private void rotateWeapon()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - playerWeaponPivotPoint.transform.position;
        float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg + 90;
        playerWeaponPivotPoint.transform.rotation = Quaternion.Euler(0f, 0f, rotation_z + weaponOffset);
    }

    private void fire()
    {
        if (Input.GetButton("Fire1"))
        {
            if (isInCooldown) return;

            StartCoroutine(fireWithDelay(.2f));
        }
    }

    private IEnumerator fireWithDelay(float fireCooldown)
    {
        isInCooldown = true;

        Instantiate(bulletPrefab, playerWeapon.transform.position, playerWeaponPivotPoint.transform.rotation);

        yield return new WaitForSeconds(fireCooldown);
        isInCooldown = false;
    }

    public void addAmmo (int nbrOfAmmo)
    {
        Debug.Log("Find " + nbrOfAmmo +" ammo(s) !");

    }
}