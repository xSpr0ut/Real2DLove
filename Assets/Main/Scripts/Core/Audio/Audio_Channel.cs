<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Audio{

    public class Audio_Channel{
        
        private const string trackContainerNameFormat = "Channel: [{0}]";
        public int channelIndex { get; private set; }
        public Transform trackContainer { get; private set; } = null;
        public Audio_Track activeTrack { get; private set; } = null;
        private List<Audio_Track> tracks = new List<Audio_Track>();

        private Coroutine Co_Volume_Leveling = null;
        private bool isLevelingVolume => Co_Volume_Leveling != null;

        public Audio_Channel(int channel){

            channelIndex = channel;

            trackContainer = new GameObject(string.Format(trackContainerNameFormat, channel)).transform;
            trackContainer.SetParent(Audio_Manager.instance.transform);

        }

        public Audio_Track Play_Track(AudioClip clip, bool loop, float startingVolume, float volumeCap, float pitch, string filePath){

            if(Try_Get_Track(clip.name, out Audio_Track existingTrack)){

                if(!existingTrack.isPlaying){

                    existingTrack.Play();

                }

                Set_As_Active_Track(existingTrack);
                return existingTrack;

            }

            Audio_Track track = new Audio_Track(clip, loop , startingVolume, volumeCap, pitch, this, Audio_Manager.instance.musicMixer, filePath);
            track.Play();
            Set_As_Active_Track(track);

            return track;

        }

        public bool Try_Get_Track(string trackName, out Audio_Track value){

            trackName = trackName.ToLower();

            foreach(var track in tracks){

                if(track.name.ToLower() == trackName){

                    value = track;
                    return true;

                }

            }

            value = null;
            return false;

        }

        private void Set_As_Active_Track(Audio_Track track){

            if(!tracks.Contains(track)){

                tracks.Add(track);

            }

            activeTrack = track;

            Try_Start_Volume_Leveling();

        }

        private void Try_Start_Volume_Leveling(){

            if(!isLevelingVolume){

                Co_Volume_Leveling = Audio_Manager.instance.StartCoroutine(Volume_Leveling());

            }

        }

        private IEnumerator Volume_Leveling(){

            while((activeTrack != null && (tracks.Count > 1 || activeTrack.volume != activeTrack.volumeCap)) || (activeTrack == null && tracks.Count > 0)){

                for(int i = tracks.Count - 1; i >= 0; i--){

                    Audio_Track track = tracks[i];
                    float targetVol = activeTrack == track ? track.volumeCap : 0;

                    if(track == activeTrack && track.volume == targetVol){

                        continue;

                    }

                    track.volume = Mathf.MoveTowards(track.volume, targetVol, Audio_Manager.trackTransitionSpeed * Time.deltaTime);

                    if(track != activeTrack && track.volume == 0){

                        Destroy_Track(track);

                    }

                }

                yield return null;

            }

            Co_Volume_Leveling = null;

        }

        private void Destroy_Track(Audio_Track track){

            if(tracks.Contains(track)){

                tracks.Remove(track);

            }

            Object.Destroy(track.root);

        }

        public void Stop_Track(bool immediate = false){

            if(activeTrack == null){

                return;

            }

            if(immediate){

                Destroy_Track(activeTrack);
                activeTrack = null;

            }else{

                activeTrack = null;
                Try_Start_Volume_Leveling();

            }

        }

    }

}
=======
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Audio{

    public class Audio_Channel{
        
        private const string trackContainerNameFormat = "Channel: [{0}]";
        public int channelIndex { get; private set; }
        public Transform trackContainer { get; private set; } = null;
        public Audio_Track activeTrack { get; private set; } = null;
        private List<Audio_Track> tracks = new List<Audio_Track>();

        private Coroutine Co_Volume_Leveling = null;
        private bool isLevelingVolume => Co_Volume_Leveling != null;

        public Audio_Channel(int channel){

            channelIndex = channel;

            trackContainer = new GameObject(string.Format(trackContainerNameFormat, channel)).transform;
            trackContainer.SetParent(Audio_Manager.instance.transform);

        }

        public Audio_Track Play_Track(AudioClip clip, bool loop, float startingVolume, float volumeCap, float pitch, string filePath){

            if(Try_Get_Track(clip.name, out Audio_Track existingTrack)){

                if(!existingTrack.isPlaying){

                    existingTrack.Play();

                }

                Set_As_Active_Track(existingTrack);
                return existingTrack;

            }

            Audio_Track track = new Audio_Track(clip, loop , startingVolume, volumeCap, pitch, this, Audio_Manager.instance.musicMixer, filePath);
            track.Play();
            Set_As_Active_Track(track);

            return track;

        }

        public bool Try_Get_Track(string trackName, out Audio_Track value){

            trackName = trackName.ToLower();

            foreach(var track in tracks){

                if(track.name.ToLower() == trackName){

                    value = track;
                    return true;

                }

            }

            value = null;
            return false;

        }

        private void Set_As_Active_Track(Audio_Track track){

            if(!tracks.Contains(track)){

                tracks.Add(track);

            }

            activeTrack = track;

            Try_Start_Volume_Leveling();

        }

        private void Try_Start_Volume_Leveling(){

            if(!isLevelingVolume){

                Co_Volume_Leveling = Audio_Manager.instance.StartCoroutine(Volume_Leveling());

            }

        }

        private IEnumerator Volume_Leveling(){

            while((activeTrack != null && (tracks.Count > 1 || activeTrack.volume != activeTrack.volumeCap)) || (activeTrack == null && tracks.Count > 0)){

                for(int i = tracks.Count - 1; i >= 0; i--){

                    Audio_Track track = tracks[i];
                    float targetVol = activeTrack == track ? track.volumeCap : 0;

                    if(track == activeTrack && track.volume == targetVol){

                        continue;

                    }

                    track.volume = Mathf.MoveTowards(track.volume, targetVol, Audio_Manager.trackTransitionSpeed * Time.deltaTime);

                    if(track != activeTrack && track.volume == 0){

                        Destroy_Track(track);

                    }

                }

                yield return null;

            }

            Co_Volume_Leveling = null;

        }

        private void Destroy_Track(Audio_Track track){

            if(tracks.Contains(track)){

                tracks.Remove(track);

            }

            Object.Destroy(track.root);

        }

        public void Stop_Track(bool immediate = false){

            if(activeTrack == null){

                return;

            }

            if(immediate){

                Destroy_Track(activeTrack);
                activeTrack = null;

            }else{

                activeTrack = null;
                Try_Start_Volume_Leveling();

            }

        }

    }

}
>>>>>>> c8d4f422 (Whole Template)
