using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D myRigidBody;
    SpriteRenderer _spriteRenderer;
    private float nextFire;
    private float fireRate = 10f;

    [SerializeField] private Tilemap _enemyTilemap;

    public int health = 100;

    public Transform player;
    public GameObject deathEffect;
    public GameObject _enemybulletPrefab;

    public int direction = 1;

    private bool _isSeeingPlayer = false;


    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        myRigidBody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        nextFire = Time.time + fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * moveSpeed * direction * Time.deltaTime);

        Vector3Int enemyPos = _enemyTilemap.WorldToCell(transform.position);
        if (_enemyTilemap.GetTile(enemyPos) != null)
        {
            direction *= -1;
            if (direction > 0)
            {
                _spriteRenderer.flipX = false;

            }
            else
            {
                _spriteRenderer.flipX = true;
            }
        }

        if (_isSeeingPlayer == true)
        {
            StartCoroutine(EnemyShoot());
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("Player Detected!");
            _isSeeingPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("Player Lost!");
            _isSeeingPlayer = false;
        }
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
        {
            other.gameObject.GetComponent<Player>();
        }
    }

    IEnumerator EnemyShoot()
    {
        while(_isSeeingPlayer == true)
        {
            if(Time.time > nextFire)
            {
            direction = 0;
            Vector3 enemybulletPos = transform.position + new Vector3(0.8f, 0, 0);
            Instantiate(_enemybulletPrefab, enemybulletPos, Quaternion.identity);
            nextFire = Time.time + fireRate;
            }
            yield return new WaitForSeconds(fireRate);
        }
    }

}
