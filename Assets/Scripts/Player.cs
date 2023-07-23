using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    GameController gameController;
    public GameObject centre;
    public GameObject cir;
    public GameObject[] p;
    public GameObject[] k;
    Vector3 velocity;
    float alpha;
    bool isMove;
    int lever;
    public float point;
    public float limitPoint;
    float speed;
    public Transform x;
    public GameObject map;
    public GameObject bgSta, sta;
    private Vector2 startingPoint;
    private int leftTouch = 99;
    private int rightTouch = 99;
    Cir _cir;
    public Vector3 pastPlayer;
    public Transform _cut, _x2;
    bool isX2;
    // Start is called before the first frame update
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        _cir = FindObjectOfType<Cir>();
        velocity = new Vector3(0, 0, 0);
        isMove = true;
        lever = 0;
        point = 0;
        isX2 = false;
        limitPoint = 100f;
        speed = gameController.speedObj;
        map = GameObject.Find("map");
    }
    // Update is called once per frame
    void Update()
    {
        checkPoint();
        int i = 0;
        while (i < Input.touchCount)
        {
            Touch t = Input.GetTouch(i);
            Vector2 touchPos = getTouchPosition(t.position); // * -1 for perspective cameras
            if (t.phase == TouchPhase.Began)
            {
                if (t.position.x > Screen.width / 2)
                {
                    rightTouch = t.fingerId;
                    if (!_cir.checkIsCut())
                    {
                        if (touchPos.x > _cut.position.x - 0.8f && touchPos.x < _cut.position.x + 0.8f && touchPos.y > _cut.position.y - 0.8f && touchPos.y < _cut.position.y + 0.8f)
                        {
                            //Debug.Log("Cut");
                            _cir.getCut();
                        }
                        if (!isX2 && point > 0)
                        {
                            if (touchPos.x > _x2.position.x - 0.65f && touchPos.x < _x2.position.x + 0.65f && touchPos.y > _x2.position.y - 0.65f && touchPos.y < _x2.position.y + 0.65f)
                            {
                                //Debug.Log("X2");
                                isX2 = true;
                                changeSpeed(true);
                            }
                        }    
                    }    
                }
                else
                {
                    leftTouch = t.fingerId;
                    startingPoint = touchPos;
                    pastPlayer = transform.position;
                }
            }
            else if (t.phase == TouchPhase.Moved && leftTouch == t.fingerId)
            {
                Vector2 change = transform.position - pastPlayer;
                pastPlayer = transform.position;
                startingPoint += change;

                Vector2 offset = touchPos - startingPoint;
                Vector2 direction = Vector2.ClampMagnitude(offset, 1.0f);

                velocity = direction;
                
                sta.transform.position = new Vector3(bgSta.transform.position.x + direction.x, bgSta.transform.position.y + direction.y, sta.transform.position.z);

            }
            else if (t.phase == TouchPhase.Ended && rightTouch == t.fingerId && isX2)
            {
                rightTouch = 99;
                changeSpeed(false);
                isX2 = false;
            }
            else if (t.phase == TouchPhase.Ended && leftTouch == t.fingerId)
            {
                leftTouch = 99;
                pastPlayer = transform.position;
                velocity = new Vector3(0, 0, 0);
                sta.transform.position = new Vector3(bgSta.transform.position.x, bgSta.transform.position.y, sta.transform.position.z);
            }
            ++i;
        }
        if (isMove) // nếu đang chém thì không di chuyển
        {
            float len = Mathf.Sqrt(velocity.x * velocity.x + velocity.y * velocity.y);
            if (len != 0)
            {
                alpha = Mathf.Acos((velocity.y) / len) * Mathf.Rad2Deg;
                velocity.x = speed * (velocity.x / len);
                velocity.y = speed * (velocity.y / len);
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
            velocity.z = 0;
            centre.transform.position += velocity;
            if (centre.transform.position.x < -40.5f || centre.transform.position.x > 40.5f || centre.transform.position.y < -25f || centre.transform.position.y > 26f)
                centre.transform.position -= velocity;
        }
        if (isX2)
        {
            if (point > 0)
                inceasePoint(-0.2f);
            else
            {
                changeSpeed(false);
                isX2 = false;
            }

        }
        if (map)
            x.position = map.transform.position + new Vector3(centre.transform.position.x / 14.8f, centre.transform.position.y / 14.85f, 0f);
        else
            map = GameObject.Find("map");
    }
    Vector2 getTouchPosition(Vector2 touchPosition)
    {
        return FindObjectOfType<Camera>().ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y, transform.position.z));
    }
    public void checkIsCut(bool trans)
    {
        isMove = trans;
    }    
    public Vector3 getPosition()
    {
        return transform.position + new Vector3(-6.485f, -2.594f, 0);
    }    
    void changeSpeed(bool sta)
    {
        if (sta)
            speed *= 1.7f;
        else
            speed /= 1.7f;
    }
    void checkPoint()
    {
        if (point >= limitPoint)
        {
            point = 1;
            if (lever<3)
            {
                p[lever + 1].SetActive(true);
                k[lever + 1].SetActive(true);
                p[lever].SetActive(false);
                k[lever].SetActive(false);
                lever++;
                limitPoint += 50f;
            }    
        }
    }
    public void inceasePoint(float k)
    {
        point += k;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Square"))
        {
            point += 10;
        }
        if (collision.gameObject.CompareTag("Sword"))
        {
            Debug.Log("lose!");
            gameController.letEnd();
            Destroy(centre);
        }
    }
}
