/*
Joe Del Balzo
10/29/2026
1.0

Handles the individual railroad: instantiates obstacles and track position
*/

using UnityEngine;

public class Railroad : MonoBehaviour
{
    public trackPosition order;
    private TimeInputScript gameController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //spawns the given obstacle at the given pos on the track
    public void SpawnObstacle(GameObject obstacle, float worldX){
        gameController = GameObject.Find("ScriptHandler").GetComponent<TimeInputScript>();

        Vector3 pos = new Vector3(worldX, obstacle.transform.position.y, transform.position.z);
        GameObject newObst = Instantiate(obstacle, pos, Quaternion.identity);
        newObst.transform.SetParent(transform, true);
        gameController.AddObstacle(newObst);
    }

    //setter for track position
    public void SetOrder(trackPosition pos){
        order = pos;
    }

    //getter for track position
    public trackPosition GetOrder(){
        return order;
    }
}
