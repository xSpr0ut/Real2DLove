<<<<<<< HEAD
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System.Linq;

namespace Dialog.Logical_Lines{

    public class Logical_Line_Manager{
        
        private Dialog_System dialogSystem => Dialog_System.instance;
        private List<ILogical_Lines> logicalLines = new List<ILogical_Lines>();

        public Logical_Line_Manager() => LoadLogicalLines();

        private void LoadLogicalLines(){
            
            Assembly assembly = Assembly.GetExecutingAssembly();
            Type[] lineTypes = assembly.GetTypes()
                .Where(t => typeof(ILogical_Lines).IsAssignableFrom(t) && !t.IsInterface)
                .ToArray();

            foreach(Type lineType in lineTypes){

                ILogical_Lines line = (ILogical_Lines)Activator.CreateInstance(lineType);
                logicalLines.Add(line);

            }

        }

        public bool Try_Get_Logic(Dialog_Line line, out Coroutine logic){

            foreach(var logicalLine in logicalLines){

                if(logicalLine.Matches(line)){

                    logic = dialogSystem.StartCoroutine(logicalLine.Execute(line));
                    return true;

                }

            }

            logic = null;
            return false;

        }

    }

}
=======
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System.Linq;

namespace Dialog.Logical_Lines{

    public class Logical_Line_Manager{
        
        private Dialog_System dialogSystem => Dialog_System.instance;
        private List<ILogical_Lines> logicalLines = new List<ILogical_Lines>();

        public Logical_Line_Manager() => LoadLogicalLines();

        private void LoadLogicalLines(){
            
            Assembly assembly = Assembly.GetExecutingAssembly();
            Type[] lineTypes = assembly.GetTypes()
                .Where(t => typeof(ILogical_Lines).IsAssignableFrom(t) && !t.IsInterface)
                .ToArray();

            foreach(Type lineType in lineTypes){

                ILogical_Lines line = (ILogical_Lines)Activator.CreateInstance(lineType);
                logicalLines.Add(line);

            }

        }

        public bool Try_Get_Logic(Dialog_Line line, out Coroutine logic){

            foreach(var logicalLine in logicalLines){

                if(logicalLine.Matches(line)){

                    logic = dialogSystem.StartCoroutine(logicalLine.Execute(line));
                    return true;

                }

            }

            logic = null;
            return false;

        }

    }

}
>>>>>>> c8d4f422 (Whole Template)
