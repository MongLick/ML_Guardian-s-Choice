using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

public class SceneManager : Singleton<SceneManager>
{
	[Header("Components")]
	[SerializeField] Image fade;
	private BaseScene curScene;

	[Header("Specs")]
	[SerializeField] float fadeTime;
	internal static Action<UnityEngine.SceneManagement.Scene, UnityEngine.SceneManagement.LoadSceneMode> sceneLoaded;

	public BaseScene GetCurScene()
	{
		if (curScene == null)
		{
			curScene = FindObjectOfType<BaseScene>();
		}
		return curScene;
	}

	public T GetCurScene<T>() where T : BaseScene
	{
		if (curScene == null)
		{
			curScene = FindObjectOfType<BaseScene>();
		}
		return curScene as T;
	}

	public void LoadScene(string sceneName)
	{
		StartCoroutine(LoadingRoutine(sceneName));
	}

	private IEnumerator LoadingRoutine(string sceneName)
	{
		fade.gameObject.SetActive(true);
		yield return FadeOut();

		Manager.Pool.ClearPool();

		Time.timeScale = 0f;

		AsyncOperation oper = UnitySceneManager.LoadSceneAsync(sceneName);
		while (oper.isDone == false)
		{
			yield return null;
		}

		BaseScene curScene = GetCurScene();

		Time.timeScale = 1f;

		yield return FadeIn();
		fade.gameObject.SetActive(false);
	}

	private IEnumerator FadeOut()
	{
		float rate = 0;
		Color fadeOutColor = new Color(fade.color.r, fade.color.g, fade.color.b, 1f);
		Color fadeInColor = new Color(fade.color.r, fade.color.g, fade.color.b, 0f);

		while (rate <= 1)
		{
			rate += Time.deltaTime / fadeTime;
			fade.color = Color.Lerp(fadeInColor, fadeOutColor, rate);
			yield return null;
		}
	}

	private IEnumerator FadeIn()
	{
		float rate = 0;
		Color fadeOutColor = new Color(fade.color.r, fade.color.g, fade.color.b, 1f);
		Color fadeInColor = new Color(fade.color.r, fade.color.g, fade.color.b, 0f);

		while (rate <= 1)
		{
			rate += Time.deltaTime / fadeTime;
			fade.color = Color.Lerp(fadeOutColor, fadeInColor, rate);
			yield return null;
		}
	}
}
