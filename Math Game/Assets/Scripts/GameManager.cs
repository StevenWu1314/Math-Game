using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Threading;
using Unity.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject spawners;
    public GameObject boss;
    public static event Action bossEntrance;
    public float timeTillBoss = 60f;
    public bool bossSummoned;
    public AudioSource music;
    public AudioClip bossMusic;
    // Start is called before the first frame update
    void Start()
    {
        changeState(States.Progressing);
    }

    // Update is called once per frame
    void Update()
    {
        timeTillBoss -= Time.deltaTime;
        if (timeTillBoss <= 0 && !bossSummoned) {
            changeState(States.bossTime);
            
        }
    }

    void changeState(States state) {
        switch (state){
            case States.Progressing:
                spawners.SetActive(true);
                break;
            case States.bossTime:
                bossSummoned = true;
                boss.SetActive(true);
                spawners.SetActive(false);
                bossEntrance?.Invoke();
                music.clip = bossMusic;
                music.Play();
                break;
        }
    }

    enum States{
        Progressing,
        bossTime
       
    }
}
