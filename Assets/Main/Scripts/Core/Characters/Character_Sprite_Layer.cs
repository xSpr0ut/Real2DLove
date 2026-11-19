<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Characters{

    public class Character_Sprite_Layer{
        
        private Character_Manager characterManager => Character_Manager.instance;
        private const float defaultTransitionSpeed = 3f;
        private float transitionSpeedMultiplier = 1;
        public int layer{get; private set;} = 0;
        public Image renderer{get; private set;} = null;
        public CanvasGroup rendererCG => renderer.GetComponent<CanvasGroup>();
        private List<CanvasGroup> oldRenderers = new List<CanvasGroup>();
        private Coroutine Co_Transitioning_Layer = null;
        private Coroutine Co_Leveling_Alpha = null;
        private Coroutine Co_Changing_Color = null;
        private Coroutine Co_Flipping = null;
        private bool isFacingLeft = Character.defaultOrientationFacingLeft;        
        public bool isTransitioningLayer => Co_Transitioning_Layer != null;
        public bool isLevelingAlpha => Co_Leveling_Alpha != null;
        public bool isChangingColor => Co_Changing_Color != null;
        public bool isFlipping => Co_Flipping != null;

        public Character_Sprite_Layer (Image defaultRenderer, int layer = 0){

            renderer = defaultRenderer;
            this.layer = layer;

        }

        public void Set_Sprite(Sprite sprite){

            renderer.sprite = sprite;

        }

        public Coroutine Transition_Sprite(Sprite sprite, float speed = 1){

            if(sprite == renderer.sprite){

                return null;

            }

            if(isTransitioningLayer){

                characterManager.StopCoroutine(Co_Transitioning_Layer);

            }

            Co_Transitioning_Layer = characterManager.StartCoroutine(Transitioning_Sprite(sprite, speed));

            return Co_Transitioning_Layer;

        }

        private IEnumerator Transitioning_Sprite(Sprite sprite, float speedMultiplier){

            transitionSpeedMultiplier = speedMultiplier;
            Image newRenderer = Create_Renderer(renderer.transform.parent);
            newRenderer.sprite = sprite;

            yield return Try_Start_Leveling_Alphas();

            Co_Transitioning_Layer = null;

        }

        private Image Create_Renderer(Transform parent){

            Image newRenderer = Object.Instantiate(renderer, parent);
            oldRenderers.Add(rendererCG);

            newRenderer.name = renderer.name;
            renderer = newRenderer;
            renderer.gameObject.SetActive(true);
            rendererCG.alpha = 0;

            return newRenderer;

        }

        private Coroutine Try_Start_Leveling_Alphas(){

            if(isLevelingAlpha){

                characterManager.StopCoroutine(Co_Leveling_Alpha);

            }

            Co_Leveling_Alpha = characterManager.StartCoroutine(Run_Alpha_Leveling());

            return Co_Leveling_Alpha;

        }

        private IEnumerator Run_Alpha_Leveling(){

            while(rendererCG.alpha < 1 || oldRenderers.Any(oldCG => oldCG.alpha > 0)){

                float speed = defaultTransitionSpeed * transitionSpeedMultiplier * Time.deltaTime;
                rendererCG.alpha = Mathf.MoveTowards(rendererCG.alpha, 1, speed);

                for(int i = oldRenderers.Count - 1; i >= 0; i--){

                    CanvasGroup oldCG = oldRenderers[i];
                    oldCG.alpha = Mathf.MoveTowards(oldCG.alpha, 0, speed);

                    if(oldCG.alpha <= 0){

                        oldRenderers.RemoveAt(i);
                        Object.Destroy(oldCG.gameObject);

                    }

                }

                yield return null;

            }

            Co_Leveling_Alpha = null;

        }

        public void Set_Color(Color color){

            renderer.color = color;

            foreach(CanvasGroup oldCG in oldRenderers){

                oldCG.GetComponent<Image>().color = color;

            }

        }

        public Coroutine Transition_Color(Color color, float speed){

            if(isChangingColor){

                characterManager.StopCoroutine(Co_Changing_Color);

            }

            Co_Changing_Color = characterManager.StartCoroutine(Changing_Color(color, speed));

            return Co_Changing_Color;

        }

        public void Stop_Changing_Color(){

            if(!isChangingColor){

                return;

            }

            characterManager.StopCoroutine(Co_Changing_Color);
            Co_Changing_Color = null;

        }

        private IEnumerator Changing_Color(Color color, float speedMultiplier){

            Color oldColor = renderer.color;
            List<Image> oldImages = new List<Image>();

            foreach(var oldCG in oldRenderers){

                oldImages.Add(oldCG.GetComponent<Image>());

            }

            float colorPercent = 0;
            while(colorPercent < 1){

                colorPercent += defaultTransitionSpeed * speedMultiplier * Time.deltaTime;
                renderer.color = Color.Lerp(oldColor, color, colorPercent);

                for(int i = oldImages.Count - 1; i>= 0; i--){

                    Image image = oldImages[i];

                    if(image != null){

                        image.color = renderer.color;

                    }else{

                        oldImages.RemoveAt(i);

                    }

                }

                yield return null;

            }

            Co_Changing_Color = null;

        }

        public Coroutine Flip(float speed = 1, bool immediate = false){

            if(isFacingLeft){

                return Face_Right(speed, immediate);

            }else{

                return Face_Left(speed, immediate);

            }

        }

        public Coroutine Face_Left(float speed = 1, bool immediate = false){

            if(isFlipping){

                characterManager.StopCoroutine(Co_Flipping);

            }

            isFacingLeft = true;
            Co_Flipping = characterManager.StartCoroutine(Face_Direction(isFacingLeft, speed, immediate));

            return Co_Flipping;

        }

        public Coroutine Face_Right(float speed = 1, bool immediate = false){

            if(isFlipping){

                characterManager.StopCoroutine(Co_Flipping);

            }

            isFacingLeft = false;
            Co_Flipping = characterManager.StartCoroutine(Face_Direction(isFacingLeft, speed, immediate));

            return Co_Flipping;

        }

        private IEnumerator Face_Direction(bool faceLeft, float speedMultiplier, bool immediate){

            float xScale = faceLeft ? 1 : -1;
            Vector3 newScale = new Vector3(xScale, 1, 1);

            if(!immediate){

                Image newRenderer = Create_Renderer(renderer.transform.parent);
                newRenderer.transform.localScale = newScale;
                transitionSpeedMultiplier = speedMultiplier;
                Try_Start_Leveling_Alphas();

                while(isLevelingAlpha){

                    yield return null;

                }

            }else{

                renderer.transform.localScale = new Vector3(xScale, 1, 1);

            }

            Co_Flipping = null;

        }

    }

}
=======
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Characters{

    public class Character_Sprite_Layer{
        
        private Character_Manager characterManager => Character_Manager.instance;
        private const float defaultTransitionSpeed = 3f;
        private float transitionSpeedMultiplier = 1;
        public int layer{get; private set;} = 0;
        public Image renderer{get; private set;} = null;
        public CanvasGroup rendererCG => renderer.GetComponent<CanvasGroup>();
        private List<CanvasGroup> oldRenderers = new List<CanvasGroup>();
        private Coroutine Co_Transitioning_Layer = null;
        private Coroutine Co_Leveling_Alpha = null;
        private Coroutine Co_Changing_Color = null;
        private Coroutine Co_Flipping = null;
        private bool isFacingLeft = Character.defaultOrientationFacingLeft;        
        public bool isTransitioningLayer => Co_Transitioning_Layer != null;
        public bool isLevelingAlpha => Co_Leveling_Alpha != null;
        public bool isChangingColor => Co_Changing_Color != null;
        public bool isFlipping => Co_Flipping != null;

        public Character_Sprite_Layer (Image defaultRenderer, int layer = 0){

            renderer = defaultRenderer;
            this.layer = layer;

        }

        public void Set_Sprite(Sprite sprite){

            renderer.sprite = sprite;

        }

        public Coroutine Transition_Sprite(Sprite sprite, float speed = 1){

            if(sprite == renderer.sprite){

                return null;

            }

            if(isTransitioningLayer){

                characterManager.StopCoroutine(Co_Transitioning_Layer);

            }

            Co_Transitioning_Layer = characterManager.StartCoroutine(Transitioning_Sprite(sprite, speed));

            return Co_Transitioning_Layer;

        }

        private IEnumerator Transitioning_Sprite(Sprite sprite, float speedMultiplier){

            transitionSpeedMultiplier = speedMultiplier;
            Image newRenderer = Create_Renderer(renderer.transform.parent);
            newRenderer.sprite = sprite;

            yield return Try_Start_Leveling_Alphas();

            Co_Transitioning_Layer = null;

        }

        private Image Create_Renderer(Transform parent){

            Image newRenderer = Object.Instantiate(renderer, parent);
            oldRenderers.Add(rendererCG);

            newRenderer.name = renderer.name;
            renderer = newRenderer;
            renderer.gameObject.SetActive(true);
            rendererCG.alpha = 0;

            return newRenderer;

        }

        private Coroutine Try_Start_Leveling_Alphas(){

            if(isLevelingAlpha){

                characterManager.StopCoroutine(Co_Leveling_Alpha);

            }

            Co_Leveling_Alpha = characterManager.StartCoroutine(Run_Alpha_Leveling());

            return Co_Leveling_Alpha;

        }

        private IEnumerator Run_Alpha_Leveling(){

            while(rendererCG.alpha < 1 || oldRenderers.Any(oldCG => oldCG.alpha > 0)){

                float speed = defaultTransitionSpeed * transitionSpeedMultiplier * Time.deltaTime;
                rendererCG.alpha = Mathf.MoveTowards(rendererCG.alpha, 1, speed);

                for(int i = oldRenderers.Count - 1; i >= 0; i--){

                    CanvasGroup oldCG = oldRenderers[i];
                    oldCG.alpha = Mathf.MoveTowards(oldCG.alpha, 0, speed);

                    if(oldCG.alpha <= 0){

                        oldRenderers.RemoveAt(i);
                        Object.Destroy(oldCG.gameObject);

                    }

                }

                yield return null;

            }

            Co_Leveling_Alpha = null;

        }

        public void Set_Color(Color color){

            renderer.color = color;

            foreach(CanvasGroup oldCG in oldRenderers){

                oldCG.GetComponent<Image>().color = color;

            }

        }

        public Coroutine Transition_Color(Color color, float speed){

            if(isChangingColor){

                characterManager.StopCoroutine(Co_Changing_Color);

            }

            Co_Changing_Color = characterManager.StartCoroutine(Changing_Color(color, speed));

            return Co_Changing_Color;

        }

        public void Stop_Changing_Color(){

            if(!isChangingColor){

                return;

            }

            characterManager.StopCoroutine(Co_Changing_Color);
            Co_Changing_Color = null;

        }

        private IEnumerator Changing_Color(Color color, float speedMultiplier){

            Color oldColor = renderer.color;
            List<Image> oldImages = new List<Image>();

            foreach(var oldCG in oldRenderers){

                oldImages.Add(oldCG.GetComponent<Image>());

            }

            float colorPercent = 0;
            while(colorPercent < 1){

                colorPercent += defaultTransitionSpeed * speedMultiplier * Time.deltaTime;
                renderer.color = Color.Lerp(oldColor, color, colorPercent);

                for(int i = oldImages.Count - 1; i>= 0; i--){

                    Image image = oldImages[i];

                    if(image != null){

                        image.color = renderer.color;

                    }else{

                        oldImages.RemoveAt(i);

                    }

                }

                yield return null;

            }

            Co_Changing_Color = null;

        }

        public Coroutine Flip(float speed = 1, bool immediate = false){

            if(isFacingLeft){

                return Face_Right(speed, immediate);

            }else{

                return Face_Left(speed, immediate);

            }

        }

        public Coroutine Face_Left(float speed = 1, bool immediate = false){

            if(isFlipping){

                characterManager.StopCoroutine(Co_Flipping);

            }

            isFacingLeft = true;
            Co_Flipping = characterManager.StartCoroutine(Face_Direction(isFacingLeft, speed, immediate));

            return Co_Flipping;

        }

        public Coroutine Face_Right(float speed = 1, bool immediate = false){

            if(isFlipping){

                characterManager.StopCoroutine(Co_Flipping);

            }

            isFacingLeft = false;
            Co_Flipping = characterManager.StartCoroutine(Face_Direction(isFacingLeft, speed, immediate));

            return Co_Flipping;

        }

        private IEnumerator Face_Direction(bool faceLeft, float speedMultiplier, bool immediate){

            float xScale = faceLeft ? 1 : -1;
            Vector3 newScale = new Vector3(xScale, 1, 1);

            if(!immediate){

                Image newRenderer = Create_Renderer(renderer.transform.parent);
                newRenderer.transform.localScale = newScale;
                transitionSpeedMultiplier = speedMultiplier;
                Try_Start_Leveling_Alphas();

                while(isLevelingAlpha){

                    yield return null;

                }

            }else{

                renderer.transform.localScale = new Vector3(xScale, 1, 1);

            }

            Co_Flipping = null;

        }

    }

}
>>>>>>> c8d4f422 (Whole Template)
