using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public float score = 0;
    public int scoreModifier = 1;

    private void Update()
    {
        score += Time.deltaTime * scoreModifier;
        UIManager.Instance.scoreText.text = ((int) (score * 25)).ToString();
    }
}
