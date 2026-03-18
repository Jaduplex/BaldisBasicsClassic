using UnityEngine;

public class SwingDoorAnimationScript : MonoBehaviour
{
	public GameControllerScript gc;

	public MeshRenderer inside;

	public MeshRenderer outside;

	public Material closed;

	public Material open;

	public AudioClip doorOpen;

	public AudioClip doorClose;

	private int openTime;

	private AudioSource myAudio;

	private void Start()
	{
		myAudio = GetComponent<AudioSource>();
	}

	private void Update()
	{
		if (openTime > -1)
		{
			openTime--;
		}
		if (openTime == 0)
		{
			inside.sharedMaterial = closed;
			outside.sharedMaterial = closed;
			myAudio.PlayOneShot(doorClose, 1f);
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (gc.spoopMode)
		{
			inside.sharedMaterial = open;
			outside.sharedMaterial = open;
			openTime = 120;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		myAudio.PlayOneShot(doorOpen, 1f);
	}
}
