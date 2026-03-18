using UnityEngine;

public class TapePlayerScript : MonoBehaviour
{
	public Sprite closedSprite;

	public SpriteRenderer sprite;

	private int audVal;

	public AudioClip recording;

	public BaldiScript baldi;

	private AudioSource audioDevice;

	private void Start()
	{
		audioDevice = GetComponent<AudioSource>();
	}

	private void Update()
	{
	}

	public void Play()
	{
		sprite.sprite = closedSprite;
		audioDevice.PlayOneShot(recording);
		baldi.ActivateAntiHearing(30f);
	}
}
