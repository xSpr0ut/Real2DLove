<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Commands;

public class Test_Commamds : MonoBehaviour{

    void Start(){

        StartCoroutine(Running());

    }

    IEnumerator Running(){{

        yield return Command_Manager.instance.Execute("print");
        yield return Command_Manager.instance.Execute("print_1parameter", "Nuke.");
        yield return Command_Manager.instance.Execute("print_2parameter", "Line1", "Line2", "Line3");
        
        yield return Command_Manager.instance.Execute("lambda");
        yield return Command_Manager.instance.Execute("lambda_1parameter", "Nuke lambda.");
        yield return Command_Manager.instance.Execute("lambda_2parameter", "Lambda1", "Lambda2", "Lambda3");

        yield return Command_Manager.instance.Execute("process");
        yield return Command_Manager.instance.Execute("process_1parameter", "3");
        yield return Command_Manager.instance.Execute("process_2parameter", "Process1", "Prcoess2", "Process3");

    }}
    
}
=======
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Commands;

public class Test_Commamds : MonoBehaviour{

    void Start(){

        StartCoroutine(Running());

    }

    IEnumerator Running(){{

        yield return Command_Manager.instance.Execute("print");
        yield return Command_Manager.instance.Execute("print_1parameter", "Nuke.");
        yield return Command_Manager.instance.Execute("print_2parameter", "Line1", "Line2", "Line3");
        
        yield return Command_Manager.instance.Execute("lambda");
        yield return Command_Manager.instance.Execute("lambda_1parameter", "Nuke lambda.");
        yield return Command_Manager.instance.Execute("lambda_2parameter", "Lambda1", "Lambda2", "Lambda3");

        yield return Command_Manager.instance.Execute("process");
        yield return Command_Manager.instance.Execute("process_1parameter", "3");
        yield return Command_Manager.instance.Execute("process_2parameter", "Process1", "Prcoess2", "Process3");

    }}
    
}
>>>>>>> c8d4f422 (Whole Template)
