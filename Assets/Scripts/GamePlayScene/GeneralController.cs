using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralController : MonoBehaviour
{
    public static GeneralController instance { get; private set; }

    public GameObject   puckObj;
    public GameObject   arrow;
    public GameObject   playerObj;

    public GameObject[] blocksObj;


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
        InputController.instance.slightShootHandler(playerObj, puckObj);      // start Shot
        EnemyController.instance.EnemyAi();                                   // start enemy Ai
        GameStateController();                                                // start game state controller
    }


    // Game State check
    void GameStateController()
    {
        //Check active puck
        if (puckObj.activeInHierarchy == false)
            puckObj.SetActive(true);

        //Check active blocks 
        if (blocksObj[0].activeInHierarchy == false && blocksObj[1].activeInHierarchy == false  && blocksObj[2].activeInHierarchy == false)
        {
            EnemyController.instance.enemySpeed++;
            UiController.instance.levelScore.text = "Level " + EnemyController.instance.enemySpeed;
            for (int i = 0; i < blocksObj.Length; i++)
            {
                blocksObj[i].SetActive(true);
            }           
        }

        //Player Cube Clamp boards
        if (playerObj.transform.position.x <= -4)      
            playerObj.transform.position = new Vector3(-3.99f, playerObj.transform.position.y, playerObj.transform.position.z);
        if (playerObj.transform.position.x >= 4) 
            playerObj.transform.position = new Vector3(3.99f, playerObj.transform.position.y, playerObj.transform.position.z);
        if (playerObj.transform.position.z >= 5.4f)
            playerObj.transform.position = new Vector3(playerObj.transform.position.x, playerObj.transform.position.y, 5.39f);
        if (playerObj.transform.position.z <= -2.7f)
            playerObj.transform.position = new Vector3(playerObj.transform.position.x, playerObj.transform.position.y, -2.69f);
    }

}
