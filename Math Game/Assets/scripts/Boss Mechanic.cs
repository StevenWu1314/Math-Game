using System;
using System.Collections;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class BossMechanic : MonoBehaviour
{
    
    public float health = 10;
    public float maxHealth = 10;
    public TMPro.TMP_Text ProblemDisplay;
    
    public TMPro.TMP_Text givenAnswer;
    public UnityEngine.UI.Image healthsquare;
    public float num1;
    public float num2;
    public float sign;
    public float answer;
    public GameObject bossProj;
    public bool answerRecieved = false;
    public GameObject[] answerNums;
    public Animator animator;
    public bool Tutorial = false;
    public bool invulnerbility;

    public static event Action exposeWeakness; 
    public static event Action wrongAnswer;
    public static event Action rightAnswer;
    public static event Action bossDead;
   
    // Start is called before the first frame update
    void Start()
    {
        createProblem();
        StartCoroutine(normalAttack());
        for(int i = 0; i < answerNums.Length; i++) {
            answerNums[i].SetActive(true);
        }
        PlayerController.inPosition += weaknessExposed;
    }

    // Update is called once per frame
    void createProblem() {
        num1 = (int)UnityEngine.Random.Range(1, 10);
        num2 = (int)UnityEngine.Random.Range(1, 10);
        sign = (int)UnityEngine.Random.Range(1, 5);
        if (sign == 2){
            while (num2 > num1){
                num2 = (int)UnityEngine.Random.Range(1, 10);
            }
        }
        if (sign == 4) {
            while(num1 % num2 != 0) {
                num2 = (int)UnityEngine.Random.Range(1, 10);
            }
        }
        
        switch (sign)
        {
            case 1:
                ProblemDisplay.text = $"{num1} + {num2} = ";
                answer = num1 + num2;
                break;
            case 2:
                ProblemDisplay.text = $"{num1} - {num2} = ";
                answer = num1 - num2;
                break;
            case 3:
                ProblemDisplay.text = $"{num1} * {num2} = ";
                answer = num1 * num2;
                break;
            case 4:
                ProblemDisplay.text = $"{num1} / {num2} = ";
                answer = num1 / num2;
                break;
        }
        answerRecieved = false;
        

    }

    public void checkAnswer(){
        if (givenAnswer.text == "") {
            
        }
        else if (float.Parse(givenAnswer.text) == answer) 
        {
            if(Tutorial) {
                rightAnswer?.Invoke();
            }
            givenAnswer.text = "";
            exposeWeakness?.Invoke();
            Debug.Log("correct");
            
            
        }
        else if (float.Parse(givenAnswer.text) != answer) 
        {
            if(Tutorial) {
                wrongAnswer?.Invoke();
            }
            StartCoroutine(releaseFireBallWave());
            Debug.Log($"{givenAnswer.text} != {answer}");
            givenAnswer.text = "";

        }
    }

    IEnumerator normalAttack()
    {
        while (!answerRecieved) {
            GameObject fireball = Instantiate(bossProj, transform.position + new Vector3(0, UnityEngine.Random.Range(-5, 5), 0), quaternion.identity);
            
            yield return new WaitForSeconds(2f);
        }
        yield return new WaitForSeconds(1f);
    }

    IEnumerator releaseFireBallWave(){
        for(int i = 0; i < 3; i++) {
            for(int z = 0; z < 10; z++) {
                GameObject fireball = Instantiate(bossProj, transform.position, quaternion.identity);
                fireball.transform.Rotate(new Vector3(0, 0, UnityEngine.Random.Range(-90, 90)), Space.World);
            }
            yield return new WaitForSeconds(2f);
        }
        yield return null;
    }
    void weaknessExposed() {
        StartCoroutine(WeaknessExposed());
    }
    IEnumerator WeaknessExposed() {
        givenAnswer.text = "";
        yield return new WaitForSeconds(1.8f);
        givenAnswer.text = "";
        for(int i = 0; i < answerNums.Length; i++) {
            answerNums[i].SetActive(false);
        }
        yield return new WaitForSeconds(5f);
        for(int i = 0; i < answerNums.Length; i++) {
            answerNums[i].SetActive(true);
        }
        createProblem();
        yield return null;
    }


    private void OnDestroy(){
        for(int i = 0; i < answerNums.Length; i++) {
            answerNums[i].SetActive(false);
        }
        if(Tutorial) {
            bossDead?.Invoke();
        }
        SceneManager.LoadScene(7);

        
    }

    public void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("PlayerProj") && !invulnerbility) {
            health -= 1;
            healthsquare.fillAmount = health/maxHealth;
            Debug.Log("took 1 damage");
            if (health == 0) {
                animator.SetTrigger("death");
                Destroy(gameObject, 28f/60f);
                if(Tutorial) {
                    bossDead?.Invoke();
                }
            } else{
                StartCoroutine(Invulnerbility());
            }
            
        }
    }

    IEnumerator Invulnerbility(){
        invulnerbility = true;
		animator.SetTrigger("take damage");
        yield return new WaitForSeconds(3f);
        invulnerbility = false;
		animator.SetTrigger("take damage");
        yield return null;
    }

}

