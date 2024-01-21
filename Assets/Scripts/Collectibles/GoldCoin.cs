using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCoin : MonoBehaviour
{
    private void Update()
    {
        transform.Rotate(0f, 0f, 100f * Time.deltaTime);
    }
}
