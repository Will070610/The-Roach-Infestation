using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    private float bulletspeed = 9f;

    public EnemyMovement EnemyMovement;


    // Start is called before the first frame update
    void Start()
    {
        EnemyMovement = GameObject.Find("EnemyMovement").GetComponent<EnemyMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        //player is looking right enemy shoots right

      
           // transform.Translate(Vector2.horizontal * bulletspeed * Time.deltaTime);

    }


}
