<<<<<<< HEAD
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.Save_Load{

    public class Save_Load_Navigation : MonoBehaviour{

        private const int maxButtons = 17;
        
        [SerializeField] private Save_And_Load_Menu menu;
        [SerializeField] private GameObject buttonPrefab;
        [SerializeField] private GameObject previousButton;
        [SerializeField] private GameObject nextButton;

        private bool initialized = false;
        
        public int selectedPage {get; private set;}
        private int maxPages = 0;

        void Start(){

            Initialize_Menu();
            
        }

        private void Initialize_Menu(){

            if(initialized){

                return;

            }

            maxPages = Mathf.CeilToInt((float)Save_And_Load_Menu.maxFiles / menu.slotsPerPage);
            
            for(int i = 1; i <= maxButtons; i++){

                GameObject obj = Instantiate(buttonPrefab.gameObject, buttonPrefab.transform.parent);
                obj.SetActive(true);

                Button button = obj.GetComponent<Button>();

                obj.name = i.ToString();
                TextMeshProUGUI text = button.GetComponentInChildren<TextMeshProUGUI>();
                text.text = i.ToString();
                int closureIndex = i;
                button.onClick.AddListener(() => Select_Save_File_Page(closureIndex));

            }

            previousButton.SetActive(true);
            nextButton.SetActive(true);
            nextButton.transform.SetAsLastSibling();
            initialized = true;

        }

        private void Select_Save_File_Page(int pageNumber){

            selectedPage = pageNumber;
            menu.Populate_Save_Slots_For_Page(pageNumber);

        }

        public void To_Next_Page(){

            if(selectedPage < maxPages){
                
                if(selectedPage == 0){

                    selectedPage++;
                    
                }

                Select_Save_File_Page(selectedPage + 1);

            }

        }

        public void To_Previous_Page(){

            if(selectedPage > 1){

                Select_Save_File_Page(selectedPage - 1);

            }

        }

    }

}
=======
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.Save_Load{

    public class Save_Load_Navigation : MonoBehaviour{

        private const int maxButtons = 5;
        
        [SerializeField] private Save_And_Load_Menu menu;
        [SerializeField] private GameObject buttonPrefab;
        [SerializeField] private GameObject previousButton;
        [SerializeField] private GameObject nextButton;

        private bool initialized = false;
        
        public int selectedPage {get; private set;}
        private int maxPages = 0;

        void Start(){

            Initialize_Menu();
            
        }

        private void Initialize_Menu(){

            if(initialized){

                return;

            }

            maxPages = Mathf.CeilToInt((float)Save_And_Load_Menu.maxFiles / menu.slotsPerPage);
            int pageButtonLimit = maxButtons < maxPages ? maxButtons : maxPages;

            for(int i = 1; i <= pageButtonLimit; i++){

                GameObject obj = Instantiate(buttonPrefab.gameObject, buttonPrefab.transform.parent);
                obj.SetActive(true);

                Button button = obj.GetComponent<Button>();

                obj.name = i.ToString();
                TextMeshProUGUI text = button.GetComponentInChildren<TextMeshProUGUI>();
                text.text = i.ToString();
                int closureIndex = i;
                button.onClick.AddListener(() => Select_Save_File_Page(closureIndex));

            }

            previousButton.SetActive(pageButtonLimit < maxPages);
            nextButton.SetActive(pageButtonLimit < maxPages);
            nextButton.transform.SetAsLastSibling();
            initialized = true;

        }

        private void Select_Save_File_Page(int pageNumber){

            selectedPage = pageNumber;
            menu.Populate_Save_Slots_For_Page(pageNumber);

        }

        public void To_Next_Page(){

            if(selectedPage < maxPages){
                Debug.Log($"{selectedPage}, {maxPages}");
                Select_Save_File_Page(selectedPage + 1);

            }

        }

        public void To_Previous_Page(){

            if(selectedPage > 1){

                Select_Save_File_Page(selectedPage - 1);

            }

        }

    }

}
>>>>>>> c8d4f422 (Whole Template)
