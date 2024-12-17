using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarPort : MonoBehaviour
{
	//attached to each starport and holds the amount of commodities players can pick up from here.
	//these are what is available from all the planets in the sector plus what is sold by traders
	public int amount_resource_3;
	public int amount_resource_2;
	public int amount_resource_1;
	public int amount_fuel;
	public int price_resource_3;
	public int price_resource_2;
	public int price_resource_1;
	public int price_fuel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
