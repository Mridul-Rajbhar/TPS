using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    [SerializeField]
    AudioClip riffle_shoot, pistol_shoot, no_ammo, pick_up, footsteps;
    void play_sound(AudioSource audioSource, AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    public void play_riffle_sound(AudioSource audioSource)
    {
        play_sound(audioSource, riffle_shoot);
    }

    public void play_pistol_shoot(AudioSource audioSource)
    {
        play_sound(audioSource, pistol_shoot);
    }

    public void play_no_ammo(AudioSource audioSource)
    {
        play_sound(audioSource, no_ammo);
    }

    public void play_pick_up(AudioSource audioSource)
    {
        play_sound(audioSource, pick_up);
    }

    public void play_footsteps(AudioSource audioSource)
    {
        play_sound(audioSource, footsteps);
    }
}
