using UnityEngine;

public class DoorScript : MonoBehaviour
{
	public float openingDistance;

	public Transform player;

	public MeshCollider barrier;

	public MeshCollider trigger;

	public MeshCollider invisibleBarrier;

	public MeshRenderer inside;

	public MeshRenderer outside;

	public AudioClip doorOpen;

	public AudioClip doorClose;

	public Material closed;

	public Material open;

	private bool locked;

	private float openTime;

	private AudioSource myAudio;

	private void Start()
	{
		myAudio = GetComponent<AudioSource>();
	}

	private void Update()
	{
		if (openTime > -1f)
		{
			openTime -= 1f;
		}
		if (openTime == 0f)
		{
			barrier.enabled = true;
			invisibleBarrier.enabled = true;
			inside.sharedMaterial = closed;
			outside.sharedMaterial = closed;
		}
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hitInfo;
			if (Physics.Raycast(ray, out hitInfo) && ((hitInfo.collider == trigger) & (Vector3.Distance(player.position, base.transform.position) < openingDistance)))
			{
				myAudio.PlayOneShot(doorOpen, 1f);
				barrier.enabled = false;
				invisibleBarrier.enabled = false;
				inside.sharedMaterial = open;
				outside.sharedMaterial = open;
				openTime = 180f;
			}
		}
	}
}
