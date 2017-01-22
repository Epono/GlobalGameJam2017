using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketCollisionScript : MonoBehaviour {

	[SerializeField]
	private RocketScript rocketAttributes;

    [SerializeField]
    private AudioClip rocketExplosedClip;

    [SerializeField]
    private AudioClip isleHurtClip;

    void OnCollisionEnter2D(Collision2D collision)
	{

		var hit = collision.gameObject;
		if(hit.tag == "WARSHIP")
		{
            GetComponent<AudioSource>().PlayOneShot(rocketExplosedClip);
			var health = hit.GetComponent<PlayerMovementScript>();
			if (health != null)
			{
				health.TakeDamage(rocketAttributes.Damages);
			}
        } else if(hit.tag == "ISLAND")
		{
            GetComponent<AudioSource>().PlayOneShot(isleHurtClip);
			var health = hit.GetComponent<IsleAttribute>();
			if (health != null)
			{
				health.HealthPoint--;
				if ((health.HealthPoint-1) >= 0)
					hit.GetComponent<SpriteRenderer>().sprite = health.spriteList[health.HealthPoint - 1];
				else if (health.HealthPoint <=0 )
				{
					Destroy(hit);
				}
			}
        }

        Destroy(gameObject);
	}
}
