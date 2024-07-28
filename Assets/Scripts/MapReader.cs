using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MapReader : MonoBehaviour {
    public GameObject title;

    public GameObject player, star, ground, crackableGround, jump, thorn, leftMover, rightMover, dashItem, jumpItem, fallingRock;
    private GameObject temp;

    private float x, y;

    public static int starNum;

    private List<Dictionary<string, object>> map;

    // Start is called before the first frame update
    void Start() {
        starNum = 0;
        map = GameManager.gm.map[GameManager.gm.currentStage];
        for (int i = 0; i < map.Count; i++) {
            if (map[i]["Tag"].ToString().Equals("Player")) {
                temp = Instantiate(player);
            } else if (map[i]["Tag"].ToString().Equals("Star")) {
                temp = Instantiate(star);
                starNum++;
            } else if (map[i]["Tag"].ToString().Equals("Ground")) {
                temp = Instantiate(ground);
                temp.tag = "Ground";
            } else if (map[i]["Tag"].ToString().Equals("GroundTop")) {
                temp = Instantiate(ground);
            } else if (map[i]["Tag"].ToString().Equals("CrackableGround")) {
                temp = Instantiate(crackableGround);
            } else if (map[i]["Tag"].ToString().Equals("Jump")) {
                temp = Instantiate(jump);
            } else if (map[i]["Tag"].ToString().Equals("Thorn")) {
                temp = Instantiate(thorn);
            } else if (map[i]["Tag"].ToString().Equals("LeftMover")) {
                temp = Instantiate(leftMover);
            } else if (map[i]["Tag"].ToString().Equals("RightMover")) {
                temp = Instantiate(rightMover);
            } else if (map[i]["Tag"].ToString().Equals("DashItem")) {
                temp = Instantiate(dashItem);
            } else if (map[i]["Tag"].ToString().Equals("JumpItem")) {
                temp = Instantiate(jumpItem);
            } else if (map[i]["Tag"].ToString().Equals("FallingRock")) {
                temp = Instantiate(fallingRock);
                temp.GetComponent<FallingRockScript>().delay = float.Parse(map[i]["Delay"].ToString());
                temp.GetComponent<FallingRockScript>().period = float.Parse(map[i]["Period"].ToString());
            }
            x = float.Parse(map[i]["X"].ToString());
            y = float.Parse(map[i]["Y"].ToString());
            temp.transform.position = new Vector3(x, y, 0);
        }

        title.GetComponent<TextMeshProUGUI>().text = GameManager.gm.getStageName(GameManager.gm.currentStage);
    }

    // Update is called once per frame
    void Update() {

    }
}
