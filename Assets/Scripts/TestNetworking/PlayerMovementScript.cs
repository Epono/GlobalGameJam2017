using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class PlayerMovementScript : NetworkBehaviour
{

    public GameObject bulletPrefab;
    public Transform bulletSpawn;

    Vector2 forward;

    public float MovementMagicNumber = 0.05f; // value for Input.GetAxis("Vertical") * Time.deltaTime * 3.0f; needed hard coded

    void Update()
    {
        transform.Translate(0, MovementMagicNumber, 0);
        if (!isLocalPlayer)
        {
            return;
        }

        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        //var y = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

        transform.Rotate(0, 0, -x);
        //transform.Translate(0, y, 0);
        forward.x = bulletSpawn.position.x - transform.position.x;
        forward.y = bulletSpawn.position.y - transform.position.y;
        forward.Normalize();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CmdFire(forward);
        }
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

        //get Axis From Joystick
        //front of warship at first
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

}
