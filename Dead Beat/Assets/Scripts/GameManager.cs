/*
Joe Del Balzo
10/29/2026
1.0

handles overarching game mechanics : obstacles being added
*/

using UnityEngine;

public class GameManager : MonoBehaviour
{
    //variables for adding obstacles
    public Scroller scroller;

    //obstacle prefabs
    public GameObject jumpBoxPrefab;
    public GameObject slideBoxPrefab;
    
    //shortcuts for a track position enum
    private trackPosition left = trackPosition.LEFT;
    private trackPosition middle = trackPosition.MIDDLE;
    private trackPosition right = trackPosition.RIGHT;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scroller.AddObstacle(jumpBoxPrefab, 3f, left);
        scroller.AddObstacle(jumpBoxPrefab, 12f, right);
        scroller.AddObstacle(slideBoxPrefab, 12.5f, middle);
        scroller.AddObstacle(slideBoxPrefab, 8f, right);
        scroller.AddObstacle(jumpBoxPrefab, 15f, left);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
