using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSelector : MonoBehaviour {

    List<PlayerInfo> actualPlayers = new List<PlayerInfo>();
    public Controller controller;
    public int id;

    public enum Controller
    {
        J,
        K
    }

    void Start ()
    {
		
	}
	
	void Update ()
    {
	
	}

    public void AddNewPlayer(PlayerInfo info)
    {
        actualPlayers.Add(info);
    }
}
