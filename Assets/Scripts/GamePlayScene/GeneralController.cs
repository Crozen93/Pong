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

    private void Start()
    {
       

     
    }
    
    private void Update()
    {
        

        slightShootHandler();
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
     //   vector.y = Mathf.Clamp(vector.y, bottomBoundary, 1000);
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


}
