using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dodec : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //rotate attached object 1 whole rotation every second
        transform.Rotate(0, 0, 360 * Time.deltaTime/5);
    }

    //when Main Camera collides the trigger activates and Dodec locks to the center of the dodec
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "MainCamera")
        {
            // lock MainCamera to BackEnd
            other.transform.parent = GameObject.FindWithTag("BackEnd").transform;
        }
        //remove FPSController component from object tagged MainCamera
        if (other.gameObject.tag == "MainCamera")
        {
            Destroy(other.gameObject.GetComponent<FPSController>());
        }
        
        //remove boid component from tag FrontEnd when MainCamera is triggered
        if (other.gameObject.tag == "MainCamera")
        {
            Destroy(GameObject.FindWithTag("FrontEnd").GetComponent<Boid>());
            Destroy(GameObject.FindWithTag("FrontEnd").GetComponent<NoiseWander>());

        }
        //add FPSController component from tag FrontEnd when MainCamera is triggered
        if (other.gameObject.tag == "MainCamera")
        {
            GameObject.FindWithTag("FrontEnd").AddComponent<FPSController>();
        }

    }
    void OnTriggerStay(Collider other)
    {
       //smoothly rotate the MainCamera to match the BackEnd
        if (other.gameObject.tag == "MainCamera")
        {
            other.transform.rotation = Quaternion.Slerp(other.transform.rotation, GameObject.FindWithTag("BackEnd").transform.rotation, Time.deltaTime * 1);
        }

        //smoothly move tag MainCamera to center of dodec
        if (other.gameObject.tag == "MainCamera")
        {
            other.transform.position = Vector3.Lerp(other.transform.position, GameObject.FindWithTag("Dodec").transform.position, Time.deltaTime * 1);
        }

    }
    void OnTriggerExit(Collider other)
    {   //remove tag MainCamera from parent
        if (other.gameObject.tag == "MainCamera")
        {
            other.transform.parent = null;
        }

    }
}
