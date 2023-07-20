using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip arrowHit;
 
    public static AudioManager Instance;
    public AudioSource audioSource;
    private void Awake()
    {
        Instance = this;
    }

    public void PlayArrowHitSound()
    {
        audioSource.PlayOneShot(arrowHit);
    }


}
