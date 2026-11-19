<<<<<<< HEAD
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VN;

namespace Menu.Save_Load{

    public class Save_And_Load_Menu : Menu_Page{

        public static Save_And_Load_Menu instance {get; private set;}
        
        [SerializeField] private GameObject[] panels; 
        public const int maxFiles = 99;
        private int currentPage = 1;
        private bool loadedFirstTime = false;

        public enum Menu_Function{save, load}
        public Menu_Function menuFunction = Menu_Function.save;

        public Save_Load_Slot[] saveslots;
        public int slotsPerPage => saveslots.Length;

        public Texture emptyFileImage;
        private GameObject activePanel;

        private void Awake(){

            instance = this;
            
        }
        
        private void Start(){
            
            for( int i = 0; i < panels.Length; i++){

                panels[i].SetActive(i == 0);

            }

            activePanel = panels[0];
            
        }

        public override void Open(){

            base.Open();

            if(!loadedFirstTime){

                Populate_Save_Slots_For_Page(currentPage);

            }

        }
        
           public void Open_Panel(string panelName){

            GameObject panel = panels.First(p => p.name.ToLower() == panelName.ToLower());

            if(panel == null){

                Debug.LogError($"there ain't no panel called {panelName} in the config menu.");
                return;

            }

            if(activePanel != null && activePanel != panel){

                activePanel.SetActive(false);

            }

            panel.SetActive(true);
            activePanel = panel;

        }


        public void Populate_Save_Slots_For_Page(int pageNumber){
            
            currentPage = pageNumber;

            int startingFile = ((currentPage - 1) * slotsPerPage) + 1;
            int endingFile = startingFile + slotsPerPage - 1;

            for(int i = 0; i < slotsPerPage; i++){

                int fileNum = startingFile + i;
                Save_Load_Slot slot = saveslots[i];

                if(fileNum <= maxFiles){

                    slot.root.SetActive(true);
                    string filePath = $"{File_Paths.gameSaves}{fileNum}{VN_Save_Game.fileType}";
                    slot.fileNumber = fileNum;
                    slot.filePath = filePath;
                    slot.Populate_Details(menuFunction);

                }else{

                    slot.root.SetActive(false);

                }

            }

        }

    }

}

=======
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VN;

namespace Menu.Save_Load{

    public class Save_And_Load_Menu : Menu_Page{

        public static Save_And_Load_Menu instance {get; private set;}
        
        [SerializeField] private GameObject[] panels; 
        public const int maxFiles = 99;
        private int currentPage = 1;
        private bool loadedFirstTime = false;

        public enum Menu_Function{save, load}
        public Menu_Function menuFunction = Menu_Function.save;

        public Save_Load_Slot[] saveslots;
        public int slotsPerPage => saveslots.Length;

        public Texture emptyFileImage;
        private GameObject activePanel;

        private void Awake(){

            instance = this;
            
        }
        
        private void Start(){
            
            for( int i = 0; i < panels.Length; i++){

                panels[i].SetActive(i == 0);

            }

            activePanel = panels[0];
            
        }

        public override void Open(){

            base.Open();

            if(!loadedFirstTime){

                Populate_Save_Slots_For_Page(currentPage);

            }

        }
        
           public void Open_Panel(string panelName){

            GameObject panel = panels.First(p => p.name.ToLower() == panelName.ToLower());

            if(panel == null){

                Debug.LogError($"there ain't no panel called {panelName} in the config menu.");
                return;

            }

            if(activePanel != null && activePanel != panel){

                activePanel.SetActive(false);

            }

            panel.SetActive(true);
            activePanel = panel;

        }


        public void Populate_Save_Slots_For_Page(int pageNumber){

            currentPage = pageNumber;

            int startingFile = ((currentPage - 1) * slotsPerPage) + 1;
            int endingFile = startingFile + slotsPerPage - 1;

            for(int i = 0; i < slotsPerPage; i++){

                int fileNum = startingFile + i;
                Save_Load_Slot slot = saveslots[i];

                if(fileNum <= maxFiles){

                    slot.root.SetActive(true);
                    string filePath = $"{File_Paths.gameSaves}{fileNum}{VN_Save_Game.fileType}";
                    slot.fileNumber = fileNum;
                    slot.filePath = filePath;
                    slot.Populate_Details(menuFunction);

                }else{

                    slot.root.SetActive(false);

                }

            }

        }

    }

}

>>>>>>> c8d4f422 (Whole Template)
