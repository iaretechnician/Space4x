using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchCamera : MonoBehaviour {
//each camera we have 
	public GameObject mainCam,sectorCam,loginCam;
    void Start()
    {
        
        
    }

	public void showMainCamera()
	{
		sectorCam.SetActive(false);
		loginCam.SetActive(false);
		mainCam.SetActive(true);
	
	}
      
    public void showSectorCamera()
    {
	    sectorCam.SetActive(true);
	    loginCam.SetActive(false);
	    mainCam.SetActive(false);
    }
    public void showShipCamera()
    {
	    sectorCam.SetActive(false);
	    loginCam.SetActive(true);
	    mainCam.SetActive(false);
    }
}
