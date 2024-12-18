using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Vectrosity;
using Mirror;

public class MapClick : NetworkBehaviour
{
	/*
	Attached to the Chart on player prefab. Handles clicking on the map and showing the current star system
	
	*/

	public TMP_Text coords,planetName;
	public GameObject starSystem,emptySector;
	public Camera mapCamera;
	starSysteminfo info;
	public TMP_Text systemName,systemcoords,resources,civtype,population,fuel;
	Vector3 spacePos;
	public GameObject shipPrefab;
	public GameObject shipObject;
		
	Vector3 startMeasure= new Vector3(0f,1f,0f);
	Vector3 endMeasure = new Vector3(0f,1f,0f);
	Vector3 currentVector3 = new Vector3(0f, 1f, 0f);
	Vector3 mouseVector3 = new Vector3(0f, 1f, 0f);
	Vector3 v3,selectedVector3;
	public enum measure_enum{measuring,started,completed};
	public measure_enum _measuring;
	private bool _startMeasuring = false;
	private bool _endMeasuring = false;
	private VectorLine lastLine,shipDot;

	public TMP_Text distanceText;
	public TMP_Text displayCoords;

	
    // Start is called before the first frame update
    void Start()
	{
		_measuring = measure_enum.completed;
	    starSystem = GameObject.Find("_Star");
	    emptySector = GameObject.Find("EmptySector");
	    
    }

    // Update is called once per frame
    void Update()
	{
		v3 = Input.mousePosition;
		v3 = Camera.main.ScreenToWorldPoint(v3);
		v3.y = 1f;
		v3.x=Mathf.Floor(v3.x*10)/10;
		v3.z=Mathf.Floor(v3.z*10)/10;

		displayCoords.text=v3.x +" - "+v3.z;
		
		
		if(_measuring == measure_enum.measuring)
		{
			if(lastLine != null) VectorLine.Destroy (ref lastLine);
			lastLine = VectorLine.SetLine3D (Color.green, startMeasure,v3);
			distanceText.text = System.Math.Round(Vector3.Distance(startMeasure, v3), 2) + " parsecs</color>";

		}
    	
		if (Input. GetMouseButtonDown(0))
		{

			selectedVector3 = Input.mousePosition;
			selectedVector3 = Camera.main.ScreenToWorldPoint(v3);
			selectedVector3.y = 1f;
			selectedVector3.x=Mathf.Floor(v3.x*10)/10;
			selectedVector3.z=Mathf.Floor(v3.z*10)/10;
			Debug.Log("Selected Vector "+selectedVector3);
			
			if(EventSystem.current.IsPointerOverGameObject()) return;
			
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if(Physics.Raycast(ray, out hit))
			{
				if(hit.collider.gameObject.tag =="StarSystem")
				{
					coords.text = hit.collider.gameObject.transform.position.x +"-"+hit.collider.gameObject.transform.position.z;
					planetName.text=hit.collider.gameObject.name;
					info = hit.collider.gameObject.GetComponent<starSysteminfo>();
					starSystem.SetActive(true);
					int counter = 0;
					foreach(Transform child in starSystem.transform)
					{
						if (counter >= info.planetcount)
						{
							child.gameObject.SetActive(false);
						}else{
							child.gameObject.SetActive(true);
						}
						counter++;
					
					}
				
					//save the current system to the player
					playerManager.instance.currentlySelectedSystem = hit.collider.gameObject;
					systemName.text = hit.collider.gameObject.name;
					
					Debug.Log("SELECTED " +hit.collider.gameObject.name+ " AT "+hit.collider.gameObject.transform.position.x +"-"+hit.collider.gameObject.transform.position.z);
			
				}
				if(hit.collider.gameObject.tag =="EmptySpace")
				{
					var ray_mouseclick = Camera.main.ScreenPointToRay(Input.mousePosition);
					RaycastHit hit_mouseclick = new RaycastHit();
					if (Physics.Raycast (ray_mouseclick, out hit_mouseclick))
					{
						Debug.Log(hit_mouseclick.collider.gameObject.name + " ");
					}
					spawnShip();
					
					planetName.text = "Empty Space";
					coords.text = Mathf.Round(hit.point.x).ToString() +":"+Mathf.Round(hit.point.z).ToString();
					spacePos.Set(hit.point.x,0f,hit.point.z);
					//
					playerManager.instance.currentlySelectedSystem = emptySector;
					systemName.text = "Empty Space";
					resources.text = "";
					population.text	= "";
					fuel.text = "";
					starSystem.SetActive(false);

				}
			}
		}
		
		
	
	
	if (Input.GetMouseButtonDown(1)) 
	{
		
		switch (_measuring)
		{				
		case measure_enum.completed:
			Debug.Log("MEASURING VALUE "+_measuring);

			//_measuring = measure_enum.started;
			_measuring = measure_enum.measuring;
			v3 = Input.mousePosition;
			v3 = Camera.main.ScreenToWorldPoint(v3);
			v3.y = 1f;
			v3.x=Mathf.Floor(v3.x*10)/10;
			v3.z=Mathf.Floor(v3.z*10)/10;
			
			startMeasure.Set(v3.x, 1f, v3.z);
			endMeasure.Set(v3.x, 1f, v3.z);
			Debug.Log("Starting "+startMeasure + " Endmeasure "+v3);

			distanceText.text = System.Math.Round(Vector3.Distance(startMeasure, endMeasure), 2) + " parsecs</color>";
			
			break;
			
		case measure_enum.measuring:
			Debug.Log("MEASURING VALUE "+_measuring);

			_measuring = measure_enum.completed;
			distanceText.text = "";
			break;
			
		case measure_enum.started:
			Debug.Log("MEASURING VALUE "+_measuring);

			//this is the first click
			_measuring = measure_enum.measuring;
			v3 = Input.mousePosition;
			v3 = Camera.main.ScreenToWorldPoint(v3);
			v3.y = 1f;
			v3.x=Mathf.Floor(v3.x*10)/10;
			v3.z=Mathf.Floor(v3.z*10)/10;
			
			startMeasure.Set(v3.x, 1f, v3.z);
			endMeasure.Set(v3.x, 1f, v3.z);
			Debug.Log("Starting "+startMeasure + " Endmeasure "+v3);

			distanceText.text = System.Math.Round(Vector3.Distance(startMeasure, endMeasure), 2) + " parsecs</color>";
			break;
		
			//we are measuring, this is second click
	
		
		default:
			_measuring = measure_enum.completed;
			break;
		}
	}
	
	}//update
	
	public void spawnShip()
	{
		//List<Vector3> pointLoc = new List<Vector3>();
		//pointLoc.Add(selectedVector3);
		//shipDot = new VectorLine("Points", pointLoc, 100f, LineType.Points);
		//shipDot.Draw();
		
		shipObject = Instantiate(shipPrefab);
		NetworkServer.Spawn(shipObject);
		shipObject.transform.position = selectedVector3;
		
		print("ship created");
		//return;
	}

		
}
