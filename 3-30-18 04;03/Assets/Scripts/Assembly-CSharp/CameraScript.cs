using UnityEngine;

public class CameraScript : MonoBehaviour
{
	public GameObject player;

	public PlayerScript ps;

	public Transform baldi;

	private Vector3 offset;

	private void Start()
	{
		offset = base.transform.position - player.transform.position;
	}

	private void LateUpdate()
	{
		base.transform.position = player.transform.position + offset;
		if (!ps.gameOver)
		{
			base.transform.rotation = player.transform.rotation * Quaternion.Euler(0f, Input.GetAxisRaw("Look Behind") * 180f, 0f);
		}
		else
		{
			base.transform.LookAt(new Vector3(baldi.position.x, baldi.position.y + 5f, baldi.position.z));
		}
	}
}
