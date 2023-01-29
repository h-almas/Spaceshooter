using UnityEngine;
using UnityEngine.Audio;

namespace AudioManager
{
    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip audioClip;
        public AudioMixerGroup audioMixerGroup;
        [Range(0,1)] public float volume;
        public bool loop;
        [HideInInspector] public AudioSource audioSource;
    }
}
