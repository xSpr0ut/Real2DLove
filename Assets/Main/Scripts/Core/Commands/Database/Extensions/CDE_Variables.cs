<<<<<<< HEAD
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Commands{

    public class CDE_Variables : Command_Database_Extension{
        
        new public static void Extend(Command_Database database){

            database.Add_Command("set_player_name", new Action<string>(Set_Player_Name_Variable));

        }

        private static void Set_Player_Name_Variable(string data){

            VN.VN_Save_Game.activeFile.playerName = data;

        }

    }

}
=======
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Commands{

    public class CDE_Variables : Command_Database_Extension{
        
        new public static void Extend(Command_Database database){

            database.Add_Command("set_player_name", new Action<string>(Set_Player_Name_Variable));

        }

        private static void Set_Player_Name_Variable(string data){

            VN.VN_Save_Game.activeFile.playerName = data;

        }

    }

}
>>>>>>> c8d4f422 (Whole Template)
