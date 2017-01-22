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

    [SerializeField]
    private AudioClip warshipHurtClip;

    void OnCollisionEnter2D(Collision2D collision)
	{
        SoundsSingletonScript.playClip(AudioClips.rocketExplosedClip);

		var hit = collision.gameObject;
		if(hit.tag == "WARSHIP")
		{
            SoundsSingletonScript.playClip(AudioClips.warshipHurtClip);
			var health = hit.GetComponent<PlayerMovementScript>();
			if (health != null)
			{
				health.TakeDamage(rocketAttributes.Damages);
			}
        } else if(hit.tag == "ISLAND")
		{
            SoundsSingletonScript.playClip(AudioClips.isleHurtClip);
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
