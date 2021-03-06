﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEntrance : MonoBehaviour
{
    public string transitionID;
    private bool transit;

    private float transitTimerBase = 1f;
    private float transitTimer;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerController.instance.transitioning && PlayerController.instance.transitionID == transitionID)
        {
            Debug.Log("AAAAAAAAAA");
            //PlayerController.instance.gameObject.SetActive(false);
            PlayerController.instance.transform.position = transform.position;
            //PlayerController.instance.gameObject.SetActive(true);           
            CameraContorller.instance.transform.position = new Vector3(transform.position.x, transform.position.y, CameraContorller.instance.transform.position.z);
            PlayerController.instance.transitioning = false;
            transitTimer = transitTimerBase;
            transit = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(transit)
        {
            if(transitTimer >= 0)
            {
                transitTimer -= Time.deltaTime;
            }
            if(transitTimer < 0)
            {
                UIHolder.instance.FadeFromBlack();
                PlayerController.instance.playerLocked = false;
                transit = false;
            }
            
        }
    }
}
