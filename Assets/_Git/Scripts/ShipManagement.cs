using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Vectrosity;
using Mirror;


public class ShipManagement : NetworkBehaviour
{
	public GameObject shipPrefab;
	public GameObject shipObject;
	public Vector3 currentVector;
	MapClick localMapclick = null;
	
    // Start is called before the first frame update
    void Start()
    {
	    localMapclick = gameObject.GetComponent<MapClick>();
    }

    // Update is called once per frame
    void Update()
	{
		currentVector = localMapclick.selectedVector3;
        
    }
    
	public void SpawnShip()
	{
		//List<Vector3> pointLoc = new List<Vector3>();
		//pointLoc.Add(selectedVector3);
		//shipDot = new VectorLine("Points", pointLoc, 100f, LineType.Points);
		//shipDot.Draw();
		
		shipObject = Instantiate(shipPrefab);
		NetworkServer.Spawn(shipObject);
		shipObject.transform.position = currentVector;
		
		print("ship created");
		//return;
	}
}
