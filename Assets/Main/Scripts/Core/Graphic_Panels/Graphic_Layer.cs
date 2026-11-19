<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

namespace Graphics{

    public class Graphic_Layer{
        
        public const  string layerNameFormat = "Layer: {0}";
        public int layerDepth = 0;
        public Transform panel;
        public Graphic_Object currentGraphic = null;
        public List<Graphic_Object> oldGraphics = new List<Graphic_Object>();

        public Coroutine Set_Texture(string filePath, float transitionSpeed = 1f, Texture blendingTexture = null, bool immediate = false){

            Texture texture = Resources.Load<Texture2D>(filePath);

            if(texture == null){

                Debug.LogError($"{texture} could not be loaded from {filePath}.");
                return null;

            }

            return Set_Texture(texture, transitionSpeed, blendingTexture, filePath, immediate);

        }

        public Coroutine Set_Texture(Texture texture, float transitionSpeed = 1f, Texture blendingTexture = null, string filePath = "", bool immediate = false){

            return Create_Graphic(texture, transitionSpeed, filePath, blendingTexture: blendingTexture, immediate: immediate);

        }

        public Coroutine Set_Video(string filePath, float transitionSpeed = 1f, bool useAudio = true, Texture blendingTexture = null, bool immediate = false){

            VideoClip clip = Resources.Load<VideoClip>(filePath);

            if(clip == null){

                Debug.LogError($"{clip} could not be loaded from {filePath}.");
                return null;

            }

            return Set_Video(clip, transitionSpeed, useAudio, blendingTexture, filePath);

        }

        public Coroutine Set_Video(VideoClip video, float transitionSpeed = 1f, bool useAudio = true, Texture blendingTexture = null, string filePath = "", bool immediate = false){

            return Create_Graphic(video, transitionSpeed, filePath, useAudio, blendingTexture, immediate);

        }

        private Coroutine Create_Graphic<T>(T graphicData, float transitionSpeed, string filePath, bool useAudioForVideo = true, Texture blendingTexture = null, bool immediate = false){

            Graphic_Object newGraphic = null;

            if(graphicData is Texture){

                newGraphic = new Graphic_Object(this, filePath, graphicData as Texture, immediate);

            }else if(graphicData is VideoClip){

                newGraphic = new Graphic_Object(this, filePath, graphicData as VideoClip, useAudioForVideo, immediate);

            }

            if(currentGraphic != null && !oldGraphics.Contains(currentGraphic)){

                oldGraphics.Add(currentGraphic);

            }

            currentGraphic = newGraphic;

            if(!immediate){

                return currentGraphic.Fade_In(transitionSpeed, blendingTexture);

            }

            //If immediate
            Destroy_Old_Graphics();
            return null;

        }

        public void Destroy_Old_Graphics(){

            foreach(var g in oldGraphics){

                Object.Destroy(g.renderer.gameObject);

            }

            oldGraphics.Clear();

        }

        public void Clear(float transitionSpeed = 1, Texture blendTexture = null, bool immediate = false){

            if(currentGraphic != null){
            
                if(!immediate){

                    currentGraphic.Fade_Out(transitionSpeed, blendTexture);

                }else{

                    currentGraphic.Destroy();

                }

            }

            foreach(var g in oldGraphics){

                if(!immediate){

                    g.Fade_Out(transitionSpeed, blendTexture);

                }else{

                    currentGraphic.Destroy();

                }

            }

        }

    }

}
=======
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

namespace Graphics{

    public class Graphic_Layer{
        
        public const  string layerNameFormat = "Layer: {0}";
        public int layerDepth = 0;
        public Transform panel;
        public Graphic_Object currentGraphic = null;
        public List<Graphic_Object> oldGraphics = new List<Graphic_Object>();

        public Coroutine Set_Texture(string filePath, float transitionSpeed = 1f, Texture blendingTexture = null, bool immediate = false){

            Texture texture = Resources.Load<Texture2D>(filePath);

            if(texture == null){

                Debug.LogError($"{texture} could not be loaded from {filePath}.");
                return null;

            }

            return Set_Texture(texture, transitionSpeed, blendingTexture, filePath, immediate);

        }

        public Coroutine Set_Texture(Texture texture, float transitionSpeed = 1f, Texture blendingTexture = null, string filePath = "", bool immediate = false){

            return Create_Graphic(texture, transitionSpeed, filePath, blendingTexture: blendingTexture, immediate: immediate);

        }

        public Coroutine Set_Video(string filePath, float transitionSpeed = 1f, bool useAudio = true, Texture blendingTexture = null, bool immediate = false){

            VideoClip clip = Resources.Load<VideoClip>(filePath);

            if(clip == null){

                Debug.LogError($"{clip} could not be loaded from {filePath}.");
                return null;

            }

            return Set_Video(clip, transitionSpeed, useAudio, blendingTexture, filePath);

        }

        public Coroutine Set_Video(VideoClip video, float transitionSpeed = 1f, bool useAudio = true, Texture blendingTexture = null, string filePath = "", bool immediate = false){

            return Create_Graphic(video, transitionSpeed, filePath, useAudio, blendingTexture, immediate);

        }

        private Coroutine Create_Graphic<T>(T graphicData, float transitionSpeed, string filePath, bool useAudioForVideo = true, Texture blendingTexture = null, bool immediate = false){

            Graphic_Object newGraphic = null;

            if(graphicData is Texture){

                newGraphic = new Graphic_Object(this, filePath, graphicData as Texture, immediate);

            }else if(graphicData is VideoClip){

                newGraphic = new Graphic_Object(this, filePath, graphicData as VideoClip, useAudioForVideo, immediate);

            }

            if(currentGraphic != null && !oldGraphics.Contains(currentGraphic)){

                oldGraphics.Add(currentGraphic);

            }

            currentGraphic = newGraphic;

            if(!immediate){

                return currentGraphic.Fade_In(transitionSpeed, blendingTexture);

            }

            //If immediate
            Destroy_Old_Graphics();
            return null;

        }

        public void Destroy_Old_Graphics(){

            foreach(var g in oldGraphics){

                Object.Destroy(g.renderer.gameObject);

            }

            oldGraphics.Clear();

        }

        public void Clear(float transitionSpeed = 1, Texture blendTexture = null, bool immediate = false){

            if(currentGraphic != null){
            
                if(!immediate){

                    currentGraphic.Fade_Out(transitionSpeed, blendTexture);

                }else{

                    currentGraphic.Destroy();

                }

            }

            foreach(var g in oldGraphics){

                if(!immediate){

                    g.Fade_Out(transitionSpeed, blendTexture);

                }else{

                    currentGraphic.Destroy();

                }

            }

        }

    }

}
>>>>>>> c8d4f422 (Whole Template)
