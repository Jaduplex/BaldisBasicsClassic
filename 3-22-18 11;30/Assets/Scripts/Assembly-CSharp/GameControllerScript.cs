using UnityEngine;
using UnityEngine.UI;

public class GameControllerScript : MonoBehaviour
{
	public float baldiAnger;

	public float angerRate;

	public int notebooks;

	public bool spoopMode;

	private bool gamePaused;

	private bool learningActive;

	public Text notebookCount;

	private void Start()
	{
		UpdateNotebookCount();
		spoopMode = false;
	}

	private void Update()
	{
		if (!learningActive)
		{
			if (Input.GetButtonDown("Pause"))
			{
				if (!gamePaused)
				{
					PauseGame();
				}
				else
				{
					UnpauseGame();
				}
			}
			if (!gamePaused & (Time.timeScale != 1f))
			{
				Time.timeScale = 1f;
			}
		}
		else if (Time.timeScale != 0f)
		{
			Time.timeScale = 0f;
		}
	}

	private void UpdateNotebookCount()
	{
		notebookCount.text = notebooks + "/7";
	}

	public void CollectNotebook()
	{
		notebooks++;
		UpdateNotebookCount();
	}

	private void PauseGame()
	{
		Time.timeScale = 0f;
		gamePaused = true;
	}

	private void UnpauseGame()
	{
		Time.timeScale = 1f;
		gamePaused = false;
	}

	private void ActivateSpoopMode()
	{
		spoopMode = true;
	}

	public void GetAngry(float value)
	{
		if (!spoopMode)
		{
			ActivateSpoopMode();
		}
		baldiAnger = angerRate * value;
	}

	public void ActivateLearningGame()
	{
		learningActive = true;
	}

	public void DeactivateLearningGame(GameObject subject)
	{
		learningActive = false;
		subject.SetActive(false);
	}
}
