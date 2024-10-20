using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class panelManager : MonoBehaviour {
    /*
     * enable and disable all UI panels for the current view
     
      */
	public  GameObject[] displayPanels;
	public GameObject loginView,buttonRow,mapView,shipView,baseView,planetView;
	public GameObject loginCamera,mapCamera,sectorCamera;
	public GameObject ButtonRow;
	public GameObject CreateRaceView;
    

void Start()
    {

    }
    
	public void switchPanelView(int _index)
	{
		foreach(GameObject go in displayPanels)
		{
			go.SetActive(false);
			
		}
		//ButtonRow.SetActive(true);
		displayPanels[_index].SetActive(true);
	}
	public void enableMapView()
	{
		foreach(GameObject go in displayPanels)
		{
			go.SetActive(false);
			
		}
		//ButtonRow.SetActive(true);
		displayPanels[1].SetActive(true);
		loginCamera.SetActive(false);
		mapCamera.SetActive(true);
		
	}
	
	public void enableSystemView(int systemID)
	{
		
		loginCamera.SetActive(false);
		mapCamera.SetActive(false);
		sectorCamera.SetActive(true);
	}
	
	public void createRace()
	{
		CreateRaceView.SetActive(true);
	}
	
	
	
}
