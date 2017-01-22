using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketCollisionScript : MonoBehaviour {

    [SerializeField]
    private RocketScript rocketAttributes;

    void OnCollisionEnter2D(Collision2D collision)
    {

        var hit = collision.gameObject;
        if(hit.tag == "WARSHIP")
        {
            var health = hit.GetComponent<PlayerMovementScript>();
            if (health != null)
            {
                health.TakeDamage(rocketAttributes.Damages);
            }
        }
        else if(hit.tag == "ISLAND")
        {
            var health = hit.GetComponent<IsleAttribute>();
            if (health != null)
            {
                health.HealthPoint--;
              
                hit.GetComponent<SpriteRenderer>().sprite = health.spriteList[health.HealthPoint - 1];
                if (health.HealthPoint <=0 )
                {
                    Destroy(hit);
                }
            }
        }

        Destroy(gameObject);
    }
}
