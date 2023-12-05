using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
	public float speed = 5f;
	
	public float min_Y, max_Y;
	public float health = 3;
	public Animator animator;
	public TMPro.TMP_Text givenAnswer;
	public static event Action inPosition;
	[SerializeField]
	private GameObject player_Bullet;
	public GameObject[] hearts;
	
	[SerializeField]
	private Transform attack_Point;
	private bool invulnerbility = false;

	private bool dead = false;
	
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
		if(Input.GetAxisRaw("Vertical") > 0f && !dead)
		{
			
			Vector3 temp = transform.position;
			temp.y += speed * Time.deltaTime;
			
			if(temp.y > max_Y)
				temp.y = max_Y;
			
			transform.position = temp;
			
		}else if (Input.GetAxisRaw("Vertical") < 0f && !dead)
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
		invulnerbility = true;
		while(transform.position != new Vector3(-9, 0, 0)) {
			transform.position = Vector3.MoveTowards(transform.position, new Vector3(-9, 0, 0), Time.deltaTime * 8);
			yield return new WaitForSeconds(0.01f);
		}
		inPosition?.Invoke();
		GameObject fireball = Instantiate(player_Bullet, attack_Point.position, quaternion.identity);
		fireball.transform.localScale= new Vector3 (3, 3, 1);
		StartCoroutine(playAttackAnimation());
		invulnerbility = false;
		yield return null;
	}
	IEnumerator Invulnerbility(){
        invulnerbility = true;
		animator.SetTrigger("take damage");
        yield return new WaitForSeconds(1.5f);
        invulnerbility = false;
		animator.SetTrigger("take damage");
        yield return null;
    }

	IEnumerator DeathAnimation() {
		animator.SetTrigger("death");
		dead = true;
		for(int i = 0; i < 100; i++) {
			if (i < 50) {
				gameObject.transform.position += Vector3.up * 0.02f;
				yield return new WaitForSeconds(0.01f);
			} 
			else {
				gameObject.transform.position += Vector3.down * 0.02f;
				yield return new WaitForSeconds(0.01f);
			}
		}
		SceneManager.LoadScene(8);
		yield return null;
	}
	void OnTriggerEnter2D(Collider2D other){
		if (!other.CompareTag("PlayerProj") && !invulnerbility) {
			Debug.Log("You've been hit");
			health -= 1;
			hearts[(int)health].SetActive(false);
			Destroy(other.gameObject);
			if(health <= 0) {
				StartCoroutine(DeathAnimation());
				Destroy(gameObject, 1f);
			} else
			{
				StartCoroutine(Invulnerbility());
			}

		}
		
	}
	void OnDestroy(){
		SceneManager.LoadScene(8);
	}
			
}
