using UnityEngine;

public class SwingingDoorScript : MonoBehaviour
{
	public GameControllerScript gc;

	public MeshCollider barrier;

	public MeshCollider trigger;

	private void Start()
	{
	}

	private void Update()
	{
		if (gc.spoopMode)
		{
			barrier.enabled = false;
		}
		else
		{
			barrier.enabled = true;
		}
	}
}
