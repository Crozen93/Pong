using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public static InputController instance { get; private set; }

    [Header("bool")]
    [SerializeField] private bool shoot;                      //Bool - for detect shoot
    [SerializeField] private bool isMouseDown;                //Bool - for detect when mouse button down

    [Header("Vector3")]
    [SerializeField] private Vector3 currentPosition;         // main cords for calculate impulse and player position
    [SerializeField] private Vector3 playerPosition;          // player cords position
    [SerializeField] private Vector3 initImpulse;             // Vector3 - impulse rigidbody puck

    [SerializeField] private Transform center;
    [SerializeField] private Transform idlePosition;

    [Header("Line")]
    [SerializeField] private LineRenderer lr;                 // line renderer

    [Header("Float")]
    [SerializeField] private float maxLength;
    [SerializeField] private float bottomBoundary;



    private void Awake()
    {
        instance = this;
    }

    //Mouse button DOWN
    public void OnMouseDown()
    {
        isMouseDown = true;
    }

    //Mouse button UP
    public void OnMouseUp()
    {
        isMouseDown = false;
        GeneralController.instance.playerObj.SetActive(false);
        lr.enabled = false;

        impulsePuck();
        currentPosition = idlePosition.position;
        gameObject.GetComponent<BoxCollider>().enabled = false;
    }

    public void slightShootHandler(GameObject player, GameObject puck)
    {
        if (isMouseDown)
        {
            //Line
            lr.enabled = true;
            DrowLineRend(lr.gameObject, player.transform.position, puck.transform.position, 0.2f); // Draw Line

            player.SetActive(true);

            //get mouse position
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 22;

            // calculation player position Vector3
            playerPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            currentPosition = player.transform.position;
            currentPosition = center.position + Vector3.ClampMagnitude(currentPosition - center.position, maxLength);
            currentPosition = new Vector3(currentPosition.x * 4, currentPosition.y, currentPosition.z * 8);


            // player cube transform position and rotation
            player.transform.LookAt(GeneralController.instance.puckObj.transform);
            player.transform.position = new Vector3(playerPosition.x, -1.88f, playerPosition.z);
            
        }
    }
    
    //impulse puck - rigidbody
    void impulsePuck()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (currentPosition.z <= 2)
            {
                initImpulse = new Vector3(currentPosition.x, 0, currentPosition.z + 15f);
            }
            else
            {
                initImpulse = new Vector3(currentPosition.x, 0, currentPosition.z);
            }

            GeneralController.instance.puckObj.GetComponent<Rigidbody>().AddForce(initImpulse, ForceMode.Impulse);
            shoot = true;
        }
    }

    //Mathf Clamp
    Vector3 ClampBoundary(Vector3 vector)
    {
        vector.y = Mathf.Clamp(vector.y, bottomBoundary, 1000);
        return vector;
    }

    //Draw Line
    void DrowLineRend(GameObject line, Vector3 start, Vector3 end, float duration = 0.2f)
    {
        line.transform.position = start;
        LineRenderer lr = line.GetComponent<LineRenderer>();

        lr.SetPosition(0, start);   // set start line position
        lr.SetPosition(1, end);     // set end line position
        lr.SetWidth(0.2f, 0.2f);    // set line width
    }

 
}
