using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gm = null;
    public List<Dictionary<string, object>>[] map;
    public int currentStage = 0, maxStage = 0, expertStage = 0;

    // Start is called before the first frame update\
    private void Awake() {
        if (gm == null) {
            gm = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
            return;
        }

        maxStage = 15;
        expertStage = 13;

        map = new List<Dictionary<string, object>>[maxStage + 1];
        for (int i = 0; i <= maxStage; i++) {
            map[i] = CSVReader.Read(getStageName(i));
        }
        //map[0] = CSVReader.Read(getStageName(0));

        if (!PlayerPrefs.HasKey("ClearStage")) {
            PlayerPrefs.SetInt("ClearStage", -1);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openStage(int n) {
        currentStage = n;
        if (currentStage <= maxStage) {
            SceneManager.LoadScene(2);
        }
        else {
            openSelectStage();
        }
    }
    public void openStartScene() {
        SceneManager.LoadScene(0);
    }
    public void openSelectStage() {
        SceneManager.LoadScene(1);
    }
    public void exitGame() {
        Application.Quit();
    }
    public void clearSave() {
        PlayerPrefs.SetInt("ClearStage", -1);
    }
    public void openEditStage() {
        SceneManager.LoadScene(3);
    }

    public string getStageName(int stageNum) {
        if(stageNum == 0) {
            return "TUTORIAL";
        }
        else if(stageNum < expertStage) {
            return "Stage" + stageNum;
        } else if(stageNum >= expertStage){
            return "Expert Stage" + (stageNum - expertStage + 1);
        }
        return "";
    }
}