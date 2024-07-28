using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class ButtonScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openStage() {
        GameManager.gm.openStage(Int32.Parse(EventSystem.current.currentSelectedGameObject.name));
    }
    public void openStartScene() {
        GameManager.gm.openStartScene();
    }
    public void openStageSelect() {
        GameManager.gm.openSelectStage();
    }
    public void exitGame() {
        GameManager.gm.exitGame();
    }
    public void clearSave() {
        GameManager.gm.clearSave();
    }

    public void openEditStage() {
        GameManager.gm.openEditStage();
    }
}
