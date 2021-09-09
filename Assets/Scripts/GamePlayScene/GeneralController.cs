using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralController : MonoBehaviour
{
    public GameObject   puckObj;
    public GameObject   enemyObj;
    public GameObject   arrow;

    public GameObject[] blocksObj;


    public bool shoot;
    public bool isMouseDown;

    public Vector3 initImpulse;

    public Vector3 currentPosition;
    public Transform center;
    public Transform idlePosition;

    public float maxLength;
    public float bottomBoundary;
    public float puckSpeed;
    public float enemySpeed;

    private void Start()
    {
        

        UiController.instance.startGameButton.onClick.AddListener(()    => UiController.instance.StartGameHandler());
        UiController.instance.exitGameButton.onClick.AddListener(()     => UiController.instance.ExitGameHandler());
        UiController.instance.menuGameButton.onClick.AddListener(()     => UiController.instance.pauseMenuHandler());
        UiController.instance.resumeGameButton.onClick.AddListener(()   => UiController.instance.ResumeGameHandler());
        UiController.instance.exit2GameButton.onClick.AddListener(()    => UiController.instance.ExitGameHandler());
    }
    
    private void Update()
    {
        

        slightShootHandler();

        EnemyAi();
        GameStateController();
    }

    void slightShootHandler()
    {
        if (isMouseDown)
        {
            arrow.SetActive(true);
            arrow.transform.eulerAngles = Input.mousePosition;
           // arrow.transform.eulerAngles = new Vector3(90, arrow.transform.eulerAngles.y, arrow.transform.eulerAngles.z);

            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10;

            currentPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            currentPosition = center.position - Vector3.ClampMagnitude(currentPosition - center.position, maxLength);

          //  arrow.transform.eulerAngles = Vector3.ClampMagnitude(currentPosition, 10);

           currentPosition = ClampBoundary(currentPosition);
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
            initImpulse = new Vector3(currentPosition.x, 0, puckSpeed);
            puckObj.GetComponent<Rigidbody>().AddForce(initImpulse, ForceMode.Impulse);
            shoot = true;
        }
    }

    void EnemyAi()
    {
        float step = enemySpeed * Time.deltaTime;
        enemyObj.transform.position = Vector3.MoveTowards(new Vector3(enemyObj.transform.position.x, -2.15f, 11.87f), puckObj.transform.position, step);
    }

    void GameStateController()
    {
        if (puckObj.activeInHierarchy == false)
        {
            puckObj.SetActive(true);
        }

        if (blocksObj[0].activeInHierarchy == false && blocksObj[1].activeInHierarchy == false  && blocksObj[2].activeInHierarchy == false)
        {
            enemySpeed++;
            UiController.instance.levelScore.text = "Level " + enemySpeed;
            for (int i = 0; i < blocksObj.Length; i++)
            {
                blocksObj[i].SetActive(true);
            }
            
        }
    }
}
