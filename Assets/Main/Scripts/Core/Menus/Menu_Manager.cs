<<<<<<< HEAD
using System.IO;
using System.Linq;
using Dialog;
using Menu.Config;
using Menu.Save_Load;
using UnityEngine;
using VN;

namespace Menu{

    public class Menu_Manager : MonoBehaviour{

        public static Menu_Manager instance; 

        [SerializeField] private Menu_Page[] pages;
        [SerializeField] private CanvasGroup root;

        private Canvas_Group_Controller rootCG;
        private Menu_Page activePage = null;
        private bool isOpen = false;

        private UI_Confirmation_Menu uiChoiceMenu => UI_Confirmation_Menu.instance;

        private void Awake(){

            instance = this;
            
        }

        void Start(){

            rootCG = new Canvas_Group_Controller(this, root);
            
        }

        private Menu_Page Get_Page(Menu_Page.Page_Type pageType){

            return pages.FirstOrDefault(page => page.pageType == pageType);

        }

        public void Open_Save_Page(){

            var page = Get_Page(Menu_Page.Page_Type.Save_And_Load);
            var saveLoadMenu = page.animator.GetComponentInParent<Save_And_Load_Menu>();

            saveLoadMenu.menuFunction = Save_And_Load_Menu.Menu_Function.save;

            Open_Page(page);

        } 

        public void Open_Load_Page(){

            var page = Get_Page(Menu_Page.Page_Type.Save_And_Load);
            var saveLoadMenu = page.animator.GetComponentInParent<Save_And_Load_Menu>();

            saveLoadMenu.menuFunction = Save_And_Load_Menu.Menu_Function.load;

            Open_Page(page);

        }

        public void Open_Config_Page(){

            var page = Get_Page(Menu_Page.Page_Type.Config);

            Open_Page(page);

        }

        public void Open_Help_Page(){

            var page = Get_Page(Menu_Page.Page_Type.Help);

            Open_Page(page);

        }

        private void Open_Page(Menu_Page page){

            if(page == null){

                return;

            }

            if(activePage != null && activePage != page){

                activePage.Close();

            }

            page.Open();
            activePage = page;

            if(!isOpen){

                Open_Root();

            }

        }

        public void Open_Root(){

            rootCG.Show();
            rootCG.Set_Interactable_State(true);
            isOpen = true;

        }

        public void Close_Root(){

            rootCG.Hide();
            rootCG.Set_Interactable_State(false);
            isOpen = false;

        }
        
        public void On_Click_Home(){

            VN_Save_Game.activeFile.Autosave();
            uiChoiceMenu.Show("Return home? (Progress has autosaved)", new UI_Confirmation_Menu.Confirmation_Button("Yup", () => UnityEngine.SceneManagement.SceneManager.LoadScene("Main_Menu")), new UI_Confirmation_Menu.Confirmation_Button("Nah", null));

        }

        public void On_Click_Quit(){

            uiChoiceMenu.Show("Leave game? :(", new UI_Confirmation_Menu.Confirmation_Button("See ya", () => Quit()), new UI_Confirmation_Menu.Confirmation_Button("Maybe not", null));

        }
        
        private void Quit(){

            Application.Quit();
            
        }

    }

}
=======
using System.IO;
using System.Linq;
using Dialog;
using Menu.Config;
using Menu.Save_Load;
using UnityEngine;
using VN;

namespace Menu{

    public class Menu_Manager : MonoBehaviour{

        public static Menu_Manager instance; 

        [SerializeField] private Menu_Page[] pages;
        [SerializeField] private CanvasGroup root;

        private Canvas_Group_Controller rootCG;
        private Menu_Page activePage = null;
        private bool isOpen = false;

        private UI_Confirmation_Menu uiChoiceMenu => UI_Confirmation_Menu.instance;

        private void Awake(){

            instance = this;
            
        }

        void Start(){

            rootCG = new Canvas_Group_Controller(this, root);
            
        }

        private Menu_Page Get_Page(Menu_Page.Page_Type pageType){

            return pages.FirstOrDefault(page => page.pageType == pageType);

        }

        public void Open_Save_Page(){

            var page = Get_Page(Menu_Page.Page_Type.Save_And_Load);
            var saveLoadMenu = page.animator.GetComponentInParent<Save_And_Load_Menu>();

            saveLoadMenu.menuFunction = Save_And_Load_Menu.Menu_Function.save;

            Open_Page(page);

        } 

        public void Open_Load_Page(){

            var page = Get_Page(Menu_Page.Page_Type.Save_And_Load);
            var saveLoadMenu = page.animator.GetComponentInParent<Save_And_Load_Menu>();

            saveLoadMenu.menuFunction = Save_And_Load_Menu.Menu_Function.load;

            Open_Page(page);

        }

        public void Open_Config_Page(){

            var page = Get_Page(Menu_Page.Page_Type.Config);

            Open_Page(page);

        }

        public void Open_Help_Page(){

            var page = Get_Page(Menu_Page.Page_Type.Help);

            Open_Page(page);

        }

        private void Open_Page(Menu_Page page){

            if(page == null){

                return;

            }

            if(activePage != null && activePage != page){

                activePage.Close();

            }

            page.Open();
            activePage = page;

            if(!isOpen){

                Open_Root();

            }

        }

        public void Open_Root(){

            rootCG.Show();
            rootCG.Set_Interactable_State(true);
            isOpen = true;

        }

        public void Close_Root(){

            rootCG.Hide();
            rootCG.Set_Interactable_State(false);
            isOpen = false;

        }
        
        public void On_Click_Home(){

            VN_Save_Game.activeFile.Autosave();
            uiChoiceMenu.Show("Return home? (Progress has autosaved)", new UI_Confirmation_Menu.Confirmation_Button("Yup", () => UnityEngine.SceneManagement.SceneManager.LoadScene("Main Menu")), new UI_Confirmation_Menu.Confirmation_Button("Nah", null));

        }

        public void On_Click_Quit(){

            uiChoiceMenu.Show("Leave game? :(", new UI_Confirmation_Menu.Confirmation_Button("See ya", () => Application.Quit()), new UI_Confirmation_Menu.Confirmation_Button("Maybe not", null));

        }

    }

}
>>>>>>> c8d4f422 (Whole Template)
