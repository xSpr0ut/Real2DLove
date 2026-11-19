<<<<<<< HEAD
using System.Collections.Generic;
using Audio;
using UnityEngine;

namespace History{

    [System.Serializable]

    public class Audio_Data{
        
        public int channel = 0;
        public string trackname;
        public string trackPath;
        public float trackVolume;
        public float trackPitch;
        public bool loop;

        public Audio_Data(Audio_Channel channel){

            this.channel = channel.channelIndex;

            if(channel.activeTrack == null){

                return;

            }

            var track = channel.activeTrack;
            trackname = track.name;
            trackPath = track.path;
            trackVolume = track.volumeCap;
            trackPitch = track.pitch;
            loop = track.loop;

        }

        public static List<Audio_Data> Capture(){

            List<Audio_Data> audioChannels = new List<Audio_Data>();

            foreach(var channel in Audio_Manager.instance.channels){

                if(channel.Value.activeTrack == null){

                    continue;

                }

                Audio_Data data = new Audio_Data(channel.Value);
                audioChannels.Add(data);

            }

            return audioChannels;

        }

        public static void Apply(List<Audio_Data> data){

            List<int> cache = new List<int>();

            foreach(var channelData in data){

                Audio_Channel channel = Audio_Manager.instance.Try_Get_Channel(channelData.channel, createIfNonexistent: true);

                if(channel.activeTrack == null || channel.activeTrack.name != channelData.trackname){

                    AudioClip clip = History_Cache.Load_Audio(channelData.trackPath);

                    if(clip != null){

                        channel.Stop_Track(true);
                        channel.Play_Track(clip, channelData.loop, channelData.trackVolume, channelData.trackVolume, channelData.trackPitch, channelData.trackPath);

                    }else{

                        Debug.LogWarning($"How am I suppsoed to load this audio {channelData.trackPath}?!");

                    }

                }

                cache.Add(channelData.channel);

            }

            foreach(var channel in Audio_Manager.instance.channels){

                if(!cache.Contains(channel.Value.channelIndex)){

                    channel.Value.Stop_Track(immediate: true);

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

    public class Audio_Data{
        
        public int channel = 0;
        public string trackname;
        public string trackPath;
        public float trackVolume;
        public float trackPitch;
        public bool loop;

        public Audio_Data(Audio_Channel channel){

            this.channel = channel.channelIndex;

            if(channel.activeTrack == null){

                return;

            }

            var track = channel.activeTrack;
            trackname = track.name;
            trackPath = track.path;
            trackVolume = track.volumeCap;
            trackPitch = track.pitch;
            loop = track.loop;

        }

        public static List<Audio_Data> Capture(){

            List<Audio_Data> audioChannels = new List<Audio_Data>();

            foreach(var channel in Audio_Manager.instance.channels){

                if(channel.Value.activeTrack == null){

                    continue;

                }

                Audio_Data data = new Audio_Data(channel.Value);
                audioChannels.Add(data);

            }

            return audioChannels;

        }

        public static void Apply(List<Audio_Data> data){

            List<int> cache = new List<int>();

            foreach(var channelData in data){

                Audio_Channel channel = Audio_Manager.instance.Try_Get_Channel(channelData.channel, createIfNonexistent: true);

                if(channel.activeTrack == null || channel.activeTrack.name != channelData.trackname){

                    AudioClip clip = History_Cache.Load_Audio(channelData.trackPath);

                    if(clip != null){

                        channel.Stop_Track(true);
                        channel.Play_Track(clip, channelData.loop, channelData.trackVolume, channelData.trackVolume, channelData.trackPitch, channelData.trackPath);

                    }else{

                        Debug.LogWarning($"How am I suppsoed to load this audio {channelData.trackPath}?!");

                    }

                }

                cache.Add(channelData.channel);

            }

            foreach(var channel in Audio_Manager.instance.channels){

                if(!cache.Contains(channel.Value.channelIndex)){

                    channel.Value.Stop_Track(immediate: true);

                }

            }

        }

    }

}
>>>>>>> c8d4f422 (Whole Template)
