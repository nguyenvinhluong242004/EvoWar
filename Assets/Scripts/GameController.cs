using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject sceneStart, scenePlay, sceneSetting, onMusic, offMusic, onSound, offSound;
    public GameObject sq, pl;
    public GameObject[] ene;
    Player player;
    bool isStart;
    bool isEnd;
    int time;
    bool isMusic;
    public bool isSound;
    public float speedObj;
    public int speedCut;
    public Enemy[] enemy;
    // Start is called before the first frame update
    void Start()
    {
        isStart = false;
        isEnd = false;
        time = 400;
        isMusic = true;
        isSound = true;
        speedObj = 0.07f;
        speedCut = 12;
    }

    // Update is called once per frame
    void Update()
    {
        checkStart();
        time--;
        if (time < 0)
        {
            creatEnemy(1);
            time = 1000;
        }
        if (time % 150 == 0)
        {
            creatSq();
        }
        if (time % 300 == 0)
        {
            creatEnemy(0);
        }
        enemy = FindObjectsOfType<Enemy>();
    }   
    void checkStart()
    {
        if (isStart)
        {
            for (int i = 0; i < 300; i++)
            {
                creatSq();
            }
            for (int i = 0; i < 20; i++)
            {
                creatEnemy(0);
            }    
            isStart = false;
            creatEnemy(1);
            creatEnemy(2);
            creatEnemy(3);
        }    
    }    
    void creatSq()
    {
        GameObject newObject = Instantiate(sq, new Vector3(Random.Range(-40f, 40f), Random.Range(-25f, 26f), 0), transform.rotation);
    }    
    public void creatEnemy(int i)
    {
        GameObject newObject = Instantiate(ene[i], new Vector3(Random.Range(-40f, 40f), Random.Range(-25f, 26f), 0), transform.rotation);
    }    
    public void letEnd()
    {
        scenePlay.SetActive(false);
        sceneSetting.SetActive(false);
        sceneStart.SetActive(true);
        isEnd = true;
    }
    public void letPlay()
    {
        sceneStart.SetActive(false);
        scenePlay.SetActive(true);
        if (isEnd)
        {
            player = FindObjectOfType<Player>();
            if (!player)
            {
                GameObject newObject = Instantiate(pl, new Vector3(0, 0, 0), transform.rotation);
            }
            isEnd = false;
            isStart = false;
        }
        else
            isStart = true;
    }    
    public void letSetting()
    {
        sceneStart.SetActive(false);
        sceneSetting.SetActive(true);
    }    
    public void setMusic(bool sta)
    {
        isMusic = sta;
        if (isMusic)
        {
            
            offMusic.SetActive(false);
            onMusic.SetActive(true);
        }
        else
        {
            onMusic.SetActive(false);
            offMusic.SetActive(true);
        }
    }
    public void setSound(bool sta)
    {
        isSound = sta;
        if (isSound)
        {

            offSound.SetActive(false);
            onSound.SetActive(true);
        }
        else
        {
            onSound.SetActive(false);
            offSound.SetActive(true);
        }
    }
    public void setSpeedObj(bool sta)
    {
        if(sta)
        {
            speedObj += 0.01f;
        }
        else if (speedObj>0.01f)
        {
            speedObj -= 0.01f;
        }
    }
    public void setSpeedCut(bool sta)
    {
        if (sta)
        {
            speedCut++;
        }
        else if (speedCut > 1)
        {
            speedCut--;
        }
    }
}
