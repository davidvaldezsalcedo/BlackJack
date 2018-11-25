using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIBehavior : MonoBehaviour 
{
	[SerializeField]
	private BoolVariable _LoadGame;
	
	public void LoadScene(string sceneToLoad)
	{
		_LoadGame.Value = false;
		SceneManager.LoadScene(sceneToLoad);
	}

	public void LoadGame()
	{
		_LoadGame.Value = true;
		SceneManager.LoadScene("MainGame");
	}

	public void Quit()
	{
		Application.Quit();
	}
}
