<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;
using UnityEngine.Video;

namespace Graphics{

    public class Graphic_Object{
        
        private const string nameFormat = "Graphic: [{0}]";
        private const string defaultUIMaterial = "Default UI Material";
        private const string materialPath = "Materials/layerTransitionMaterial";
        private const string materialFieldColor = "_Color";
        private const string materialFieldMainTexture = "_MainTex";
        private const string materialFieldBlendTexture = "_BlendTex";
        private const string materialFieldBlend = "_Blend";
        private const string materialFieldAlpha = "_Alpha";
        public RawImage renderer;

        private Graphic_Layer layer;

        public bool isVideo { get { return video != null; } }
        public bool useAudio => audio != null ? !audio.mute : false;
        public VideoPlayer video = null;
        public AudioSource audio = null;
        public string graphicPath = "";
        public string graphicName { get; private set; }

        private Coroutine Co_Fading_In = null;
        private Coroutine Co_Fading_Out = null;

        public Graphic_Object(Graphic_Layer layer, string graphicPath, Texture texture, bool immediate){

            this.graphicPath = graphicPath;
            this.layer = layer;

            GameObject obj = new GameObject();
            obj.transform.SetParent(layer.panel);
            renderer = obj.AddComponent<RawImage>();

            graphicName = texture.name;

            Initialize_Graphic(immediate);

            renderer.name = string.Format(nameFormat, graphicName);
            renderer.material.SetTexture(materialFieldMainTexture, texture);

        }

        public Graphic_Object(Graphic_Layer layer, string graphicPath, VideoClip clip, bool useAudio, bool immediate){

            this.graphicPath = graphicPath;
            this.layer = layer;

            GameObject obj = new GameObject();
            obj.transform.SetParent(layer.panel);
            renderer = obj.AddComponent<RawImage>();

            graphicName = clip.name;
            renderer.name = string.Format(nameFormat, graphicName);

            Initialize_Graphic(immediate);

            RenderTexture texture = new RenderTexture(Mathf.RoundToInt(clip.width), Mathf.RoundToInt(clip.height), 0);
            renderer.material.SetTexture(materialFieldMainTexture, texture);

            video = renderer.AddComponent<VideoPlayer>();
            video.playOnAwake = true;
            video.source = VideoSource.VideoClip;
            video.clip = clip;
            video.renderMode = VideoRenderMode.RenderTexture;
            video.targetTexture = texture;
            video.isLooping = true;

            video.audioOutputMode = VideoAudioOutputMode.AudioSource;
            audio = video.AddComponent<AudioSource>();
            audio.volume = immediate ? 1 : 0;

            if(!useAudio){

                audio.mute = true;

            }

            video.SetTargetAudioSource(0, audio);
            video.frame = 0;
            video.Prepare();
            video.Play();

            video.enabled = false;
            video.enabled = true;

        }

        private void Initialize_Graphic(bool immediate){

            renderer.transform.localPosition = Vector3.zero;
            renderer.transform.localScale = Vector3.one;

            RectTransform rect = renderer.GetComponent<RectTransform>();
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.one;

            renderer.material = Get_Transition_Material();
            
            float startingOpacity = immediate ? 1f : 0f;
            renderer.material.SetFloat(materialFieldBlend, startingOpacity);
            renderer.material.SetFloat(materialFieldAlpha, startingOpacity);

        }

        private Material Get_Transition_Material(){

            Material material = Resources.Load<Material>(materialPath);

            if(material != null){

                return new Material(material);

            }

            return null;

        }

        Graphic_Panel_Manager panelManager => Graphic_Panel_Manager. instance;

        public Coroutine Fade_In(float speed = 1f, Texture blend = null){

            if(Co_Fading_Out != null){

                panelManager.StopCoroutine(Co_Fading_Out);

            }

            if(Co_Fading_In != null){

                return Co_Fading_In;

            }

            Co_Fading_In = panelManager.StartCoroutine(Fading(1f, speed, blend));
            return Co_Fading_In;

        }

        public Coroutine Fade_Out(float speed = 1f, Texture blend = null){

            if(Co_Fading_In != null){

                panelManager.StopCoroutine(Co_Fading_In);

            }

            if(Co_Fading_Out != null){

                return Co_Fading_Out;

            }

            Co_Fading_Out = panelManager.StartCoroutine(Fading(0f, speed, blend));
            return Co_Fading_Out;
            
        }

