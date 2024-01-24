using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    private AudioSource audioSource;
    
    [SerializeField] private AudioClip coinSound;
    [SerializeField] private GameObject Abilities;
    [SerializeField] private AudioClip magnetSound;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "GoldCoin")
        {
            audioSource.PlayOneShot(coinSound);
            other.gameObject.SetActive(false);
        }
        else if (other.gameObject.tag == "Magnet")
        {
            audioSource.PlayOneShot(magnetSound);
            other.gameObject.SetActive(false);
            AbilitiesManager.Instance.EnableMagnet();
        }
    }
}
