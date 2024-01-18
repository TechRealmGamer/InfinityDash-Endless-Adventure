using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    private void OnEnable()
    {
        foreach (Transform child in transform)
            child.gameObject.SetActive(true);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<LevelManager>().AddNewFloor();
            gameObject.SetActive(false);
        }
    }
}
