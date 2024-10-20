using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Mirror;

public class mapFunctions : MonoBehaviour {

	public GameObject starmap;
    public GameObject solarSystem;//(unused)the object that contains all our sun and planets
    public switchCamera cams;
    public Text _infoDisplay;//display current sector
	//public Text coordinateText;
    public GameObject[] _planets;//[0]is sun, then all planets.
	// public Dropdown starbaselistUI;
	//public GameObject starBase;
    private string distanceText;
	private string displayCoords;

         
    private bool sectorview = false;
    private int _x,_z;//coords for current sector
    
    Vector3 startMeasure= new Vector3(0f,0f,0f);
    Vector3 endMeasure = new Vector3(0f,0f,0f);
    Vector3 currentVector3 = new Vector3(0f, 0f, 0f);
    Vector3 mouseVector3 = new Vector3(0f, 0f, 0f);
    private int _measuring = 0;

    void Start()
    {
               
    }

    // Update is called once per frame
    void Update ()
	{
                //measuring. set the end position of the line to current mouse position and check for right clicks
                GetSectorCoords();
                //while measuring if we right click it moves to next step
                if (Input.GetMouseButtonDown(1)) _measuring += 1;
                
                switch (_measuring)
                {
                    case 0:
                           //the first time we right click, it enters this switch and measure is zero.
                        _measuring = 1;//now we are currently printing distance
                        startMeasure.Set(mouseVector3.x, 0f, mouseVector3.z);
                        distanceText = "";

                        print ("change the cursor for measuring");
                        //Cursor.SetCursor();
                        break;
                    case 1://measuring from start to current mouse position, save distance
                        endMeasure.Set(mouseVector3.x, 0f, mouseVector3.z);
                        distanceText = "<color=yellow>\nDistance: " + System.Math.Round(Vector3.Distance(startMeasure, endMeasure), 2) + " parsecs</color>";
                        break;
                        
                    case 2://we right clicked to end the measuring process,reset to map
                        _measuring = 0;
                        distanceText = "";
	                //fsm.currentstate = fsmstates.starmap;
                        break;
                    default://all done, remove text.
                        _measuring = 0;
	                //fsm.currentstate = fsmstates.starmap;
                        distanceText = "";
                        break;
                }
              


    }// END UPDATE
	
    void OnGUI()
	{
		/*
        //get true world coordinates. For **some reason** it only works inside of ongui. So
        //just made the vector public ...
        Camera c = Camera.main;
        Event e = Event.current;
        //Vector2 mousePos = new Vector2();

        // Get the mouse position from Event.
        // Note that the y position from Event is inverted.
       // mousePos.x = e.mousePosition.x;
       // mousePos.y = c.pixelHeight - e.mousePosition.y;

        mouseVector3 = c.ScreenToWorldPoint(Input.mousePosition);

		*/
    }
	
    public void GetSectorCoords()
    {

           
        
    }
	/*
    public void ShowSector()
    {
        //display sector on LEFT click
       // set the sun and planets for the selected system
        
        //array of all children in solarsystem.[0] is the sun
        if (playerConnected.clientSector[_x, _z].star > 0)//there is a sun
        {
            for (int i = 0; i <playerConnected.clientSector[_x, _z].planetIDX.Length +1; i++)
            {//set the sun and planets active
                _planets[i].SetActive(true);
                //this is convoluted.  The array of "planets" start with the sun and work outwards so the sun is planets[0].
                //but the clientSector.planetIDX starts at zero for the first planet, its the index in the allplanets array
                //of the current planet. So that idx will start at 0 ... but the planet array 0 is the sun, 1 is first planet
                //FIX THIS SOMEDAY
                    
                    if(i > 0)
                {
                    //this gets the allplanets index of the first planet, but i is the sun so we 
                    //have to wait until we are at i = 1 then subtract 1 from it
                    int _planetIDX = playerConnected.clientSector[_x, _z].planetIDX[i-1];
                    //now we are on the correct planets array object, i=1 is first planet.
                    TextMesh txtMesh = _planets[i].GetComponentInChildren<TextMesh>();
                    txtMesh.text = "Fuel: " + playerConnected.clientPlanets[_planetIDX].res_fuel + "\n" ;
                    txtMesh.text = txtMesh.text + "Minerals: " + playerConnected.clientPlanets[_planetIDX].resource_2 + "\n";
                    txtMesh.text = txtMesh.text + "Water: " + playerConnected.clientPlanets[_planetIDX].resource_3 + "\n";
                    txtMesh.text = txtMesh.text + "Food: " + playerConnected.clientPlanets[_planetIDX].resource_1 + "\n";
                }
                    
  

            }
        }
        else //no sun or system, disable them all
        {
            for (int i = 0; i < 8; i++)
            {
                _planets[i].SetActive(false);
            }
        }
        //is there a starbase, if yes then show it
        if (playerConnected.clientSector[_x, _z].starport > 0)
        {
            int starportIdx = playerConnected.clientSector[_x, _z].starport;
            TextMesh txtMesh = starBase.GetComponentInChildren<TextMesh>();
            //playerConnected.clientStarbases[starportIdx];
            txtMesh.text = "Fuel: " + playerConnected.clientStarbases[starportIdx].res_fuel + "\n";
            txtMesh.text = txtMesh.text + "Minerals: " + playerConnected.clientStarbases[starportIdx].resource_2 + "\n";
            txtMesh.text = txtMesh.text + "Water: " + playerConnected.clientStarbases[starportIdx].resource_3 + "\n";
            txtMesh.text = txtMesh.text + "Food: " + playerConnected.clientStarbases[starportIdx].resource_1 + "\n";
            starBase.SetActive(true);
        }
        else
        {
            starBase.SetActive(false);
        }

        cams.showSectorCamera();
        fsm.currentstate = fsmstates.sector;
    }*/

    public void ShowStarMap()//called from buttons on Sector View
    {
        //&& !EventSystem.current.IsPointerOverGameObject()
        //ENLARGE SECTOR MAP
        //disable solar system
      
        for (int i = 0; i < 8; i++)
            {
                _planets[i].SetActive(false);
            }

        cams.showMainCamera();
	    //fsm.currentstate = fsmstates.starmap;

    }
    
    
	}
