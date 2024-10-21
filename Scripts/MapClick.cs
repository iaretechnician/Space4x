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
			
			Ray ray = Camera.main.ScreenPointToRay(Input. mousePosition);
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
	}
}
