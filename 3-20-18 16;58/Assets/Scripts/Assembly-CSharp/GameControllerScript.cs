using UnityEngine;

public class GameControllerScript : MonoBehaviour
{
	public bool spoopMode;

	private bool gamePaused;

	private void Start()
	{
	}

	private void Update()
	{
		if (Input.GetButtonDown("Pause"))
		{
			if (!gamePaused)
			{
				Time.timeScale = 0f;
				gamePaused = true;
			}
			else
			{
				Time.timeScale = 1f;
				gamePaused = false;
			}
		}
	}
}
