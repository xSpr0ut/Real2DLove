<<<<<<< HEAD
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Commands{

    public abstract class Command_Database_Extension{

        public static void Extend(Command_Database database){ }
        public static Command_Parameters Convert_Data_To_Parameters(string[] data, int startingIndex = 0) => new Command_Parameters(data, startingIndex);

    }

}
=======
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Commands{

    public abstract class Command_Database_Extension{

        public static void Extend(Command_Database database){ }
        public static Command_Parameters Convert_Data_To_Parameters(string[] data, int startingIndex = 0) => new Command_Parameters(data, startingIndex);

    }

}
>>>>>>> c8d4f422 (Whole Template)
