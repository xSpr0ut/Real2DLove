<<<<<<< HEAD
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Dialog{

    public class Dialog_Continue_Prompt : MonoBehaviour{
        
        private RectTransform root;
        [SerializeField] private Animator animator;
        [SerializeField] private TextMeshProUGUI tmpro;
        [SerializeField] private Image image;
        public string spriteName = "DCP_Omniscient";
        public static Dialog_Continue_Prompt instance { get; private set; }

        public bool isShowing => animator.gameObject.activeSelf;

        void Awake(){

            if(instance == null){

                instance = this;

            }
            
        }

        void Start(){

            root = GetComponent<RectTransform>();
            
        }

        public void Show(){

            tmpro = Dialog_System.instance.textBox;

            if(tmpro.text == string.Empty){

                if(isShowing){

                    Hide();

                }

                return;

            }

            tmpro.ForceMeshUpdate();

            animator.gameObject.SetActive(true);
            root.transform.SetParent(tmpro.transform);

            TMP_CharacterInfo finalCharacter = tmpro.textInfo.characterInfo[tmpro.textInfo.characterCount - 1];
            Vector3 targetPos = finalCharacter.bottomRight;
            float characterWidth = finalCharacter.pointSize * 0.5f;
            targetPos = new Vector3(targetPos.x + characterWidth, targetPos.y, 0);

            root.localPosition = targetPos;

        }

        public void Hide(){

            animator.gameObject.SetActive(false);

        }

        public void Set_Sprite(string spriteName){

            Sprite newSprite = Get_Sprite(spriteName);

            if(image == null){

                return;

            }

            image.sprite = newSprite;
            this.spriteName = spriteName;

        }

        public Sprite Get_Sprite(string spriteName){

            Sprite sprite;
            sprite = Resources.Load<Sprite>(File_Paths.Get_Path_To_Resource(File_Paths.resourcesDCP, spriteName));

            return sprite;

        }

    }

}
=======
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Dialog{

    public class Dialog_Continue_Prompt : MonoBehaviour{
        
        private RectTransform root;
        [SerializeField] private Animator animator;
        [SerializeField] private TextMeshProUGUI tmpro;
        [SerializeField] private Image image;
        public string spriteName = "DCP_Omniscient";
        public static Dialog_Continue_Prompt instance { get; private set; }

        public bool isShowing => animator.gameObject.activeSelf;

        void Awake(){

            if(instance == null){

                instance = this;

            }
            
        }

        void Start(){

            root = GetComponent<RectTransform>();
            
        }

        public void Show(){

            tmpro = Dialog_System.instance.textBox;

            if(tmpro.text == string.Empty){

                if(isShowing){

                    Hide();

                }

                return;

            }

            tmpro.ForceMeshUpdate();

            animator.gameObject.SetActive(true);
            root.transform.SetParent(tmpro.transform);

            TMP_CharacterInfo finalCharacter = tmpro.textInfo.characterInfo[tmpro.textInfo.characterCount - 1];
            Vector3 targetPos = finalCharacter.bottomRight;
            float characterWidth = finalCharacter.pointSize * 0.5f;
            targetPos = new Vector3(targetPos.x + characterWidth, targetPos.y, 0);

            root.localPosition = targetPos;

        }

        public void Hide(){

            animator.gameObject.SetActive(false);

        }

        public void Set_Sprite(string spriteName){

            Sprite newSprite = Get_Sprite(spriteName);

            if(image == null){

                return;

            }

            image.sprite = newSprite;
            this.spriteName = spriteName;

        }

        public Sprite Get_Sprite(string spriteName){

            Sprite sprite;
            sprite = Resources.Load<Sprite>(File_Paths.Get_Path_To_Resource(File_Paths.resourcesDCP, spriteName));

            return sprite;

        }

    }

}
>>>>>>> c8d4f422 (Whole Template)
