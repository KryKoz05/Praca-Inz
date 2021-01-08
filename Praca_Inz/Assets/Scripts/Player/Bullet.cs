using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;

    public Rigidbody2D rb;
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        Debug.Log(other.name);
        if (other.gameObject.CompareTag("Enemy"))
        {
            
            Score.enemyScore += 1;
            other.gameObject.GetComponent<EnemyAI>().alive = false;
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        else if(!other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
