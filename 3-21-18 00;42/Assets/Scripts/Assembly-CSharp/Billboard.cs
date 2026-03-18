using UnityEngine;

public class Billboard : MonoBehaviour
{
	public Camera m_Camera;

	private void Update()
	{
		base.transform.LookAt(base.transform.position + m_Camera.transform.rotation * Vector3.forward, m_Camera.transform.rotation * Vector3.up);
	}
}
