using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class Hazard : MonoBehaviour
{
    public enum Type {None, Fire, Virus, ShortCircuit, BrokenPiece, LowEnergy}
    public Type type;
    public Tool.Type requiredToolType;
    public int fixLoops;
    public AudioClip[] fixingAudios;
    public event Action onFix;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Fix()
    {
        onFix?.Invoke();
        Destroy(this.gameObject);
    }

    public float PlayRandomSound()
    {
        AudioClip selectedSound = fixingAudios[UnityEngine.Random.Range(0, fixingAudios.Length)];
        audioSource.PlayOneShot(selectedSound);
        return selectedSound.length;
    }
}
