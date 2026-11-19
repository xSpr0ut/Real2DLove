<<<<<<< HEAD
using System;
using System.Collections;
using Graphics;
using Menu.Gallery;
using UnityEngine;

namespace Commands{

    public class CDE_Gallery : Command_Database_Extension{

        private static string[] parameterMedia = new string[] { "#m", "#media" };
        private static string[] parameterSpeed = new string[] { "#spd", "#speed" };
        private static string[] parameterImmediate = new string[] { "#i", "#immediate" };
        private static string[] parameterBlendTexture = new string[] { "#b", "#blend" };
        
        new public static void Extend(Command_Database database){

            database.Add_Command("show_gallery_image", new Func<string[], IEnumerator>(Show_Gallery_Image));
            database.Add_Command("hide_gallery_image", new Func<string[], IEnumerator>(Hide_Gallery_Image));

        }

        public static IEnumerator Hide_Gallery_Image(string[] data){

            Graphic_Layer graphicLayer = Graphic_Panel_Manager.instance.Get_Panel("Cinematic").Get_Layer(0, createIfNonexistent: true);

            if(graphicLayer.currentGraphic == null){

                yield break;

            }

            float transitionSpeed = 0;
            bool immediate = false;
            string blendName = "";
            Texture blendTex = null;

            var parameters = Convert_Data_To_Parameters(data);

            parameters.Try_Get_Value(parameterImmediate, out immediate, defaultValue: false);
            parameters.Try_Get_Value(parameterSpeed, out transitionSpeed, defaultValue: 1);
            parameters.Try_Get_Value(parameterBlendTexture, out blendName);

            if(!immediate && blendName != string.Empty){

                blendTex = Resources.Load<Texture>(File_Paths.resourcesBlendTextures + blendName);

            }

            if(!immediate){

                //Command_Manager.instance.Add_Termination_Action_To_Current_Process(() => {graphicLayer.Clear(immediate: true); });

            }

            graphicLayer.Clear(transitionSpeed, blendTex, immediate);

            if(graphicLayer.currentGraphic != null){

                var graphicObject = graphicLayer.currentGraphic;
                yield return new WaitUntil(() => graphicObject == null);

            }

        }

        public static IEnumerator Show_Gallery_Image(string[] data){

            string mediaName = "";
            float transitionSpeed = 0;
            bool immediate = false;
            string blendName = "";
            Texture blendTex = null;

            var parameters = Convert_Data_To_Parameters(data);

            parameters.Try_Get_Value(parameterMedia, out mediaName);
            parameters.Try_Get_Value(parameterImmediate, out immediate, defaultValue: false);
            parameters.Try_Get_Value(parameterSpeed, out transitionSpeed, defaultValue: 1);
            parameters.Try_Get_Value(parameterBlendTexture, out blendName);
            
            string pathToGraphic = File_Paths.resourcesGallery + mediaName;
            Texture graphic = Resources.Load<Texture>(pathToGraphic);

            if(graphic == null){

                Debug.LogError($"What kind of gallery image is {mediaName}?");
                yield break;

            }

            if(!immediate && blendName != string.Empty){

                blendTex = Resources.Load<Texture>(File_Paths.resourcesBlendTextures + blendName);

            }

            Graphic_Layer graphicLayer = Graphic_Panel_Manager.instance.Get_Panel("Cinematic").Get_Layer(0, createIfNonexistent: true);

            if(!immediate){

                Command_Manager.instance.Add_Termination_Action_To_Current_Process(() => {graphicLayer?.Set_Texture(graphic, filePath: pathToGraphic, immediate: immediate); });

            }

            Gallery_Config.Unlock_Image(mediaName);

            yield return graphicLayer.Set_Texture(graphic, transitionSpeed, blendTex, pathToGraphic, immediate);

        }

    }

}
=======
using System;
using System.Collections;
using Graphics;
using Menu.Gallery;
using UnityEngine;

namespace Commands{

    public class CDE_Gallery : Command_Database_Extension{

        private static string[] parameterMedia = new string[] { "#m", "#media" };
        private static string[] parameterSpeed = new string[] { "#spd", "#speed" };
        private static string[] parameterImmediate = new string[] { "#i", "#immediate" };
        private static string[] parameterBlendTexture = new string[] { "#b", "#blend" };
        
        new public static void Extend(Command_Database database){

            database.Add_Command("show_gallery_image", new Func<string[], IEnumerator>(Show_Gallery_Image));
            database.Add_Command("hide_gallery_image", new Func<string[], IEnumerator>(Hide_Gallery_Image));

        }

        public static IEnumerator Hide_Gallery_Image(string[] data){

            Graphic_Layer graphicLayer = Graphic_Panel_Manager.instance.Get_Panel("Cinematic").Get_Layer(0, createIfNonexistent: true);

            if(graphicLayer.currentGraphic == null){

                yield break;

            }

            float transitionSpeed = 0;
            bool immediate = false;
            string blendName = "";
            Texture blendTex = null;

            var parameters = Convert_Data_To_Parameters(data);

            parameters.Try_Get_Value(parameterImmediate, out immediate, defaultValue: false);
            parameters.Try_Get_Value(parameterSpeed, out transitionSpeed, defaultValue: 1);
            parameters.Try_Get_Value(parameterBlendTexture, out blendName);

            if(!immediate && blendName != string.Empty){

                blendTex = Resources.Load<Texture>(File_Paths.resourcesBlendTextures + blendName);

            }

            if(!immediate){

                //Command_Manager.instance.Add_Termination_Action_To_Current_Process(() => {graphicLayer.Clear(immediate: true); });

            }

            graphicLayer.Clear(transitionSpeed, blendTex, immediate);

            if(graphicLayer.currentGraphic != null){

                var graphicObject = graphicLayer.currentGraphic;
                yield return new WaitUntil(() => graphicObject == null);

            }

        }

        public static IEnumerator Show_Gallery_Image(string[] data){

            string mediaName = "";
            float transitionSpeed = 0;
            bool immediate = false;
            string blendName = "";
            Texture blendTex = null;

            var parameters = Convert_Data_To_Parameters(data);

            parameters.Try_Get_Value(parameterMedia, out mediaName);
            parameters.Try_Get_Value(parameterImmediate, out immediate, defaultValue: false);
            parameters.Try_Get_Value(parameterSpeed, out transitionSpeed, defaultValue: 1);
            parameters.Try_Get_Value(parameterBlendTexture, out blendName);
            
            string pathToGraphic = File_Paths.resourcesGallery + mediaName;
            Texture graphic = Resources.Load<Texture>(pathToGraphic);

            if(graphic == null){

                Debug.LogError($"What kind of gallery image is {mediaName}?");
                yield break;

            }

            if(!immediate && blendName != string.Empty){

                blendTex = Resources.Load<Texture>(File_Paths.resourcesBlendTextures + blendName);

            }

            Graphic_Layer graphicLayer = Graphic_Panel_Manager.instance.Get_Panel("Cinematic").Get_Layer(0, createIfNonexistent: true);

            if(!immediate){

                Command_Manager.instance.Add_Termination_Action_To_Current_Process(() => {graphicLayer?.Set_Texture(graphic, filePath: pathToGraphic, immediate: immediate); });

            }

            Gallery_Config.Unlock_Image(mediaName);

            yield return graphicLayer.Set_Texture(graphic, transitionSpeed, blendTex, pathToGraphic, immediate);

        }

    }

}
>>>>>>> c8d4f422 (Whole Template)
