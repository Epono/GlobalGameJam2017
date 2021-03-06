﻿using System.Collections;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovementScript : NetworkBehaviour
{
	[SerializeField]
	private Animator animator;

	[SerializeField]
	private GameObject bulletPrefab;

	[SerializeField]
	private Transform bulletSpawn;

	[SerializeField]
	private WarshipScript script;

	[SerializeField]
	private ScanScript scanScript;

	[SerializeField]
	private AudioClip rocketLaunchedClip;

	[SerializeField]
	private HealthUI healthUIScript;

	[SerializeField]
	private AudioClip rocketExplosedClip;


	[SerializeField]
	private AudioClip sonarLaunchedClip;

	public WarshipAttributes attributes;

	[SyncVar]
	private int currentHealth;

	[SyncVar]
	private bool loose = false;

	[SyncVar]
	private Vector2 forward = new Vector2(0,1);

	[SyncVar]
	private bool _state; //0 INSIDE, 1 OUTSIDE

	private Vector2 aim;
	private bool shootFire = false;
	private bool shootSonar = false;
	private bool _canFire = true;
	private float currentSpeed = 0.5f;
	private bool _immerge = false;


	float x = 0.0f;
	public float MovementMagicNumber = 0.05f; // value for Input.GetAxis("Vertical") * Time.deltaTime * 3.0f; needed hard coded

	void Start()
	{
		attributes = script.Attributes;
		currentHealth = attributes.HealthPoint;

		healthUIScript = FindObjectOfType<HealthUI>();
		// Debug.LogError(NetworkServer.connections.Count);

	}

	public void readInfo( InfoSend infos )
	{
		x = infos.move;
		aim = infos.aimAngle;
		shootFire = infos.inputListing[Action.FIRE];
		shootSonar = infos.inputListing[Action.WAVESHOT];
		if( infos.inputListing[Action.ACCELERATE] )
		{
			currentSpeed += 0.1f;
			if( currentSpeed >= 1.5f )
				currentSpeed = 1.5f;
		}
		if( infos.inputListing[Action.DECCELERATE] )
		{
			currentSpeed -= 0.1f;
			if( currentSpeed <= 0.2f )
				currentSpeed = 0.2f;
		}
		_immerge = infos.inputListing[Action.IMMERGE];

	}

	public IEnumerator tempo()
	{
		if( isServer )
		{
			yield return new WaitForSeconds(0.5f);
			if( loose && isLocalPlayer )
			{
				SceneManager.LoadScene("YOULOOSE");
			}
			if( loose && !isLocalPlayer )
			{
				SceneManager.LoadScene("YOUWIN");
			}
		}
		else
		{
			if( loose && isLocalPlayer )
			{
				SceneManager.LoadScene("YOULOOSE");
			}
			if( loose && !isLocalPlayer )
			{
				SceneManager.LoadScene("YOUWIN");
			}
		}

	}
	public IEnumerator ImmergeCoroutine()
	{
		_state = true;//OUTSIDE
        animator.SetBool("isImmerge", false);
        SoundsSingletonScript.playClip(AudioClips.warshipEmergingClip);
        CmdImmerge(_state);
		int step = 5;
		int min = 100;
		int max = 255;
		int incr = (max - min) / step;
		for (var i = 0 ; i < step ; ++i )
		{
			script.WarshipSprite.color = new Color(255, 255, 255, 100 + i * incr);
			yield return new WaitForSeconds(0.5f);
		}
		
		_state = false;//INSIDE

        animator.SetBool("isImmerge", true);
        SoundsSingletonScript.playClip(AudioClips.warshipDivingClip);
        CmdImmerge(_state);
		attributes.Ammunition = 15;
		for( var i = 0 ; i < step ; ++i )
		{
			script.WarshipSprite.color = new Color(255, 255, 255, 255 - i * incr);
			yield return new WaitForSeconds(0.5f);
		}		
		//gestion de L'ui et gestion du son
		yield return null;

	}
	void Update()
	{
		if( loose )
			StartCoroutine("tempo");


		if( _state == false )//INSIDE
		{
			if( !isLocalPlayer )
			{
				script.WarshipSprite.enabled = false;
				//alpha
			}
		}

		if( _state == true )//OUTSIDE
		{
			script.WarshipSprite.enabled = true;
			//alpha
		}



		transform.Translate(0, currentSpeed * Time.deltaTime * 3.0f, 0);
		bulletSpawn.position = transform.position;
		bulletSpawn.position += new Vector3(forward.x, forward.y, 0) * 0.5f;
		if( !isLocalPlayer )
		{
			return;
		}

		//var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
		//var y = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

		//Debug.LogError(_state);
		if( _immerge && _state == false )
		{
			Debug.Log("Start Coroutine");
			_state = true;
			
			StartCoroutine("ImmergeCoroutine");
			//ne rien faire d'autre
		}
		//else if( _immerge && _state == true )
		//{
		//	Debug.Log("Start Coroutine");
		//	_state = false;
		//	CmdImmerge(_state);
		//	//StartCoroutine("ImmergeCoroutine");
		//	//ne rien faire d'autre
		//}

		transform.Rotate(0, 0, -x);
		//transform.Translate(0, y, 0);

		if( aim.magnitude >= 0.5f )
		{
			forward = aim.normalized;
			forward.y *= -1;

		}
		else
		{
			forward.x = bulletSpawn.position.x;
			forward.y = bulletSpawn.position.y;
		}
		forward.Normalize();
		if( isClient )
			CmdAim(forward);

		if( shootFire )
		{
			if( _canFire )
			{
				_canFire = false;
				if( attributes.Ammunition > 0 )
				{
					CmdFire(forward);
					attributes.Ammunition--;
				}
				else
				{
					SoundsSingletonScript.playClip(AudioClips.noAmmosClip);
				}
				StartCoroutine("OnBulletFired");
			}
		}

		if( shootSonar )
		{
			//gestion coolDown
			scanScript.RunScan();
			SoundsSingletonScript.playClip(AudioClips.sonarLaunchedClip);
		}
	}

	[Command]
	void CmdImmerge( bool b)
	{
		_state = b;
	}
	[Command]
	void CmdAim( Vector2 forward )
	{
		bulletSpawn.position = transform.position;
		bulletSpawn.position += new Vector3(forward.x, forward.y, 0) * 0.5f;
	}

	// This [Command] code is called on the Client …
	// … but it is run on the Server!
	[Command]
	void CmdFire( Vector2 forward )
	{
		// Create the Bullet from the Bullet Prefab
		var bullet = (GameObject)Instantiate(
			bulletPrefab,
			bulletSpawn.position,
			bulletSpawn.rotation);

		// Add velocity to the bullet
		bullet.transform.rotation.SetLookRotation(bullet.transform.position + new Vector3(forward.x, forward.y, 0));
		bullet.GetComponent<Rigidbody2D>().velocity = forward * 6;

		SoundsSingletonScript.playClip(AudioClips.rocketLaunchedClip);

		// Spawn the bullet on the Clients
		NetworkServer.Spawn(bullet);

		// Destroy the bullet after 2 seconds
		Destroy(bullet, 2.0f);
	}

	public override void OnStartLocalPlayer()
	{
		//GetComponent<MeshRenderer>().material.color = Color.blue;
	}

	public void TakeDamage( int amount )
	{

		if( !isServer )
		{
			return;
		}

		currentHealth -= amount;
		attributes.HealthPoint = currentHealth;

		healthUIScript.UpdateHealth(currentHealth, attributes.MaxHealth);

		Debug.Log(currentHealth);
		if( currentHealth <= 0 )
		{
            animator.SetBool("IsAlive", false);
            currentHealth = 0;
			loose = true;
			Debug.Log("Dead!");
			//LUCAS BOOLEAN ICI



		}
	}

	void OnCollisionEnter2D( Collision2D collision )
	{
		SoundsSingletonScript.playClip(AudioClips.warshipCollisionClip);

		Debug.Log("lolilol");

		var hit = collision.gameObject;
		if( hit.tag == "WARSHIP" )
		{
			var health = hit.GetComponent<PlayerMovementScript>();
			if( health != null )
			{
				health.TakeDamage(attributes.CollisionDamage);
				TakeDamage(health.attributes.CollisionDamage);
			}
		}
		else if( hit.tag == "ISLAND" )
		{
			var health = hit.GetComponent<IsleAttribute>();
			if( health != null )
			{
				//health.HealthPoint--;
				TakeDamage(attributes.CollisionDamage);
				if( ( health.HealthPoint - 1 ) > 0 )
					hit.GetComponent<SpriteRenderer>().sprite = health.spriteList[health.HealthPoint - 1];
				if( health.HealthPoint <= 0 )
				{
					Destroy(hit);
					//change sprit of islands
				}
			}
		}


	}
	public IEnumerator OnBulletFired()
	{
		yield return new WaitForSeconds(0.5f);
		_canFire = true;
		yield return null;
	}
}
