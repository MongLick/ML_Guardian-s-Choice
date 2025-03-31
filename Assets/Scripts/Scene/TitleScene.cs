public class TitleScene : BaseScene
{
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
