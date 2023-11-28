using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_Spawner : MonoBehaviour
{
    [SerializeField] private float spawnrate;
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private bool canSpawn = true;

    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(Spawner());
    }

   

    private IEnumerator Spawner ()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnrate);

        while (canSpawn)
        {
            yield return wait;
            int rand = Random.Range(0, enemyPrefabs.Length);
            GameObject enemyToSpawn = enemyPrefabs[rand];

            Instantiate(enemyToSpawn, transform.position + new Vector3(0, UnityEngine.Random.Range(-5, 5), 0), Quaternion.identity);
           
        }
    }

}
