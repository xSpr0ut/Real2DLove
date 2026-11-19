<<<<<<< HEAD
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Characters{

    public class Sprite_Character : Character{

        private const string spriteRenderParentName = "Renderer";
        //For sprite sheets, call the default sheet "Default" so if 
        //the name can't be found, it will pick the default image
        private const string spriteSheetDefaultName = "Default";
        private const char spriteSheetSpriteDelimiter = '_';
        private CanvasGroup rootCG => root.GetComponent<CanvasGroup>();
        public List<Character_Sprite_Layer> layers = new List<Character_Sprite_Layer>();
        private string artAssetDirectory = "";
        public override bool isVisible{

            get{return isRevealing || rootCG.alpha == 1;}
            set{rootCG.alpha = value ? 1 : 0;}

        }

        public Sprite_Character(string name, Character_Configuration_Data configurationData, GameObject prefab, string rootAssetFolder) : base(name, configurationData, prefab){

            //If true, alpha = 1, else alpha = 0
            rootCG.alpha = enableOnStart ? 1 : 0;
            artAssetDirectory = rootAssetFolder + "/Images";
            Get_Layers();
            //Debug.Log($"Created Sprite Character: '{name}'");

        }

        private void Get_Layers(){

            Transform rendererRoot = animator.transform.Find(spriteRenderParentName);

            if(rendererRoot == null){

                return;

            }

            for(int i = 0; i < rendererRoot.transform.childCount; i++){

                Transform child = rendererRoot.transform.GetChild(i);
                Image rendererImage = child.GetComponentInChildren<Image>();

                if(rendererImage != null){

                    Character_Sprite_Layer layer = new Character_Sprite_Layer(rendererImage, i);
                    layers.Add(layer);
                    child.name = $"Layer_{i}";

                }

            }

        }

        public void Set_Sprite(Sprite sprite, int layer = 0){

            layers[layer].Set_Sprite(sprite);

        }

        public Sprite Get_Sprite(string spriteName){

            if(configurationData.sprites.Count > 0){

                if(configurationData.sprites.TryGetValue(spriteName, out Sprite sprite)){

                    return sprite;

                }

            }

            if(configurationData.characterType == Character_Type.SpriteSheet){

                string[] data = spriteName.Split(spriteSheetSpriteDelimiter);
                Sprite[] spriteArray = new Sprite[0];
                string textureName = "";

                if(data.Length == 2){

                    textureName = data[0];
                    spriteName = data[1];

                }else{

                    textureName = spriteSheetDefaultName;

                }

                spriteArray = Resources.LoadAll<Sprite>($"{artAssetDirectory}/{textureName}");

                if(spriteArray.Length == 0){

                    Debug.LogWarning($"{name} has no art asset called '{textureName}.'");

                }

                return Array.Find(spriteArray, sprite => sprite.name == spriteName);

            }else{

                return Resources.Load<Sprite>($"{artAssetDirectory}/{spriteName}");

            }

        }

        public Coroutine Transition_Sprite(Sprite sprite, int layer = 0, float speed = 1){

            Character_Sprite_Layer spriteLayer = layers[layer];

            return spriteLayer.Transition_Sprite(sprite, speed);

        }

        public override IEnumerator Showing_Or_Hiding(bool show, float speed){
            
            float targetAlpha = show ? 1f : 0;
            CanvasGroup self = rootCG;

            while(self.alpha != targetAlpha){

                self.alpha = Mathf.MoveTowards(self.alpha, targetAlpha, speed * Time.deltaTime);
                yield return null;

            }

            Co_Revealing = null;
            Co_Hiding = null;

        }

        public override void Set_Color(Color color, float alpha = 1){

            base.Set_Color(color);
            color = displayColor;
            color = color.Set_Alpha(alpha);

            foreach(Character_Sprite_Layer layer in layers){

                layer.Stop_Changing_Color();
                layer.Set_Color(color);

            }

        }

        public override IEnumerator Changing_Color(Color color, float speed){
            
            foreach(Character_Sprite_Layer layer in layers){

                layer.Transition_Color(color, speed);

            }

            yield return null;

            while(layers.Any(l => l.isChangingColor)){

                yield return null;

            }

            Co_Changing_Color = null;

        }

        public override IEnumerator Highlighting(float speedMultiplier, bool immediate = false){
            
            Color targetColor = displayColor;

            foreach(Character_Sprite_Layer layer in layers){

                if(immediate){

                    layer.Set_Color(targetColor);

                }else{

                    layer.Transition_Color(targetColor, speedMultiplier);

                }

            }

            yield return null;

            while(layers.Any(l => l.isChangingColor)){

                yield return null;

            }

            Co_Highlighting = null;

        }

        public override IEnumerator Face_Direction(bool faceLeft, float speedMultiplier, bool immediate){

            foreach(Character_Sprite_Layer layer in layers){

                if(faceLeft){

                    layer.Face_Left(speedMultiplier, immediate);

                }else{

                    layer.Face_Right(speedMultiplier, immediate);

                }

            }

            yield return null;

            while(layers.Any(l => l.isFlipping)){

                yield return null;

            }

            Co_Flipping = null;

        }

        public override void On_Recieve_Casting_Expression(int layer, string expression){
            
            Sprite sprite = Get_Sprite(expression);

            if(sprite == null){

                Debug.LogWarning($"{expression} could not be found for {name}");
                return;

            }       

            Set_Sprite(sprite, layer);     

        }
       
    }

}
=======
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Characters{

    public class Sprite_Character : Character{

        private const string spriteRenderParentName = "Renderer";
        //For sprite sheets, call the default sheet "Default" so if 
        //the name can't be found, it will pick the default image
        private const string spriteSheetDefaultName = "Default";
        private const char spriteSheetSpriteDelimiter = '_';
        private CanvasGroup rootCG => root.GetComponent<CanvasGroup>();
        public List<Character_Sprite_Layer> layers = new List<Character_Sprite_Layer>();
        private string artAssetDirectory = "";
        public override bool isVisible{

            get{return isRevealing || rootCG.alpha == 1;}
            set{rootCG.alpha = value ? 1 : 0;}

        }

        public Sprite_Character(string name, Character_Configuration_Data configurationData, GameObject prefab, string rootAssetFolder) : base(name, configurationData, prefab){

            //If true, alpha = 1, else alpha = 0
            rootCG.alpha = enableOnStart ? 1 : 0;
            artAssetDirectory = rootAssetFolder + "/Images";
            Get_Layers();
            //Debug.Log($"Created Sprite Character: '{name}'");

        }

        private void Get_Layers(){

            Transform rendererRoot = animator.transform.Find(spriteRenderParentName);

            if(rendererRoot == null){

                return;

            }

            for(int i = 0; i < rendererRoot.transform.childCount; i++){

                Transform child = rendererRoot.transform.GetChild(i);
                Image rendererImage = child.GetComponentInChildren<Image>();

                if(rendererImage != null){

                    Character_Sprite_Layer layer = new Character_Sprite_Layer(rendererImage, i);
                    layers.Add(layer);
                    child.name = $"Layer: {i}";

                }

            }

        }

        public void Set_Sprite(Sprite sprite, int layer = 0){

            layers[layer].Set_Sprite(sprite);

        }

        public Sprite Get_Sprite(string spriteName){

            if(configurationData.sprites.Count > 0){

                if(configurationData.sprites.TryGetValue(spriteName, out Sprite sprite)){

                    return sprite;

                }

            }

            if(configurationData.characterType == Character_Type.SpriteSheet){

                string[] data = spriteName.Split(spriteSheetSpriteDelimiter);
                Sprite[] spriteArray = new Sprite[0];
                string textureName = "";

                if(data.Length == 2){

                    textureName = data[0];
                    spriteName = data[1];

                }else{

                    textureName = spriteSheetDefaultName;

                }

                spriteArray = Resources.LoadAll<Sprite>($"{artAssetDirectory}/{textureName}");

                if(spriteArray.Length == 0){

                    Debug.LogWarning($"{name} has no art asset called '{textureName}.'");

                }

                return Array.Find(spriteArray, sprite => sprite.name == spriteName);

            }else{

                return Resources.Load<Sprite>($"{artAssetDirectory}/{spriteName}");

            }

        }

        public Coroutine Transition_Sprite(Sprite sprite, int layer = 0, float speed = 1){

            Character_Sprite_Layer spriteLayer = layers[layer];

            return spriteLayer.Transition_Sprite(sprite, speed);

        }

        public override IEnumerator Showing_Or_Hiding(bool show, float speed){
            
            float targetAlpha = show ? 1f : 0;
            CanvasGroup self = rootCG;

            while(self.alpha != targetAlpha){

                self.alpha = Mathf.MoveTowards(self.alpha, targetAlpha, speed * Time.deltaTime);
                yield return null;

            }

            Co_Revealing = null;
            Co_Hiding = null;

        }

        public override void Set_Color(Color color, float alpha = 1){

            base.Set_Color(color);
            color = displayColor;
            color = color.Set_Alpha(alpha);

            foreach(Character_Sprite_Layer layer in layers){

                layer.Stop_Changing_Color();
                layer.Set_Color(color);

            }

        }

        public override IEnumerator Changing_Color(Color color, float speed){
            
            foreach(Character_Sprite_Layer layer in layers){

                layer.Transition_Color(color, speed);

            }

            yield return null;

            while(layers.Any(l => l.isChangingColor)){

                yield return null;

            }

            Co_Changing_Color = null;

        }

        public override IEnumerator Highlighting(float speedMultiplier, bool immediate = false){
            
            Color targetColor = displayColor;

            foreach(Character_Sprite_Layer layer in layers){

                if(immediate){

                    layer.Set_Color(targetColor);

                }else{

                    layer.Transition_Color(targetColor, speedMultiplier);

                }

            }

            yield return null;

            while(layers.Any(l => l.isChangingColor)){

                yield return null;

            }

            Co_Highlighting = null;

        }

        public override IEnumerator Face_Direction(bool faceLeft, float speedMultiplier, bool immediate){

            foreach(Character_Sprite_Layer layer in layers){

                if(faceLeft){

                    layer.Face_Left(speedMultiplier, immediate);

                }else{

                    layer.Face_Right(speedMultiplier, immediate);

                }

            }

            yield return null;

            while(layers.Any(l => l.isFlipping)){

                yield return null;

            }

            Co_Flipping = null;

        }

        public override void On_Recieve_Casting_Expression(int layer, string expression){
            
            Sprite sprite = Get_Sprite(expression);

            if(sprite == null){

                Debug.LogWarning($"{expression} could not be found for {name}");
                return;

            }       

            Set_Sprite(sprite, layer);     

        }
       
    }

}
>>>>>>> c8d4f422 (Whole Template)
