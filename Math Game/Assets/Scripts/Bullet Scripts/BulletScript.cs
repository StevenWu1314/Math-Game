using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
	public float speed = 5f;
	public float deactive_timer = 3f;
	
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DeactivateGameObject", deactivate_Timer);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
	Attack();
    }
	
	void Move(){
		Vector3 temp = transform.position;
		temp.x += speed * Time.deltaTime;
		transform.position = temp;
	}

	void Attack() {
		if(Input.GetKeyDown(KeyCode.K))
		{
			Instantiate(player_Bullet, attack_Point.position, Quaternion.identity);
		}
	}

	void DeactivateGameObject()
	{
		gameObject.SetActive(false);
	}
}
