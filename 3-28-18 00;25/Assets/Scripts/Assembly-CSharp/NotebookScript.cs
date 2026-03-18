using UnityEngine;

public class NotebookScript : MonoBehaviour
{
	public float openingDistance;

	public GameControllerScript gc;

	public Transform player;

	public GameObject learningGame;

	private void Start()
	{
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hitInfo;
			if (Physics.Raycast(ray, out hitInfo) && ((hitInfo.transform.tag == "Notebook") & (Vector3.Distance(player.position, base.transform.position) < openingDistance)))
			{
				base.gameObject.SetActive(false);
				gc.CollectNotebook();
				learningGame.SetActive(true);
			}
		}
	}
}
