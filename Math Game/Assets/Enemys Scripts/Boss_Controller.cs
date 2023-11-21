using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Controller : MonoBehaviour
{
    public float bossHealth;
    [SerializeField] private float spawnrate;
    [SerializeField] private GameObject[] enemyPrefabs;
    private bool canAttack = true;
    // Start is called before the first frame update

    private void Start()
    {
        StartCoroutine(BossAttck());
    }


    // Update is called once per frame
    void Update()
    {
        

        if (bossHealth <= 0)
        {
            Destroy(gameObject);
        }

    }

    void ApplyDamage(float damage)
    {
        bossHealth -= damage;
    }

   void Attack1()
    {
        int rand = Random.Range(0, enemyPrefabs.Length);
        GameObject enemyToSpawn = enemyPrefabs[rand];

        Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
    }

    void Attack2() 
    {
        int rand = Random.Range(0, enemyPrefabs.Length);
        GameObject enemyToSpawn = enemyPrefabs[rand];

        Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
    }
    void Attack3() 
    {
        int rand = Random.Range(0, enemyPrefabs.Length);
        GameObject enemyToSpawn = enemyPrefabs[rand];

        Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
    }

    private IEnumerator BossAttck()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnrate);

        while (canAttack)
        {
            yield return wait;
            int rand = Random.Range(0, 2);
            if (rand == 0)
            {
                Attack1();
            }
            else if (rand == 1)
            {
                Attack2();
            }
            else if (rand == 3)
            {
                Attack3();
            }
        }
    }

}
