<<<<<<< HEAD
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Video;

namespace History{

    public class History_Cache {
        
        public static Dictionary<string, (object asset, int saleIndex)> loadedAssets = new Dictionary<string, (object asset, int saleIndex)>();

        public static T Try_Load_Object<T>(string key){

            object resource = null;

            if(loadedAssets.ContainsKey(key)){

                resource = (T)loadedAssets[key].asset;

            }else{

                resource = Resources.Load(key);

                if(resource != null){

                    loadedAssets[key] = (resource, 0);

                }

            }

            if(resource != null){

                if(resource is T){

                    return (T)resource;

                }else{

                    Debug.LogWarning($"{key} is not the type I was expecting.");

                }

            }

            Debug.LogWarning($"How am I supposed to load from {key}?");
            return default (T);

        }

        public static TMP_FontAsset Load_Font(string key) => Try_Load_Object<TMP_FontAsset>(key);
        public static AudioClip Load_Audio(string key) => Try_Load_Object<AudioClip>(key);
        public static Texture2D Load_Image(string key) => Try_Load_Object<Texture2D>(key);
        public static VideoClip Load_Video(string key) => Try_Load_Object<VideoClip>(key);
        public static Sprite Load_DCP(string key) => Try_Load_Object<Sprite>(key);

    }

}
=======
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Video;

namespace History{

    public class History_Cache {
        
        public static Dictionary<string, (object asset, int saleIndex)> loadedAssets = new Dictionary<string, (object asset, int saleIndex)>();

        public static T Try_Load_Object<T>(string key){

            object resource = null;

            if(loadedAssets.ContainsKey(key)){

                resource = (T)loadedAssets[key].asset;

            }else{

                resource = Resources.Load(key);

                if(resource != null){

                    loadedAssets[key] = (resource, 0);

                }

            }

            if(resource != null){

                if(resource is T){

                    return (T)resource;

                }else{

                    Debug.LogWarning($"{key} is not the type I was expecting.");

                }

            }

            Debug.LogWarning($"How am I supposed to load from {key}?");
            return default (T);

        }

        public static TMP_FontAsset Load_Font(string key) => Try_Load_Object<TMP_FontAsset>(key);
        public static AudioClip Load_Audio(string key) => Try_Load_Object<AudioClip>(key);
        public static Texture2D Load_Image(string key) => Try_Load_Object<Texture2D>(key);
        public static VideoClip Load_Video(string key) => Try_Load_Object<VideoClip>(key);
        public static Sprite Load_DCP(string key) => Try_Load_Object<Sprite>(key);

    }

}
>>>>>>> c8d4f422 (Whole Template)
