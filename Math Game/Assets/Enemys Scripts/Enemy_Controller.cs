using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Controller : MonoBehaviour
{
    public float enemyHealth;
    public float enemySpeed;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = Vector3.down * enemySpeed;

        if( enemyHealth <= 0) 
        {
            Destroy(gameObject);
        }
    }

    void ApplyDamage(float damage)
    {
        enemyHealth -= damage;
    }

}
