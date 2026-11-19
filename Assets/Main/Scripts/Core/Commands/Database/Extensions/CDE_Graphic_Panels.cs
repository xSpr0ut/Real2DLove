<<<<<<< HEAD
using System;
using System.Collections;
using System.Collections.Generic;
using Graphics;
using UnityEngine;
using UnityEngine.Video;

namespace Commands{

    public class CDE_Graphic_Panels : Command_Database_Extension{

        private static string[] parameterPanel = new string[] { "#p", "#panel" };
        private static string[] parameterLayer = new string[] { "#l", "#layer" };
        private static string[] parameterMedia = new string[] { "#m", "#media" };
        private static string[] parameterSpeed = new string[] { "#spd", "#speed" };
        private static string[] parameterImmediate = new string[] { "#i", "#immediate" };
        private static string[] parameterBlendTexture = new string[] { "#b", "#blend" };
        private static string[] parameterUseVideoAudio = new string[] { "#aud", "#audio" };

        new public static void Extend(Command_Database database){

            database.Add_Command("set_layer_media", new Func<string[], IEnumerator>(Set_Layer_Media));
            database.Add_Command("clear_layer_media", new Func<string[], IEnumerator>(Clear_Layer_Media));

        }

        private static IEnumerator Set_Layer_Media(string[] data){

            string panelName = "";
            int layer = 0;
            string mediaName = "";
            float transitionSpeed = 0;
            bool immediate = false;
            string blendName = "";
            bool useAudio = false;

            string pathToGraphic = "";
            UnityEngine.Object graphic = null;
            Texture blendTexture = null;

            var parameters = Convert_Data_To_Parameters(data);

            parameters.Try_Get_Value(parameterPanel, out panelName);
            Graphic_Panel panel = Graphic_Panel_Manager.instance.Get_Panel(panelName);

            if(panel == null){

                Debug.LogError($"Cannot get {panelName}.");
                yield break;

            }

            parameters.Try_Get_Value(parameterLayer, out layer, defaultValue: 0);
            parameters.Try_Get_Value(parameterMedia, out mediaName);
            parameters.Try_Get_Value(parameterImmediate, out immediate, defaultValue: false);
            parameters.Try_Get_Value(parameterSpeed, out transitionSpeed, defaultValue: 1);
            parameters.Try_Get_Value(parameterBlendTexture, out blendName);
            parameters.Try_Get_Value(parameterUseVideoAudio, out useAudio, defaultValue: false);

            pathToGraphic = File_Paths.Get_Path_To_Resource(File_Paths.resourcesBackgroundImages, mediaName);
            graphic = Resources.Load<Texture>(pathToGraphic);

            if(graphic == null){

                pathToGraphic = File_Paths.Get_Path_To_Resource(File_Paths.resourcesBackgroundVideos, mediaName);
                graphic = Resources.Load<VideoClip>(pathToGraphic);

            }

            if(graphic == null){

                Debug.LogError($"The fuck is {mediaName}?");
                yield break;

            }

            if(!immediate && blendName != string.Empty){

                blendTexture = Resources.Load<Texture>(File_Paths.resourcesBlendTextures + blendName);

            }

            //Getting the layer
            Graphic_Layer graphicLayer = panel.Get_Layer(layer, createIfNonexistent: true);

            if(graphic is Texture){

                if(!immediate){

                    Command_Manager.instance.Add_Termination_Action_To_Current_Process(() => {graphicLayer?.Set_Texture(graphic as Texture, filePath: pathToGraphic, immediate: true);});

                }

                yield return graphicLayer.Set_Texture(graphic as Texture, transitionSpeed, blendTexture, pathToGraphic, immediate);

            }else{

                yield return graphicLayer.Set_Video(graphic as VideoClip, transitionSpeed, useAudio, blendTexture, pathToGraphic, immediate);

            }

        }

        private static IEnumerator Clear_Layer_Media(string[] data){

            string panelName = "";
            int layer = 0;
            float transitionSpeed = 0;
            bool immediate = false;
            string blendName = "";

            Texture blendTexture = null;

            var parameters = Convert_Data_To_Parameters(data);

            parameters.Try_Get_Value(parameterPanel, out panelName);
            Graphic_Panel panel = Graphic_Panel_Manager.instance.Get_Panel(panelName);

            if(panel == null){

                Debug.LogError($"Cannot get {panelName}.");
                yield break;

            }

            parameters.Try_Get_Value(parameterLayer, out layer, defaultValue: -1);
            parameters.Try_Get_Value(parameterImmediate, out immediate, defaultValue: false);
            parameters.Try_Get_Value(parameterSpeed, out transitionSpeed, defaultValue: 1);
            parameters.Try_Get_Value(parameterBlendTexture, out blendName);
            
            if(!immediate && blendName != string.Empty){

                blendTexture = Resources.Load<Texture>(File_Paths.resourcesBlendTextures + blendName);

            }

            if(layer == -1){

                panel.Clear(transitionSpeed, blendTexture, immediate);

            }else{

                Graphic_Layer graphicLayer = panel.Get_Layer(layer);

                if(graphicLayer == null){

                    Debug.LogError($"Could not clear {layer} on panel: {panel.panelName}.");
                    yield break;

                }

                graphicLayer.Clear(transitionSpeed, blendTexture, immediate);

            }

        }

    }

}
=======
using System;
using System.Collections;
using System.Collections.Generic;
using Graphics;
using UnityEngine;
using UnityEngine.Video;

