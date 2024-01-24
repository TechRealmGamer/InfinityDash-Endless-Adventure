using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCoin : MonoBehaviour
{
    private void Update()
    {
        transform.Rotate(0f, 0f, 100f * Time.deltaTime);
    }

    public void Magnetize()
    {
        Debug.Log("Coin Magnet triggered");
        StartCoroutine(MagnetizeRoutine());
    }

    private IEnumerator MagnetizeRoutine()
    {
        float timer = 0f;
        float duration = 0.2f;
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = PlayerController.Instance.transform.position;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            transform.position = Vector3.Lerp(startPosition, targetPosition, timer / duration);
            yield return null;
        }
    }
}
