using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class BossMechanic : MonoBehaviour
{
    public GameObject healthBar;
    public int health = 10;
    public int maxHealth = 10;
    public TMPro.TMP_Text healthDisplay;
    public TMPro.TMP_Text ProblemDisplay;
    public GameObject boss;
    public TMPro.TMP_InputField givenAnswer;
    public float num1;
    public float num2;
    public float sign;

    public float answer;
   
    // Start is called before the first frame update
    void Start()
    {
        createProblem();
    }

    // Update is called once per frame
    void createProblem() {
        num1 = (int)Random.Range(1, 10);
        num2 = (int)Random.Range(1, 10);
        sign = (int)Random.Range(1, 5);
        switch (sign)
        {
            case 1:
                ProblemDisplay.text = $"{num1} + {num2} = ?";
                answer = num1 + num2;
                break;
            case 2:
                ProblemDisplay.text = $"{num1} - {num2} = ?";
                answer = num1 - num2;
                break;
            case 3:
                ProblemDisplay.text = $"{num1} * {num2} = ?";
                answer = num1 * num2;
                break;
            case 4:
                ProblemDisplay.text = $"{num1} / {num2} = ?";
                answer = num1/num2;
                break;
        }
        givenAnswer.text = "";
        givenAnswer.ActivateInputField();

    }

    public void checkAnswer(){
        if (float.Parse(givenAnswer.text) == answer) 
        {
            health -= 1;
            healthDisplay.text = $"{health}/{maxHealth}";
            createProblem();
            if(health == 0){
                Destroy(boss);
                gameObject.SetActive(false);
            }
            Debug.Log("correct");
        }
        else if (float.Parse(givenAnswer.text) != answer) 
        {
            boss.GetComponent<Boss>().attack();
            Debug.Log($"{givenAnswer} != {answer}");
        }
    }
}
