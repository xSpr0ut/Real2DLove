<<<<<<< HEAD
using System;
using System.Collections;
using System.Collections.Generic;
using History;
using Menu;
using Menu.Gallery;
using UnityEngine;
using UnityEngine.InputSystem;
using VN;

namespace Dialog{

    public class Player_Input_Manager : MonoBehaviour{
      
        private PlayerInput input;
        private List<(InputAction action, Action<InputAction.CallbackContext> command)> actions = new List<(InputAction action, Action<InputAction.CallbackContext> command)>();
        private void Awake(){

            input = GetComponent<PlayerInput>();
            Initialize_Actions();
            
        }

        private void Initialize_Actions(){

            actions.Add((input.actions["Next"], On_Next));
            actions.Add((input.actions["History_Backward"], On_History_Backward));
            actions.Add((input.actions["History_Forward"], On_History_Forward));
            actions.Add((input.actions["History_Logs"], On_History_Log_Toggle));
            actions.Add((input.actions["Quick_Save"], Quick_Save));
            actions.Add((input.actions["Quick_Load"], Quick_Load));
            actions.Add((input.actions["Close_Menu"], Close_Menu));

        }

        private void OnEnable(){

            foreach(var inputAction in actions){

                inputAction.action.performed += inputAction.command;

            }

        }

        private void OnDisable(){

            foreach(var inputAction in actions){

                inputAction.action.performed -= inputAction.command;

            }

        }

        public void On_Next(InputAction.CallbackContext c){

            Dialog_System.instance.Next_On_Player_Input();

        }

        public void On_History_Backward(InputAction.CallbackContext c){

            History_Manager.instance.Go_Backward();

        }

        public void On_History_Forward(InputAction.CallbackContext c){

            History_Manager.instance.Go_Forward();

        }

        public void On_History_Log_Toggle(InputAction.CallbackContext c){

            var logs = History_Manager.instance.logManager;

            if(!logs.isOpen){

                logs.Open();

            }else{

                logs.Close();

            }

        }
        
        public void Quick_Save(InputAction.CallbackContext c){

            VN_Save_Game.activeFile.Quick_Save();
            
        }

        public void Quick_Load(InputAction.CallbackContext c){

            VN_Save_Game.activeFile.Quick_Load();

        }
        
        public void Close_Menu(InputAction.CallbackContext c){

            Menu_Manager menuManager = Menu_Manager.instance;

            menuManager.Close_Root();

            Gallery_Menu galleryMenu = Gallery_Menu.instance;

            galleryMenu.Close();

            if (UI_Confirmation_Menu.instance.isOpen)
            {
                
                UI_Confirmation_Menu.instance.Hide();

            }

        }

    }

}
=======
using System;
using System.Collections;
using System.Collections.Generic;
using History;
using UnityEngine;
using UnityEngine.InputSystem;
using VN;

namespace Dialog{

    public class Player_Input_Manager : MonoBehaviour{
      
        private PlayerInput input;
        private List<(InputAction action, Action<InputAction.CallbackContext> command)> actions = new List<(InputAction action, Action<InputAction.CallbackContext> command)>();
        private void Awake(){

            input = GetComponent<PlayerInput>();
            Initialize_Actions();
            
        }

        private void Initialize_Actions(){

            actions.Add((input.actions["Next"], On_Next));
            actions.Add((input.actions["History_Backward"], On_History_Backward));
            actions.Add((input.actions["History_Forward"], On_History_Forward));
            actions.Add((input.actions["History_Logs"], On_History_Log_Toggle));
            actions.Add((input.actions["Quick_Save"], Quick_Save));
            actions.Add((input.actions["Quick_Load"], Quick_Load));

        }

        private void OnEnable(){

            foreach(var inputAction in actions){

                inputAction.action.performed += inputAction.command;

            }

        }

        private void OnDisable(){

            foreach(var inputAction in actions){

                inputAction.action.performed -= inputAction.command;

            }

        }

        public void On_Next(InputAction.CallbackContext c){

            Dialog_System.instance.Next_On_Player_Input();

        }

        public void On_History_Backward(InputAction.CallbackContext c){

            History_Manager.instance.Go_Backward();

        }

        public void On_History_Forward(InputAction.CallbackContext c){

            History_Manager.instance.Go_Forward();

        }

        public void On_History_Log_Toggle(InputAction.CallbackContext c){

            var logs = History_Manager.instance.logManager;

            if(!logs.isOpen){

                logs.Open();

            }else{

                logs.Close();

            }

        }
        
        public void Quick_Save(InputAction.CallbackContext c){

            VN_Save_Game.activeFile.Quick_Save();
            
        }
        
        public void Quick_Load(InputAction.CallbackContext c){

            VN_Save_Game.activeFile.Quick_Load();
            
        }

    }

}
>>>>>>> c8d4f422 (Whole Template)
