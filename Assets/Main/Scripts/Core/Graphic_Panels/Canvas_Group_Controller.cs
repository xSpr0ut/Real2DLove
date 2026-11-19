<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

namespace Dialog{

    public class Canvas_Group_Controller{

        private const float defaultFadeSpeed = 3f;
        private MonoBehaviour owner;
        private CanvasGroup rootCG;

        private Coroutine Co_Showing = null;
        private Coroutine Co_Hiding = null;
        public bool isShowing => Co_Showing != null;
        public bool isHiding => Co_Hiding != null;
        public bool isFading => isShowing || isHiding;
        public bool isVisible => Co_Showing != null || rootCG.alpha > 0;
        public float alpha { get { return rootCG.alpha; } set { rootCG.alpha = value; } }

        public Canvas_Group_Controller(MonoBehaviour owner, CanvasGroup rootCG){

            this.owner = owner;
            this.rootCG = rootCG;

        }

        public Coroutine Show(float speed = 1f, bool immediate = false){

            if(isShowing){

                return Co_Showing;

            }else if(isHiding){

                owner.StopCoroutine(Co_Hiding);
                Co_Hiding = null;

            }

            Co_Showing = owner.StartCoroutine(Fading(1, speed, immediate));
            return Co_Showing;

        }

        public Coroutine Hide(float speed = 1f, bool immediate = false){

            if(isHiding){

                return Co_Hiding;

            }else if(isShowing){

                owner.StopCoroutine(Co_Showing);
                Co_Showing = null;

            }

            Co_Hiding = owner.StartCoroutine(Fading(0, speed, immediate));
            return Co_Hiding;

        }

        public IEnumerator Fading(float alpha, float speed, bool immediate){

            CanvasGroup cg = rootCG;

            if(immediate){

                cg.alpha = alpha;

            }

            while(cg.alpha != alpha){

                cg.alpha = Mathf.MoveTowards(cg.alpha, alpha, Time.deltaTime * defaultFadeSpeed * speed);
                yield return null;

            }

            Co_Showing = null;
            Co_Hiding = null;

        }

        public void Set_Interactable_State(bool active){

            rootCG.interactable = active;
            rootCG.blocksRaycasts = active;

        }

    }

}
=======
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

namespace Dialog{

    public class Canvas_Group_Controller{

        private const float defaultFadeSpeed = 3f;
        private MonoBehaviour owner;
        private CanvasGroup rootCG;

        private Coroutine Co_Showing = null;
        private Coroutine Co_Hiding = null;
        public bool isShowing => Co_Showing != null;
        public bool isHiding => Co_Hiding != null;
        public bool isFading => isShowing || isHiding;
        public bool isVisible => Co_Showing != null || rootCG.alpha > 0;
        public float alpha { get { return rootCG.alpha; } set { rootCG.alpha = value; } }

        public Canvas_Group_Controller(MonoBehaviour owner, CanvasGroup rootCG){

            this.owner = owner;
            this.rootCG = rootCG;

        }

        public Coroutine Show(float speed = 1f, bool immediate = false){

            if(isShowing){

                return Co_Showing;

            }else if(isHiding){

                owner.StopCoroutine(Co_Hiding);
                Co_Hiding = null;

            }

            Co_Showing = owner.StartCoroutine(Fading(1, speed, immediate));
            return Co_Showing;

        }

        public Coroutine Hide(float speed = 1f, bool immediate = false){

            if(isHiding){

                return Co_Hiding;

            }else if(isShowing){

                owner.StopCoroutine(Co_Showing);
                Co_Showing = null;

            }

            Co_Hiding = owner.StartCoroutine(Fading(0, speed, immediate));
            return Co_Hiding;

        }

        public IEnumerator Fading(float alpha, float speed, bool immediate){

            CanvasGroup cg = rootCG;

            if(immediate){

                cg.alpha = alpha;

            }

            while(cg.alpha != alpha){

                cg.alpha = Mathf.MoveTowards(cg.alpha, alpha, Time.deltaTime * defaultFadeSpeed * speed);
                yield return null;

            }

            Co_Showing = null;
            Co_Hiding = null;

        }

        public void Set_Interactable_State(bool active){

            rootCG.interactable = active;
            rootCG.blocksRaycasts = active;

        }

    }

}
>>>>>>> c8d4f422 (Whole Template)
