<<<<<<< HEAD
using UnityEngine;
using UnityEngine.EventSystems;

namespace Button_Actions{

    public class Button_Behaviors : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler{

        public Animator animator;
        private static Button_Behaviors selectedButton;
        public void OnPointerEnter(PointerEventData eventData){
                
                if(selectedButton != null && selectedButton != this){

                    selectedButton.OnPointerExit(null);

                }

                animator.Play("Enter");
                selectedButton = this;

            }

            public void OnPointerExit(PointerEventData eventData){

                animator.Play("Exit");

            }

    }

}
=======
using UnityEngine;
using UnityEngine.EventSystems;

namespace Button_Actions{

    public class Button_Behaviors : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler{

        public Animator animator;
        private static Button_Behaviors selectedButton;
        public void OnPointerEnter(PointerEventData eventData){
                
                if(selectedButton != null && selectedButton != this){

                    selectedButton.OnPointerExit(null);

                }

                animator.Play("Enter");
                selectedButton = this;

            }

            public void OnPointerExit(PointerEventData eventData){

                animator.Play("Exit");

            }

    }

}
>>>>>>> c8d4f422 (Whole Template)
