<<<<<<< HEAD
using System;
using System.Collections;
using System.Collections.Generic;
using Audio;
using UnityEngine;

namespace Commands{

    public class CDE_Audio : Command_Database_Extension{

        private static string[] parameterSFX = new string[] { "#s", "#sfx" };
        private static string[] parameterVolume = new string[] { "#v", "#vol", "#volume" };
        private static string[] parameterPitch = new string[] { "#p", "#pitch" };
        private static string[] parameterLoop = new string[] { "#lo", "#loop" };

        private static string[] parameterChannel = new string[] { "#c", "#channel" };
        private static string[] parameterStartVolume = new string[] { "#sv", "#startvolume" };
        private static string[] parameterSong= new string[] { "#s", "#song" };
        private static string[] parameterAmbience = new string[] { "#a", "#ambience" };
        
        new public static void Extend(Command_Database database){

            database.Add_Command("play_sfx", new Action<string[]>(Play_SFX));
            database.Add_Command("stop_sfx", new Action<string>(Stop_SFX));

            database.Add_Command("play_voice", new Action<string[]>(Play_Voice));
            database.Add_Command("stop_voice", new Action<string>(Stop_SFX));

            database.Add_Command("play_music", new Action<string[]>(Play_Music));
            database.Add_Command("play_ambience", new Action<string[]>(Play_Ambience));

            database.Add_Command("stop_music", new Action<string>(Stop_Music));
            database.Add_Command("stop_ambience", new Action<string>(Stop_Ambience));

        }

        private static void Play_SFX(string[] data){

            string filePath;
            float volume, pitch;
            bool loop;

            var parameters = Convert_Data_To_Parameters(data);

            parameters.Try_Get_Value(parameterSFX, out filePath);
            parameters.Try_Get_Value(parameterVolume, out volume, defaultValue: 1f);
            parameters.Try_Get_Value(parameterPitch, out pitch, defaultValue: 1f);
            parameters.Try_Get_Value(parameterLoop, out loop, defaultValue: false);

            string resourcesPath = File_Paths.Get_Path_To_Resource(File_Paths.resourcesSFX, filePath);
            AudioClip sound = Resources.Load<AudioClip>(resourcesPath);

            if(sound == null){

                return;

            }

            Audio_Manager.instance.Play_Sound_Effect(sound, volume: volume, pitch: pitch, loop: loop, filePath: resourcesPath);

        }

        private static void Stop_SFX(string data){

            Audio_Manager.instance.Stop_Sound_Effect(data);

        }

        private static void Play_Voice(string[] data){

            string filePath;
            float volume, pitch;
            bool loop;

            var parameters = Convert_Data_To_Parameters(data);

            parameters.Try_Get_Value(parameterSFX, out filePath);
            parameters.Try_Get_Value(parameterVolume, out volume, defaultValue: 1f);
            parameters.Try_Get_Value(parameterPitch, out pitch, defaultValue: 1f);
            parameters.Try_Get_Value(parameterLoop, out loop, defaultValue: false);

            AudioClip sound = Resources.Load<AudioClip>(File_Paths.Get_Path_To_Resource(File_Paths.resourcesVoices, filePath));

            if(sound == null){

                return;

            }

            Audio_Manager.instance.Play_Voice(sound, volume: volume, pitch: pitch, loop: loop);

        }

        private static void Play_Music(string[] data){

            string filePath;
            int channel;

            var parameters = Convert_Data_To_Parameters(data);

            parameters.Try_Get_Value(parameterSong, out filePath);
            filePath = File_Paths.Get_Path_To_Resource(File_Paths.resourcesMusic, filePath);

            parameters.Try_Get_Value(parameterChannel, out channel, defaultValue: 1);

            Play_Track(filePath, channel, parameters);

        }

        private static void Play_Ambience(string[] data){

            string filePath;
            int channel;

            var parameters = Convert_Data_To_Parameters(data);

            parameters.Try_Get_Value(parameterAmbience, out filePath);
            filePath = File_Paths.Get_Path_To_Resource(File_Paths.resourcesAmbience, filePath);

            parameters.Try_Get_Value(parameterChannel, out channel, defaultValue: 1);

            Play_Track(filePath, channel, parameters);
            
        }

        private static void Play_Track(string filePath, int channel, Command_Parameters parameters){

            bool loop;
            float volumeCap;
            float startingVolume;
            float pitch;

            parameters.Try_Get_Value(parameterVolume, out volumeCap, defaultValue: 1f);
            parameters.Try_Get_Value(parameterStartVolume, out startingVolume, defaultValue: 0f);
            parameters.Try_Get_Value(parameterPitch, out pitch, defaultValue: 1f);
            parameters.Try_Get_Value(parameterLoop, out loop, defaultValue: true);
            
            AudioClip sound = Resources.Load<AudioClip>(filePath);

            if(sound == null){

                return;

            }

            Audio_Manager.instance.Play_Track(sound, channel, loop: loop, startingVolume: startingVolume, volumeCap: volumeCap, pitch: pitch, filePath);

        }

        private static void Stop_Music(string data){

            if(data == string.Empty){

                Stop_Track("1");

            }else{

                Stop_Track(data);

            }

        }

        private static void Stop_Ambience(string data){

            if(data == string.Empty){

                Stop_Track("0");

            }else{

                Stop_Track(data);

            }

        }

        private static void Stop_Track(string data){

            if(int.TryParse(data, out int channel)){

                Audio_Manager.instance.Stop_Track(channel);

            }else{

                Audio_Manager.instance.Stop_Track(data);

            }

        }

    }

}
=======
using System;
using System.Collections;
using System.Collections.Generic;
using Audio;
using UnityEngine;

