using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private float currentPositionForFloor = 50;

    private ObjectPool floorsPool;

    private void Awake()
    {
        floorsPool = GetComponent<ObjectPool>();
    }

    private void Start()
    {
        for(int i = 0; i < 5; i++)
        {
            AddNewFloor();
        }
    }

    public void AddNewFloor()
    {
        GameObject floor = floorsPool.GetFromPool();
        floor.transform.position = Vector3.forward * currentPositionForFloor;
        currentPositionForFloor += 100;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
