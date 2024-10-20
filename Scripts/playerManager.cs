using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

public class playerManager : NetworkBehaviour
{
	public static playerManager instance;
	public GameObject SERVER;
	//IMPORTANT! This is the connection to the object that controls our ships,planets,bases etc on the server
	public NetworkIdentity NETID;
	public GameObject myController;
	public GameObject canvas;
	public panelManager panelmgr;
	public TMP_InputField TMPusername,TMPpassword;
	public GameObject loginCamera,mapCamera;
	public GameObject currentlySelectedSystem;
	public StarSystemSO currentSystemSO;
	public GameObject outpostPrefab;
	GameObject buildObject;//used to instantiate any player built object
	
	void Awake()
	{
		instance = this;
		NETID = GetComponent<NetworkIdentity>();
	}

	
    // Start is called before the first frame update
    void Start()
	{
		if(isServer) 
		{
			currentSystemSO = Resources.Load<StarSystemSO>("Sol");
			Debug.Log("SO OBJECT IS "+currentSystemSO.name + " and the system name is "+currentSystemSO.GetsystemName);
		}
		//display the login panel
		if(isLocalPlayer)
		{
			loginCamera.SetActive(true);
			canvas.SetActive(true);
			panelmgr.switchPanelView(0);
			TMPusername.text = PlayerPrefs.GetString("username","");
			TMPpassword.text = PlayerPrefs.GetString("password","");
		}
		if(isServer)
		{
			SERVER = GameObject.Find("_SERVER");

		}

    }

    // Update is called once per frame
    void Update()
	{
    	
	   
    }
    
	public void JoinGame()
	{
		if(isLocalPlayer) 
		{
			
			panelmgr.enableMapView();
			PlayerPrefs.SetInt("NetworkID",(int)NETID.netId);
			PlayerPrefs.SetString("username",TMPusername.text);
			PlayerPrefs.SetString("password",TMPpassword.text);
		}
		cmdJoinGame(TMPusername.text,TMPpassword.text);
	}
	[Command]
	void cmdJoinGame(string username, string password)
	{
		myController = SERVER.GetComponent<Master>().CreatePlayerController(username,password);
	}
	
	
	[Command]
	public void cmdSetCurrentSystem(GameObject clientSideSystem)
	{
		currentlySelectedSystem = clientSideSystem;
	}
	
	//**************************************** BUILD STARBASE IN CURRENT SYSTEM ******************
	public void BuildStarbase()
	{
		cmdSetCurrentSystem(currentlySelectedSystem);
		cmdBuildStarbase();
	}
	[Command]
	public void cmdBuildStarbase()
	{
		currentlySelectedSystem.GetComponent<starSysteminfo>().playerStarbase = 1;

	}
	
	public void BuildOutpost()
	{
		cmdSetCurrentSystem(currentlySelectedSystem);
		cmdBuildOutpost();

	}
	
	[Command]
	public void cmdBuildOutpost()
	{
		buildObject = Instantiate(outpostPrefab);
		NetworkServer.Spawn(buildObject);
		buildObject.transform.position = currentlySelectedSystem.transform.position;
		buildObject.name = "Outpost";
		myController.GetComponent<ServerSidePlayerController>().myBases.Add(buildObject);

		

	}
	
}
