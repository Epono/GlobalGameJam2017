using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Action
{
    ACCELERATE,
    DECCELERATE,
    RIGHT,
    LEFT,
    FIRE,
    WAVESHOT,
    AIM,
    NEARSCAN,
    IMMERGE,
    SWITCH_ROCKET,
}

public class InfoSend
{
    public float move;
    public Vector2 aimAngle;
    public Dictionary<Action,bool> inputListing;


    public InfoSend(float x, Vector2 aim, Dictionary<Action,bool> list)
    {
        move = x;
        aimAngle = aim;
        inputListing = list;
    }


};

public class PlayerController : MonoBehaviour {

    public float move;
    public Vector2 aimAngle;
    //public Dictionary<Action, bool> inputListing;

    public PlayerMovementScript mov;
    Vector2 aimVector = new Vector2(0, 0);
    
    public Dictionary<Action, bool> infoList = new Dictionary<Action, bool>();
    
    public Action myAction;
    public InfoSend infoToTransmit;
    // Use this for initialization
    void Start () {

        mov = gameObject.GetComponent<PlayerMovementScript>();
      
        foreach(Action action in Enum.GetValues(typeof(Action)))
        {
            infoList.Add(action, false);
        }
		
	}
	
	// Update is called once per frame
	void Update () {
        //float x = Input.GetAxis("Horizontal");
        //Debug.Log(x);
        getInput();
       
	}



    void getInput()
    {

        float x = Input.GetAxis("Horizontal")/* * Time.deltaTime * 150.0f*/;
        float fire       = Input.GetAxis("Fire");
        float waveshot   = Input.GetAxis("Sonar");
        float aimX       = Input.GetAxis("aimX");
        float aimY       = Input.GetAxis("aimY");

        if (aimX != 0 || aimY != 0)
        {

            infoList[Action.AIM] = true;
        }

        if (waveshot > 0.5)
        {
            infoList[Action.WAVESHOT] = true;
        }

        else
        {
            infoList[Action.WAVESHOT] = false;
        }

        if (fire > 0.5)
        {
            infoList[Action.FIRE] = true;
        }

        else
        {
            infoList[Action.FIRE] = false;
        }

        if (x < 0.0f)
        {
            infoList[Action.LEFT] = true;
        }
        else if (x > 0.0f)
        {
            infoList[Action.RIGHT] = true;
        }

        if (Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            //NearScan
            // NearScan();
            infoList[Action.NEARSCAN] = true;
        }

        //LB
        if (Input.GetKeyDown(KeyCode.Joystick1Button4))
        {
            // Deccelerate();
            infoList[Action.DECCELERATE] = true;
        }
        else
        {
            infoList[Action.DECCELERATE] = false;
        }
        //RB
        if (Input.GetKeyDown(KeyCode.Joystick1Button5))
        {

            //Accelerate();
            infoList[Action.ACCELERATE] = true;
        }
        else
        {
            //Accelerate();
            infoList[Action.ACCELERATE] = false;
        }
        //X
        if (Input.GetKeyDown(KeyCode.Joystick1Button2))
        {
            //SwitchRocket();
            infoList[Action.IMMERGE] = true;
        }
		else
		{
			//Accelerate();
			infoList[Action.IMMERGE] = false;
		}

		//move = x;
		infoToTransmit = new InfoSend(x, new Vector2(aimX, aimY), infoList);
       // Debug.Log(x);
       mov.readInfo(infoToTransmit);



    }



    //void sendTab(




}
