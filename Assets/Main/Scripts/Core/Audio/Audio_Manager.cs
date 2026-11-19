<<<<<<< HEAD
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Audio{

    public class Audio_Manager : MonoBehaviour{

        public const string musicVolumeParameterName = "musicVolume";
        public const string sfxVolumeParameterName = "sfxVolume";
        public const string voicesVolumeParameterName = "voicesVolume";
        public const float mutedVolumeLevel = -80f;
        private const string sfxParentName = "SFX";
        public static readonly char[] sfxNameFormatContainers = new char[] {'[', ']'};
        private static string sfxNameFormat = $"SFX: {sfxNameFormatContainers[0]}" + "{0}" + $"{sfxNameFormatContainers[1]}";
        public const float trackTransitionSpeed = 1f;

        public static Audio_Manager instance { get; private set; }
        public Dictionary<int, Audio_Channel> channels = new Dictionary<int, Audio_Channel>();
        
        public AudioMixerGroup musicMixer;
        public AudioMixerGroup sfxMixer;
        public AudioMixerGroup voicesMixer;

        public AnimationCurve audioFallOffCurve;

        private Transform sfxRoot;

        public AudioSource[] allSFX => sfxRoot.GetComponentsInChildren<AudioSource>();

        private void Awake(){

            if(instance == null){

                transform.SetParent(null);
                DontDestroyOnLoad(gameObject);
                instance = this;

            }else{

                DestroyImmediate(gameObject);
                return;

            }

            sfxRoot = new GameObject(sfxParentName).transform;
            sfxRoot.SetParent(transform);

        }

        public AudioSource Play_Sound_Effect(string filePath, AudioMixerGroup mixer = null, float volume = 1, float pitch = 1, bool loop = false){

            AudioClip clip = Resources.Load<AudioClip>(filePath);

            if(clip == null){

                Debug.LogError($"The fuck is this audio {filePath}?");
                return null;

            }

            return Play_Sound_Effect(clip, mixer, volume, pitch, loop, filePath);

        }

        public AudioSource Play_Sound_Effect(AudioClip clip, AudioMixerGroup mixer = null, float volume = 1, float pitch = 1, bool loop = false, string filePath = ""){

            string fileName = clip.name;

            if(filePath != string.Empty){

                fileName = filePath;

            }

            AudioSource effectSource = new GameObject(string.Format(sfxNameFormat, fileName)).AddComponent<AudioSource>();
            effectSource.transform.SetParent(sfxRoot);
            effectSource.transform.position = sfxRoot.position;

            effectSource.clip = clip;

            if(mixer == null){

                mixer = sfxMixer;

            }

            effectSource.outputAudioMixerGroup = mixer;
            effectSource.volume = volume;
            effectSource.spatialBlend = 0;
            effectSource.pitch = pitch;
            effectSource.loop = loop;

            if(!loop){

                Destroy(effectSource.gameObject, (clip.length /pitch) + 1);

            }

            effectSource.Play();

            return effectSource;

        }

        public AudioSource Play_Voice(string filePath, float volume = 1, float pitch = 1, bool loop = false){

            return Play_Sound_Effect(filePath, voicesMixer, volume, pitch, loop);

        }

        public AudioSource Play_Voice(AudioClip clip, float volume = 1, float pitch = 1, bool loop = false){

            return Play_Sound_Effect(clip, voicesMixer, volume, pitch, loop);

        }

        public void Stop_Sound_Effect(AudioClip clip) => Stop_Sound_Effect(clip.name);

        public void Stop_Sound_Effect(string soundName){

            soundName = soundName.ToLower();
            AudioSource[] sources = sfxRoot.GetComponentsInChildren<AudioSource>();

            foreach(var source in sources){

                Destroy(source.gameObject);
                return;

            }

        }

        public bool Is_Playing_SFX(string soundName){

            soundName = soundName.ToLower();
            AudioSource[] sources = sfxRoot.GetComponentsInChildren<AudioSource>();

            foreach(var source in sources){

                if(source.clip.name.ToLower() == soundName){

                    return true;

                }

            }

            return false;

        }

        public void Stop_All_Sound_Effects(){

            AudioSource[] sources = sfxRoot.GetComponentsInChildren<AudioSource>();

            foreach(var source in sources){

                Destroy(source.gameObject);

            }

        }

        public Audio_Track Play_Track(string filePath, int channel = 0, bool loop = true, float startingVolume = 0f, float volumeCap = 1f, float pitch = 1f){

            AudioClip clip = Resources.Load<AudioClip>(filePath);

            if(clip == null){

                Debug.LogError($"The fuck is this audio file {filePath}?");
                return null;

            }

            return Play_Track(clip, channel, loop, startingVolume, volumeCap, pitch, filePath);

        }

        public Audio_Track Play_Track(AudioClip clip, int channel = 0, bool loop = true, float startingVolume = 0f, float volumeCap = 1f, float pitch = 1f, string filePath = ""){

            Audio_Channel audioChannel = Try_Get_Channel(channel, createIfNonexistent: true);
            Audio_Track track = audioChannel.Play_Track(clip, loop, startingVolume, volumeCap, pitch, filePath);
            return track;

        }

        public void Stop_Track(int channel){

            Audio_Channel c = Try_Get_Channel(channel, createIfNonexistent: false);

            if(c == null){

                return;

            }

            c.Stop_Track();

        }

        public void Stop_Track(string trackName){

            trackName = trackName.ToLower();

            foreach(var channel in channels.Values){

                if(channel.activeTrack != null && channel.activeTrack.name.ToLower() == trackName){

                    channel.Stop_Track();
                    return;

                }

            }

        }

        public void Stop_All_Tracks(){

            foreach(Audio_Channel channel in channels.Values){

                channel.Stop_Track();

            }

        }

        public Audio_Channel Try_Get_Channel(int channelNumber, bool createIfNonexistent = false){

            Audio_Channel channel = null;

            if(channels.TryGetValue(channelNumber, out channel)){

                return channel;

            }else if(createIfNonexistent){

                channel = new Audio_Channel(channelNumber);
                channels.Add(channelNumber, channel);
                return channel;

            }

            return null;

        }

        public void Set_Music_Volume(float volume, bool muted){

            volume = muted ? mutedVolumeLevel : audioFallOffCurve.Evaluate(volume);
            musicMixer.audioMixer.SetFloat(musicVolumeParameterName, volume);  

        }

        public void Set_SFX_Volume(float volume, bool muted){

            volume = muted ? mutedVolumeLevel : audioFallOffCurve.Evaluate(volume);
            sfxMixer.audioMixer.SetFloat(sfxVolumeParameterName, volume);  

        }

        public void Set_Voices_Volume(float volume, bool muted){

            volume = muted ? mutedVolumeLevel : audioFallOffCurve.Evaluate(volume);
            voicesMixer.audioMixer.SetFloat(voicesVolumeParameterName, volume);  

        }

    }

}
=======
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Audio{

    public class Audio_Manager : MonoBehaviour{

        public const string musicVolumeParameterName = "musicVolume";
        public const string sfxVolumeParameterName = "sfxVolume";
        public const string voicesVolumeParameterName = "voicesVolume";
        public const float mutedVolumeLevel = -80f;
        private const string sfxParentName = "SFX";
        public static readonly char[] sfxNameFormatContainers = new char[] {'[', ']'};
        private static string sfxNameFormat = $"SFX: {sfxNameFormatContainers[0]}" + "{0}" + $"{sfxNameFormatContainers[1]}";
        public const float trackTransitionSpeed = 1f;

        public static Audio_Manager instance { get; private set; }
        public Dictionary<int, Audio_Channel> channels = new Dictionary<int, Audio_Channel>();
        
        public AudioMixerGroup musicMixer;
        public AudioMixerGroup sfxMixer;
        public AudioMixerGroup voicesMixer;

        public AnimationCurve audioFallOffCurve;

        private Transform sfxRoot;

        public AudioSource[] allSFX => sfxRoot.GetComponentsInChildren<AudioSource>();

        private void Awake(){

            if(instance == null){

                transform.SetParent(null);
                DontDestroyOnLoad(gameObject);
                instance = this;

            }else{

                DestroyImmediate(gameObject);
                return;

            }

            sfxRoot = new GameObject(sfxParentName).transform;
            sfxRoot.SetParent(transform);

        }

        public AudioSource Play_Sound_Effect(string filePath, AudioMixerGroup mixer = null, float volume = 1, float pitch = 1, bool loop = false){

            AudioClip clip = Resources.Load<AudioClip>(filePath);

            if(clip == null){

                Debug.LogError($"The fuck is this audio {filePath}?");
                return null;

            }

            return Play_Sound_Effect(clip, mixer, volume, pitch, loop, filePath);

        }

        public AudioSource Play_Sound_Effect(AudioClip clip, AudioMixerGroup mixer = null, float volume = 1, float pitch = 1, bool loop = false, string filePath = ""){

            string fileName = clip.name;

            if(filePath != string.Empty){

                fileName = filePath;

            }

            AudioSource effectSource = new GameObject(string.Format(sfxNameFormat, fileName)).AddComponent<AudioSource>();
            effectSource.transform.SetParent(sfxRoot);
            effectSource.transform.position = sfxRoot.position;

            effectSource.clip = clip;

            if(mixer == null){

                mixer = sfxMixer;

            }

            effectSource.outputAudioMixerGroup = mixer;
            effectSource.volume = volume;
            effectSource.spatialBlend = 0;
            effectSource.pitch = pitch;
            effectSource.loop = loop;

            if(!loop){

                Destroy(effectSource.gameObject, (clip.length /pitch) + 1);

            }

            effectSource.Play();

            return effectSource;

        }

        public AudioSource Play_Voice(string filePath, float volume = 1, float pitch = 1, bool loop = false){

            return Play_Sound_Effect(filePath, voicesMixer, volume, pitch, loop);

        }

        public AudioSource Play_Voice(AudioClip clip, float volume = 1, float pitch = 1, bool loop = false){

            return Play_Sound_Effect(clip, voicesMixer, volume, pitch, loop);

        }

        public void Stop_Sound_Effect(AudioClip clip) => Stop_Sound_Effect(clip.name);

        public void Stop_Sound_Effect(string soundName){

            soundName = soundName.ToLower();
            AudioSource[] sources = sfxRoot.GetComponentsInChildren<AudioSource>();

            foreach(var source in sources){

                Destroy(source.gameObject);
                return;

            }

        }

        public bool Is_Playing_SFX(string soundName){

            soundName = soundName.ToLower();
            AudioSource[] sources = sfxRoot.GetComponentsInChildren<AudioSource>();

            foreach(var source in sources){

                if(source.clip.name.ToLower() == soundName){

                    return true;

                }

            }

            return false;

        }

        public void Stop_All_Sound_Effects(){

            AudioSource[] sources = sfxRoot.GetComponentsInChildren<AudioSource>();

            foreach(var source in sources){

                Destroy(source.gameObject);

            }

        }

        public Audio_Track Play_Track(string filePath, int channel = 0, bool loop = true, float startingVolume = 0f, float volumeCap = 1f, float pitch = 1f){

            AudioClip clip = Resources.Load<AudioClip>(filePath);

            if(clip == null){

                Debug.LogError($"The fuck is this audio file {filePath}?");
                return null;

            }

            return Play_Track(clip, channel, loop, startingVolume, volumeCap, pitch, filePath);

        }

        public Audio_Track Play_Track(AudioClip clip, int channel = 0, bool loop = true, float startingVolume = 0f, float volumeCap = 1f, float pitch = 1f, string filePath = ""){

            Audio_Channel audioChannel = Try_Get_Channel(channel, createIfNonexistent: true);
            Audio_Track track = audioChannel.Play_Track(clip, loop, startingVolume, volumeCap, pitch, filePath);
            return track;

        }

        public void Stop_Track(int channel){

            Audio_Channel c = Try_Get_Channel(channel, createIfNonexistent: false);

            if(c == null){

                return;

            }

            c.Stop_Track();

        }

        public void Stop_Track(string trackName){

            trackName = trackName.ToLower();

            foreach(var channel in channels.Values){

                if(channel.activeTrack != null && channel.activeTrack.name.ToLower() == trackName){

                    channel.Stop_Track();
                    return;

                }

            }

        }

        public void Stop_All_Tracks(){

            foreach(Audio_Channel channel in channels.Values){

                channel.Stop_Track();

            }

        }

        public Audio_Channel Try_Get_Channel(int channelNumber, bool createIfNonexistent = false){

            Audio_Channel channel = null;

            if(channels.TryGetValue(channelNumber, out channel)){

                return channel;

            }else if(createIfNonexistent){

                channel = new Audio_Channel(channelNumber);
                channels.Add(channelNumber, channel);
                return channel;

            }

            return null;

        }

        public void Set_Music_Volume(float volume, bool muted){

            volume = muted ? mutedVolumeLevel : audioFallOffCurve.Evaluate(volume);
            musicMixer.audioMixer.SetFloat(musicVolumeParameterName, volume);  

        }

        public void Set_SFX_Volume(float volume, bool muted){

            volume = muted ? mutedVolumeLevel : audioFallOffCurve.Evaluate(volume);
            sfxMixer.audioMixer.SetFloat(sfxVolumeParameterName, volume);  

        }

        public void Set_Voices_Volume(float volume, bool muted){

            volume = muted ? mutedVolumeLevel : audioFallOffCurve.Evaluate(volume);
            voicesMixer.audioMixer.SetFloat(voicesVolumeParameterName, volume);  

        }

    }

}
>>>>>>> c8d4f422 (Whole Template)
