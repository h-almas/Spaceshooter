using UnityEngine;

namespace AudioManager
{
    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip audioClip;
        [Range(0,1)] public float volume;
        public bool loop;
        [HideInInspector] public AudioSource audioSource;
    }
}
