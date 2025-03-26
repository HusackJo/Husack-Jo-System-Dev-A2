using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerObject;

    public delegate void SpawnPlayer();
    public event SpawnPlayer spawnPlayer;

    public void CuePlayerSpawn()
    {
        playerObject.SetActive(true);
        spawnPlayer?.Invoke();
    }
}
