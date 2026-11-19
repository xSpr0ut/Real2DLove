<<<<<<< HEAD
using UnityEngine;

public class File_Paths 
{

    private const string homeDirectorySymbol = "~/";
    public static readonly string root = $"{Application.dataPath}/Game_Data/";

    //Runtime Paths
    public static readonly string gameSaves = $"{runtime_Path}Save Files/";

    //Resources Paths
    public static readonly string resourcesFont = "Fonts/";

    public static readonly string resourcesGraphics = "Graphics/";
    public static readonly string resourcesBackgroundImages = $"{resourcesGraphics}Background_Images/";
    public static readonly string resourcesBackgroundVideos = $"{resourcesGraphics}Background_Videos/";
    public static readonly string resourcesBlendTextures = $"{resourcesGraphics}Transitions/";
    public static readonly string resourcesGallery = $"{resourcesGraphics}Gallery/";
    public static readonly string resourcesDCP = $"{resourcesGraphics}DCP/";

    public static readonly string resourcesAudio = "Audio/";
    public static readonly string resourcesSFX = $"{resourcesAudio}SFX/";
    public static readonly string resourcesVoices= $"{resourcesAudio}Voices/";
    public static readonly string resourcesMusic = $"{resourcesAudio}Music/";
    public static readonly string resourcesAmbience = $"{resourcesAudio}Ambience/";

    public static readonly string resourcesDialogFiles = "Dialog_Files/";

    public static string Get_Path_To_Resource(string defaultPath, string resourceName){

            if(resourceName.StartsWith(homeDirectorySymbol)){

                return resourceName.Substring(homeDirectorySymbol.Length);

            }

            return defaultPath + resourceName;

        }

    public static string runtime_Path{

        get{

            #if UNITY_EDITOR

                return "Assets/appdata/";

            #else

                return Application.persistentDataPath + "/appdata/";

            #endif

        }

    }

}
=======
using UnityEngine;

public class File_Paths 
{

    private const string homeDirectorySymbol = "~/";
    public static readonly string root = $"{Application.dataPath}/Game_Data/";

    //Runtime Paths
    public static readonly string gameSaves = $"{runtime_Path}Save Files/";

    //Resources Paths
    public static readonly string resourcesFont = "Fonts/";

    public static readonly string resourcesGraphics = "Graphics/";
    public static readonly string resourcesBackgroundImages = $"{resourcesGraphics}Background_Images/";
    public static readonly string resourcesBackgroundVideos = $"{resourcesGraphics}Background_Videos/";
    public static readonly string resourcesBlendTextures = $"{resourcesGraphics}Transitions/";
    public static readonly string resourcesGallery = $"{resourcesGraphics}Gallery/";
    public static readonly string resourcesDCP = $"{resourcesGraphics}DCP/";

    public static readonly string resourcesAudio = "Audio/";
    public static readonly string resourcesSFX = $"{resourcesAudio}SFX/";
    public static readonly string resourcesVoices= $"{resourcesAudio}Voices/";
    public static readonly string resourcesMusic = $"{resourcesAudio}Music/";
    public static readonly string resourcesAmbience = $"{resourcesAudio}Ambience/";

    public static readonly string resourcesDialogFiles = "Dialog_Files/";

    public static string Get_Path_To_Resource(string defaultPath, string resourceName){

            if(resourceName.StartsWith(homeDirectorySymbol)){

                return resourceName.Substring(homeDirectorySymbol.Length);

            }

            return defaultPath + resourceName;

        }

    public static string runtime_Path{

        get{

            #if UNITY_EDITOR

                return "Assets/appdata/";

            #else

                return Application.persistentDataPath + "/appdata/";

            #endif

        }

    }

}
>>>>>>> c8d4f422 (Whole Template)
