<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Dialog{
    
    [System.Serializable]
    public class Name_Container{
    
    //Name box & name text 

        [SerializeField] private GameObject root;
        [field:SerializeField] public TextMeshProUGUI nameText {get; private set;}
        [SerializeField] public GameObject nameBox;
        private float namePadding = 5;
        [HideInInspector] public Slider nameBoxSize;
    
        public void Show(string nameToShow = ""){

            root.SetActive(true);

            if(nameToShow != string.Empty){

                nameText.text = nameToShow;
                Show_Name_Box();

            }

        }
        
        private void Show_Name_Box (){

            nameBoxSize = nameBox.GetComponent<Slider>();
            nameBoxSize.value = nameText.text.Length * 3 + namePadding;
            
        }

        public void Hide(){

            root.SetActive(false);

        }

        public void Set_Name_Color(Color color) => nameText.color = color;
        public void Set_Name_Font(TMP_FontAsset font) => nameText.font = font;
        public void Set_Name_Font_Size(float size) => nameText.fontSize = size;

    }

}
=======
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Dialog{
    
    [System.Serializable]
    public class Name_Container{
    
    //Name box & name text 

        [SerializeField] private GameObject root;
        [field:SerializeField] public TextMeshProUGUI nameText {get; private set;}
        [SerializeField] public GameObject nameBox;
        private float namePadding = 5;
        [HideInInspector] public Slider nameBoxSize;
    
        public void Show(string nameToShow = ""){

            root.SetActive(true);

            if(nameToShow != string.Empty){

                nameText.text = nameToShow;
                Show_Name_Box();

            }

        }
        
        private void Show_Name_Box (){

            nameBoxSize = nameBox.GetComponent<Slider>();
            nameBoxSize.value = nameText.text.Length * 3 + namePadding;
            
        }

        public void Hide(){

            root.SetActive(false);

        }

        public void Set_Name_Color(Color color) => nameText.color = color;
        public void Set_Name_Font(TMP_FontAsset font) => nameText.font = font;
        public void Set_Name_Font_Size(float size) => nameText.fontSize = size;

    }

}
>>>>>>> c8d4f422 (Whole Template)
