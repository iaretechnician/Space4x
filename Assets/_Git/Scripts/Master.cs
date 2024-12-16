using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Master : MonoBehaviour
{
	/*
	This is the interface between the player and his playercontroller when he creates or joins the game.
	*/
	public static Master instance;
	
	public GameObject playerController;
    // Start is called before the first frame update
    void Start()
	{
	
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
	public GameObject CreatePlayerController(string userid,string password)
	{
		//playercontroller should be server side only. Player asks to create one and will communicate with that directly
		//from his server side player object.  The player controller has his userid and password so it can connect with 
		//player when he rejoins later.
		bool playerExists = false;
		GameObject controller = null;
		
		GameObject[] controllers = GameObject.FindGameObjectsWithTag("ServerSidePlayerController");
		Debug.Log("CONTROLLERS "+controllers.Length);
		foreach(GameObject _controller in controllers)
		{
			Debug.Log(_controller.GetComponent<ServerSidePlayerController>().playerName+ " " +userid);
			Debug.Log(_controller.GetComponent<ServerSidePlayerController>().playerPassword+" "+password);
			if(_controller.GetComponent<ServerSidePlayerController>().playerName==userid  && _controller.GetComponent<ServerSidePlayerController>().playerPassword==password)
			{
				Debug.Log("PLAYER CAN JOIN");
				playerExists = true;
				controller = _controller;
			}
		}
		
		if(!playerExists)
		{
			Debug.Log("PLAYER CAN CREATE AN ACCOUNT");
			controller = Instantiate(playerController);
			controller.GetComponent<ServerSidePlayerController>().playerName = userid;
			controller.GetComponent<ServerSidePlayerController>().playerPassword = password;
			//lets get a homeworld
		
			
		}
		return controller;
	}
}
