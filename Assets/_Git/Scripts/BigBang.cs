using UnityEngine;
using UnityEngine.UI;
using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Vectrosity;

public class BigBang : NetworkBehaviour {

	public static BigBang instance;
	public int mapSizeX = 1000;
	public int mapSizeY = 1000;
    public int starSystemChance = 100;
	private static bool BIGBANG = false;
	//Gameobject in the scene that we parent all the sector objects to
    public GameObject starmap;
	//This is the gameobject that represents the visual and data for each sector.
	//We can change the sprite to show on the map what is in this sector
	public GameObject sectorPrefab;
	//Images we put onto the sectorPrefab to visually show what is in the sector
	public Sprite yellowStar_sprite,starPort_sprite,blackHole_sprite;
	//This is the actual object in the scene that contains all the visual and data info
	GameObject sectorObject;
    
    public TextAsset fileStarSystemNames;
    private string[] StarSystemNames; //array of all StarSystemNames sorted from TextAsset file

    //defines all the info in each sector
    public struct structStarSystem {
        public int x; //the SECTOR coords.Worldspace is derived by 
        public int y; //the SECTOR coord
        public int ID; //index
        public string systemname; //index of starname from StarSystemNames[]
        public int star; //does it have a star
        public int numPlanets; //how many planets
        public int npcStarport; //0 is no, keep array of prices
        public int playerStarbase;
        public int resource_1;
        public int resource_2;
        public int resource_3;
        public int res_fuel;
        public int civType; //affects how much tax and resources(like food lol) we can get from them
        public int population;
    }

    
	//This contains all the data for every sector in the game so the server can cycle through the list
    public structStarSystem[,] sectorGrid;
   
    void Start() {
        //sort out StarSystemNames on client and server so we can use an index
        StarSystemNames = fileStarSystemNames.text.Split("\n"[0]);

        //only create galaxy on the server and only run ONCE (server object is being created twice in error)
        if (isServer && BIGBANG == false) {
            //removing duplicate names from StarSystemNames file
            List<string> _tempStarSystemNames = new List<string>();
            for (int i = 0; i < StarSystemNames.Length; i++) {
                if (_tempStarSystemNames.Contains(StarSystemNames[i]) == false) _tempStarSystemNames.Add(StarSystemNames[i]);
            }
            StarSystemNames = _tempStarSystemNames.ToArray();
            print(StarSystemNames.Length + " unique StarSystemNames available");
	        //------------------------------------------
	        //This is a list of ALL the sectors in the universe
	        sectorGrid = new structStarSystem[mapSizeX, mapSizeY];
            bigBang();
        }
    }

