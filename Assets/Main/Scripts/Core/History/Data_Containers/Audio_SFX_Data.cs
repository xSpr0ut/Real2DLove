<<<<<<< HEAD
using System.Collections.Generic;
using Audio;
using UnityEngine;

namespace History{

    [System.Serializable]

    public class Audio_SFX_Data{
        
        public string filePath;
        public string fileName;
        public float volume;
        public float pitch;

        public static List<Audio_SFX_Data> Capture(){

            List<Audio_SFX_Data> audioList = new List<Audio_SFX_Data>();

            AudioSource[] sfx = Audio_Manager.instance.allSFX;

            foreach(var sound in sfx){

                if(!sound.loop){

                    continue;

                }

                Audio_SFX_Data data = new Audio_SFX_Data();
                data.volume = sound.volume;
                data.pitch = sound.pitch;
                data.fileName = sound.clip.name;

                string resourcesPath = sound.gameObject.name.Split(Audio_Manager.sfxNameFormatContainers)[1];

                data.filePath = resourcesPath;
                audioList.Add(data);

            }

            return audioList;

        }

        public static void Apply(List<Audio_SFX_Data> sfx){

            List<string> soundCache = new List<string>();

            foreach(var sound in sfx){

                if(!Audio_Manager.instance.Is_Playing_SFX(sound.fileName)){

                    Audio_Manager.instance.Play_Sound_Effect(sound.filePath, volume: sound.volume, pitch: sound.pitch, loop: true);

                }

                soundCache.Add(sound.fileName);

            }

            foreach(var source in Audio_Manager.instance.allSFX){

                if(!soundCache.Contains(source.clip.name)){

                    Audio_Manager.instance.Stop_Sound_Effect(source.clip);

                }

            }

        }

    }

}
=======
using System.Collections.Generic;
using Audio;
using UnityEngine;

namespace History{

    [System.Serializable]

    public class Audio_SFX_Data{
        
        public string filePath;
        public string fileName;
        public float volume;
        public float pitch;

        public static List<Audio_SFX_Data> Capture(){

            List<Audio_SFX_Data> audioList = new List<Audio_SFX_Data>();

            AudioSource[] sfx = Audio_Manager.instance.allSFX;

            foreach(var sound in sfx){

                if(!sound.loop){

                    continue;

                }

                Audio_SFX_Data data = new Audio_SFX_Data();
                data.volume = sound.volume;
                data.pitch = sound.pitch;
                data.fileName = sound.clip.name;

                string resourcesPath = sound.gameObject.name.Split(Audio_Manager.sfxNameFormatContainers)[1];

                data.filePath = resourcesPath;
                audioList.Add(data);

            }

            return audioList;

        }

        public static void Apply(List<Audio_SFX_Data> sfx){

            List<string> soundCache = new List<string>();

            foreach(var sound in sfx){

                if(!Audio_Manager.instance.Is_Playing_SFX(sound.fileName)){

                    Audio_Manager.instance.Play_Sound_Effect(sound.filePath, volume: sound.volume, pitch: sound.pitch, loop: true);

                }

                soundCache.Add(sound.fileName);

            }

            foreach(var source in Audio_Manager.instance.allSFX){

                if(!soundCache.Contains(source.clip.name)){

                    Audio_Manager.instance.Stop_Sound_Effect(source.clip);

                }

            }

        }

    }

}
>>>>>>> c8d4f422 (Whole Template)
