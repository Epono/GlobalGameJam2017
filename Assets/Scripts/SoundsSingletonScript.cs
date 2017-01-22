using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AudioClips {
    isleHurtClip,
    rocketLaunchedClip,
    rocketExplosedClip,
    sonarLaunchedClip,
    warshipHurtClip,
    warshipCollisionClip,
    warshipEmergingClip,
    warshipDivingClip,
    noAmmosClip
}

public class SoundsSingletonScript : MonoBehaviour {

    private static SoundsSingletonScript instance = null;

    private static AudioSource audioSource;

    static AudioClip isleHurtClip;
    static AudioClip rocketLaunchedClip;
    static AudioClip rocketExplosedClip;
    static AudioClip sonarLaunchedClip;
    static AudioClip warshipHurtClip;
    static AudioClip warshipCollisionClip;
    static AudioClip warshipEmergingClip;
    static AudioClip warshipDivingClip;
    static AudioClip noAmmosClip;

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

    void Start() {
        audioSource = gameObject.GetComponent<AudioSource>();


        isleHurtClip = (AudioClip)Resources.Load("Audio/Sounds/isle_hurt");

        rocketLaunchedClip = (AudioClip)Resources.Load("Audio/Sounds/rocket_launch");
        rocketExplosedClip = (AudioClip)Resources.Load("Audio/Sounds/rocket_explosion");

        sonarLaunchedClip = (AudioClip)Resources.Load("Audio/Sounds/sonar");

        warshipHurtClip = (AudioClip)Resources.Load("Audio/Sounds/warship_hurt");
        warshipCollisionClip = (AudioClip)Resources.Load("Audio/Sounds/warship_collision");

        warshipEmergingClip = (AudioClip)Resources.Load("Audio/Sounds/warship_emerging");
        warshipDivingClip = (AudioClip)Resources.Load("Audio/Sounds/warship_diving");

        noAmmosClip = (AudioClip)Resources.Load("Audio/Sounds/no_ammos");
    }

    public static void playClip(AudioClips audioClip) {
        switch(audioClip) {
            case AudioClips.isleHurtClip:
                audioSource.clip = isleHurtClip;
                break;
            case AudioClips.rocketLaunchedClip:
                audioSource.clip = rocketLaunchedClip;
                break;
            case AudioClips.rocketExplosedClip:
                audioSource.clip = rocketExplosedClip;
                break;
            case AudioClips.sonarLaunchedClip:
                audioSource.clip = sonarLaunchedClip;
                break;
            case AudioClips.warshipHurtClip:
                audioSource.clip = warshipHurtClip;
                break;
            case AudioClips.warshipCollisionClip:
                audioSource.clip = warshipCollisionClip;
                break;
            case AudioClips.warshipEmergingClip:
                audioSource.clip = warshipEmergingClip;
                break;
            case AudioClips.warshipDivingClip:
                audioSource.clip = warshipDivingClip;
                break;
            case AudioClips.noAmmosClip:
                audioSource.clip = noAmmosClip;
                break;
        }
        // Tentative pour jouer 2 sons pendant la même frame
        //if (audioSource.isPlaying)
        //{
        //    AudioSource a = new AudioSource();
        //    a.clip = audioSource.clip;
        //    a.enabled = true;
        //    a.Play();
        //}
        audioSource.Play();
    }

}
