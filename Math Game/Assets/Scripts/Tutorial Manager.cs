using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    private string[] tutorialLines = {
        "This is you, a proud Dragon Knight. You can control your dragon by pressing W/up arrow to go up, S/down arrow to go down",
        "You can ask your dragon to breath fire by pressing K", 
        "Watchout! There are enemies coming, you don't want to crash into these guys!",
        "you can dodge them by going up or down, or shoot fireballs at them to blow them up!", 
        "Defeat/dodge a cuple more of these guys",
        "You've made it to the big bad dragon! He has put up a wall of numbers between you and him and lauching fireballs at you! Dodge!",
        "Luckily with your years of training, you're able to find the weakness of these walls by solving math problems.",
        "They'll appear on top of your screen, hit the parts of the wall with the number that makes up the answer to answer the problem!",
        "if you hit the wrong part, press delete to take it off your answer",
        "Once you're done, hit the return/enter key to submit your answer, if it's right, your dragon will lauch a super fireball at the wall and break it!",
        "Oh no! you gave the wrong answer! the boss is now firing waves of fireball at you! Dodge!",
        "You've just broke the wall! Now's your chance to attack, fire at the boss!",
        "The wall is put back up after a couple of seconds, time to solve questions!",
        "You've beat the evil dragon! Congratulations, time to challenge the rest!"
        };
    public TMPro.TMP_Text textbox;
    public int lineNumber = 0;
    public Button nextButton;
    public GameObject player;
    public GameObject[] enemies;
    public GameObject boss;
    // Start is called before the first frame update
    void Start()
    {
        BossMechanic.wrongAnswer += wrongAnswer;
        BossMechanic.rightAnswer += rightAnswer;
        BossMechanic.bossDead += bossDown;
    }

    public void nextLine() 
    {
        if(lineNumber == 0){
            textbox.text = tutorialLines[0];
            player.SetActive(true);
            lineNumber++;
        }
        else if(lineNumber == 1){
            textbox.text = tutorialLines[1];
            lineNumber++;
            
        }
        else if(lineNumber == 2){
            textbox.text = tutorialLines[2];
            lineNumber++;
            enemies[0].SetActive(true);
            nextButton.gameObject.SetActive(false);
            StartCoroutine(autoprogress(3f));
        }
        else if(lineNumber == 3){
            textbox.text = tutorialLines[3];
            lineNumber++;
            StartCoroutine(autoprogress(8f));
        }
        else if(lineNumber == 4){
            textbox.text = tutorialLines[4];
            lineNumber++;
            nextButton.enabled = false;
            enemies[1].SetActive(true);
            enemies[2].SetActive(true);
            enemies[3].SetActive(true);
            StartCoroutine(autoprogress(10f));
        }
        else if(lineNumber == 5){
            textbox.text = tutorialLines[5];
            lineNumber++;
            nextButton.enabled = false;
            boss.SetActive(true);
            StartCoroutine(autoprogress(8f));
        }
        else if(lineNumber == 6){
            textbox.text = tutorialLines[6];
            lineNumber++;
            nextButton.enabled = false;
            StartCoroutine(autoprogress(8f));
        }
        else if(lineNumber == 7){
            textbox.text = tutorialLines[7];
            lineNumber++;
            nextButton.enabled = false;
            StartCoroutine(autoprogress(8f));
        }
        else if(lineNumber == 8){
            textbox.text = tutorialLines[8];
            lineNumber++;
            nextButton.enabled = false;
            StartCoroutine(autoprogress(8f));
        }
        else if(lineNumber == 9){
            textbox.text = tutorialLines[9];
            lineNumber++;
            nextButton.enabled = false;
            StartCoroutine(autoprogress(8f));
        }
        
        else if(lineNumber == 11){
            textbox.text = tutorialLines[12];
            lineNumber++;
            nextButton.enabled = false;
            
        }
        else if(lineNumber == 13){
            SceneManager.LoadScene(0);
        }

    }

    public void wrongAnswer()
    {
        textbox.text = tutorialLines[10];
    }

    public void rightAnswer()
    {
        textbox.text = tutorialLines[11];
        lineNumber = 11;
        StartCoroutine(autoprogress(8f));
    }

    public void bossDown() {
        StopAllCoroutines();
        textbox.text = tutorialLines[13];
        lineNumber = 13;
        StartCoroutine(autoprogress(4f));
        
    }


    IEnumerator autoprogress(float time) {
        yield return new WaitForSeconds(time);
        nextLine();
        
    }
}
