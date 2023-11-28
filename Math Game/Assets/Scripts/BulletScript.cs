using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
	public float speed = 1f;
	public float deactive_timer = 3f;
    private bool collided = false;
    
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        transform.up = new Vector3 (9000, 0, 0);
    }
	
	void Move(){
		Vector3 temp = transform.position;
		temp.x += speed * Time.deltaTime;
		transform.position = temp;
	}

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Num") && !collided) {
            other.GetComponent<AnswerNumMob>().enterNum();
            collided = true;
            Destroy(gameObject);
            
        }
        else if (other.gameObject.CompareTag("Enemy")) {
            other.GetComponent<Enemy_Controller>().ApplyDamage(1f);
            Destroy(gameObject);
        }
        
    }
}
