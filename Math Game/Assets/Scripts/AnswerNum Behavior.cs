using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerNumMob : Enemy_Controller
{
    public int value;
    public GameObject Boss;
    // Start is called before the first frame update
    void Start() {
        enemyHealth = 10000000000000000;
        enemySpeed = 0;
        rb = gameObject.GetComponent<Rigidbody2D>();
        TMPro.TMP_Text text = transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<TMPro.TMP_Text>();
        text.text = value.ToString();
    }
    public void enterNum() {
        
        Boss = GameObject.FindGameObjectWithTag("Boss");
        Boss.GetComponent<BossMechanic>().givenAnswer.text += value.ToString();
        Debug.Log("answer given");
        
    }

    
}
