using UnityEngine;

public class CameraScript : MonoBehaviour
{
	public GameObject player;

	private Vector3 offset;

	private void Start()
	{
		offset = base.transform.position - player.transform.position;
	}

	private void LateUpdate()
	{
		base.transform.position = player.transform.position + offset;
		base.transform.rotation = player.transform.rotation * Quaternion.Euler(0f, Input.GetAxisRaw("Look Behind") * 180f, 0f);
	}
}
