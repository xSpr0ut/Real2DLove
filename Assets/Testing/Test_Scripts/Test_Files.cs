<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using VN;
using Dialog;

namespace Testing{

    public class Test_Files : MonoBehaviour{
        
        [SerializeField] private TextAsset fileName = null;

        void Start(){

            Test_Dialog_Files();
            
        }

        IEnumerator Run(){

            List<string> lines = File_Manager.Read_Text_Asset(fileName, false);

            foreach(string line in lines){

                Debug.Log(line);

            }

            yield return null;

        }

        void Test_Dialog_Files(){

            string fullPath = AssetDatabase.GetAssetPath(fileName);
            int resourcesIndex = fullPath.IndexOf("Resources/");
            string relativePath = fullPath.Substring(resourcesIndex + 10);

            string filePath = Path.ChangeExtension(relativePath, null);

            Load_File(filePath);

        }

        public void Load_File(string filePath){

            List<string> lines = new List<string>();
            TextAsset file = Resources.Load<TextAsset>(filePath);

            try{

                lines = File_Manager.Read_Text_Asset(file);

            }catch{

                Debug.LogError($"There ain't no file at Resources/{filePath}.");
                return;

            }

            Dialog_System.instance.Say(lines);

        }

    }

}
=======
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using VN;
using Dialog;

namespace Testing{

    public class Test_Files : MonoBehaviour{
        
        [SerializeField] private TextAsset fileName = null;

        void Start(){

            Test_Dialog_Files();
            
        }

        IEnumerator Run(){

            List<string> lines = File_Manager.Read_Text_Asset(fileName, false);

            foreach(string line in lines){

                Debug.Log(line);

            }

            yield return null;

        }

        void Test_Dialog_Files(){

            string fullPath = AssetDatabase.GetAssetPath(fileName);
            int resourcesIndex = fullPath.IndexOf("Resources/");
            string relativePath = fullPath.Substring(resourcesIndex + 10);

            string filePath = Path.ChangeExtension(relativePath, null);

            Load_File(filePath);

        }

        public void Load_File(string filePath){

            List<string> lines = new List<string>();
            TextAsset file = Resources.Load<TextAsset>(filePath);

            try{

                lines = File_Manager.Read_Text_Asset(file);

            }catch{

                Debug.LogError($"There ain't no file at Resources/{filePath}.");
                return;

            }

            Dialog_System.instance.Say(lines);

        }

    }

}
>>>>>>> c8d4f422 (Whole Template)
