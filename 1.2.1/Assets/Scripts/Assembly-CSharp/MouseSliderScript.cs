using UnityEngine;
using UnityEngine.UI;

public class MouseSliderScript : MonoBehaviour
{
	public Slider slider;

	private void Start()
	{
		slider = GetComponent<Slider>();
		slider.value = PlayerPrefs.GetFloat("MouseSensitivity");
	}

	private void Update()
	{
		PlayerPrefs.SetFloat("MouseSensitivity", slider.value);
	}
}
