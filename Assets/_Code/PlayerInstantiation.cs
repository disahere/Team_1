using System;
using UnityEngine;

public class PlayerInstantiation : MonoBehaviour
{
    private GameObject playerPrefab;
    private Vector3 spawnPosition;
    
    [NonSerialized] public GameObject player;

    private void Awake()
    {
        spawnPosition = transform.position;
        playerPrefab = Resources.Load("Prefabs/Player") as GameObject;
        
        player = Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
        Destroy(gameObject);
    }
}