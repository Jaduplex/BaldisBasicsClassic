using UnityEngine;

public class PickupScript : MonoBehaviour
{
	public GameControllerScript gc;

	public Transform player;

	private void Start()
	{
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hitInfo;
			if (Physics.Raycast(ray, out hitInfo) && ((hitInfo.transform.name == "Pickup_BarWithEnergy") & (Vector3.Distance(player.position, base.transform.position) < 10f)))
			{
				base.gameObject.SetActive(false);
				gc.CollectItem(1);
			}
		}
	}
}
