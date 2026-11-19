<<<<<<< HEAD
using System.Collections.Generic;

namespace Dialog{

    public class Conversation{
        
        private List<string> lines = new List<string>();
        private int progress = 0;

        public string file { get; private set; }
        public int fileStartIndex { get; private set; }
        public int fileEndIndex { get; private set; }

        public Conversation(List<string> lines, int progress = 0, string file = "", int fileStartIndex = -1, int fileEndIndex = -1){

            this.lines = lines;
            this.progress = progress;
            this.file = file;

            if(fileStartIndex == -1){

                fileStartIndex = 0;

            }

            if(fileEndIndex == -1){

                fileEndIndex = lines.Count - 1;

            }

            this.fileStartIndex = fileStartIndex;
            this.fileEndIndex = fileEndIndex;

        }

        public int Get_Progress() => progress;
        public void Set_Progress(int value) => progress = value;
        public void Increment_Progress() => progress++;
        public int Count => lines.Count;
        public List<string> Get_Lines() => lines;
        public string Current_Line() => lines[progress];
        public bool Has_Reached_End() => progress >= lines.Count;

    }

}
=======
using System.Collections.Generic;

namespace Dialog{

    public class Conversation{
        
        private List<string> lines = new List<string>();
        private int progress = 0;

        public string file { get; private set; }
        public int fileStartIndex { get; private set; }
        public int fileEndIndex { get; private set; }

        public Conversation(List<string> lines, int progress = 0, string file = "", int fileStartIndex = -1, int fileEndIndex = -1){

            this.lines = lines;
            this.progress = progress;
            this.file = file;

            if(fileStartIndex == -1){

                fileStartIndex = 0;

            }

            if(fileEndIndex == -1){

                fileEndIndex = lines.Count - 1;

            }

            this.fileStartIndex = fileStartIndex;
            this.fileEndIndex = fileEndIndex;

        }

        public int Get_Progress() => progress;
        public void Set_Progress(int value) => progress = value;
        public void Increment_Progress() => progress++;
        public int Count => lines.Count;
        public List<string> Get_Lines() => lines;
        public string Current_Line() => lines[progress];
        public bool Has_Reached_End() => progress >= lines.Count;

    }

}
>>>>>>> c8d4f422 (Whole Template)
