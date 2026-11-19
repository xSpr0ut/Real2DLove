<<<<<<< HEAD
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Menu.Gallery{

    [System.Serializable]

    public class Gallery_Config{

        public static Gallery_Config activeConfig;
        public const bool encrypt = false;

        public static string filePath => $"{File_Paths.root}gallery.vng";
        
        public List<string> unlockedImages = new List<string>();

        public static void Load(){

            if(File.Exists(filePath)){

                activeConfig = File_Manager.Load<Gallery_Config>(filePath, encrypt: encrypt);

            }else{

                activeConfig = new Gallery_Config();

            }

        }

        public static void Save() => File_Manager.Save(filePath, JsonUtility.ToJson(activeConfig), encrypt);

        public static void Erase(){

            if(activeConfig == null){

                activeConfig = new Gallery_Config();

            }

            activeConfig.unlockedImages = new List<string>();

            Save();

        }

        public static void Unlock_Image(string imageName){

            if(activeConfig == null){

                Load();

            }

            if(!activeConfig.unlockedImages.Contains(imageName)){

                activeConfig.unlockedImages.Add(imageName);
                Save();

            }

        }

        public static bool Image_Is_Unlocked(string imageName){

            if(activeConfig == null){

                Load();

            }

            return activeConfig.unlockedImages.Contains(imageName);

        }

    }

}
=======
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Menu.Gallery{

    [System.Serializable]

    public class Gallery_Config{

        public static Gallery_Config activeConfig;
        public const bool encrypt = false;

        public static string filePath => $"{File_Paths.root}gallery.vng";
        
        public List<string> unlockedImages = new List<string>();

        public static void Load(){

            if(File.Exists(filePath)){

                activeConfig = File_Manager.Load<Gallery_Config>(filePath, encrypt: encrypt);

            }else{

                activeConfig = new Gallery_Config();

            }

        }

        public static void Save() => File_Manager.Save(filePath, JsonUtility.ToJson(activeConfig), encrypt);

        public static void Erase(){

            if(activeConfig == null){

                activeConfig = new Gallery_Config();

            }

            activeConfig.unlockedImages = new List<string>();

            Save();

        }

        public static void Unlock_Image(string imageName){

            if(activeConfig == null){

                Load();

            }

            if(!activeConfig.unlockedImages.Contains(imageName)){

                activeConfig.unlockedImages.Add(imageName);
                Save();

            }

        }

        public static bool Image_Is_Unlocked(string imageName){

            if(activeConfig == null){

                Load();

            }

            return activeConfig.unlockedImages.Contains(imageName);

        }

    }

}
>>>>>>> c8d4f422 (Whole Template)
