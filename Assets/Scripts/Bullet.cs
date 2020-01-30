using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 40;
    public Rigidbody2D rb;

    // Use this for initialization
     void Start()
    {
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        EnemyMovement enemy = hitInfo.GetComponent<EnemyMovement>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
        
        Destroy(gameObject);
    }

    private void Update()
    {
        StartCoroutine(DestroyBullet());
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Ground")
        {
            Destroy(this.gameObject);
        }
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }
}


