using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D myRigidBody;

    public int health = 100;

    public Transform player;
    public bool facingRight;
    public GameObject deathEffect;
    public GameObject _enemybulletPrefab;


    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((player.position.x < transform.position.x && !facingRight) || (player.position.x > transform.position.x) && facingRight){
            FlipEnemy();
        }

        if (IsFacingRight())
        {
            myRigidBody.velocity = new Vector2(moveSpeed, 0f);
        }
        else
        {
            myRigidBody.velocity = new Vector2(-moveSpeed, 0f);
        }
    }

    public void EnemyLook()
    {
        transform.LookAt(player);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        //Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }


    public bool IsFacingRight()
    {
        return transform.localScale.x > 0;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        FlipEnemy();
        //transform.localScale = new Vector2(-(Mathf.Sign(myRigidBody.velocity.x)), 1f);
    }

    void FlipEnemy()
    {
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.collider.tag == "Player")   
            other.gameObject.GetComponent<Player>();
            Vector3 enemybulletPos = transform.position + new Vector3(0.8f, 0, 0);
            Instantiate(_enemybulletPrefab, enemybulletPos, Quaternion.identity);
        }
    }
       

