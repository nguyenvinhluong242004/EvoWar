using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject centre;
    public GameObject cir;
    public GameObject[] p;
    public GameObject[] k;
    GameController gameController;
    public Vector3 velocity;
    int time;
    float alpha;
    public int lever;
    float point;
    float limitPoint;
    public Transform x;
    public GameObject map;
    // Start is called before the first frame update
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        time = 300;
        velocity = new Vector3(0.08f, 0.08f, 0);
        //lever = 0;
        point = 0;
        limitPoint = 100 + lever * 50;
        setLever(lever);
        map = GameObject.Find("map");
    }
    // Update is called once per frame
    void Update()
    {
        checkPoint();
        if (time % 150 == 0)
        {
            velocity.x = Random.Range(-2, 2);
            velocity.y = Random.Range(-2, 2);
            while (velocity.x == 0f && velocity.y == 0f)
            {
                velocity.x = Random.Range(-2, 2);
                velocity.y = Random.Range(-2, 2);
            }
            float k = Mathf.Sqrt(velocity.x * velocity.x + velocity.y * velocity.y) / gameController.speedObj;
            velocity = new Vector3(velocity.x / k, velocity.y / k, 0);
            float len = Mathf.Sqrt(velocity.x * velocity.x + velocity.y * velocity.y);
            alpha = Mathf.Acos((velocity.y) / len) * Mathf.Rad2Deg;
            if (velocity.x < 0)
            {
                transform.eulerAngles = new Vector3(0, 0, alpha);
                cir.transform.eulerAngles = new Vector3(0, 0, alpha);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, -alpha);
                cir.transform.eulerAngles = new Vector3(0, 0, -alpha);
            }
        }
        time--;
        if (time <= 0)
            time = 300;
        velocity.z = 0;
        centre.transform.position += velocity;
        if (centre.transform.position.x < -40.5f || centre.transform.position.x > 40.5f || centre.transform.position.y < -25f || centre.transform.position.y > 26f)
            centre.transform.position -= velocity;
        if (!map)
            map = GameObject.Find("map");
        else
            x.position = map.transform.position + new Vector3(centre.transform.position.x / 14.8f, centre.transform.position.y / 14.85f, 0f);
    }
    public void setLever(int _lever)
    {
        if (_lever!=0)
        {
            p[_lever].SetActive(true);
            k[_lever].SetActive(true);
            p[0].SetActive(false);
            k[0].SetActive(false);
            lever = _lever;
            limitPoint = 100 + lever * 50;
        }    
    }    
    void checkPoint()
    {
        if (point >= limitPoint)
        {
            point = 0;
            if (lever < 3)
            {
                p[lever + 1].SetActive(true);
                k[lever + 1].SetActive(true);
                p[lever].SetActive(false);
                k[lever].SetActive(false);
                lever++;
                limitPoint += 50;
            }
        }
    }   
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Square"))
        {
            point += 10;
        }
        if (collision.gameObject.CompareTag("Sword"))
        {
            //Debug.Log("loseeeeeee!");
            Destroy(centre);
        }
    }
}
