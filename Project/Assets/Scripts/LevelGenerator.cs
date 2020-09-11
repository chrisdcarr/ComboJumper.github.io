using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    /*
    public GameObject platform_prefab;
    public int num_of_platforms=50;
    public float level_width = 3f;
    public float min_y = 1.5f;
    public float max_y = 5f;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 spawn_pos = new Vector3();
        spawn_pos.y = -1;
        for (int i=0;i<num_of_platforms;i++)
        {
            spawn_pos.y += Random.Range(min_y, max_y);
            spawn_pos.x = Random.Range(-level_width, level_width);
            Instantiate(platform_prefab, spawn_pos, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    */

    class PoolObject
    {
        //the transform of the object (so u can get location data)
        public Transform transform;
        //is the item on screen to be used
        public bool inUse;
        //constructor to get and set transform
        public int index;
        public PoolObject(Transform t)
        {
            transform = t;
        }
        //sets in use to true 
        public void Use()
        {
            inUse = true;
        }
        //sets in use to false
        public void Dispose()
        {
            inUse = false;
        }

        public int GetIndex()
        {
            return index;
        }

        public void SetIndex(int i)
        {
            index = i;
        }

    }

    [System.Serializable]
    public struct YspawnRange
    {
        public float min;
        public float max;
    }

    //the game object that is being pooled
    public GameObject Prefab;
    //how many exist at one point
    public int poolSize;
    //the spawn range on the y axis
    public YspawnRange YSpawnRange;
    //what area of the screen does the object instanciate
    public Vector3 defaultSpawnPos;
    //to have the default spawn pos be relative to the aspect ratio of the device used
    //public Vector2 targetAspectRatio;
    //the width of the screen
    public float levelWidth = 3.5f;
    //screen height
    public float screenHeight;
    //array to hold the pool objects
    PoolObject[] poolObjects;

    private void Awake()
    {
        Configure();
    }

    private void Start()
    {
        for (int i=0;i<poolSize;i++)
        {
            Spawn();
        }
        
    }

    private void Update()
    {
        for (int i=0 ; i < poolSize ; i++)
        {
            CheckDisposeObject(poolObjects[i]);
        }
    }

    void Configure()
    {
        //get the screen height 
        screenHeight = Camera.main.orthographicSize;
        poolObjects = new PoolObject[poolSize];
        for (int i=0;i<poolObjects.Length;i++)
        {
            //instantiate the game object
            GameObject go = Instantiate(Prefab) as GameObject;
            //get the transform of the object
            Transform t = go.transform;
            //set the parent of the object to the object this script is attached to
            t.SetParent(transform,false);
            //turn off the object 
            t.gameObject.SetActive(false);
            //create new object from the class to instanciate it with the transform
            poolObjects[i] = new PoolObject(t);
            poolObjects[i].SetIndex(i);
        }
    }

    void Spawn()
    {
        Transform t = GetPoolObject();
        if(t == null)
        {
            return; //if true pool object too small
        }
        
        
        //fresh position
        Vector3 pos = Vector3.zero;
        //a width between the left of the screen and right of the screen (subject to change)
        pos.x = Random.Range(-levelWidth, levelWidth);
        //if the game now started then spawn from a low point 
        //and go up otherwise get the position of the element 
        //before you and spawn on top of it
        int score = gameObject.GetComponent<MasterScript>().GetScore();
        if(score==0)
        {
            
            
            
            for (int i = 0; i < poolObjects.Length; i++)
            {
                if (t.transform.position == poolObjects[i].transform.position)
                {
                    
                    //if its element 0 then refernce element "length-1"
                    if (i == 0)
                    {
                        //pos.y = poolObjects[poolSize - 1].transform.position.y + Random.Range(YSpawnRange.min, YSpawnRange.max);
                        pos.y = Random.Range(2,4)*(-screenHeight/5);
                        t.name = i.ToString();
                    }
                    else
                    {
                        pos.y = poolObjects[i - 1].transform.position.y + Random.Range(YSpawnRange.min, YSpawnRange.max);
                        //pos.y = 3;
                        t.name = i.ToString();
                    }

                    //Debug.Log(t.name + " is at " + i);
                    break;
                }
            }
        }
        else
        {
            
            //spawn above previous item
            for (int i = 0; i < poolObjects.Length; i++)
            {
                if (t.transform.position == poolObjects[i].transform.position)
                {
                    //if its element 0 then refernce element "length-1"
                    if(i==0)
                    {
                        pos.y = poolObjects[poolSize-1].transform.position.y + Random.Range(YSpawnRange.min, YSpawnRange.max);
                    }
                    else
                    {
                        pos.y = poolObjects[i - 1].transform.position.y + Random.Range(YSpawnRange.min, YSpawnRange.max);
                        
                    }
                    //Debug.Log(t.name + " is at " + i);
                }
            }
        }

        t.position = pos; //Debug.Log("object global pos ="+t.position);
        t.gameObject.SetActive(true);
    }

    void CheckDisposeObject(PoolObject poolObject)
    {
        //if the pool object is far off screen 
       
        if(poolObject.transform.position.y < Camera.main.transform.position.y-screenHeight&& poolObject.inUse)
        {
            Debug.Log("offscreen");
            poolObject.Dispose();
            poolObject.transform.gameObject.SetActive(false);
            Spawn();
        }
    }

    //return the transform of a pool object
    Transform GetPoolObject()
    {
        for(int i =0;i<poolObjects.Length;i++)
        {
            //check if the pool object is not in use
            if(!poolObjects[i].inUse)
            {
                //set the object to in use and return the transform of it
                poolObjects[i].Use();
                return poolObjects[i].transform;
            }
        }

        return null;
    }
}
