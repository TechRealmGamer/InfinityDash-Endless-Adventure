using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{

	[SerializeField] private AudioMixer audioMixer;
	[SerializeField] private Slider musicSlider;
	[SerializeField] private Slider soundSlider;
	public GameObject LoadingScreen;
	public Image LoadingBarFill;
	


	IEnumerator LoadSceneAsync(string sceneName,float delayInSeconds)
	{
		AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
		LoadingScreen.SetActive(true);
		yield return new WaitForSeconds(delayInSeconds);
		while (!operation.isDone)
		{
			float progress = Mathf.Clamp01(operation.progress / .9f);
			LoadingBarFill.fillAmount = progress;
			yield return null;
		}
	}
	



	public void LoadScene(string sceneName)
	{
		StartCoroutine(LoadSceneAsync(sceneName,2f));
	}

	public void ExitGameBtn()
	{
		Application.Quit();
	}

	public void OnMusicSliderChange()
	{
		audioMixer.SetFloat("MusicVol", musicSlider.value);
	}

	public void OnSfxSliderChange()
	{
		audioMixer.SetFloat("SfxVol", soundSlider.value);
	}

}
