<<<<<<< HEAD
using UnityEngine;

namespace Menu{

    public class Menu_Page : MonoBehaviour{

        private const string open = "Open";
        private const string close = "Close";
        
        [SerializeField] public Animator animator;

        public enum Page_Type {Save_And_Load, Config, Help}
        public Page_Type pageType;

        public virtual void Open(){

            animator.SetTrigger(open);

        }

        public virtual void Close(bool closeAllMenus = false){

            animator.SetTrigger(close);

            if(closeAllMenus){

                Menu_Manager.instance.Close_Root();

            }

        }

    }

}
=======
using UnityEngine;

namespace Menu{

    public class Menu_Page : MonoBehaviour{

        private const string open = "Open";
        private const string close = "Close";
        
        [SerializeField] public Animator animator;

        public enum Page_Type {Save_And_Load, Config, Help}
        public Page_Type pageType;

        public virtual void Open(){

            animator.SetTrigger(open);

        }

        public virtual void Close(bool closeAllMenus = false){

            animator.SetTrigger(close);

            if(closeAllMenus){

                Menu_Manager.instance.Close_Root();

            }

        }

    }

}
>>>>>>> c8d4f422 (Whole Template)
