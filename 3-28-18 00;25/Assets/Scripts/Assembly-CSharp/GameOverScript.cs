using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour
{
	private Image image;

	private float delay;

	public Sprite[] images = new Sprite[5];

	private void Start()
	{
		image = GetComponent<Image>();
		delay = 5f;
		image.sprite = images[Random.Range(0, 4)];
	}

	private void Update()
	{
		delay -= 1f * Time.deltaTime;
		if (delay <= 0f)
		{
			Cursor.lockState = CursorLockMode.None;
			SceneManager.LoadScene(0);
		}
	}
}
