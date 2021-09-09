using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralController : MonoBehaviour
{
    public static GeneralController instance { get; private set; }

    public GameObject   puckObj;
    public GameObject   arrow;
    public GameObject   playerObj;

    public LineRenderer lr;

    public GameObject[] blocksObj;

    public bool shoot;
    public bool isMouseDown;                //Bool - for detect when mouse button down

    public Vector3 initImpulse;             //Vector3 - impulse rigidbody puck

    public Vector3 currentPosition;
    public Vector3 currentPosition2;
    public Transform center;
    public Transform idlePosition;

    public float maxLength;
    public float bottomBoundary;
    public float puckSpeed;

    public float xValue;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        

        //buttons onClick Handlers
        UiController.instance.startGameButton.onClick.AddListener(()    => UiController.instance.StartGameHandler());
        UiController.instance.exitGameButton.onClick.AddListener(()     => UiController.instance.ExitGameHandler());
        UiController.instance.menuGameButton.onClick.AddListener(()     => UiController.instance.pauseMenuHandler());
        UiController.instance.resumeGameButton.onClick.AddListener(()   => UiController.instance.ResumeGameHandler());
        UiController.instance.exit2GameButton.onClick.AddListener(()    => UiController.instance.ExitGameHandler());
    }
    
    private void Update()
    {
        
        slightShootHandler();

        EnemyController.instance.EnemyAi();
        GameStateController();


        if (playerObj.transform.position.x <= -4)
        {
            playerObj.transform.position = new Vector3(-3.99f, playerObj.transform.position.y, playerObj.transform.position.z);
        }

        if (playerObj.transform.position.x >= 4)
        {
            playerObj.transform.position = new Vector3(3.99f, playerObj.transform.position.y, playerObj.transform.position.z);
        }

        if (playerObj.transform.position.z >= 5.4f)
        {
            playerObj.transform.position = new Vector3(playerObj.transform.position.x, playerObj.transform.position.y, 5.39f);
        }

        if (playerObj.transform.position.z <= -2.7f)
        {
            playerObj.transform.position = new Vector3(playerObj.transform.position.x, playerObj.transform.position.y, -2.69f);
        }

    }


    void DrowLineRend(GameObject line, Vector3 start, Vector3 end, float duration = 0.2f)
    {
        line.transform.position = start;
       // line.AddComponent<LineRenderer>();
        LineRenderer lr = line.GetComponent<LineRenderer>();

        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        lr.SetWidth(0.2f, 0.2f); // set big width
    }


    void slightShootHandler()
    {
        if (isMouseDown)
        {
            lr.enabled = true;
            DrowLineRend(lr.gameObject, playerObj.transform.position, puckObj.transform.position, 0.2f); // Draw Line
            playerObj.SetActive(true);


            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 22;

            //currentPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            //currentPosition = center.position + Vector3.ClampMagnitude(currentPosition - center.position, maxLength);
            //currentPosition = new Vector3(currentPosition.x * 4, currentPosition.y, currentPosition.z * 6);

            currentPosition2 = Camera.main.ScreenToWorldPoint(mousePosition);
            //currentPosition2 = center.position + Vector3.ClampMagnitude(currentPosition2 - center.position, maxLength);
            //currentPosition2 = new Vector3(currentPosition2.x * 4, currentPosition2.y, currentPosition2.z * 6);

            currentPosition = playerObj.transform.position;
            currentPosition = center.position + Vector3.ClampMagnitude(currentPosition - center.position, maxLength);
            currentPosition = new Vector3(currentPosition.x * 4, currentPosition.y, currentPosition.z * 8);

            //arrow rotation
            Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 dir = Input.mousePosition - pos;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            //playerObj.transform.rotation = Quaternion.AngleAxis(angle, Vector3.down);
            //float vv = Mathf.Clamp(playerObj.transform.eulerAngles.y, -90, 90);
            //playerObj.transform.eulerAngles = new Vector3(0, playerObj.transform.eulerAngles.y, playerObj.transform.eulerAngles.z);

            playerObj.transform.LookAt(puckObj.transform);
            playerObj.transform.eulerAngles = new Vector3(0, playerObj.transform.eulerAngles.y, playerObj.transform.eulerAngles.z);
            playerObj.transform.position = new Vector3(currentPosition2.x, -1.88f, currentPosition2.z);
            //ayerObj.transform.position = currentPosition2;
        }
    }

    private void OnMouseDown()
    {
        isMouseDown = true;
    }

    private void OnMouseUp()
    {
        isMouseDown = false;

        // arrow.SetActive(false);
        playerObj.SetActive(false);
        lr.enabled = false;
        inputFinger();
        currentPosition = idlePosition.position;
        gameObject.GetComponent<BoxCollider>().enabled = false;
    }

    Vector3 ClampBoundary(Vector3 vector)
    {
        vector.y = Mathf.Clamp(vector.y, bottomBoundary, 1000);
        return vector;
    }


    void inputFinger()
    {
        if(Input.GetMouseButtonUp(0))
        {
            if (currentPosition.z <= 2)
            {
                initImpulse = new Vector3(currentPosition.x, 0, currentPosition.z + 15f);
            }
            else
            {
                initImpulse = new Vector3(currentPosition.x, 0, currentPosition.z);
            }
            
            puckObj.GetComponent<Rigidbody>().AddForce(initImpulse, ForceMode.Impulse);
            shoot = true;
        }
    }

   

    void GameStateController()
    {
        if (puckObj.activeInHierarchy == false)
        {
            puckObj.SetActive(true);
        }

        if (blocksObj[0].activeInHierarchy == false && blocksObj[1].activeInHierarchy == false  && blocksObj[2].activeInHierarchy == false)
        {
            EnemyController.instance.enemySpeed++;
            UiController.instance.levelScore.text = "Level " + EnemyController.instance.enemySpeed;
            for (int i = 0; i < blocksObj.Length; i++)
            {
                blocksObj[i].SetActive(true);
            }
            
        }
    }
}
