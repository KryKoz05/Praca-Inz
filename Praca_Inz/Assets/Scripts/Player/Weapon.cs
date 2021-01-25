using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;

   

   public void Shot()
    {
        ObjectPooler.Instance.PoolSpawner("Bullet", firePoint.position, firePoint.rotation);
    }
}
