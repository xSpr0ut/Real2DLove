<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Graphics{

    [System.Serializable]
    public class Graphic_Panel{
        
        public string panelName;
        public GameObject rootPanel;
        public List<Graphic_Layer> layers { get; private set; } = new List<Graphic_Layer>();

        public bool isClear => layers == null || layers.Count == 0 || layers.All(layer => layer.currentGraphic == null);

        public Graphic_Layer Get_Layer(int layerDepth, bool createIfNonexistent = false){

            for(int i = 0; i < layers.Count; i++){

                if(layers[i].layerDepth == layerDepth){

                    return layers[i];

                }

            }

            if(createIfNonexistent){

                return Create_Layer(layerDepth);

            }

            return null;

        }

        private Graphic_Layer Create_Layer(int layerDepth){

            Graphic_Layer layer = new Graphic_Layer();
            GameObject panel = new GameObject(string.Format(Graphic_Layer.layerNameFormat, layerDepth));
            RectTransform rect = panel.AddComponent<RectTransform>();
            panel.AddComponent<CanvasGroup>();
            panel.transform.SetParent(rootPanel.transform, false);

            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.one;

            layer.panel = panel.transform;
            layer.layerDepth = layerDepth;
            
            int index = layers.FindIndex(l => l.layerDepth > layerDepth);

            if(index == -1){

                layers.Add(layer);

            }else{

                layers.Insert(index, layer);

            }

            for(int i = 0; i < layers.Count; i++){

                layers[i].panel.SetSiblingIndex(layers[i].layerDepth);

            }

            return layer;

        }

        public void Clear(float transitionSpeed = 1, Texture blendTexture = null, bool immediate = false){

            foreach(var layer in layers){

                layer.Clear(transitionSpeed, blendTexture, immediate);

            }

        }

    }

}
=======
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Graphics{

    [System.Serializable]
    public class Graphic_Panel{
        
        public string panelName;
        public GameObject rootPanel;
        public List<Graphic_Layer> layers { get; private set; } = new List<Graphic_Layer>();

        public bool isClear => layers == null || layers.Count == 0 || layers.All(layer => layer.currentGraphic == null);

        public Graphic_Layer Get_Layer(int layerDepth, bool createIfNonexistent = false){

            for(int i = 0; i < layers.Count; i++){

                if(layers[i].layerDepth == layerDepth){

                    return layers[i];

                }

            }

            if(createIfNonexistent){

                return Create_Layer(layerDepth);

            }

            return null;

        }

        private Graphic_Layer Create_Layer(int layerDepth){

            Graphic_Layer layer = new Graphic_Layer();
            GameObject panel = new GameObject(string.Format(Graphic_Layer.layerNameFormat, layerDepth));
            RectTransform rect = panel.AddComponent<RectTransform>();
            panel.AddComponent<CanvasGroup>();
            panel.transform.SetParent(rootPanel.transform, false);

            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.one;

            layer.panel = panel.transform;
            layer.layerDepth = layerDepth;
            
            int index = layers.FindIndex(l => l.layerDepth > layerDepth);

            if(index == -1){

                layers.Add(layer);

            }else{

                layers.Insert(index, layer);

            }

            for(int i = 0; i < layers.Count; i++){

                layers[i].panel.SetSiblingIndex(layers[i].layerDepth);

            }

            return layer;

        }

        public void Clear(float transitionSpeed = 1, Texture blendTexture = null, bool immediate = false){

            foreach(var layer in layers){

                layer.Clear(transitionSpeed, blendTexture, immediate);

            }

        }

    }

}
>>>>>>> c8d4f422 (Whole Template)
