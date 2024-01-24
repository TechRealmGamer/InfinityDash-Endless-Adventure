using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitiesManager : MonoBehaviour
{
    public static AbilitiesManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(Instance.gameObject);
        Instance = this;
    }

    public void EnableMagnet()
    {
        GetComponent<CoinMagnet>().enabled = true;
    }
}
