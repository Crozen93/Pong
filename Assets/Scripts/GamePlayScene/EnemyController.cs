using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public static EnemyController instance { get; private set; }
    [SerializeField] private GameObject enemyObj;
    public float enemySpeed;                        //enemy speed

    private void Awake()
    {
        instance = this;
    }

    public void EnemyAi()
    {
        float step = enemySpeed * Time.deltaTime;
        enemyObj.transform.position = Vector3.MoveTowards(new Vector3(enemyObj.transform.position.x, -2.15f, 11.87f), GeneralController.instance.puckObj.transform.position, step);
    }
}
