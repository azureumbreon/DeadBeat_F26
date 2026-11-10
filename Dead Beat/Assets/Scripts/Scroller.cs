/*
Joe Del Balzo
10/23/2026
1.0

Handles moving the environment towards the player
*/
using UnityEngine;
using System.Collections.Generic;

public class Scroller : MonoBehaviour
{
    //singleton variable
    public static Scroller instance;
    
    //variables
    public float speed = 10f;
    public Transform despawnPOS;
    private const float trackLength = 14f; //length of an individual track
    private float pivotOffset = trackLength / 2f;

    public List<TrackTile> tiles = new List<TrackTile>();
    public TrackTile trackPrefab;
    private int nextIndex = 0;

    //structure that will hold an obstacle that needs to be spawn
    private struct PendingObstacle {
        public GameObject prefab;
        public trackPosition track;
        public float localX;
    }

    //holds all obstacles that need to be spawned
    private Dictionary<int, List<PendingObstacle>> pending = new Dictionary<int, List<PendingObstacle>>();

    void Awake()
    {
        if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.GetInGame() == true){
            for (int i = tiles.Count - 1; i >= 0; i--){
                TrackTile tile = tiles[i];

                //moves toward the player
                tile.transform.Translate(Vector3.left * speed * Time.deltaTime);

                //checks if a tile has reached the despawn point
                //if it has, start its despawn, remove it from the list, and add a new one after the last tile
                if(tile.transform.position.x <= despawnPOS.position.x){
                    tile.StartDespawn();
                    tiles.RemoveAt(i);
                    TrackTile temp = Instantiate(trackPrefab, new Vector3(tiles[tiles.Count - 1].transform.position.x + trackLength, 0, 0), Quaternion.identity);
                    SetUpTile(temp);
                    tiles.Add(temp);
                }
            }
        }
    }

    //sets up the starting tiles when the game is first loaded
    public void SetUpStartingTiles(){
        for(int i = 0; i < tiles.Count; i++){
            SetUpTile(tiles[i]);
        }
    }

    //adds the obstacle to list of obstacles to be spawned
    public void AddObstacle(GameObject obstacle, float second, trackPosition order){
        //converst second into a distance and then a location
        float distance = second * speed;
        float fromStart = distance - (tiles[0].transform.position.x - pivotOffset);
        int index = Mathf.FloorToInt(fromStart / trackLength); //determines what tile to spawn on
        float localX = fromStart - index * trackLength; //offset the position into the tile

        if(!pending.ContainsKey(index)){
            pending[index] = new List<PendingObstacle>();
        }
        pending[index].Add(new PendingObstacle{ prefab = obstacle, track = order, localX = localX});
    }

    //helper method that checks if an obstacle should be on a tile and adds that obstacle
    private void SetUpTile(TrackTile tile){
        int index = nextIndex++;
        if(pending.TryGetValue(index, out List<PendingObstacle> list)){
            foreach(PendingObstacle p in list){
                tile.NewObstacle(p.prefab, p.localX, p.track);
            }
            pending.Remove(index);
        }
    }
}
