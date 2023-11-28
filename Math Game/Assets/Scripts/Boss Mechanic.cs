using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class BossMechanic : MonoBehaviour
{
    public GameObject healthBar;
    public int health = 10;
    public int maxHealth = 10;
    public TMPro.TMP_Text healthDisplay;
    public TMPro.TMP_Text ProblemDisplay;
    public GameObject boss;
    public TMPro.TMP_Text givenAnswer;
    
    public float num1;
    public float num2;
    public float sign;
    public float answer;
    public GameObject bossProj;
    public bool answerRecieved = false;

    public GameObject[] answerNums;

    public Animator animator;
   
    // Start is called before the first frame update
    void Start()
    {
        createProblem();
        StartCoroutine(normalAttack());
        for(int i = 0; i < answerNums.Length; i++) {
            answerNums[i].SetActive(true);
        }
    }

    // Update is called once per frame
    void createProblem() {
        num1 = (int)UnityEngine.Random.Range(1, 10);
        num2 = (int)UnityEngine.Random.Range(1, 10);
        sign = (int)UnityEngine.Random.Range(1, 3);
        if (sign == 2){
            while (num2 > num1){
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
        }
        answerRecieved = false;
        

    }

    public void checkAnswer(){
        if (givenAnswer.text == "") {
            
        }
        else if (float.Parse(givenAnswer.text) == answer) 
        {
            health -= 1;
            healthDisplay.text = $"{health}/{maxHealth}";
            answerRecieved = true;
            createProblem();
            if(health == 0){
                animator.SetTrigger("death");
                Destroy(gameObject, 28f/60f );
            }
            Debug.Log("correct");
            
            givenAnswer.text = "";
        }
        else if (float.Parse(givenAnswer.text) != answer) 
        {
            
            StartCoroutine(releaseFireBallWave());
            Debug.Log($"{givenAnswer.text} != {answer}");
            givenAnswer.text = "";
        }
    }

    IEnumerator normalAttack()
    {
        while (!answerRecieved) {
            GameObject fireball = Instantiate(bossProj, transform.position + new Vector3(0, UnityEngine.Random.Range(-5, 5), 0), quaternion.identity);
            fireball.transform.Rotate(new Vector3(0, 0, 90), Space.World);
            yield return new WaitForSeconds(2f);
        }
        yield return new WaitForSeconds(1f);
    }

    IEnumerator releaseFireBallWave(){
        for(int i = 0; i < 3; i++) {
            for(int z = 0; z < 10; z++) {
                GameObject fireball = Instantiate(bossProj, transform.position, quaternion.identity);
                fireball.transform.Rotate(new Vector3(0, 0, UnityEngine.Random.Range(0, 180)), Space.World);
            }
            yield return new WaitForSeconds(2f);
        }
        yield return null;
    }

    private void OnDestroy(){
        for(int i = 0; i < answerNums.Length; i++) {
            answerNums[i].SetActive(false);
        }
        
    }

}

