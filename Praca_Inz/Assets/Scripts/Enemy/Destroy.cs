using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    
    public Transform weakPoint;
    public float weakPointRad = 1.5f;
    public LayerMask isPlayer;


    private void FixedUpdate()
    {
        Collider2D colide = Physics2D.OverlapCircle(weakPoint.position, weakPointRad, isPlayer);



        if (colide)
        {
            Score.enemyScore += 1;
            Destroy(this.gameObject);
            this.gameObject.GetComponent<EnemyAI>().alive = false;
        }

    }
}

