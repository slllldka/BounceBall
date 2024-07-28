using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CSVWriter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] allObjects;
        int count = 0;
        float delay, period;
        string line = "Index,Tag,X,Y,Delay,Period";

        string fullpth = "Assets/Resources/MapCSV.csv";
        StreamWriter sw;
        if (false == File.Exists(fullpth)) {
            sw = new StreamWriter(fullpth);
            Debug.Log("Succeed");
        } else {
            Debug.Log("File Exists");
            return;
        }

        sw.WriteLine(line);
        Debug.Log(line);

        allObjects = SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (GameObject go in allObjects) {
            delay = 0;
            period = 0;
            if (go.CompareTag("Player")) {
            } else if (go.CompareTag("Star")) {
            } else if (go.CompareTag("Ground")) {
            } else if (go.CompareTag("GroundTop")) {
            } else if (go.CompareTag("CrackableGround")) {
            } else if (go.CompareTag("Jump")) {
            } else if (go.CompareTag("Thorn")) {
            } else if (go.CompareTag("LeftMover")) {
            } else if (go.CompareTag("RightMover")) {
            } else if (go.CompareTag("DashItem")) {
            } else if (go.CompareTag("JumpItem")) {
            } else if (go.CompareTag("FallingRock")) {
                delay = go.GetComponent<FallingRockScript>().delay;
                period = go.GetComponent<FallingRockScript>().period;
            } else {
                continue;
            }

            line = count + "," + go.tag + "," + go.transform.position.x + "," + go.transform.position.y + "," + delay + "," + period;
            sw.WriteLine(line);
            Debug.Log(line);
            count++;
        }

        sw.Flush();
        sw.Close();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
