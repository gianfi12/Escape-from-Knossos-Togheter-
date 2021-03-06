﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombinationPanel : InteractableObject
{
    [SerializeField] private int slotsNumber;
    [SerializeField] private bool isTotem;
    private GameObject panel;
    private Canvas canvasToReturn;
    private ItemSlot[] slots;
    private PlayerControllerMap playerController;
    private bool hasBeenSetted;

    public GameObject Panel => panel;

    public ItemSlot[] Slots => slots;

    void Start()
    {
        slots = new ItemSlot[slotsNumber];
        panel = transform.parent.GetComponentInChildren<Canvas>().GetComponentInChildren<Image>().gameObject;
        GameObject slotPanel = panel.GetComponentInChildren<GridLayoutGroup>().gameObject;
        for (int i = 0; i < slotPanel.transform.childCount; i++)
        {
            slots[i] = slotPanel.transform.GetChild(i).GetComponent<ItemSlot>();
        }
        panel.gameObject.SetActive(false);
    }

    public override void Interact(GameObject player)
    {
        if (!hasBeenSetted)
        {
            hasBeenSetted = true;
            playerController = player.GetComponent<PlayerControllerMap>();
        }

        if (!_hasBeenActivated)
        {
            _hasBeenActivated = true;
            player.GetComponent<PlayerInput>()._canMove = false;
            canvasToReturn = panel.transform.parent.GetComponent<Canvas>();
            Canvas playerCanvas = player.GetComponentInChildren<Canvas>();
            panel.transform.SetParent(playerCanvas.transform);
            panel.transform.SetAsFirstSibling();
            foreach (var slot in slots)
            {
                slot.SlotImage.Canvas = playerCanvas;
            }
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            panel.gameObject.SetActive(true);
            playerController.IsPanelActive = true;
            if (isTotem) FindObjectOfType<AudioManager>().Play("TotemOn");
            else FindObjectOfType<AudioManager>().Play("RunePanelOn");
        }
        else
        {
            ClosePanel(0);
        }
    }

    public void ClosePanel(float time)
    {
        StartCoroutine(WaitBeforeClosing(time));
    }

    IEnumerator WaitBeforeClosing(float time)
    {
        if (time > 0) yield return new WaitForSeconds(time);
        _hasBeenActivated = false;
        playerController.GetComponent<PlayerInput>()._canMove = true;
        panel.transform.SetParent(canvasToReturn.transform);
        foreach (var slot in slots)
        {
            slot.SlotImage.Canvas = canvasToReturn;
        }
        panel.gameObject.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        playerController.IsPanelActive = false;
    }

    public void TriggerWrongCombination() {
        if(playerController != null) playerController.IncrementTimer(-20);
    }
}
