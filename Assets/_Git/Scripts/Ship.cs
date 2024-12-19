using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

public class Ship : MonoBehaviour
{
	/*
	Ships exist on the server.  If a player serverside detects this ship then it is sent to that player
	with its CURRENT location so the ship needs to be continually detected to create a track
	on the plsyers game.  Ships are always sent to the owning player and allies
	*/
	//id of the player owner 0 is none
	public int owner = 0;
	public int xCoord;
	public int yCoord;
	public int speed = 0;
	public int heading;
	public int damage = 0;
	public Vector3 currentLocation;
	public List<Vector3> locationHistory = new List<Vector3>();
	public List<Vector3> waypoints = new List<Vector3>();
	public bool displayWaypoints;
	public bool displayTrack = true;
	public VectorLine shipTrace;
	
	//cargo
	
	//Ship customirization specifics
	
    // Start is called before the first frame update
    void Start()
	{
		locationHistory.Add(gameObject.transform.position);
		
    }

    // Update is called once per frame
    void Update()
	{
		if(shipTrace != null) VectorLine.Destroy (ref shipTrace	);
		shipTrace = VectorLine.SetLine3D (Color.green, locationHistory[0],gameObject.transform.position);
		gameObject.transform.Translate(.01f,0F,0F);
		print("UPDATE ON SHIP");
    }
    
	public void ShowPath()
	{
		//create a line path of travel history on the map.  The line is a 3d object so its displayed on the map like other gameobjects
		
	}
    
	
}
