using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float speed = 5f;
	
	public float min_Y, max_Y;
	public Animator animator;
	public TMPro.TMP_Text givenAnswer;
	public static event Action inPosition;
	
	
	[SerializeField]
	private GameObject player_Bullet;
	
	[SerializeField]
	private Transform attack_Point;
	
    // Start is called before the first frame update
    void Start()
    {
        BossMechanic.exposeWeakness += StartSuperAttack;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
		Attack();
		if(Input.GetKeyDown(KeyCode.Return)){
			GameObject.FindGameObjectWithTag("Boss").GetComponent<BossMechanic>().checkAnswer();
		}
		if(Input.GetKeyDown(KeyCode.Backspace)) {
			Debug.Log("trying to delete");
			givenAnswer.text = givenAnswer.text.Remove(givenAnswer.text.Length - 1);
		}
		
    }
	
	void MovePlayer()
	{
		if(Input.GetAxisRaw("Vertical") > 0f)
		{
			
			Vector3 temp = transform.position;
			temp.y += speed * Time.deltaTime;
			
			if(temp.y > max_Y)
				temp.y = max_Y;
			
			transform.position = temp;
			
		}else if (Input.GetAxisRaw("Vertical") < 0f)
		{
			Vector3 temp = transform.position;
			temp.y -= speed * Time.deltaTime;
			
			if(temp.y < min_Y)
				temp.y = min_Y;
			
			transform.position = temp;
		}
	}
	
	void Attack() {
		if(Input.GetKeyDown(KeyCode.K))
		{
			GameObject fireball = Instantiate(player_Bullet, attack_Point.position, quaternion.identity);
			StartCoroutine(playAttackAnimation());
			
		}
	}

	void StartSuperAttack() {
		
		StartCoroutine(SuperAttack());
		
		
	}

	public IEnumerator playAttackAnimation(){
		animator.SetTrigger("Attack");
		yield return new WaitForSeconds(0.5f);
		animator.SetTrigger("Attack");
	}
	public IEnumerator SuperAttack() {
		while(transform.position != new Vector3(-9, 0, 0)) {
			transform.position = Vector3.MoveTowards(transform.position, new Vector3(-9, 0, 0), Time.deltaTime * 4);
			yield return new WaitForSeconds(0.01f);
		}
		inPosition?.Invoke();
		GameObject fireball = Instantiate(player_Bullet, attack_Point.position, quaternion.identity);
		fireball.transform.localScale= new Vector3 (3, 3, 1);
		StartCoroutine(playAttackAnimation());
		yield return null;
	}
	void OnTriggerEnter2D(Collider2D other){
		Debug.Log("you've been hit!");
	}
			
}
