<<<<<<< HEAD
using Dialog.Logical_Lines;
using UnityEngine;

namespace VN{

    public class VN_Database_Link_Setup : MonoBehaviour{
        
        public void Setup_External_Links(){

            Variable_Store.Create_Variable("Player.playerName", "", () => VN_Save_Game.activeFile.playerName, value => VN_Save_Game.activeFile.playerName = value);
            Variable_Store.Create_Variable("Event_Select.eventNum", 1, () => VN_Save_Game.activeFile.num, value => VN_Save_Game.activeFile.num = value);
            
        }

    }

}
=======
using Dialog.Logical_Lines;
using UnityEngine;

namespace VN{

    public class VN_Database_Link_Setup : MonoBehaviour{
        
        public void Setup_External_Links(){

            Variable_Store.Create_Variable("Player.playerName", "", () => VN_Save_Game.activeFile.playerName, value => VN_Save_Game.activeFile.playerName = value);
            Variable_Store.Create_Variable("Event_Select.eventNum", 1, () => VN_Save_Game.activeFile.num, value => VN_Save_Game.activeFile.num = value);
            
        }

    }

}
>>>>>>> c8d4f422 (Whole Template)
