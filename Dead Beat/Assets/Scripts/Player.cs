/*
Joe Del Balzo
10/23/2026
1.0

Handles the player's movement
*/

using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class Player : MonoBehaviour
{

    //track POS variables - left = 0, middle = 1, right = 2
    public Transform[] positions = new Transform[3];
    private Vector3 targetPOS;
    private int index = 1;

    //movement variables
    public float speed = 10f;

    //sliding variables
    private bool isSliding = false;
    public float slamSpeed = 10f;
    private Coroutine slideRoutine;

    //jumping variables
    private bool isJumping = false;
    public float jumpForce = 8f;
    public float gravity = -20f;
    public float groundY = 1f;
    private float velocityY;

     //health variables - when currentDamage = maxDamage -> the player has died
    private const int maxDamage = 3;
    private int currentDamage = 0;
    private bool isRegening = false;
    private float regenTime = 5f; //the amount of time the player has to not get hit to regen 
    private float regenCountdown;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetPOS = PosToVector3(positions[index]);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;

        //moves the players z position (horizontal)
        pos.z = Mathf.Lerp(pos.z, targetPOS.z, speed * Time.deltaTime);

        //moves the players y position (vertical)
        velocityY += gravity * Time.deltaTime;
        pos.y += velocityY *Time.deltaTime;

        //makes it so the player doesn't fall below the floor
        float floorY = isSliding ? 0.5f : groundY;
        if(pos.y < floorY){
            pos.y = floorY;
            velocityY = 0;
            isJumping = false;
        }

        //moves the player to the correct pos
        transform.position = pos;

        //regen logic
        if (isRegening)
        {
            //if the time is up
            if(Time.time >= regenCountdown)
            {
                //decrease the damage and tell the game manager
                currentDamage--;
                regenCountdown = Time.time + regenTime;
                GameManager.instance.AdjustCamera(currentDamage);
                //if we are at full health, stop regenning
                if(currentDamage == 0)
                    isRegening = false;;
            }
        }
    }

    //moves the player to one track to the left
    void OnMoveLeft(InputValue value){
        if(index <= 0 || (isJumping && isSliding)) return; //checks to make sure we are not the most left

        index--;
        targetPOS = PosToVector3(positions[index]);
        isSliding = false;
    }

    //moves the player to one track to the right
    void OnMoveRight(InputValue value){
        if(index >= 2 || (isJumping && isSliding)) return;

        index++;
        targetPOS = PosToVector3(positions[index]);
        isSliding = false;
    }

    void OnJump(InputValue value){
        if(isJumping) return;

        //stands up first if sliding so the jump doesn't get eaten
        if (isSliding){
            Vector3 temp = transform.position;
            temp.y = groundY;
            transform.position = temp;
        }

        isSliding = false;
        isJumping = true;
        velocityY = jumpForce;
    }

    //makes the player slide
    void OnSlide(InputValue value){
        if (slideRoutine != null) StopCoroutine(slideRoutine);
        slideRoutine = StartCoroutine(Slide());
    }

    //rotates and lowers the player to simulate sliding
    private IEnumerator Slide(){
        transform.localEulerAngles = new Vector3(0, 0, 90);
        targetPOS.y = 0.5f;
        isSliding = true;
        float endTime = Time.time + 1f;
        if (isJumping){ //makes the player fall faster if sliding when jumping
            velocityY = Mathf.Min(velocityY, -slamSpeed);
        }
        //ends the slide when 1 second is over or if the player cancels the slide
        yield return new WaitUntil(() => Time.time >= endTime || !isSliding);
        isSliding = false;
        transform.localEulerAngles = new Vector3(0, 0, 0);
        targetPOS.y = 1f;
    }

    //helper method to turn a transform pos into a Vector3
    private Vector3 PosToVector3(Transform pos){
        return new Vector3(pos.position.x, transform.position.y, pos.position.z);
    }

    //what happens when the player collides with an object
    void OnTriggerEnter(Collider col){
        if(col.tag == "Obstacle"){
            currentDamage++;
            isRegening = true;
            regenCountdown = Time.time + regenTime;
            GameManager.instance.AdjustCamera(currentDamage);
            GameManager.instance.ShakeCamera();
        }
    }
    


}
