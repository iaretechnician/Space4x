using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class starSysteminfo : NetworkBehaviour
{
	//This is attached to every star on the map and contains the data for that system.
	//The data (resources, population, starbase etc) can change over time
	//The data is on the server and synced to players.  Data that is NOT synced
	//like starbase, is only synced when it is detected by the players.
	
	//The possible images shown on the map
	public Sprite yellowStar,StarPort,starBase,blackHole;
	[SyncVar]
	public int ID;
	[SyncVar]
	public string name;
	[SyncVar]
	public int planetcount;
	[SyncVar]
	public int x_coord;
	[SyncVar]
	public int y_coord;
	[SyncVar]
	public bool hasStarport;
	public StarPort starPort;
	public int playerStarbase;//0 is no, > than zero is the index in starbasearray
	public string[] resourceNames;
	public int[] resourceAmounts;
	public int[] resourcePrices;
	public int civType;//affects how much tax and resources we can get from them
	public int population;

	void Awake()
	{
		gameObject.transform.SetParent(GameObject.Find("Starmap").transform);
	}
    // Start is called before the first frame update
    void Start()
	{
		gameObject.name = name;
        
	}
	
	void Update()
	{
	
	}

    // Update is called once per frame
	void FixedUpdate()
	{
		/* Update this systems resources etc on the SERVER.  If a player detects this object, then the info will be 
		replicated on the client.
		*/
		

		if(isServer)
		{
			population +=1;
		
			
		}
        
	}
	
	public void OnColliderEnter()
	{
		GetCurrentData();
	}
	
	[Command(requiresAuthority=false)]
	public void GetCurrentData()
	{
		TestRPC(population);

	}
    
	[ClientRpc]
	public void TestRPC(int newpop)
	{
		population = newpop;
		Debug.Log("CLIENT SIDE POP UPDATE for "+name + " : "+newpop);
	}
}
