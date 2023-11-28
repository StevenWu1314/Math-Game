using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Controller : MonoBehaviour
{
    public float enemyHealth;
    public float enemySpeed;
    protected Rigidbody2D rb;
    public ScoreTracking scoreTracker;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.bossEntrance += die;
        rb = GetComponent<Rigidbody2D>();
        scoreTracker = GameObject.FindGameObjectWithTag("ScoreTracker").GetComponent<ScoreTracking>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = Vector3.left * enemySpeed;
    }

    public void ApplyDamage(float damage)
    {
        enemyHealth -= damage;
        if( enemyHealth <= 0) 
        {
            die();
        }
    }

    public void die() {
        animator.SetTrigger("death");
        Destroy(gameObject, 28f/60f);
        scoreTracker.IncreaseScore(50);
         GameManager.bossEntrance -= die;
    }
}
