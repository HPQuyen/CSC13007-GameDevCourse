using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ReferentialAudioSource : MonoBehaviour
{
    [SerializeField]
    private ReferentialAudioSoundClip soundClip = null;
    private void Awake()
    {
        if (soundClip == null)
        {
            Debug.LogWarning("ReferentialAudioSoundClip is null");
            return;
        }
        var audioSource = GetComponent<AudioSource>();
        if(audioSource.clip != null)
            audioSource.clip = soundClip.Clip;
        if (audioSource.playOnAwake)
            audioSource.Play();
    }
}
