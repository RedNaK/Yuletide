using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System;

public class ExternalTextManager : MonoBehaviour
{
    /*TODO ADAPT TO THE NEW TEXT AND NEW INTERFACE*/
    //public Text uiText;
    public GameObject canvasLignes;
    private Text[] lignes;

    void Start()
    {
        lignes = canvasLignes.GetComponentsInChildren<Text>();
        parseXmlFile("lol");
    }

    void parseXmlFile(string xmlData)
    {
        string totVal = "";
        XmlDocument xmlDoc = new XmlDocument();
        string path = Path.Combine(Application.streamingAssetsPath, "citations.xml");
        xmlDoc.Load(path);

        string xmlPathPattern = "//refuge/citation";
        XmlNodeList myNodeList = xmlDoc.SelectNodes(xmlPathPattern);

        int i = 0;

        foreach (XmlNode node in myNodeList)
        {
            XmlNode planteref = node.FirstChild;
            XmlNode phrase = planteref.NextSibling;
            XmlNode auteur = phrase.NextSibling;
            totVal += phrase.InnerText + " " + planteref.InnerText + " " + auteur.InnerText;
            lignes[i].text = "\"" + phrase.InnerText + "\" - " + auteur.InnerText ;
            i++;
        }
    }
}
