/*
Joe Del Balzo
10/23/2026
1.0

handles the individual track tile
*/

using UnityEngine;
using System.Collections;

public enum trackPosition {LEFT, MIDDLE, RIGHT}

public class TrackTile : MonoBehaviour
{
    public float pivotOffset = 7f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //starts the despawn process
    public void StartDespawn(){
        StartCoroutine(Despawn());
    }

    //after a second, destroys the track
    private IEnumerator Despawn(){
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }


    //determines the correct track the obstacle is being spawned on 
    // and tells that railroad to spawn it
    public void NewObstacle(GameObject obstacle, float localX, trackPosition order){
        foreach(Railroad r in GetComponentsInChildren<Railroad>()){
            if(order == r.GetOrder()){
                r.SpawnObstacle(obstacle, transform.position.x - pivotOffset + localX);
                break;
            }
        }
    }
}
