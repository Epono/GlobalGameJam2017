using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
<<<<<<< HEAD
using UnityEngine.SceneManagement;
=======
>>>>>>> dddd7fdce5b0935c5d8008dfe8f97032f64f66a6

public class PlayerMovementScript : NetworkBehaviour
{
	[SerializeField]
	private GameObject bulletPrefab;

	[SerializeField]
	private Transform bulletSpawn;

	[SerializeField]
	private WarshipScript script;

	[SerializeField]
	private ScanScript scanScript;

<<<<<<< HEAD
	public WarshipAttributes attributes;

    //pour le test de la fin
    bool isAlive = true;
=======
	private WarshipAttributes attributes;
>>>>>>> dddd7fdce5b0935c5d8008dfe8f97032f64f66a6

	[SyncVar]
	private int currentHealth;

	private Vector2 forward = new Vector2(0,1);
	private Vector2 aim;
	private bool shootFire = false;
	private bool shootSonar = false;
	private bool _canFire = true;

    


    float  x = 0.0f;
	public float MovementMagicNumber = 0.05f; // value for Input.GetAxis("Vertical") * Time.deltaTime * 3.0f; needed hard coded
	
	void Start()
	{
        
        //ID = new NetworkSceneId();

        attributes = script.Attributes;
		currentHealth = attributes.HealthPoint;
	}

	public void readInfo(InfoSend infos)
	{
		x = infos.move;
		aim = infos.aimAngle;
		shootFire  = infos.inputListing[Action.FIRE];
		shootSonar = infos.inputListing[Action.WAVESHOT];
	}

	void Update()
	{
		//transform.Translate(0, MovementMagicNumber, 0);// attributes.MoveSpeed * Time.deltaTime * 3.0f, 0);
		if (!isLocalPlayer)
		{
			return;
		}

		var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
		var y = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

		

		transform.Rotate(0, 0, -x);
		transform.Translate(0, y, 0);
		

		if(aim.magnitude >= 0.5f)
		{
			forward = aim.normalized;
			forward.y *= -1;
			bulletSpawn.position = transform.position;
			bulletSpawn.position += new Vector3(forward.x, forward.y, 0)*0.5f;
		}
		

<<<<<<< HEAD
=======
	float  x = 0.0f;
	public float MovementMagicNumber = 0.05f; // value for Input.GetAxis("Vertical") * Time.deltaTime * 3.0f; needed hard coded
	
	void Start()
	{
		attributes = script.Attributes;
		currentHealth = attributes.HealthPoint;
	}

	public void readInfo(InfoSend infos)
	{
		x = infos.move;
		aim = infos.aimAngle;
		shootFire  = infos.inputListing[Action.FIRE];
		shootSonar = infos.inputListing[Action.WAVESHOT];
	}

	void Update()
	{
		//transform.Translate(0, MovementMagicNumber, 0);// attributes.MoveSpeed * Time.deltaTime * 3.0f, 0);
		if (!isLocalPlayer)
		{
			return;
		}

		var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
		var y = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

		

		transform.Rotate(0, 0, -x);
		transform.Translate(0, y, 0);
		

		if(aim.magnitude >= 0.5f)
		{
			forward = aim.normalized;
			forward.y *= -1;
			bulletSpawn.position = transform.position;
			bulletSpawn.position += new Vector3(forward.x, forward.y, 0)*0.5f;
		}
		

>>>>>>> dddd7fdce5b0935c5d8008dfe8f97032f64f66a6
		if (shootFire)
		{
			if( _canFire )
			{
				_canFire = false;
				if( attributes.Ammunition > 0 )
				{
					CmdFire(forward);
					attributes.Ammunition--;
				}
				StartCoroutine("OnBulletFired");
			}
		}

		if(shootSonar)
		{
			//gestion coolDown
			scanScript.RunScan();
		}
<<<<<<< HEAD

        if(NetworkServer.localConnections.Count == 1)
        {
            Network.Disconnect();
            SceneManager.LoadScene("YOUWIN");
        }
=======
>>>>>>> dddd7fdce5b0935c5d8008dfe8f97032f64f66a6
	}

	// This [Command] code is called on the Client …
	// … but it is run on the Server!
	[Command]
	void CmdFire(Vector2 forward)
	{
		// Create the Bullet from the Bullet Prefab
		var bullet = (GameObject)Instantiate(
			bulletPrefab,
			bulletSpawn.position,
			bulletSpawn.rotation);
		
		// Add velocity to the bullet
		bullet.GetComponent<Rigidbody2D>().velocity = forward * 6;

		// Spawn the bullet on the Clients
		NetworkServer.Spawn(bullet);

		// Destroy the bullet after 2 seconds
		Destroy(bullet, 2.0f);
	}

	public override void OnStartLocalPlayer()
	{
		//GetComponent<MeshRenderer>().material.color = Color.blue;
	}

	public void TakeDamage(int amount)
	{

		if (!isServer)
		{
			return;
		}
		currentHealth -= amount;
		attributes.HealthPoint -= currentHealth;
		Debug.Log(currentHealth);
		if (currentHealth <= 0)
		{
			currentHealth = 0;
			Debug.Log("Dead!");
<<<<<<< HEAD
            //LUCAS BOOLEAN ICI
            isAlive = false;
            Network.Disconnect();
            SceneManager.LoadScene("YOULOOSE");
            

=======
			//LUCAS BOOLEAN ICI
>>>>>>> dddd7fdce5b0935c5d8008dfe8f97032f64f66a6
		}
	}

	void OnCollisionEnter2D(Collision2D collision)
	{

		var hit = collision.gameObject;
		if (hit.tag == "WARSHIP")
		{
			var health = hit.GetComponent<PlayerMovementScript>();
			if (health != null)
			{
				health.TakeDamage(attributes.CollisionDamage);
				TakeDamage(health.attributes.CollisionDamage);
			}
		}
		else if (hit.tag == "ISLAND")
		{
			var health = hit.GetComponent<IsleAttribute>();
			if (health != null)
			{
				health.HealthPoint--;
				TakeDamage(attributes.CollisionDamage);
				if (health.HealthPoint <= 0)
				{
					Destroy(hit);
					//change sprit of islands
				}
			}
		}

		Destroy(gameObject);
	}
	public IEnumerator OnBulletFired()
	{
		yield return new WaitForSeconds(0.5f);
		_canFire = true;
		yield return null;
	}
}