    void bigBang() {
        //if we already created the galaxy dont run this again.
        //there is an error somewhere that server object is created twice so multi Bangs!
	    BIGBANG = true;
        
	    int starnameIndex = 0; //used for which name is selected from list
	    
	    //internel counters of objects created just for the log output
	    int sectorID =0;
	    int systemCount = 0; //how many systems are created
	    int planetCount = 0; //total number of planets
        int starportcount = 0; //how many starports total
	    int blackholecount = 0; //how many black holes
        
        int _xcoord = 0; //the actual coordinates on the Galaxy plane -5000 to 5000
        int _ycoord = 0; // makes outer systems more rare and place the sun sprites

        //THE GALAXY SIZE:
        //Each sector could be 1 point on an X-Y grid, but it needs to be large enough to display the star and ship images.
        //So we are making each sector 16x16
        //the coord grid on the galaxy object is -5000 in lower left to +5000 in upper right with 0x0 in center.
        //we are dividing the map which is 10,000x10,000 into "sectors" that are 10x10 square so 1000x1000 on the map.
        //our 2d Array needs indexs from [0,0] to [1000,1000]
        for (int x = 0; x < 1000; x++) {
            _xcoord = ((x * 16) - 5000);
            for (int y = 0; y < 1000; y++) {
	            _ycoord = ((y * 16) - 5000);
	            
	            //assign to each sector coords for reference, 
	            sectorGrid[x, y].x = x;
	            sectorGrid[x, y].y = y;
	            sectorGrid[x, y].ID = sectorID;
	            sectorID++;

                //rndMax is a number that is going to be HIGHER as the coords get farther from map center (0,0)
                //it also adjusts GREATER as we get farther in Y top/bottom direction
                //makes stars more likely in the center of the map in a horizontal band
                int rndMax = Mathf.Abs(_xcoord) + 4 * (Mathf.Abs(_ycoord));
                /************************
                Every sector contains something, either empty space prefab which can contain an outpost or ship
                or it has a star system. we create one prefab or the other.
                if its LESS than the starSystemChance (high .. 70-90) of a empty space, then it has nothing, 
                */

                if (Random.Range(0, rndMax + 1000) < starSystemChance) {
                    //Create the star system
	                systemCount++;
                    
                    sectorGrid[x, y].star = 1; //Random.Range(1,3); //yellow,blue,red
                    //if we run out of unique names, get and reuse some random ones from the list
                    if (systemCount >= StarSystemNames.Length) {
                        sectorGrid[x, y].systemname = StarSystemNames[Random.Range(0, StarSystemNames.Length)] + "II";
                    } else {
                        sectorGrid[x, y].systemname = StarSystemNames[systemCount];
                    }

                    sectorGrid[x, y].npcStarport = 0; //no starport by default
                    //how many planets are there
	                sectorGrid[x, y].numPlanets = Random.Range(1, 8);
	                planetCount += sectorGrid[x, y].numPlanets;
                    /* ---------------------
                    * resources are in random ranges from 0 to 100 in kilotons. 
                    * ---------------------*/
                    sectorGrid[x, y].res_fuel = (Random.Range(0, 100) * sectorGrid[x, y].numPlanets);
                    sectorGrid[x, y].resource_3 = (Random.Range(0, 100) * sectorGrid[x, y].numPlanets);
                    sectorGrid[x, y].resource_2 = (Random.Range(0, 100) * sectorGrid[x, y].numPlanets);
                    sectorGrid[x, y].resource_1 = (Random.Range(0, 100) * sectorGrid[x, y].numPlanets);
                    sectorGrid[x, y].civType = (Random.Range(0, 10));
                    sectorGrid[x, y].population = sectorGrid[x, y].civType * (Random.Range(0, 32000) * sectorGrid[x, y].numPlanets); //random population, adjust later                        

                    //is there an NPC starport? rnd -50 to 0, clamps to -1(none),0(Fed starport)
	                if ((Random.Range(0, 10)) < 1) 
	                {
                        sectorGrid[x, y].npcStarport = 1; //index of this starbase in the List, 
                        sectorGrid[x, y].playerStarbase = 0;
                        //count the Federation Starports for Log
                        starportcount++;
	                    sectorObject = Instantiate(sectorPrefab);
						NetworkServer.Spawn(sectorObject);
	                    sectorPrefab.GetComponent<SpriteRenderer>().sprite = starPort_sprite;

                    } else {
	                    sectorObject = Instantiate(sectorPrefab);
	                    NetworkServer.Spawn(sectorObject);
	                    sectorPrefab.GetComponent<SpriteRenderer>().sprite = yellowStar_sprite;
                    }

                    //all done!
	                //set the info on the scene gameobject
	                sectorObject.GetComponent<starSysteminfo>().ID = sectorGrid[x, y].ID;
                    sectorObject.GetComponent<starSysteminfo>().x_coord = x;
                    sectorObject.GetComponent<starSysteminfo>().y_coord = y;
                    sectorObject.GetComponent<starSysteminfo>().name = sectorGrid[x, y].systemname;
                    sectorObject.GetComponent<starSysteminfo>().planetcount = sectorGrid[x, y].numPlanets;
                    sectorObject.GetComponent<starSysteminfo>().resource_1 = sectorGrid[x, y].resource_1;
                    sectorObject.GetComponent<starSysteminfo>().resource_2 = sectorGrid[x, y].resource_2;
                    sectorObject.GetComponent<starSysteminfo>().resource_3 = sectorGrid[x, y].resource_3;
                    sectorObject.GetComponent<starSysteminfo>().res_fuel = sectorGrid[x, y].res_fuel;
                    sectorObject.GetComponent<starSysteminfo>().civType = Random.Range(0, 3);
                    sectorObject.GetComponent<starSysteminfo>().population = sectorObject.GetComponent<starSysteminfo>().civType * Random.Range(10, 32000);

                    sectorObject.transform.position = new Vector3((float)x, 1f, (float)y);
                    sectorObject.GetComponentInChildren<TextMesh>().text = sectorGrid[x, y].systemname;

                } else {
	                //Not a star system. Can contain a black hole or other
                    if (Random.Range(0, 50000) < 1) {
	                    sectorObject = Instantiate(sectorPrefab);
	                    NetworkServer.Spawn(sectorObject);
	                    sectorPrefab.GetComponent<SpriteRenderer>().sprite = blackHole_sprite;
                        sectorObject.transform.position = new Vector3((float)x, 1f, (float)y);
                        blackholecount++;
                    } else{
	                    sectorGrid[x, y].star = 0; //Random.Range(1,3); //yellow,blue,red
	                    sectorGrid[x, y].npcStarport = 0; //no starport by default
	                    //how many planets are there
	                    sectorGrid[x, y].numPlanets = 0;
                    	//this is empty space, no sprite
                    }
                }
	            //end creating current sector coords
            }
        }
	    //Test fuction
	    //CreateRandomLines(0);
	    Debug.Log("BIGBANG OUTPUT: \nTotal Sectors: "+sectorGrid.Length +"\nStar Systems: "+systemCount +"\nPlanets: "+planetCount+"\nStarPorts: "+starportcount+"\nBlack Holes: "+blackholecount+"\n");
        //Galaxy has been created.
        //We have a prefab in each sector that represents the star system and its potential starbase
    }
			
	

    public void CreateRandomLines(int lineCount) {
        Transform[] children = starmap.GetComponentsInChildren<Transform>();
        for (int i = 0; i < lineCount; i++) {
            Transform start = children[Random.Range(1, children.Length)];
	        Transform end = children[Random.Range(1, children.Length)];
	        Debug.Log("Start "+start.position +" End "+start.position);
	        //VectorLine.SetLine (Color.green, new Vector2(start.position.x, start.position.y), new Vector2(Screen.width-1,
	        //  Screen.height-1));
	        
	        VectorLine.SetLine3D (Color.green, start.position, end.position);
        }
    }

   
}