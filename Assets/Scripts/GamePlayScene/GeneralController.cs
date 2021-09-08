using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralController : MonoBehaviour
{
    public GameObject   puckObj;
    public GameObject   enemyObj;
    public GameObject[] blocksObj;

    public float m_Thrust;

    public bool shoot;
    public Vector3 initImpulse;

    public bool isMouseDown;
    public Vector3 currentPosition;
    public Transform center;
    public Transform idlePosition;

    public float maxLength;
    public float bottomBoundary;

    private void Start()
    {
       

     
    }
    
    private void Update()
    {
        // enemyObj.transform.position.x = new Vector3(enemyObj.transform.position.x, enemyObj.transform.position.y, enemyObj.transform.position.z);
        if (shoot == false)
        {
           // inputFinger();
        }

        slightShootHandler();
    }

    void slightShootHandler()
    {
        if (isMouseDown)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10;

            currentPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            currentPosition = center.position - Vector3.ClampMagnitude(currentPosition - center.position, maxLength);

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
            //initImpulse = new Vector3(4, 0, 10);
            initImpulse = new Vector3(currentPosition.x, 0, 10);
            puckObj.GetComponent<Rigidbody>().AddForce(initImpulse, ForceMode.Impulse);
            initImpulse = initImpulse;
            shoot = true;
        }
    }


}
