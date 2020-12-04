﻿using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControllerMap : MonoBehaviour
{
    [SerializeField] private float _speed = 4f;
    private Vector3 _movement;
    private bool _hasChange = false;
    private bool _isDead = false;

    private GameManager gameManager;
    private GameObject diaryPanel;
    private GameObject inventoryPanel;
    [SerializeField] private Text diaryTextGUI;
    [SerializeField] private List<ItemSlot> slots;
    [SerializeField] private RuntimeAnimatorController[] runtimeanimators;
    [SerializeField] private GameObject endGamePrefab;


    public RuntimeAnimatorController[] RuntimeAnimators => runtimeanimators;

    private RoomAbstract myRoom;

    private bool networkMine;

    public void Awake() {
        diaryPanel = transform.Find("Canvas").Find("Diary-Panel").gameObject;
        diaryPanel.SetActive(false);
        inventoryPanel = transform.Find("Canvas").GetComponentInChildren<GridLayoutGroup>().gameObject;
    }

    public void SetLocation(Vector3 position)
    {
        transform.localPosition = position;
    }

    public void Move(Vector3 movement)
    {
        _movement = movement;
        _hasChange = true;
    }

    private void FixedUpdate()
    {
        if (_hasChange)
        {
            transform.position += _movement.normalized * Time.fixedDeltaTime * _speed;
            _hasChange = false;
        }
    }

    public Vector3 Movement => _movement.normalized;

    public void SetGameManager(GameManager manager)
    {
        gameManager = manager;
    }

    public void SetRoom(RoomAbstract room)
    {
        ResetInventory();
        ResetDiaryImages();
        myRoom = room;
        SetText(myRoom.DiaryText);
        BuildDiaryImages();
    }

    private void ResetDiaryImages() {
        foreach (Transform child in diaryPanel.transform){
            if (child.CompareTag("DiaryImage")) Destroy(child.gameObject);
        }
    }

    private void ResetInventory()
    {
        for (int i = 0; i < inventoryPanel.transform.childCount; i++)
        {
            inventoryPanel.transform.GetChild(i).GetComponent<ItemSlot>().RemoveImage();
        }
    }

    public void BuildDiaryImages()
    {
        List<Image> imageList = myRoom.DiaryImageList;
        foreach (Image image in imageList)
        {
            if(image!=null) Instantiate(image, diaryPanel.transform);
        }
    }

    private void SetText(Text newText)
    {
        diaryTextGUI.text = newText.text;
    }
    
    public ItemSlot GetFirstFreeSlot()
    {
        return slots.Find(s => s.GetIsFree());
    }

    public bool IsDead
    {
        get => _isDead;
        set => _isDead = value;
    }

    public void FinishGame()
    {
        gameObject.SetActive(false);
        Instantiate(endGamePrefab);
    }
}
