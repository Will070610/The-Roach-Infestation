using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    private float bulletspeed = 5f;

    private Player _player;

    private EnemyMovement EnemyMovement;

    private Rigidbody2D _rb;

    public GameObject Foreground;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindObjectOfType<Player>();
        _rb = GetComponent<Rigidbody2D>();

        //Vector3 distanceToPlayer = _player.transform.position - transform.position;
        //_rb.velocity = distanceToPlayer.normalized * bulletspeed;
        
    }

    void Update()
    {
        if(EnemyMovement.direction > 0)
        {
            float VerticalInput = Input.GetAxis("Horizontal");
            transform.Translate(Vector2.right * bulletspeed * Time.deltaTime);
        }
        else
        {
            float VerticalInput = Input.GetAxis("Horizontal");
            transform.Translate(Vector2.left * bulletspeed * Time.deltaTime);
        }
        
        StartCoroutine(DestroyBullet());
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.collider.tag == "Ground")
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
