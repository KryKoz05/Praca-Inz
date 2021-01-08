using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bullet;

   

   public void Shot()
    {
        Instantiate(bullet, firePoint.position, firePoint.rotation);
    }
}
