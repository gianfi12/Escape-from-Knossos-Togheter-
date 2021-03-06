﻿using Cinemachine;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WonGameTrigger : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            if(!PhotonNetwork.IsConnected || other.GetComponent<PhotonView>().IsMine) {
                other.GetComponent<PlayerControllerMap>().FinishGame(true); 
                FindObjectOfType<AudioManager>().Play("WinTheme");
            }
        }
    }
}
