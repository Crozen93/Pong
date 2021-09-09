using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            gameObject.SetActive(false);
            gameObject.transform.position = new Vector3(0.14f, -2.5f, -1.579f);
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            GeneralController.instance.GetComponent<BoxCollider>().enabled = true;
        }
        if (collision.gameObject.tag == "Block")
        {
            gameObject.SetActive(false);
            collision.gameObject.SetActive(false);
            gameObject.transform.position = new Vector3(0.14f, -2.5f, -1.579f);
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
            GeneralController.instance.GetComponent<BoxCollider>().enabled = true;
        }
    }
}
