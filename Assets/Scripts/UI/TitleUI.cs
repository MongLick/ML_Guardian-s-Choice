using UnityEngine;
using UnityEngine.UI;

public class TitleUI : MonoBehaviour
{
	[Header("Components")]
	[SerializeField] Button gameStartButton;
	[SerializeField] Button gmaeExitButton;

	private void Start()
	{
		gameStartButton.onClick.AddListener(GameStart);
		gmaeExitButton.onClick.AddListener(GameExit);
	}

	public void GameStart()
	{
		Manager.Scene.LoadScene("GameScene");
	}

	public void GameExit()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
	}
}
