using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMagnet : MonoBehaviour
{
    [SerializeField] private float magnetRange = 10f;
    [SerializeField] private float duration = 8f;

    private void OnEnable()
    {
        Invoke("DisableMagnet", duration);
    }

    private void Update()
    {
        if(Physics.SphereCast(PlayerController.Instance.transform.position, magnetRange, Vector3.forward, out RaycastHit hit))
        {
            if (hit.collider.CompareTag("GoldCoin"))
                hit.collider.GetComponent<GoldCoin>().Magnetize();
        }
    }

    private void DisableMagnet()
    {
        enabled = false;
    }
}
