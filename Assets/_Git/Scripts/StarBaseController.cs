using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class StarBaseController : NetworkBehaviour
{
	/*
	This is one of the controller scripts that works alongside the player controller.  
	The player controller will handle a player request and pass that to the correct 
	object controller.
	*/
	public List<GameObject> starBaseList = new List<GameObject>();
	public GameObject starBasePrefab;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
	public void CreateStarBase()
	{
		
	}
	
	public void DestroyStarBase()
	{
		
	}
}
