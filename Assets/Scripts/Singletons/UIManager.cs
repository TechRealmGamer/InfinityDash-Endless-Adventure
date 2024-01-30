using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public TMP_Text scoreText;
	private bool isGamePaused = false;
	public GameObject gamePausePanel;

	private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(Instance);
        Instance = this;
    }
	public void PauseOrResumeGame()
	{
		isGamePaused = !isGamePaused;
		gamePausePanel.SetActive(isGamePaused);
		Time.timeScale = isGamePaused ? 0.0f : 1.0f;
		/*Cursor.lockState = isGamePaused ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isGamePaused;*/
	}
}
