using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using KFA.DataStream;
using KFA.Disks;

namespace KFA.Evidence {
    public enum ActionType {
        CaseCreated,
        SessionStarted,
        SessionEnded,
        DiskImaged,
        StreamViewed,
        StreamExplored,
        FileViewed,
        Search,
        EvidenceMarked
    }

    public class CaseAction {
        public DateTime Time {get; set;}
        public String Message { get; set; }
        public ActionType Type { get; set; }

        private CaseAction() { }

        public CaseAction(String message, ActionType type) {
            Message = Util.Sanitise(message);
            Type = type;
            Time = DateTime.Now;
        }

        #region Serialization

        public static CaseAction Deserialize(string xmlPath) {
            CaseAction res = new CaseAction();
            XmlSerializer s = new XmlSerializer(typeof(CaseAction));
            TextReader r = new StreamReader(xmlPath);
            res = (CaseAction)s.Deserialize(r);
            r.Close();

            return res;
        }

        public void Serialize(string xmlPath) {
            XmlSerializer s = new XmlSerializer(typeof(CaseAction));
            TextWriter w = new StreamWriter(xmlPath);
            s.Serialize(w, this);
            w.Close();
        }
        #endregion

        public override string ToString() {
            return String.Format("{0} {1} {2}", Time, Message, Type);
        }
    }

    public class Case {

        [XmlIgnore]
        private String FilePath { get; set; }
 
        public String Name { get; set; }
        public String CaseID { get; set; }
        public String Suspect { get; set; }
        public String SeizureNotes { get; set; }
        public List<String> Investigators { get; set; }
        public String Notes { get; set; }

        public List<CaseAction> Actions {get; set;}
        public List<Image> Images { get; set;}
        //public List<IDigitalEvidence> DigitalEvidence { get; set; }

        private Case() {
        }

        public Case(String filePath, String name) {
            FilePath = filePath;
            Name = name;

            CaseID = "Unknown";
            Suspect = "Enter suspect name here";
            SeizureNotes = "Enter seizure notes here";
            Investigators = new List<string>();
            Investigators.Add("Default Investigator");
            Notes = "...";

            Actions = new List<CaseAction>();
            Images = new List<Image>();
            //this.DigitalEvidence = new List<IDigitalEvidence>();
        }

        public void AddImage(Image image) {
            Images.Add(image);
            Save();
        }

        public void LogAction(String message, ActionType actionType) {
            Actions.Add(new CaseAction(message, actionType));
            Save();
        }

        public void Save() {
            Serialize(FilePath);
        }

        #region Serialization

        public static Case Deserialize(string xmlPath) {
            Case res = new Case();
            XmlSerializer s = new XmlSerializer(typeof(Case));
            TextReader r = new StreamReader(xmlPath);
            res = (Case)s.Deserialize(r);
            r.Close();
            res.FilePath = xmlPath;
            foreach (Image image in res.Images) {
                image.LoadFileSystem();
            }

            return res;
        }

        public void Serialize(string xmlPath) {
            XmlSerializer s = new XmlSerializer(typeof(Case));
            TextWriter w = new StreamWriter(xmlPath);
            s.Serialize(w, this);
            w.Close();
        }
        #endregion
    }
}
