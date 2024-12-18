using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarPort : MonoBehaviour
{
	/*A starport is a non-player owned trading center.  It collects the resources from
	all the planets in its sector for players to buy and sell.  The price is unique and changes.
	It also holds jobs and has new ships for sale or upgrade.
	*/
	public int resource1Amount;
	public int resource2Amount;
	public int resource3Amount;
	public int fuelAmount;
	public int resource1Price;
	public int resource2Price;
	public int resource3Price;
	public int fuelPrice;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
