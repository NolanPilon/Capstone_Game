using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttack : MonoBehaviour
{
    public GameObject projectile;
    public Transform firePostion;
  
   public void ShootBullet()
    {
        Instantiate(projectile, firePostion.position,firePostion.rotation); 
    }
        
}