namespace Commands{

    public class CDE_Graphic_Panels : Command_Database_Extension{

        private static string[] parameterPanel = new string[] { "#p", "#panel" };
        private static string[] parameterLayer = new string[] { "#l", "#layer" };
        private static string[] parameterMedia = new string[] { "#m", "#media" };
        private static string[] parameterSpeed = new string[] { "#spd", "#speed" };
        private static string[] parameterImmediate = new string[] { "#i", "#immediate" };
        private static string[] parameterBlendTexture = new string[] { "#b", "#blend" };
        private static string[] parameterUseVideoAudio = new string[] { "#aud", "#audio" };

        new public static void Extend(Command_Database database){

            database.Add_Command("set_layer_media", new Func<string[], IEnumerator>(Set_Layer_Media));
            database.Add_Command("clear_layer_media", new Func<string[], IEnumerator>(Clear_Layer_Media));

        }

        private static IEnumerator Set_Layer_Media(string[] data){

            string panelName = "";
            int layer = 0;
            string mediaName = "";
            float transitionSpeed = 0;
            bool immediate = false;
            string blendName = "";
            bool useAudio = false;

            string pathToGraphic = "";
            UnityEngine.Object graphic = null;
            Texture blendTexture = null;

            var parameters = Convert_Data_To_Parameters(data);

            parameters.Try_Get_Value(parameterPanel, out panelName);
            Graphic_Panel panel = Graphic_Panel_Manager.instance.Get_Panel(panelName);

            if(panel == null){

                Debug.LogError($"Cannot get {panelName}.");
                yield break;

            }

            parameters.Try_Get_Value(parameterLayer, out layer, defaultValue: 0);
            parameters.Try_Get_Value(parameterMedia, out mediaName);
            parameters.Try_Get_Value(parameterImmediate, out immediate, defaultValue: false);
            parameters.Try_Get_Value(parameterSpeed, out transitionSpeed, defaultValue: 1);
            parameters.Try_Get_Value(parameterBlendTexture, out blendName);
            parameters.Try_Get_Value(parameterUseVideoAudio, out useAudio, defaultValue: false);

            pathToGraphic = File_Paths.Get_Path_To_Resource(File_Paths.resourcesBackgroundImages, mediaName);
            graphic = Resources.Load<Texture>(pathToGraphic);

            if(graphic == null){

                pathToGraphic = File_Paths.Get_Path_To_Resource(File_Paths.resourcesBackgroundVideos, mediaName);
                graphic = Resources.Load<VideoClip>(pathToGraphic);

            }

            if(graphic == null){

                Debug.LogError($"The fuck is {mediaName}?");
                yield break;

            }

            if(!immediate && blendName != string.Empty){

                blendTexture = Resources.Load<Texture>(File_Paths.resourcesBlendTextures + blendName);

            }

            //Getting the layer
            Graphic_Layer graphicLayer = panel.Get_Layer(layer, createIfNonexistent: true);

            if(graphic is Texture){

                if(!immediate){

                    Command_Manager.instance.Add_Termination_Action_To_Current_Process(() => {graphicLayer?.Set_Texture(graphic as Texture, filePath: pathToGraphic, immediate: true);});

                }

                yield return graphicLayer.Set_Texture(graphic as Texture, transitionSpeed, blendTexture, pathToGraphic, immediate);

            }else{

                yield return graphicLayer.Set_Video(graphic as VideoClip, transitionSpeed, useAudio, blendTexture, pathToGraphic, immediate);

            }

        }

        private static IEnumerator Clear_Layer_Media(string[] data){

            string panelName = "";
            int layer = 0;
            float transitionSpeed = 0;
            bool immediate = false;
            string blendName = "";

            Texture blendTexture = null;

            var parameters = Convert_Data_To_Parameters(data);

            parameters.Try_Get_Value(parameterPanel, out panelName);
            Graphic_Panel panel = Graphic_Panel_Manager.instance.Get_Panel(panelName);

            if(panel == null){

                Debug.LogError($"Cannot get {panelName}.");
                yield break;

            }

            parameters.Try_Get_Value(parameterLayer, out layer, defaultValue: -1);
            parameters.Try_Get_Value(parameterImmediate, out immediate, defaultValue: false);
            parameters.Try_Get_Value(parameterSpeed, out transitionSpeed, defaultValue: 1);
            parameters.Try_Get_Value(parameterBlendTexture, out blendName);
            
            if(!immediate && blendName != string.Empty){

                blendTexture = Resources.Load<Texture>(File_Paths.resourcesBlendTextures + blendName);

            }

            if(layer == -1){

                panel.Clear(transitionSpeed, blendTexture, immediate);

            }else{

                Graphic_Layer graphicLayer = panel.Get_Layer(layer);

                if(graphicLayer == null){

                    Debug.LogError($"Could not clear {layer} on panel: {panel.panelName}.");
                    yield break;

                }

                graphicLayer.Clear(transitionSpeed, blendTexture, immediate);

            }

        }

    }

}
>>>>>>> c8d4f422 (Whole Template)