        public IEnumerator Fading(float target, float speed, Texture blend){

            bool isBlending = blend != null;
            bool fadingIn = target > 0;

            if(renderer.material.name == defaultUIMaterial){

                Texture texture = renderer.material.GetTexture(materialFieldMainTexture);
                renderer.material = Get_Transition_Material();
                renderer.material.SetTexture(materialFieldMainTexture, texture);

            }

            renderer.material.SetTexture(materialFieldBlendTexture, blend);
            renderer.material.SetFloat(materialFieldAlpha, isBlending ? 1 : fadingIn ? 0 : 1);
            renderer.material.SetFloat(materialFieldBlend, isBlending ? fadingIn ? 0 : 1 : 1);

            string opacityParameter = isBlending ? materialFieldBlend : materialFieldAlpha;

            while(renderer.material.GetFloat(opacityParameter) != target){

                float opacity = Mathf.MoveTowards(renderer.material.GetFloat(opacityParameter), target, speed * Time.deltaTime);
                renderer.material.SetFloat(opacityParameter, opacity);
                
                if(isVideo){

                    audio.volume = opacity;

                }
                
                yield return null;

            }

            Co_Fading_In = null;
            Co_Fading_Out = null;

            if(target == 0){

                Destroy();

            }else{

                Destroy_Background_Graphics_On_Layer();
                renderer.texture = renderer.material.GetTexture(materialFieldMainTexture);
                renderer.material = null;

            }

        }

        public void Destroy(){

            if(layer.currentGraphic != null && layer.currentGraphic.renderer == renderer){

                layer.currentGraphic = null;

            }

            if(layer.oldGraphics.Contains(this)){

                layer.oldGraphics.Remove(this);

            }

            Object.Destroy(renderer.gameObject);

        }

        private void Destroy_Background_Graphics_On_Layer(){

            layer.Destroy_Old_Graphics();

        }

    }

}
=======
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;
using UnityEngine.Video;

namespace Graphics{

    public class Graphic_Object{
        
        private const string nameFormat = "Graphic: [{0}]";
        private const string defaultUIMaterial = "Default UI Material";
        private const string materialPath = "Materials/layerTransitionMaterial";
        private const string materialFieldColor = "_Color";
        private const string materialFieldMainTexture = "_MainTex";
        private const string materialFieldBlendTexture = "_BlendTex";
        private const string materialFieldBlend = "_Blend";
        private const string materialFieldAlpha = "_Alpha";
        public RawImage renderer;

        private Graphic_Layer layer;

        public bool isVideo { get { return video != null; } }
        public bool useAudio => audio != null ? !audio.mute : false;
        public VideoPlayer video = null;
        public AudioSource audio = null;
        public string graphicPath = "";
        public string graphicName { get; private set; }

        private Coroutine Co_Fading_In = null;
        private Coroutine Co_Fading_Out = null;

        public Graphic_Object(Graphic_Layer layer, string graphicPath, Texture texture, bool immediate){

            this.graphicPath = graphicPath;
            this.layer = layer;

            GameObject obj = new GameObject();
            obj.transform.SetParent(layer.panel);
            renderer = obj.AddComponent<RawImage>();

            graphicName = texture.name;

            Initialize_Graphic(immediate);

            renderer.name = string.Format(nameFormat, graphicName);
            renderer.material.SetTexture(materialFieldMainTexture, texture);

        }

