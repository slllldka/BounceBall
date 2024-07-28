using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class StageButtonScript : MonoBehaviour
{
    private Image img;
    private Button btn;
    public Sprite lockSprite;

    // Start is called before the first frame update
    void Start() {
        img = GetComponent<Image>();
        btn = GetComponent<Button>();
        if (Int32.Parse(gameObject.name) <= PlayerPrefs.GetInt("ClearStage") + 1) {
            transform.GetChild(0).gameObject.SetActive(true);
            img.sprite = null;
            btn.interactable = true;
            if(Int32.Parse(gameObject.name) <= PlayerPrefs.GetInt("ClearStage")) {
                btn.GetComponent<Image>().color = Color.yellow;
            }
        }
        else {
            transform.GetChild(0).gameObject.SetActive(false);
            img.sprite = lockSprite;
            btn.interactable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
