using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FHE
{
    class ParserXML
    {
        private static int CurrentLevel;
        private static String CurrentName;
        private static String CurentFullName;
        private static Dictionary<String, Goal> Goals = new Dictionary<string,Goal>();
        private static Dictionary<String, Characteristic> Characteristics = new Dictionary<string,Characteristic>();
        private static String CurrentLinkFunction;
        private static MembershipFunction MF;
        private static double CurrentStartX;
        private static double CurrentEndX;
        private static String CurrentUnit;

        public static List<Goal> ParseXMLFile(String PathFile)
        {
            List<Goal> goals = new List<Goal>();

            XmlDocument XmlDoc = new XmlDocument();
            XmlDoc.Load(PathFile);

            XmlNode Root = XmlDoc.FirstChild;

            ParseXMLNode(Root);

            foreach (Goal goal in Goals.Values)
            {
                goals.Add(goal);
            }

            return goals;
        }

        private static bool ParseXMLNode(XmlNode NodeXml)
        {
            switch (NodeXml.Name)
            {
                case "ROOT":
                    foreach(XmlNode ChildXmlNode in NodeXml.ChildNodes)
                    {
                        ParseXMLNode(ChildXmlNode);
                    }
                    break;
                case "HIGH":
                    foreach(XmlAttribute AttributeXml in NodeXml.Attributes)
                    {
                        switch (AttributeXml.Name)
                        {
                            case "name":
                                CurrentName = AttributeXml.Value;
                                break;
                            case "level":
                                CurrentLevel = Convert.ToInt32(AttributeXml.Value);                                
                                break;
                            case "fullName":
                                CurentFullName = AttributeXml.Value;
                                break;
                        }
                    }
                    foreach (XmlNode ChildXmlNode in NodeXml.ChildNodes)
                    {
                        ParseXMLNode(ChildXmlNode);
                    }
                    Goal NewGoal = new Goal(MF, CurrentName, new Function(CurrentLinkFunction), CurentFullName);
                    Goals.Add(CurrentName, NewGoal);

                    CurrentLevel = 0;
                    CurrentName = null;
                    CurentFullName = null;
                    CurrentLinkFunction = null;
                    MF = null;
                    break;
                case "MID":
                    foreach(XmlAttribute AttributeXml in NodeXml.Attributes)
                    {
                        switch (AttributeXml.Name)
                        {
                            case "name":
                                CurrentName = AttributeXml.Value;
                                break;
                            case "level":
                                CurrentLevel = Convert.ToInt32(AttributeXml.Value);                                
                                break;
                            case "fullName":
                                CurentFullName = AttributeXml.Value;
                                break;
                        }
                    }
                    foreach (XmlNode ChildXmlNode in NodeXml.ChildNodes)
                    {
                        ParseXMLNode(ChildXmlNode);
                    }
                    Characteristic NewCharacteristic = new Characteristic(MF, CurrentName, new Function(CurrentLinkFunction), CurentFullName, CurrentLevel);
                    Characteristics.Add(CurrentName, NewCharacteristic);

                    CurrentLevel = 0;
                    CurrentName = null;
                    CurentFullName = null;
                    CurrentLinkFunction = null;
                    MF = null;
                    break;
                case "LOW":
                    foreach(XmlAttribute AttributeXml in NodeXml.Attributes)
                    {
                        switch (AttributeXml.Name)
                        {
                            case "name":
                                CurrentName = AttributeXml.Value;
                                break;
                            case "level":
                                CurrentLevel = Convert.ToInt32(AttributeXml.Value);                                
                                break;
                            case "fullName":
                                CurentFullName = AttributeXml.Value;
                                break;
                        }
                    }
                    foreach (XmlNode ChildXmlNode in NodeXml.ChildNodes)
                    {
                        ParseXMLNode(ChildXmlNode);
                    }
                    Characteristic NewCharacteristicLow = new Characteristic(MF, CurrentName, null, CurentFullName, CurrentLevel);
                    Characteristics.Add(CurrentName, NewCharacteristicLow);

                    CurrentLevel = 0;
                    CurrentName = null;
                    CurentFullName = null;
                    CurrentLinkFunction = null;
                    MF = null;
                    break;
                case "FUNCTION":
                    foreach (XmlAttribute AttributeXml in NodeXml.Attributes)
                    {
                        switch (AttributeXml.Name)
                        {
                            case "name":
                                CurrentLinkFunction = AttributeXml.Value;
                                break;
                        }
                    }
                    break;
                case "MF":
                    foreach (XmlAttribute AttributeXml in NodeXml.Attributes)
                    {
                        switch (AttributeXml.Name)
                        {
                            case "unit":
                                CurrentUnit = AttributeXml.Value;
                                break;
                            case "startX":
                                CurrentStartX = Convert.ToDouble(AttributeXml.Value.Replace('.', ','));
                                break;
                            case "endX":
                                CurrentEndX = Convert.ToDouble(AttributeXml.Value.Replace('.', ','));
                                break;
                        }
                    }
                    MF = new MembershipFunction(CurrentUnit, CurrentStartX, CurrentEndX);
                    foreach (XmlNode ChildXmlNode in NodeXml.ChildNodes)
                    {
                        ParseXMLNode(ChildXmlNode);
                    }
                    break;
                case "POINT":
                    double x = 0, y = 0;
                    foreach (XmlAttribute AttributeXml in NodeXml.Attributes)
                    {
                        switch (AttributeXml.Name)
                        {
                            case "x":
                                x = Convert.ToDouble(AttributeXml.Value.Replace('.', ','));
                                break;
                            case "y":
                                y = Convert.ToDouble(AttributeXml.Value.Replace('.', ','));
                                break;
                        }
                    }
                    MF.addMFPoint(new MFPoint(x, y));
                    break;
                case "TRANSITION":
                    String parent = null;
                    String child = null;
                    foreach (XmlAttribute AttributeXml in NodeXml.Attributes)
                    {
                        switch (AttributeXml.Name)
                        {
                            case "parent":
                                parent = AttributeXml.Value;
                                break;
                            case "child":
                                child = AttributeXml.Value;
                                break;
                        }
                    }
                    if (parent[0] == 'Q' || parent[0] == 'q')
                    {
                        Goals[parent].AddChild(Characteristics[child]);
                    }
                    else
                    {
                        Characteristics[parent].AddChild(Characteristics[child]);
                    }
                    break;
            }
            return true;
        }
    }
}