        public Graphic_Object(Graphic_Layer layer, string graphicPath, VideoClip clip, bool useAudio, bool immediate){

            this.graphicPath = graphicPath;
            this.layer = layer;

            GameObject obj = new GameObject();
            obj.transform.SetParent(layer.panel);
            renderer = obj.AddComponent<RawImage>();

            graphicName = clip.name;
            renderer.name = string.Format(nameFormat, graphicName);

            Initialize_Graphic(immediate);

            RenderTexture texture = new RenderTexture(Mathf.RoundToInt(clip.width), Mathf.RoundToInt(clip.height), 0);
            renderer.material.SetTexture(materialFieldMainTexture, texture);

            video = renderer.AddComponent<VideoPlayer>();
            video.playOnAwake = true;
            video.source = VideoSource.VideoClip;
            video.clip = clip;
            video.renderMode = VideoRenderMode.RenderTexture;
            video.targetTexture = texture;
            video.isLooping = true;

            video.audioOutputMode = VideoAudioOutputMode.AudioSource;
            audio = video.AddComponent<AudioSource>();
            audio.volume = immediate ? 1 : 0;

            if(!useAudio){

                audio.mute = true;

            }

            video.SetTargetAudioSource(0, audio);
            video.frame = 0;
            video.Prepare();
            video.Play();

            video.enabled = false;
            video.enabled = true;

        }

        private void Initialize_Graphic(bool immediate){

            renderer.transform.localPosition = Vector3.zero;
            renderer.transform.localScale = Vector3.one;

            RectTransform rect = renderer.GetComponent<RectTransform>();
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.one;

            renderer.material = Get_Transition_Material();
            
            float startingOpacity = immediate ? 1f : 0f;
            renderer.material.SetFloat(materialFieldBlend, startingOpacity);
            renderer.material.SetFloat(materialFieldAlpha, startingOpacity);

        }

        private Material Get_Transition_Material(){

            Material material = Resources.Load<Material>(materialPath);

            if(material != null){

                return new Material(material);

            }

            return null;

        }

        Graphic_Panel_Manager panelManager => Graphic_Panel_Manager. instance;

        public Coroutine Fade_In(float speed = 1f, Texture blend = null){

            if(Co_Fading_Out != null){

                panelManager.StopCoroutine(Co_Fading_Out);

            }

            if(Co_Fading_In != null){

                return Co_Fading_In;

            }

            Co_Fading_In = panelManager.StartCoroutine(Fading(1f, speed, blend));
            return Co_Fading_In;

        }

        public Coroutine Fade_Out(float speed = 1f, Texture blend = null){

            if(Co_Fading_In != null){

                panelManager.StopCoroutine(Co_Fading_In);

            }

            if(Co_Fading_Out != null){

                return Co_Fading_Out;

            }

            Co_Fading_Out = panelManager.StartCoroutine(Fading(0f, speed, blend));
            return Co_Fading_Out;
            
        }

        public IEnumerator Fading(float target, float speed, Texture blend){

            bool isBlending = blend != null;
            bool fadingIn = target > 0;

            if(renderer.material.name == defaultUIMaterial){

                Texture texture = renderer.material.GetTexture(materialFieldMainTexture);
                renderer.material = Get_Transition_Material();
                renderer.material.SetTexture(materialFieldMainTexture, texture);

            }

            renderer.material.SetTexture(materialFieldBlendTexture, blend);
            renderer.material.SetFloat(materialFieldAlpha, isBlending ? 1 : fadingIn ? 0 : 1);
            renderer.material.SetFloat(materialFieldBlend, isBlending ? fadingIn ? 0 : 1 : 1);

            string opacityParameter = isBlending ? materialFieldBlend : materialFieldAlpha;

            while(renderer.material.GetFloat(opacityParameter) != target){

                float opacity = Mathf.MoveTowards(renderer.material.GetFloat(opacityParameter), target, speed * Time.deltaTime);
                renderer.material.SetFloat(opacityParameter, opacity);
                
                if(isVideo){

                    audio.volume = opacity;

                }
                
                yield return null;

            }

            Co_Fading_In = null;
            Co_Fading_Out = null;

            if(target == 0){

                Destroy();

            }else{

                Destroy_Background_Graphics_On_Layer();
                renderer.texture = renderer.material.GetTexture(materialFieldMainTexture);
                renderer.material = null;

            }

        }

        public void Destroy(){

            if(layer.currentGraphic != null && layer.currentGraphic.renderer == renderer){

                layer.currentGraphic = null;

            }

            if(layer.oldGraphics.Contains(this)){

                layer.oldGraphics.Remove(this);

            }

            Object.Destroy(renderer.gameObject);

        }

        private void Destroy_Background_Graphics_On_Layer(){

            layer.Destroy_Old_Graphics();

        }

    }

}
>>>>>>> c8d4f422 (Whole Template)
