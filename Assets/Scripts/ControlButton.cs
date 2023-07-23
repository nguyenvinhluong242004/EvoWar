using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlButton : MonoBehaviour
{
    GameController gameController;
    public int key;
    // Start is called before the first frame update
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }
    //  0: play     1: setting      2: back     3: onMusic    4: offMusic   5: onSound    6: offSound   7: incObj   8:decObj    
    //  9: incSwo   10: decSwo
    void OnMouseDown()
    {
        if (key == 0)
            gameController.letPlay();
        else if (key == 1)
            gameController.letSetting();
        else if (key == 2)
            gameController.letEnd();
        else if (key == 3)
            gameController.setMusic(false);
        else if (key == 4)
            gameController.setMusic(true);
        else if (key == 5)
            gameController.setSound(false);
        else if (key == 6)
            gameController.setSound(true);
        else if (key == 7)
            gameController.setSpeedObj(true);
        else if (key == 8)
            gameController.setSpeedObj(false);
        else if (key == 9)
            gameController.setSpeedCut(true);
        else if (key == 10)
            gameController.setSpeedCut(false);
    }
}