using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bossprojectilebehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public float lifespan;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * -1f * Time.deltaTime * speed;
        lifespan -= Time.deltaTime;
        if (lifespan <= 0) {
            Destroy(gameObject);
        }
    }
}
