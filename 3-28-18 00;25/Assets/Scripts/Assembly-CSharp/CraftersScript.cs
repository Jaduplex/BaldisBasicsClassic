using UnityEngine;
using UnityEngine.AI;

public class CraftersScript : MonoBehaviour
{
	public bool db;

	public bool angry;

	public bool gettingAngry;

	public float anger;

	private float forceShowTime;

	public Transform player;

	public Transform playerCamera;

	public Transform baldi;

	public NavMeshAgent baldiAgent;

	public GameObject sprite;

	public GameControllerScript gc;

	private NavMeshAgent agent;

	public Renderer craftersRenderer;

	public SpriteRenderer spriteImage;

	private AudioSource audioDevice;

	public AudioClip aud_Play;

	private void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		audioDevice = GetComponent<AudioSource>();
		sprite.SetActive(false);
	}

	private void Update()
	{
		if (forceShowTime > 0f)
		{
			forceShowTime -= Time.deltaTime;
		}
		if (gettingAngry)
		{
			anger += Time.deltaTime;
			if ((double)anger >= 0.5)
			{
				angry = true;
			}
		}
		if (!angry)
		{
			if ((((base.transform.position - agent.destination).magnitude <= 20f) & ((base.transform.position - player.position).magnitude >= 30f)) || forceShowTime > 0f)
			{
				sprite.SetActive(true);
			}
			else
			{
				sprite.SetActive(false);
			}
			return;
		}
		agent.speed += 20f * Time.deltaTime;
		TargetPlayer();
		if (!audioDevice.isPlaying)
		{
			audioDevice.PlayOneShot(aud_Play);
		}
	}

	private void FixedUpdate()
	{
		if (gc.notebooks >= 7)
		{
			Vector3 direction = player.position - base.transform.position;
			RaycastHit hitInfo;
			if (Physics.Raycast(base.transform.position, direction, out hitInfo, float.PositiveInfinity, 3, QueryTriggerInteraction.Ignore) & (hitInfo.transform.tag == "Player") & (Vector3.Angle(playerCamera.forward, base.transform.position - player.position) <= 35f))
			{
				gettingAngry = true;
				return;
			}
			gettingAngry = false;
			anger = 0f;
		}
	}

	public void GiveLocation(Vector3 location, bool flee)
	{
		if (!angry)
		{
			agent.SetDestination(location);
			if (flee)
			{
				forceShowTime = 3f;
			}
		}
	}

	private void TargetPlayer()
	{
		agent.SetDestination(player.position);
	}

	private void OnTriggerEnter(Collider other)
	{
		if ((other.tag == "Player") & angry)
		{
			player.position = new Vector3(5f, player.position.y, 80f);
			baldiAgent.Warp(new Vector3(5f, baldi.position.y, 125f));
			player.LookAt(new Vector3(baldi.position.x, player.position.y, baldi.position.z));
			gc.DespawnCrafters();
		}
	}
}
