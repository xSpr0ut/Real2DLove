<<<<<<< HEAD
using Dialog;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.Gallery{

    public class Gallery_Menu : MonoBehaviour{

        public static Gallery_Menu instance {get; private set;}

        private const int pageButtonLimit = 2;
        private int maxPages = 0;
        private int selectedPage = 0;

        [SerializeField] private Texture[] galleryImages;

        [SerializeField] private CanvasGroup root;
        [SerializeField] private Button[] galleryPreviewButtons;
        [SerializeField] private Button panelSelectionButtonPrefab;
        [SerializeField] private Button nextButton;
        [SerializeField] private Button backButton;

        [SerializeField] private CanvasGroup previewPanel;
        private Canvas_Group_Controller previewPanelCG;
        private Canvas_Group_Controller rootCG;

        [SerializeField] private Button previewButton;

        private bool initialized = false;
        private int previewsPerPage => galleryPreviewButtons.Length;

        private void Awake(){

            instance = this;

        }

        void Start(){

            rootCG = new Canvas_Group_Controller(this, root);
            previewPanelCG = new Canvas_Group_Controller(this, previewPanel);

            Gallery_Config.Load();
            Get_All_Gallery_Images();
            
        }

        public void Open(){

            if(!initialized){

                Initialize();

            }   

            rootCG.Show();
            rootCG.Set_Interactable_State(true);

        }

        public void Close(){

            rootCG.Hide();
            rootCG.Set_Interactable_State(false);

        }

        private void Get_All_Gallery_Images(){

            galleryImages = Resources.LoadAll<Texture>(File_Paths.resourcesGallery);

        }

        private void Initialize(){

            initialized = true;
            Construct_Nav_Bar();
            Load_Page(1);
          
        }

        private void Construct_Nav_Bar(){

            int totalImages = galleryImages.Length;
            maxPages = (int)Mathf.Ceil((float)totalImages / previewsPerPage);

            int pageLimit = pageButtonLimit < maxPages ? pageButtonLimit : maxPages;

            for(int i = 1; i <= pageLimit; i++){

                GameObject buttonObj = Instantiate(panelSelectionButtonPrefab.gameObject, panelSelectionButtonPrefab.transform.parent);
                buttonObj.SetActive(true);

                Button button = buttonObj.GetComponent<Button>();
                TextMeshProUGUI txt = button.GetComponentInChildren<TextMeshProUGUI>();

                buttonObj.name = i.ToString();
                int page = i;
                button.onClick.AddListener(() => Load_Page(page));
                txt.text = i.ToString();

            }

            backButton.gameObject.SetActive(pageLimit < maxPages);
            nextButton.gameObject.SetActive(pageLimit < maxPages);

            nextButton.transform.SetAsLastSibling();

        }

        private void Load_Page(int pageNumber){

            int startingIndex = (pageNumber - 1) * previewsPerPage;

            for(int i = 0; i < previewsPerPage; i++){

                int index = i + startingIndex;
                Button button = galleryPreviewButtons[i];

                button.onClick.RemoveAllListeners();

                if(index >= galleryImages.Length){

                    button.transform.parent.gameObject.SetActive(false);
                    continue;

                }else{

                    button.transform.parent.gameObject.SetActive(true);
                    RawImage renderer = button.targetGraphic as RawImage;
                    Texture previewImage = galleryImages[index];   

                    if(Gallery_Config.Image_Is_Unlocked(previewImage.name)){

                        renderer.color = Color.white;
                        renderer.texture = previewImage;
                        button.onClick.AddListener(() => Show_Preview_Image(previewImage));

                    }else{

                        renderer.color = Color.black;
                        renderer.texture = null;

                    }

                }

            }

            selectedPage = pageNumber;

        }

        private void Show_Preview_Image(Texture image){

            RawImage renderer = previewButton.targetGraphic as RawImage;
            renderer.texture = image;
            previewPanelCG.Show();
            previewPanelCG.Set_Interactable_State(true);

        }

        public void Hide_Preview_Image(){

            previewPanelCG.Hide();
            previewPanelCG.Set_Interactable_State(false);

        }

        public void Next_Page(){

            if(selectedPage < maxPages){

                Load_Page(selectedPage + 1);

            }

        }

        public void Back_Page(){

            if(selectedPage > 1){

                Load_Page(selectedPage - 1);

            }

        }

    }

}
=======
using Dialog;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.Gallery{

    public class Gallery_Menu : MonoBehaviour{

        private const int pageButtonLimit = 2;
        private int maxPages = 0;
        private int selectedPage = 0;

        [SerializeField] private Texture[] galleryImages;

        [SerializeField] private CanvasGroup root;
        [SerializeField] private Button[] galleryPreviewButtons;
        [SerializeField] private Button panelSelectionButtonPrefab;
        [SerializeField] private Button nextButton;
        [SerializeField] private Button backButton;

        [SerializeField] private CanvasGroup previewPanel;
        private Canvas_Group_Controller previewPanelCG;
        private Canvas_Group_Controller rootCG;

        [SerializeField] private Button previewButton;

        private bool initialized = false;
        private int previewsPerPage => galleryPreviewButtons.Length;

        void Start(){

            rootCG = new Canvas_Group_Controller(this, root);
            previewPanelCG = new Canvas_Group_Controller(this, previewPanel);

            Gallery_Config.Load();
            Get_All_Gallery_Images();
            
        }

        public void Open(){

            if(!initialized){

                Initialize();

            }   

            rootCG.Show();
            rootCG.Set_Interactable_State(true);

        }

        public void Close(){

            rootCG.Hide();
            rootCG.Set_Interactable_State(false);

        }

        private void Get_All_Gallery_Images(){

            galleryImages = Resources.LoadAll<Texture>(File_Paths.resourcesGallery);

        }

        private void Initialize(){

            initialized = true;
            Construct_Nav_Bar();
            Load_Page(1);
          
        }

        private void Construct_Nav_Bar(){

            int totalImages = galleryImages.Length;
            maxPages = (int)Mathf.Ceil((float)totalImages / previewsPerPage);

            int pageLimit = pageButtonLimit < maxPages ? pageButtonLimit : maxPages;

            for(int i = 1; i <= pageLimit; i++){

                GameObject buttonObj = Instantiate(panelSelectionButtonPrefab.gameObject, panelSelectionButtonPrefab.transform.parent);
                buttonObj.SetActive(true);

                Button button = buttonObj.GetComponent<Button>();
                TextMeshProUGUI txt = button.GetComponentInChildren<TextMeshProUGUI>();

                buttonObj.name = i.ToString();
                int page = i;
                button.onClick.AddListener(() => Load_Page(page));
                txt.text = i.ToString();

            }

            backButton.gameObject.SetActive(pageLimit < maxPages);
            nextButton.gameObject.SetActive(pageLimit < maxPages);

            nextButton.transform.SetAsLastSibling();

        }

        private void Load_Page(int pageNumber){

            int startingIndex = (pageNumber - 1) * previewsPerPage;

            for(int i = 0; i < previewsPerPage; i++){

                int index = i + startingIndex;
                Button button = galleryPreviewButtons[i];

                button.onClick.RemoveAllListeners();

                if(index >= galleryImages.Length){

                    button.transform.parent.gameObject.SetActive(false);
                    continue;

                }else{

                    button.transform.parent.gameObject.SetActive(true);
                    RawImage renderer = button.targetGraphic as RawImage;
                    Texture previewImage = galleryImages[index];   

                    if(Gallery_Config.Image_Is_Unlocked(previewImage.name)){

                        renderer.color = Color.white;
                        renderer.texture = previewImage;
                        button.onClick.AddListener(() => Show_Preview_Image(previewImage));

                    }else{

                        renderer.color = Color.black;
                        renderer.texture = null;

                    }

                }

            }

            selectedPage = pageNumber;

        }

        private void Show_Preview_Image(Texture image){

            RawImage renderer = previewButton.targetGraphic as RawImage;
            renderer.texture = image;
            previewPanelCG.Show();
            previewPanelCG.Set_Interactable_State(true);

        }

        public void Hide_Preview_Image(){

            previewPanelCG.Hide();
            previewPanelCG.Set_Interactable_State(false);

        }

        public void Next_Page(){

            if(selectedPage < maxPages){

                Load_Page(selectedPage + 1);

            }

        }

        public void Back_Page(){

            if(selectedPage > 1){

                Load_Page(selectedPage - 1);

            }

        }

    }

}
>>>>>>> c8d4f422 (Whole Template)
