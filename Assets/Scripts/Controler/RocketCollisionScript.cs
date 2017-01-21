using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketCollisionScript : MonoBehaviour {

    [SerializeField]
    private RocketScript rocketAttributes;

    void OnCollisionEnter2D(Collision2D collision)
    {

        var hit = collision.gameObject;
        if(hit == null)
            Debug.Log("Fuck you! hit");
        if(hit.tag == "WARSHIP")
        {
        var health = hit.GetComponent<PlayerMovementScript>();
            if (health != null)
            {
                health.TakeDamage(rocketAttributes.Damages);
            }
            else
                Debug.Log("Fuck you!");
        }

        Destroy(gameObject);
    }
}
