using UnityEngine;
using UnityEngine.UI;

public class BasicButtonScript : MonoBehaviour
{
	private Button button;

	public GameObject screen;

	private void Start()
	{
		button = GetComponent<Button>();
		button.onClick.AddListener(OpenScreen);
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}

	private void OpenScreen()
	{
		screen.SetActive(true);
	}
}