namespace Commands{

    public class CDE_Audio : Command_Database_Extension{

        private static string[] parameterSFX = new string[] { "#s", "#sfx" };
        private static string[] parameterVolume = new string[] { "#v", "#vol", "#volume" };
        private static string[] parameterPitch = new string[] { "#p", "#pitch" };
        private static string[] parameterLoop = new string[] { "#lo", "#loop" };

        private static string[] parameterChannel = new string[] { "#c", "#channel" };
        private static string[] parameterStartVolume = new string[] { "#sv", "#startvolume" };
        private static string[] parameterSong= new string[] { "#s", "#song" };
        private static string[] parameterAmbience = new string[] { "#a", "#ambience" };
        
        new public static void Extend(Command_Database database){

            database.Add_Command("play_sfx", new Action<string[]>(Play_SFX));
            database.Add_Command("stop_sfx", new Action<string>(Stop_SFX));

            database.Add_Command("play_voice", new Action<string[]>(Play_Voice));
            database.Add_Command("stop_voice", new Action<string>(Stop_SFX));

            database.Add_Command("play_music", new Action<string[]>(Play_Music));
            database.Add_Command("play_ambience", new Action<string[]>(Play_Ambience));

            database.Add_Command("stop_music", new Action<string>(Stop_Music));
            database.Add_Command("stop_ambience", new Action<string>(Stop_Ambience));

        }

        private static void Play_SFX(string[] data){

            string filePath;
            float volume, pitch;
            bool loop;

            var parameters = Convert_Data_To_Parameters(data);

            parameters.Try_Get_Value(parameterSFX, out filePath);
            parameters.Try_Get_Value(parameterVolume, out volume, defaultValue: 1f);
            parameters.Try_Get_Value(parameterPitch, out pitch, defaultValue: 1f);
            parameters.Try_Get_Value(parameterLoop, out loop, defaultValue: false);

            string resourcesPath = File_Paths.Get_Path_To_Resource(File_Paths.resourcesSFX, filePath);
            AudioClip sound = Resources.Load<AudioClip>(resourcesPath);

            if(sound == null){

                return;

            }

            Audio_Manager.instance.Play_Sound_Effect(sound, volume: volume, pitch: pitch, loop: loop, filePath: resourcesPath);

        }

        private static void Stop_SFX(string data){

            Audio_Manager.instance.Stop_Sound_Effect(data);

        }

        private static void Play_Voice(string[] data){

            string filePath;
            float volume, pitch;
            bool loop;

            var parameters = Convert_Data_To_Parameters(data);

            parameters.Try_Get_Value(parameterSFX, out filePath);
            parameters.Try_Get_Value(parameterVolume, out volume, defaultValue: 1f);
            parameters.Try_Get_Value(parameterPitch, out pitch, defaultValue: 1f);
            parameters.Try_Get_Value(parameterLoop, out loop, defaultValue: false);

            AudioClip sound = Resources.Load<AudioClip>(File_Paths.Get_Path_To_Resource(File_Paths.resourcesVoices, filePath));

            if(sound == null){

                return;

            }

            Audio_Manager.instance.Play_Voice(sound, volume: volume, pitch: pitch, loop: loop);

        }

        private static void Play_Music(string[] data){

            string filePath;
            int channel;

            var parameters = Convert_Data_To_Parameters(data);

            parameters.Try_Get_Value(parameterSong, out filePath);
            filePath = File_Paths.Get_Path_To_Resource(File_Paths.resourcesMusic, filePath);

            parameters.Try_Get_Value(parameterChannel, out channel, defaultValue: 1);

            Play_Track(filePath, channel, parameters);

        }

        private static void Play_Ambience(string[] data){

            string filePath;
            int channel;

            var parameters = Convert_Data_To_Parameters(data);

            parameters.Try_Get_Value(parameterAmbience, out filePath);
            filePath = File_Paths.Get_Path_To_Resource(File_Paths.resourcesAmbience, filePath);

            parameters.Try_Get_Value(parameterChannel, out channel, defaultValue: 1);

            Play_Track(filePath, channel, parameters);
            
        }

        private static void Play_Track(string filePath, int channel, Command_Parameters parameters){

            bool loop;
            float volumeCap;
            float startingVolume;
            float pitch;

            parameters.Try_Get_Value(parameterVolume, out volumeCap, defaultValue: 1f);
            parameters.Try_Get_Value(parameterStartVolume, out startingVolume, defaultValue: 0f);
            parameters.Try_Get_Value(parameterPitch, out pitch, defaultValue: 1f);
            parameters.Try_Get_Value(parameterLoop, out loop, defaultValue: true);
            
            AudioClip sound = Resources.Load<AudioClip>(filePath);

            if(sound == null){

                return;

            }

            Audio_Manager.instance.Play_Track(sound, channel, loop: loop, startingVolume: startingVolume, volumeCap: volumeCap, pitch: pitch, filePath);

        }

        private static void Stop_Music(string data){

            if(data == string.Empty){

                Stop_Track("1");

            }else{

                Stop_Track(data);

            }

        }

        private static void Stop_Ambience(string data){

            if(data == string.Empty){

                Stop_Track("0");

            }else{

                Stop_Track(data);

            }

        }

        private static void Stop_Track(string data){

            if(int.TryParse(data, out int channel)){

                Audio_Manager.instance.Stop_Track(channel);

            }else{

                Audio_Manager.instance.Stop_Track(data);

            }

        }

    }

}
>>>>>>> c8d4f422 (Whole Template)
