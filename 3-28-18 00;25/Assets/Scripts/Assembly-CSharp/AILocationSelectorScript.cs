using UnityEngine;

public class AILocationSelectorScript : MonoBehaviour
{
	public Transform[] newLocation = new Transform[29];

	private int id;

	public void GetNewTarget()
	{
		id = Random.Range(0, 28);
		base.transform.position = newLocation[id].position;
	}
}
