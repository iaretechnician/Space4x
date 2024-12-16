using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrthoCamZoom : MonoBehaviour {
	public float minOrthoSize = 100;
	public float maxOrthoSize = 1000;
    public int sizeChange = 10;
	public Camera orthoCamera;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
		if (Input.GetAxis ("Mouse ScrollWheel") < 0 && orthoCamera.orthographicSize < maxOrthoSize) 
        {
            orthoCamera.orthographicSize += sizeChange;
        }


        if (Input.GetAxis("Mouse ScrollWheel") > 0 && orthoCamera.orthographicSize > minOrthoSize)
        {
            orthoCamera.orthographicSize -= sizeChange;
        }
		
	}
}
