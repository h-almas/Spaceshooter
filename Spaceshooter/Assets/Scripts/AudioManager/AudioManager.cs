using System;
using UnityEngine;

namespace AudioManager
{
    public class AudioManager : MonoBehaviour
    {
        public string soundTitle;
        public Sound[] sounds;

        private void Awake()
        {
            foreach (Sound s in sounds)
            {
                s.audioSource = gameObject.AddComponent<AudioSource>();
                s.audioSource.clip = s.audioClip;
                s.audioSource.volume = s.volume;
                s.audioSource.loop = s.loop;
            }
        }

        private void Start()
        {
            PlaySound(soundTitle);
        }

        public void PlaySound(string soundName)
        {
            Sound s = Array.Find(sounds, sound => sound.name == soundName);
            if(s == null) return;
            s.audioSource.Play();
        }
    }
}
