/*
Will Ziegler
11/4/2026
1.0

Handles timers for individual objects
*/

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class ObstacleData : MonoBehaviour
{
    private float timer;
    private float startTime;
    private float timeFinal;
    public float speed;
    private Vector3 targetPos;

    private TimeInputScript gameController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameController = GameObject.Find("ScriptHandler").GetComponent<TimeInputScript>();
        targetPos = new Vector3(-7.5f, transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        timer = Time.time - startTime;
        /*if(gameController.GetState() == 1)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPos, step);
            timer = Time.time - startTime;
            
        }*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Start"))
        { 
            startTime = Time.time;
            Debug.Log("Time: " + startTime);
        }
    }

    public float GetTimerFinal()
    {
        return (timer);
    }
}
