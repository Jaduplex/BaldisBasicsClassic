using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
	public void LoadByIndex(int sceneIndex)
	{
		SceneManager.LoadScene(sceneIndex);
	}
}
