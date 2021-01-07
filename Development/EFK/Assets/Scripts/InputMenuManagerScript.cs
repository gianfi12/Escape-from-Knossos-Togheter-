﻿using System;
using UnityEngine;


public class InputMenuManagerScript:MonoBehaviour
{
    [SerializeField] private GameObject exitGamePrefab;
    private GameObject _exitGamePrefab;
    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if(_exitGamePrefab==null)
                _exitGamePrefab = Instantiate(exitGamePrefab);
            else
            {
                Destroy(_exitGamePrefab);
            }
        }
    }
}