using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class Logs : MonoBehaviour
{
    public Transform ContentWindow;
    public GameObject LogTextPrefab;

    private void Start()
    {
        string readFromFile = Application.dataPath + @"\Logs.txt";
        List<string> fileLines = File.ReadAllLines(readFromFile).ToList();

        foreach (string line in fileLines)
        {
            GameObject logTextObject = Instantiate(LogTextPrefab, ContentWindow);
            Text logText = logTextObject.GetComponent<Text>();
            logText.text = line;
        }
    }
}

