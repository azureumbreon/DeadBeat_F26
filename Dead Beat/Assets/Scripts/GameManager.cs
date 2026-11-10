/*
Joe Del Balzo
10/29/2026
1.0

handles overarching game mechanics : obstacles being added
*/

using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using Unity.Cinemachine;

public class GameManager : MonoBehaviour
{
    //singleton variable
    public static GameManager instance;

    //general game variables
    bool inGame = false;

    //obstacle prefabs
    public GameObject jumpBoxPrefab;
    public GameObject slideBoxPrefab;
    
    //shortcuts for a track position enum
    private trackPosition left = trackPosition.LEFT;
    private trackPosition middle = trackPosition.MIDDLE;
    private trackPosition right = trackPosition.RIGHT;

    //variables for the camera damage
    public Camera cam;
    private CinemachineImpulseSource impulseSource;
    private Volume volume;
    private Vignette vignette;
    public float vignetteSpeed = 2f;
    private float[] intensityValues = {0.25f, 0.4f, 0.5f, 0.6f};
    private float targetIntensity;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();

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

    // Update is called once per frame
    void Update()
    {
        if (inGame)
        {
            vignette.intensity.value = Mathf.Lerp(vignette.intensity.value, targetIntensity, vignetteSpeed * Time.deltaTime);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        if(scene.name == "game"){
            volume = GameObject.Find("Global Volume").GetComponent<Volume>();
            if (!volume.profile.TryGet(out vignette))
            {
                Debug.LogWarning("No Vignette override found on the volume profile");
            }
            
            GenerateObstacles();
            Scroller.instance.SetUpStartingTiles();
            inGame = true;
            targetIntensity = intensityValues[0];
            vignette.intensity.value = intensityValues[0];
        }
    }

    //sets the target intensity value to change the vignette strength
    public void AdjustCamera(int dmg){
        //quick check to make sure the dmg isn't out of bounds
        dmg = Mathf.Clamp(dmg, 0, intensityValues.Length - 1);
        targetIntensity = intensityValues[dmg];
    }

    //helper method to declog the scene loaded method
    //add obstacles to the map with the AddObstacle method from the scroller
    //the parameters should be in format: 
    //  AddObstacle(type of obstacle, second in appears, which track it appears on)
    private void GenerateObstacles(){
        Scroller.instance.AddObstacle(jumpBoxPrefab, 3f, left);
        Scroller.instance.AddObstacle(jumpBoxPrefab, 12f, right);
        Scroller.instance.AddObstacle(slideBoxPrefab, 12.5f, middle);
        Scroller.instance.AddObstacle(slideBoxPrefab, 8f, right);
        Scroller.instance.AddObstacle(jumpBoxPrefab, 15f, left);
    }

    public void ShakeCamera(float strength = 1f)
    {
        impulseSource.GenerateImpulse(strength);
    }
    
    //getters
    public bool GetInGame(){
        return inGame;
    }

    //enable and disable methods for scene loading
    void OnEnable(){
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnDisable(){
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
