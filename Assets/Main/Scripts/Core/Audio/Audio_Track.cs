<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Experimental.GlobalIllumination;

namespace Audio{

    public class Audio_Track{
        
        private const string trackNameFormat = "Track: [{0}]";
        public string name { get; private set; }
        public string path { get; private set; }
        public GameObject root => source.gameObject;
        private Audio_Channel channel;
        private AudioSource source;
        public bool loop => source.loop;
        public float volumeCap { get; private set; }
        public float pitch { get { return source.pitch; } set { source.pitch = value; } }
        public bool isPlaying => source.isPlaying;
        public float volume { get { return source.volume; } set { source.volume = value; } }

        public Audio_Track(AudioClip clip, bool loop, float startingVolume, float volumeCap, float pitch, Audio_Channel channel, AudioMixerGroup mixer, string filePath){

            name = clip.name;
            path = filePath;
            
            this.channel = channel;
            this.volumeCap = volumeCap;

            source = Create_Source();
            source.clip = clip;
            source.loop = loop;
            source.volume = startingVolume;
            source.pitch = pitch;
            source.outputAudioMixerGroup = mixer;

        }

        private AudioSource Create_Source(){

            GameObject gameObject = new GameObject(string.Format(trackNameFormat, name));
            gameObject.transform.SetParent(channel.trackContainer);
            AudioSource source = gameObject.AddComponent<AudioSource>();

            return source;

        }

        public void Play(){

            source.Play();

        }

        public void Stop(){

            source.Stop();

        }

    }

}
=======
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Experimental.GlobalIllumination;

namespace Audio{

    public class Audio_Track{
        
        private const string trackNameFormat = "Track: [{0}]";
        public string name { get; private set; }
        public string path { get; private set; }
        public GameObject root => source.gameObject;
        private Audio_Channel channel;
        private AudioSource source;
        public bool loop => source.loop;
        public float volumeCap { get; private set; }
        public float pitch { get { return source.pitch; } set { source.pitch = value; } }
        public bool isPlaying => source.isPlaying;
        public float volume { get { return source.volume; } set { source.volume = value; } }

        public Audio_Track(AudioClip clip, bool loop, float startingVolume, float volumeCap, float pitch, Audio_Channel channel, AudioMixerGroup mixer, string filePath){

            name = clip.name;
            path = filePath;
            
            this.channel = channel;
            this.volumeCap = volumeCap;

            source = Create_Source();
            source.clip = clip;
            source.loop = loop;
            source.volume = startingVolume;
            source.pitch = pitch;
            source.outputAudioMixerGroup = mixer;

        }

        private AudioSource Create_Source(){

            GameObject gameObject = new GameObject(string.Format(trackNameFormat, name));
            gameObject.transform.SetParent(channel.trackContainer);
            AudioSource source = gameObject.AddComponent<AudioSource>();

            return source;

        }

        public void Play(){

            source.Play();

        }

        public void Stop(){

            source.Stop();

        }

    }

}
>>>>>>> c8d4f422 (Whole Template)
