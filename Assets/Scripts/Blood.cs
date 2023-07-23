using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood : MonoBehaviour
{
    Player player;
    public Transform bgr;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.point>0)
        {
            float blood = (6f / player.limitPoint) * player.point;
            transform.localScale = new Vector3(blood, 0.4f, 0);
            blood = (6f - blood) / 2f;
            transform.position = new Vector3(bgr.transform.position.x - blood, bgr.transform.position.y, transform.position.z);
        }   
    }
}
