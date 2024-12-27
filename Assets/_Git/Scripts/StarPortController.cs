using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarPortController : MonoBehaviour
{
	/*
	Controller for all starports
	*/
	public static StarPortController instance;
	public List<StarPort> starPorts = new List<StarPort>();
	
	// Start is called before the first frame update
	void Awake()
	{
		if(instance == null)
		{
			instance = this;
		}else
		{
			Destroy(this);
		}
	}
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
