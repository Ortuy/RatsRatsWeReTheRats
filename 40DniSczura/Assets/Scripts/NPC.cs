﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public GameObject dialogBox;
    public Text dialogText;
    public Text nameText;
    public Image portrait;
    public string[] dialog;
    public bool playerInRange;
    public int currentLine;

    public bool beginQuest, triggerQuest;
    public int questID;
    public string questTrigger;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerInRange)
        {
            if (dialogBox.activeInHierarchy && (currentLine == dialog.Length))
            {
                ExitDialogue();
            }
            else
            {
                CameraContorller.instance.lerpSpeed = 0.1f;
                CameraContorller.instance.anchor = transform;
                CameraContorller.instance.cameraSize = 4f;
                dialogBox.SetActive(true);
                CheckIfName();
                dialogText.text = dialog[currentLine];
                currentLine++;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            ExitDialogue();
        }
    }

    private void ExitDialogue()
    {
        CameraContorller.instance.anchor = PlayerController.instance.transform;
        CameraContorller.instance.cameraSize = 5f;
        CameraContorller.instance.lerpSpeed = 0.2f;
        currentLine = 0;

        if(beginQuest)
        {
            QuestManager.instance.questList[questID].questStarted = true;
        }
        if(triggerQuest)
        {
            QuestManager.instance.TriggerQuest(questID, questTrigger);
        }

        dialogBox.SetActive(false);
    }

    private void CheckIfName()
    {
        if(dialog[currentLine].StartsWith("--"))
        {
            nameText.text = dialog[currentLine].Replace("--", "") + ":";
            portrait.sprite = GameManager.instance.GetPortrait(dialog[currentLine].Replace("--", ""));
            Debug.Log(dialog[currentLine].Replace("--", ""));
            currentLine++;
        }
    }
}