<<<<<<< HEAD
using System.Collections.Generic;
using Graphics;
using UnityEngine;
using UnityEngine.Video;

namespace History{

    [System.Serializable]

    public class Graphic_Data{
        
        public string panelName;
        public List<Layer_Data> layers;

        [System.Serializable]

        public class Layer_Data{

            public int depth = 0;
            public string graphicName;
            public string graphicPath;
            public bool isVideo;
            public bool useAudio;

            public Layer_Data(Graphic_Layer layer){

                depth = layer.layerDepth;

                if(layer.currentGraphic == null){

                    return;

                }

                var graphic = layer.currentGraphic;

                graphicName = graphic.graphicName;
                graphicPath = graphic.graphicPath;
                isVideo = graphic.isVideo;
                useAudio = graphic.useAudio;

            }

        }

        public static List<Graphic_Data> Capture(){

            List<Graphic_Data> graphicPanels = new List<Graphic_Data>();

            foreach(var panel in Graphic_Panel_Manager.instance.allPanels){

                if(panel.isClear){

                    continue;

                }

                Graphic_Data data = new Graphic_Data();
                data.panelName = panel.panelName;
                data.layers = new List<Layer_Data>();

                foreach(var layer in panel.layers){

                    Layer_Data entry = new Layer_Data(layer);
                    data.layers.Add(entry);

                }

                graphicPanels.Add(data);

            }

            return graphicPanels;

        }

        public static void Apply(List<Graphic_Data> data){

            List<string> cache = new List<string>();

            foreach(var panelData in data){

                var panel = Graphic_Panel_Manager.instance.Get_Panel(panelData.panelName);

                foreach(var layerData in panelData.layers){

                    var layer = panel.Get_Layer(layerData.depth, createIfNonexistent: true);

                    if(layer.currentGraphic == null || layer.currentGraphic.graphicName != layerData.graphicName){

                        if(!layerData.isVideo){

                            Texture texture = History_Cache.Load_Image(layerData.graphicPath);

                            if(texture != null){

                                layer.Set_Texture(texture, filePath: layerData.graphicPath, immediate: true);

                            }else{

                                Debug.LogWarning($"I can't load from image path {layerData.graphicPath}.");

                            }

                        }else{

                            VideoClip clip = History_Cache.Load_Video(layerData.graphicPath);

                            if(clip != null){

                                layer.Set_Video(clip, filePath: layerData.graphicPath, immediate: true);

                            }else{

                                Debug.LogWarning($"I can't load from video path {layerData.graphicPath}.");

                            }

                        }

                    }

                }

                cache.Add(panel.panelName);

            }

            foreach(var panel in Graphic_Panel_Manager.instance.allPanels){

                if(!cache.Contains(panel.panelName)){

                    panel.Clear(immediate: true);

                }

            }

        }

    }

}
=======
using System.Collections.Generic;
using Graphics;
using UnityEngine;
using UnityEngine.Video;

namespace History{

    [System.Serializable]

    public class Graphic_Data{
        
        public string panelName;
        public List<Layer_Data> layers;

        [System.Serializable]

        public class Layer_Data{

            public int depth = 0;
            public string graphicName;
            public string graphicPath;
            public bool isVideo;
            public bool useAudio;

            public Layer_Data(Graphic_Layer layer){

                depth = layer.layerDepth;

                if(layer.currentGraphic == null){

                    return;

                }

                var graphic = layer.currentGraphic;

                graphicName = graphic.graphicName;
                graphicPath = graphic.graphicPath;
                isVideo = graphic.isVideo;
                useAudio = graphic.useAudio;

            }

        }

        public static List<Graphic_Data> Capture(){

            List<Graphic_Data> graphicPanels = new List<Graphic_Data>();

            foreach(var panel in Graphic_Panel_Manager.instance.allPanels){

                if(panel.isClear){

                    continue;

                }

                Graphic_Data data = new Graphic_Data();
                data.panelName = panel.panelName;
                data.layers = new List<Layer_Data>();

                foreach(var layer in panel.layers){

                    Layer_Data entry = new Layer_Data(layer);
                    data.layers.Add(entry);

                }

                graphicPanels.Add(data);

            }

            return graphicPanels;

        }

        public static void Apply(List<Graphic_Data> data){

            List<string> cache = new List<string>();

            foreach(var panelData in data){

                var panel = Graphic_Panel_Manager.instance.Get_Panel(panelData.panelName);

                foreach(var layerData in panelData.layers){

                    var layer = panel.Get_Layer(layerData.depth, createIfNonexistent: true);

                    if(layer.currentGraphic == null || layer.currentGraphic.graphicName != layerData.graphicName){

                        if(!layerData.isVideo){

                            Texture texture = History_Cache.Load_Image(layerData.graphicPath);

                            if(texture != null){

                                layer.Set_Texture(texture, filePath: layerData.graphicPath, immediate: true);

                            }else{

                                Debug.LogWarning($"I can't load from image path {layerData.graphicPath}.");

                            }

                        }else{

                            VideoClip clip = History_Cache.Load_Video(layerData.graphicPath);

                            if(clip != null){

                                layer.Set_Video(clip, filePath: layerData.graphicPath, immediate: true);

                            }else{

                                Debug.LogWarning($"I can't load from video path {layerData.graphicPath}.");

                            }

                        }

                    }

                }

                cache.Add(panel.panelName);

            }

            foreach(var panel in Graphic_Panel_Manager.instance.allPanels){

                if(!cache.Contains(panel.panelName)){

                    panel.Clear(immediate: true);

                }

            }

        }

    }

}
>>>>>>> c8d4f422 (Whole Template)
