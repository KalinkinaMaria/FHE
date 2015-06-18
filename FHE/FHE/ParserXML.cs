using FHE.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
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
        private static bool isNotDefaultTag;

        public static void SaveMFToFile(String filename, List<Point> points, String unit, double startX, double endX)
        {
            XmlTextWriter textWritter = new XmlTextWriter(filename, null);
            textWritter.WriteStartElement("ROOT");
            textWritter.WriteEndElement();
            textWritter.Close();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filename);

            XmlAttribute currentAttr;
            XmlElement mf = xmlDoc.CreateElement("MF");
            currentAttr = xmlDoc.CreateAttribute("unit");
            currentAttr.Value = unit;
            mf.Attributes.Append(currentAttr);
            currentAttr = xmlDoc.CreateAttribute("startX");
            currentAttr.Value = Convert.ToString(startX);
            mf.Attributes.Append(currentAttr);
            currentAttr = xmlDoc.CreateAttribute("endX");
            currentAttr.Value = Convert.ToString(endX);
            mf.Attributes.Append(currentAttr);

            foreach (Point point in points)
            {
                XmlElement elementPoint = xmlDoc.CreateElement("POINT");
                currentAttr = xmlDoc.CreateAttribute("x");
                currentAttr.Value = Convert.ToString(point.X);
                elementPoint.Attributes.Append(currentAttr);
                currentAttr = xmlDoc.CreateAttribute("y");
                currentAttr.Value = Convert.ToString(point.Y);
                elementPoint.Attributes.Append(currentAttr);
                mf.AppendChild(elementPoint);
            }
            xmlDoc.DocumentElement.AppendChild(mf); 
        }

        public static MembershipFunction ParseXMLFileToMF(String filename)
        {
            isNotDefaultTag = false;
            MembershipFunction mf = new MembershipFunction();

            XmlDocument XmlDoc = new XmlDocument();
            XmlDoc.Load(filename);

            XmlNode Root = XmlDoc.FirstChild;

            try
            {
                ParseXMLNodeToMF(Root);
            }
            catch (FormatException exept)
            {
                return null;
            }

            if (isNotDefaultTag)
            {
                return null;
            }

            mf = MF;
            return mf;
        }

        private static void ParseXMLNodeToMF(XmlNode Root)
        {
            switch (Root.Name)
            {
                case "ROOT":
                    foreach (XmlNode ChildXmlNode in Root.ChildNodes)
                    {
                        ParseXMLNode(ChildXmlNode);
                    }
                    break;
                case "MF":
                    foreach (XmlAttribute AttributeXml in Root.Attributes)
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
                    foreach (XmlNode ChildXmlNode in Root.ChildNodes)
                    {
                        ParseXMLNode(ChildXmlNode);
                    }
                    break;
                case "POINT":
                    double x = 0, y = 0;
                    foreach (XmlAttribute AttributeXml in Root.Attributes)
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
                    MF.addMFPoint(new MFPoint(x, y, CurrentUnit));
                    break;
                default:
                    isNotDefaultTag = true;
                    break;
            }
        }

        public static void SaveToFile(String filename, List<HierarchyLevel> levels)
        {
            Dictionary<String, List<String>> transition = new Dictionary<string, List<string>>();

            XmlTextWriter textWritter = new XmlTextWriter(filename, null);
            textWritter.WriteStartElement("ROOT");
            textWritter.WriteEndElement();
            textWritter.Close();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filename);

            XmlElement currentElement;
            XmlAttribute currentAttr;
            foreach (HierarchyLevel level in levels)
            {
                foreach (AbstractHierarchyNode node in level.stackNode.Children)
                {
                    if (node is HierarchyGoal)
                    {
                        currentElement = xmlDoc.CreateElement("HIGH");
                        currentAttr = xmlDoc.CreateAttribute("name");
                        currentAttr.Value = node.textNode.Text;
                        currentElement.Attributes.Append(currentAttr);
                        currentAttr = xmlDoc.CreateAttribute("level");
                        currentAttr.Value = "1";
                        currentElement.Attributes.Append(currentAttr);
                        currentAttr = xmlDoc.CreateAttribute("fullName");
                        currentAttr.Value = node.FullName;
                        currentElement.Attributes.Append(currentAttr);

                        XmlElement functionElement = xmlDoc.CreateElement("FUNCTION");
                        currentAttr = xmlDoc.CreateAttribute("name");
                        currentAttr.Value = node.LinkFunc;
                        functionElement.Attributes.Append(currentAttr);
                        currentElement.AppendChild(functionElement);

                        XmlElement mf = xmlDoc.CreateElement("MF");
                        currentAttr = xmlDoc.CreateAttribute("unit");
                        currentAttr.Value = node.UnitMF;
                        mf.Attributes.Append(currentAttr);
                        currentAttr = xmlDoc.CreateAttribute("startX");
                        currentAttr.Value = Convert.ToString(node.StartXMF);
                        mf.Attributes.Append(currentAttr);
                        currentAttr = xmlDoc.CreateAttribute("endX");
                        currentAttr.Value = Convert.ToString(node.EndXMF);
                        mf.Attributes.Append(currentAttr);

                        foreach (Point point in node.MembershipFunction)
                        {
                            XmlElement elementPoint = xmlDoc.CreateElement("POINT");
                            currentAttr = xmlDoc.CreateAttribute("x");
                            currentAttr.Value = Convert.ToString(point.X);
                            elementPoint.Attributes.Append(currentAttr);
                            currentAttr = xmlDoc.CreateAttribute("y");
                            currentAttr.Value = Convert.ToString(point.Y);
                            elementPoint.Attributes.Append(currentAttr);
                            mf.AppendChild(elementPoint);
                        }
                        currentElement.AppendChild(mf);
                        xmlDoc.DocumentElement.AppendChild(currentElement); 
                    }
                    else if (level.Number == levels.Count)
                    {
                        currentElement = xmlDoc.CreateElement("LOW");
                        currentAttr = xmlDoc.CreateAttribute("name");
                        currentAttr.Value = node.textNode.Text;
                        currentElement.Attributes.Append(currentAttr);
                        currentAttr = xmlDoc.CreateAttribute("level");
                        currentAttr.Value = Convert.ToString(level.Number);
                        currentElement.Attributes.Append(currentAttr);
                        currentAttr = xmlDoc.CreateAttribute("fullName");
                        currentAttr.Value = node.FullName;
                        currentElement.Attributes.Append(currentAttr);

                        XmlElement mf = xmlDoc.CreateElement("MF");
                        currentAttr = xmlDoc.CreateAttribute("unit");
                        currentAttr.Value = node.UnitMF;
                        mf.Attributes.Append(currentAttr);
                        currentAttr = xmlDoc.CreateAttribute("startX");
                        currentAttr.Value = Convert.ToString(node.StartXMF);
                        mf.Attributes.Append(currentAttr);
                        currentAttr = xmlDoc.CreateAttribute("endX");
                        currentAttr.Value = Convert.ToString(node.EndXMF);
                        mf.Attributes.Append(currentAttr);

                        foreach (Point point in node.MembershipFunction)
                        {
                            XmlElement elementPoint = xmlDoc.CreateElement("POINT");
                            currentAttr = xmlDoc.CreateAttribute("x");
                            currentAttr.Value = Convert.ToString(point.X);
                            elementPoint.Attributes.Append(currentAttr);
                            currentAttr = xmlDoc.CreateAttribute("y");
                            currentAttr.Value = Convert.ToString(point.Y);
                            elementPoint.Attributes.Append(currentAttr);
                            mf.AppendChild(elementPoint);
                        }
                        currentElement.AppendChild(mf);
                        xmlDoc.DocumentElement.AppendChild(currentElement); 
                    }
                    else
                    {
                        currentElement = xmlDoc.CreateElement("MID");
                        currentAttr = xmlDoc.CreateAttribute("name");
                        currentAttr.Value = node.textNode.Text;
                        currentElement.Attributes.Append(currentAttr);
                        currentAttr = xmlDoc.CreateAttribute("level");
                        currentAttr.Value = Convert.ToString(level.Number);
                        currentElement.Attributes.Append(currentAttr);
                        currentAttr = xmlDoc.CreateAttribute("fullName");
                        currentAttr.Value = node.FullName;
                        currentElement.Attributes.Append(currentAttr);

                        XmlElement functionElement = xmlDoc.CreateElement("FUNCTION");
                        currentAttr = xmlDoc.CreateAttribute("name");
                        currentAttr.Value = node.LinkFunc;
                        functionElement.Attributes.Append(currentAttr);
                        currentElement.AppendChild(functionElement);
                        xmlDoc.DocumentElement.AppendChild(currentElement); 
                    }
                    List<String> list = new List<String>();
                    foreach (HierarchyNode child in node.childrenNode)
                    {
                        list.Add(child.textNode.Text);
                    }
                    transition.Add(node.textNode.Text, list);
                }
            }

            foreach (String key in transition.Keys)
            {
                foreach (String value in transition[key])
                {
                    currentElement = xmlDoc.CreateElement("TRANSITION");
                    currentAttr = xmlDoc.CreateAttribute("parent");
                    currentAttr.Value = key;
                    currentElement.Attributes.Append(currentAttr);
                    currentAttr = xmlDoc.CreateAttribute("child");
                    currentAttr.Value = value;
                    currentElement.Attributes.Append(currentAttr);
                    xmlDoc.DocumentElement.AppendChild(currentElement); 
                }
            }

            xmlDoc.Save(filename);
        }

        public static List<Goal> ParseXMLFile(String PathFile)
        {
            isNotDefaultTag = false;
            List<Goal> goals = new List<Goal>();

            XmlDocument XmlDoc = new XmlDocument();
            XmlDoc.Load(PathFile);

            XmlNode Root = XmlDoc.FirstChild;

            try
            {
                ParseXMLNode(Root);
            }
            catch (FormatException exept)
            {
                return null;
            }
            if (isNotDefaultTag)
            {
                return null;
            }

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
                    MF.addMFPoint(new MFPoint(x, y, CurrentUnit));
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
                default:
                    isNotDefaultTag = true;
                    break;
            }
            return true;
        }

        internal static void Clear()
        {
            Goals.Clear();
            Characteristics.Clear();
        }
    }
}
