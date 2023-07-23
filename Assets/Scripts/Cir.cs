using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cir : MonoBehaviour
{
    GameController gameController;
    Player pl;
    int al;
    int _al;
    bool isSpa;
    bool is_spa;
    int alpha;
    bool isCut;
    // Start is called before the first frame update
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        pl = FindObjectOfType<Player>();
        isSpa = false;
        is_spa = false;
        al = 60;
        _al = 60;
        isCut = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (pl && al<0)
        {
            pl.checkIsCut(true);
            al = 0;
            isSpa = false;
            is_spa = true;
        }    
        if (isSpa)
        {
            transform.eulerAngles -= new Vector3(0, 0, alpha);
            al--;
        }
        if (_al<0)
        {
            _al = 0;
            is_spa = false;
            isCut = false;
        }
        if (is_spa)
        {
            transform.eulerAngles += new Vector3(0, 0, alpha);
            _al--;
        }
    }
    public void getCut()
    {
        if (pl && !isSpa && !is_spa)
        {
            pl.checkIsCut(false);
            isSpa = true;
            alpha = gameController.speedCut;
            al = 180 / alpha;
            _al = al;
            isCut = true;
        }
    }    
    public bool checkIsCut()
    {
        return isCut;
    }    
}
