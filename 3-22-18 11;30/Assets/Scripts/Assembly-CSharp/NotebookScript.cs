using UnityEngine;

public class NotebookScript : MonoBehaviour
{
	public float openingDistance;

	public GameControllerScript gc;

	public Transform player;

	public GameObject mathGame;

	public GameObject spellingGame;

	public GameObject historyGame;

	public GameObject geologyGame;

	public GameObject geographyGame;

	public GameObject englishGame;

	public GameObject scienceGame;

	private void Start()
	{
	}

	private void Update()
	{
		if (!Input.GetMouseButtonDown(0))
		{
			return;
		}
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hitInfo;
		if (Physics.Raycast(ray, out hitInfo))
		{
			if ((hitInfo.transform.name == "MathNotebook") & (Vector3.Distance(player.position, base.transform.position) < openingDistance))
			{
				base.gameObject.SetActive(false);
				gc.CollectNotebook();
				mathGame.SetActive(true);
			}
			else if ((hitInfo.transform.name == "SpellingNotebook") & (Vector3.Distance(player.position, base.transform.position) < openingDistance))
			{
				base.gameObject.SetActive(false);
				gc.CollectNotebook();
			}
		}
	}
}
