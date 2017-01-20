using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {

    enum EventType
    {
        TSUNAMI,
        NUCLEAR_WEAPON,
        ALIEN_INVASION,
        PEARL_HARBOR
    }

    float timerBetweenEvent = 30.0f;

    // Use this for initialization
    void Start () {

        InvokeRepeating("EventGenerator", 30.0f, timerBetweenEvent);
        
    }
	
	// Update is called once per frame
	void Update () {
      

	}

    void EventGenerator()
    {
        timerBetweenEvent -= 5.0f;
        //Debug.Log("EVENT GENERATOR");
        int eventToLaunch = UnityEngine.Random.Range(0, Enum.GetNames(typeof(EventType)).Length);
        //Debug.Log(eventToLaunch);
        switch (eventToLaunch)
        {
            case 0:
                StartCoroutine("Tsunami");
                break;
            case 1:
                StartCoroutine("NuclearLaunch");
                break;
            case 2:
                StartCoroutine("AlienInvasion");
                break;
            case 3:
                StartCoroutine("PearlHarbor");
                break;
            case 4:
                StartCoroutine("Tsunami");
                break;
            default:
                Debug.Log("YOLO");
                break;
        }
    }


    IEnumerator Tsunami()
    {
        Debug.Log("TSUNAMI START ! ");
        //TODO ACTION DE L'EVENT
        yield return new WaitForEndOfFrame();
    }

    IEnumerator NuclearLaunch()
    {
        Debug.Log("NUCLEAR LAUNCH ! ");
        //TODO ACTION DE L'EVENT
        yield return new WaitForEndOfFrame();
    }

    IEnumerator AlienInvasion()
    {
        Debug.Log("ALIEN INVASION ! ");
        //TODO ACTION DE L'EVENT
        yield return new WaitForEndOfFrame();
    }

    IEnumerator PearlHarbor()
    {
        Debug.Log("PEARL HARBOR ! ");
        //TODO ACTION DE L'EVENT
        yield return new WaitForEndOfFrame();
    }

}
