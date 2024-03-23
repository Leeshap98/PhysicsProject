using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Sound Effects/Sound Effects Data")]
public class SoundEffectsData : ScriptableObject
{
    public List<AudioClip> soundEffects = new List<AudioClip>();
}
