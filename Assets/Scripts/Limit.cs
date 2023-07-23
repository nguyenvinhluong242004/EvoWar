using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limit : MonoBehaviour
{
    GameController gameController;
    int al;
    int _al;
    bool isCol;
    bool is_Col;
    int alpha;
    // Start is called before the first frame update
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        isCol = false;
        is_Col = false;
        al = 60;
        _al = 60;
    }

    // Update is called once per frame
    void Update()
    {
        if (al < 0)
        {
            al = 0;
            isCol = false;
            is_Col = true;
        }
        if (isCol)
        {
            transform.eulerAngles -= new Vector3(0, 0, alpha);
            al--;
        }
        if (_al < 0)
        {
            _al = 0;
            is_Col = false;
        }
        if (is_Col)
        {
            transform.eulerAngles += new Vector3(0, 0, alpha);
            _al--;
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Player"))
        {
            cut();
        }
    }
    void cut()
    {
        if (!isCol && !is_Col)
        {
            isCol = true;
            alpha = gameController.speedCut;
            al = 180 / alpha;
            _al = al;
        }
    }
}
