using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ReferentialAudioSoundClip", menuName = GameData.GameName + "/ReferentialAudioSoundClip")]
public class ReferentialAudioSoundClip : ScriptableObject
{
    [SerializeField]
    private AudioClip clip;
    public AudioClip Clip => clip;
}
