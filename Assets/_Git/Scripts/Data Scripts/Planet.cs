using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
	//This is attached to each planet that exists.  It holds the amount of commodities
	//that are available on this planet.  If there is a starport, then it is transferred 
	//directly to the starport when needed.   If there is no starport then players can directly
	//harvest the resources
	public PlanetTypes planetType;
	public int amount_resource_3;
	public int amount_resource_2;
	public int amount_resource_1;
	public int amount_fuel;
	public int population;
	public int civ_type;
    // Start is called before the first frame update
    void Start()
	{
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
