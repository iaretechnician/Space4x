using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class StarBase : NetworkBehaviour
{
	/*
	Attached to each starbase 
	*/
	
	public int playerID  = 0;
	public int damage = 0;
	public Vector3 coordinates;
	
	public int resource1Amount = 0;
	public int resource2Amount = 0;
	public int resource3Amount = 0;
	public int fuelAmount = 0;
	
	//ships we can build
	public List<GameObject> shipBuildList = new List<GameObject>();
	
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
