using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsSingletonScript : MonoBehaviour {

    private static SoundsSingletonScript instance = null;

    private static AudioSource audioSource;

    [SerializeField]
    private static AudioClip rocketLaunchedClip;

    [SerializeField]
    private static AudioClip sonarLaunchedClip;

    [SerializeField]
    private static AudioClip rocketExplosedClip;

    [SerializeField]
    private static AudioClip isleHurtClip;

    [SerializeField]
    private static AudioClip warshipHurtClip;

    // Game Instance Singleton
    public static SoundsSingletonScript Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake() {
        // if the singleton hasn't been initialized yet
        if(instance != null && instance != this) {
            Destroy(this.gameObject);
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    public static void playClip()
    {
        audioSource.enabled = true;
        audioSource.PlayOneShot(isleHurtClip, Single.MaxValue);
    }

}
