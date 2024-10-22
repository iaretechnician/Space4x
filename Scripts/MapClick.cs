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
	private bool _measuring = false;
	private bool _startMeasuring = false;
	private string distanceText;
	private string displayCoords;

	
    // Start is called before the first frame update
    void Start()
    {
	    starSystem = GameObject.Find("_Star");
	    emptySector = GameObject.Find("EmptySector");
	    
    }

    // Update is called once per frame
    void Update()
	{
    	
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
		v3 = Input.mousePosition;
		v3.y = 0f;
		v3 = Camera.main.ScreenToWorldPoint(v3);
		print("V3 "+v3);
		
			_startMeasuring = !_startMeasuring;
			print ("measuring "+_startMeasuring);
		}
		//Right clicked, start measruing but not currently measuring.
		//set measuring to true and startmeasture to false
		if(_startMeasuring && !_measuring)
		{
			_measuring = true;
			//the first time we right click, it enters this switch and measure is zero.
			startMeasure.Set(v3.x, 0f, v3.z);
			distanceText = "";
			//Cursor.SetCursor();
		}
		//Now second click, startmeasure is false and we had already
		//set measuring to true
		if(!_startMeasuring && _measuring)
		{
			_measuring = false;
			endMeasure.Set(v3.x, 0f, v3.z);

			print("Startmeasure "+startMeasure);
			print("endMeasure "+endMeasure );
			distanceText = "<color=yellow>\nDistance: " + System.Math.Round(Vector3.Distance(startMeasure, endMeasure), 2) + " parsecs</color>";
			print(distanceText);            
		}
	}
}
