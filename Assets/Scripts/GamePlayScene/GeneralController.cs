using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralController : MonoBehaviour
{
    public static GeneralController instance { get; private set; }

    public GameObject   puckObj;
    public GameObject   arrow;

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

   
        

    }

    void slightShootHandler()
    {
        if (isMouseDown)
        {
            arrow.SetActive(true);
            
           // arrow.transform.eulerAngles = new Vector3(90, arrow.transform.eulerAngles.y, arrow.transform.eulerAngles.z);

            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 20;
            

            currentPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            currentPosition = center.position - Vector3.ClampMagnitude(currentPosition - center.position, maxLength);
            currentPosition = new Vector3(currentPosition.x * 4, currentPosition.y, currentPosition.z * 2);

            //arrow rotation
            Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 dir = Input.mousePosition - pos;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            arrow.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            arrow.transform.eulerAngles = new Vector3(90, arrow.transform.eulerAngles.y, arrow.transform.eulerAngles.z);

        }
    }

    private void OnMouseDown()
    {
        isMouseDown = true;
    }

    private void OnMouseUp()
    {
        isMouseDown = false;

        arrow.SetActive(false);
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
            initImpulse = new Vector3(currentPosition.x, 0, currentPosition.z);
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
