using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public List<PlayerController> myPlayers = new List<PlayerController>();
    public GameObject youWin;
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            return _instance;
        }
    }

    public bool startingGame;
    public bool finishedGame;
    public float timeToStart;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        StartCoroutine(StartGame(timeToStart));
    }

    IEnumerator StartGame(float x)
    {
        while (true)
        {
            startingGame = true;
            foreach (var player in myPlayers)
                player.myAnim.Play("Stunned");
            yield return new WaitForSeconds(x);
            startingGame = false;
            break;
        }
    }

    public void FinishGame()
    {
        finishedGame = true;
        youWin.SetActive(true);
    }
}
