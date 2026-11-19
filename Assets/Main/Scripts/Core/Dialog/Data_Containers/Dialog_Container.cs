<<<<<<< HEAD
using UnityEngine;
using TMPro;

namespace Dialog{

    [System.Serializable]
    public class Dialog_Container{
        
        public GameObject root;
        public Name_Container nameContainer;
        public TextMeshProUGUI dialogText;
        public TextMeshProUGUI textDefault;
        private Canvas_Group_Controller cgController;

        public void Set_Dialog_Color(Color color) => dialogText.color = color;
        public void Set_Dialog_Font(TMP_FontAsset font) => dialogText.font = font;
        public void Set_Dialog_Font_Size(float size) => dialogText.fontSize = size;

        private bool initialized = false;

        public void Initialize(){

            if(initialized){

                return;

            }

            cgController = new Canvas_Group_Controller(Dialog_System.instance, root.GetComponent<CanvasGroup>());

        }

        public bool isVisible => cgController.isVisible;
        public Coroutine Show(float speed = 1f, bool immediate = false) => cgController.Show(speed, immediate);
        public Coroutine Hide(float speed = 1f, bool immediate = false) => cgController.Hide(speed, immediate);

    }

}
=======
using UnityEngine;
using TMPro;

namespace Dialog{

    [System.Serializable]
    public class Dialog_Container{
        
        public GameObject root;
        public Name_Container nameContainer;
        public TextMeshProUGUI dialogText;
        public TextMeshProUGUI textDefault;
        private Canvas_Group_Controller cgController;

        public void Set_Dialog_Color(Color color) => dialogText.color = color;
        public void Set_Dialog_Font(TMP_FontAsset font) => dialogText.font = font;
        public void Set_Dialog_Font_Size(float size) => dialogText.fontSize = size;

        private bool initialized = false;

        public void Initialize(){

            if(initialized){

                return;

            }

            cgController = new Canvas_Group_Controller(Dialog_System.instance, root.GetComponent<CanvasGroup>());

        }

        public bool isVisible => cgController.isVisible;
        public Coroutine Show(float speed = 1f, bool immediate = false) => cgController.Show(speed, immediate);
        public Coroutine Hide(float speed = 1f, bool immediate = false) => cgController.Hide(speed, immediate);

    }

}
>>>>>>> c8d4f422 (Whole Template)
