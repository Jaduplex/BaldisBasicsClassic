using UnityEngine;

public class TapePlayerScript : MonoBehaviour
{
	public Sprite closedSprite;

	public SpriteRenderer sprite;

	public AudioClip[] recordings = new AudioClip[5];

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
		audioDevice.PlayOneShot(recordings[Random.Range(0, 4)]);
		baldi.Hear(base.transform.position, 4f);
	}
}
