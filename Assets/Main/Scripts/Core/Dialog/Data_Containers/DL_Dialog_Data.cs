<<<<<<< HEAD
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Dialog{

    public class DL_Dialog_Data{

        public string rawData { get; private set; } = string.Empty;
        public List<Dialog_Segment> segments;
        private const string segmentIdentificationPattern = @"\{[ca]\}|{w[ca]\s\d*\.?\d*\}";

        public DL_Dialog_Data(string rawDialog){

            this.rawData = rawDialog;
            segments = RipSegments(rawDialog);

        }

        public List<Dialog_Segment> RipSegments(string rawDialog){

            List<Dialog_Segment> segments = new List<Dialog_Segment>();
            MatchCollection matches = Regex.Matches(rawDialog, segmentIdentificationPattern);

            int lastIndex = 0;

            //Taking speaker segment, the first one
            Dialog_Segment segment = new Dialog_Segment();
            segment.dialog = matches.Count == 0 ? rawDialog : rawDialog.Substring(0, matches[0].Index);
            segment.startSignal = Dialog_Segment.StartSignal.NONE;
            segment.signalDelay = 0;
            segments.Add(segment);

            if(matches.Count == 0){

                return segments;

            }else{

                lastIndex = matches[0].Index;

            }

            for(int i = 0; i < matches.Count; i++){

                Match match = matches[i];
                segment = new Dialog_Segment();

                //Getting start signal
                string signalMatch = match.Value;//{A}
                signalMatch = signalMatch.Substring(1, match.Length -2);
                string[] signalSplit = signalMatch.Split(' ');
                segment.startSignal = (Dialog_Segment.StartSignal) Enum.Parse(typeof(Dialog_Segment.StartSignal), signalSplit[0].ToUpper());

                //Getting signal delay
                if(signalSplit.Length > 1){

                    float.TryParse(signalSplit[1], out segment.signalDelay);

                }

                //Getting dialog for the segment
                int nextIndex = i +1 < matches.Count ? matches[i +1].Index : rawDialog.Length;
                segment.dialog = rawDialog.Substring(lastIndex + match.Length, nextIndex - (lastIndex + match.Length));
                lastIndex = nextIndex;
                segments.Add(segment);
            
            }

            return segments;

        }

        public struct Dialog_Segment{

            public string dialog;
            public StartSignal startSignal;
            public float signalDelay;
            public enum StartSignal{NONE, C, A, WA, WC}

            public bool appendText => (startSignal == StartSignal.A || startSignal == StartSignal.WA);

        }

    }

}
=======
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Dialog{

    public class DL_Dialog_Data{

        public string rawData { get; private set; } = string.Empty;
        public List<Dialog_Segment> segments;
        private const string segmentIdentificationPattern = @"\{[ca]\}|{w[ca]\s\d*\.?\d*\}";

        public DL_Dialog_Data(string rawDialog){

            this.rawData = rawDialog;
            segments = RipSegments(rawDialog);

        }

        public List<Dialog_Segment> RipSegments(string rawDialog){

            List<Dialog_Segment> segments = new List<Dialog_Segment>();
            MatchCollection matches = Regex.Matches(rawDialog, segmentIdentificationPattern);

            int lastIndex = 0;

            //Taking speaker segment, the first one
            Dialog_Segment segment = new Dialog_Segment();
            segment.dialog = matches.Count == 0 ? rawDialog : rawDialog.Substring(0, matches[0].Index);
            segment.startSignal = Dialog_Segment.StartSignal.NONE;
            segment.signalDelay = 0;
            segments.Add(segment);

            if(matches.Count == 0){

                return segments;

            }else{

                lastIndex = matches[0].Index;

            }

            for(int i = 0; i < matches.Count; i++){

                Match match = matches[i];
                segment = new Dialog_Segment();

                //Getting start signal
                string signalMatch = match.Value;//{A}
                signalMatch = signalMatch.Substring(1, match.Length -2);
                string[] signalSplit = signalMatch.Split(' ');
                segment.startSignal = (Dialog_Segment.StartSignal) Enum.Parse(typeof(Dialog_Segment.StartSignal), signalSplit[0].ToUpper());

                //Getting signal delay
                if(signalSplit.Length > 1){

                    float.TryParse(signalSplit[1], out segment.signalDelay);

                }

                //Getting dialog for the segment
                int nextIndex = i +1 < matches.Count ? matches[i +1].Index : rawDialog.Length;
                segment.dialog = rawDialog.Substring(lastIndex + match.Length, nextIndex - (lastIndex + match.Length));
                lastIndex = nextIndex;
                segments.Add(segment);
            
            }

            return segments;

        }

        public struct Dialog_Segment{

            public string dialog;
            public StartSignal startSignal;
            public float signalDelay;
            public enum StartSignal{NONE, C, A, WA, WC}

            public bool appendText => (startSignal == StartSignal.A || startSignal == StartSignal.WA);

        }

    }

}
>>>>>>> c8d4f422 (Whole Template)
