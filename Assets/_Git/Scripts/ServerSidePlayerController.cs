using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ServerSidePlayerController :  NetworkBehaviour{
	
	//this stores the userinfo so he can connect to this controller at any time
	public string playerName,playerPassword;
	public Sprite starBaseImg;
	public string homeWorld;

	public List<GameObject> myShips = new List<GameObject>();
	public List<GameObject> myBases = new List<GameObject>();
	public List<GameObject> myPlanets = new List<GameObject>();
	public List<GameObject> detectedShips = new List<GameObject>();
	public List<GameObject> detectedPlanets = new List<GameObject>();
	public List<GameObject> detectedBases = new List<GameObject>();
	

    // Use this for initialization
	void Start () 
	{
		gameObject.name = playerName + " Controller";
	}
	
	public void BuildStarbase(GameObject system)
	{
		//this needs to differentiate between players starports
		//then we add it to our player known objects.
		system.GetComponent<starSysteminfo>().playerStarbase = 1;
		
	}
	

	
   
  
}
