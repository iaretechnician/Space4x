using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class MapClick : MonoBehaviour
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
	public GameObject ship;
	Vector3 startMeasure= new Vector3(0f,0f,0f);
	Vector3 endMeasure = new Vector3(0f,0f,0f);
	Vector3 currentVector3 = new Vector3(0f, 0f, 0f);
	Vector3 mouseVector3 = new Vector3(0f, 0f, 0f);
	Vector3 v3;
	public enum measure_enum{waiting,started,completed};
	public measure_enum _measuring;
	private bool _startMeasuring = false;
	private bool _endMeasuring = false;

	public TMP_Text distanceText;
	public TMP_Text displayCoords;
	private LineRenderer distLine;

	
    // Start is called before the first frame update
    void Start()
	{
		_measuring = measure_enum.completed;
	    starSystem = GameObject.Find("_Star");
	    emptySector = GameObject.Find("EmptySector");
	    distLine = gameObject.GetComponent<LineRenderer>();
	    
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
		
		
		if(_measuring == measure_enum.started)
		{
			distLine.SetPosition(1, v3);
			//print("DISTLINE END"+distLine.GetPosition(1));
			distanceText.text = System.Math.Round(Vector3.Distance(startMeasure, v3), 2) + " parsecs</color>";

		}
    	
		if (Input. GetMouseButtonDown(0))
		{
			
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
					resources.text = info.resource_2.ToString();
					population.text	= info.population.ToString();
					fuel.text = info.res_fuel.ToString();
				
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
					GameObject _ship = Instantiate(ship);
					//_ship.transform.position = new Vector3(hit.point.x,1f,hit.point.z);
					//print("ship created");
					//return;
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
			//clicked the second time to complete
		case measure_enum.started:
			_measuring = measure_enum.completed;
			endMeasure.Set(v3.x, 1f, v3.z);
			print("Startmeasure "+startMeasure);
			print("endMeasure "+endMeasure );
			distanceText.text = System.Math.Round(Vector3.Distance(startMeasure, endMeasure), 2) + " parsecs</color>";
			print("MEASURING "+_measuring);

			break;
			//clicked the first time
		case measure_enum.waiting:
			_measuring = measure_enum.started;
			startMeasure.Set(v3.x, 1f, v3.z);
			distLine.SetPosition(0, startMeasure);
			distLine.SetPosition(1, startMeasure);
			print("DISTLINE START "+distLine.GetPosition(0));
			distanceText.text = "";
			print("MEASURING "+_measuring);
			break;
			//we are measuring, this is second click
	
		default:
			_measuring = measure_enum.completed;
			break;
		}
	}
	
	}//update
	public void Measure()
	{
		//Toggles right mouse button from screen move to measure
		_measuring = measure_enum.waiting;
	}
		
}
