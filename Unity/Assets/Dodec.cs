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
        
        //Set tag FrontEnd boid component control variable to true when MainCamera is triggered
        if (other.gameObject.tag == "MainCamera")
        {
            GameObject.FindWithTag("FrontEnd").GetComponent<Boid>().control = true;
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

        //smoothly move MainCamera to center of dodec
        if (other.gameObject.tag == "MainCamera")
        {
            other.transform.position = Vector3.Lerp(other.transform.position, GameObject.FindWithTag("Dodec").transform.position, Time.deltaTime * 2);
        }

        //if z key is pressed on keyboard remove all parents from Maincamera, reable boid control, swap control of the camera
        if (GameObject.FindWithTag("FrontEnd").GetComponent<Boid>().control == true && Input.GetKey(KeyCode.Z)){
            GameObject.FindWithTag("MainCamera").transform.parent = null;
            GameObject.FindWithTag("FrontEnd").GetComponent<Boid>().control = false;
            GameObject.FindWithTag("MainCamera").AddComponent<FPSController>();
            Destroy(GameObject.FindWithTag("FrontEnd").GetComponent<FPSController>());
        }
        
        //DO A BARREL ROLL!
        if (GameObject.FindWithTag("FrontEnd").GetComponent<Boid>().control == true && Input.GetKey(KeyCode.B)){
            
            GameObject.FindWithTag("FrontEnd").transform.Rotate(0, 0, 360 * Time.deltaTime/5);
            GameObject.FindWithTag("BackEnd").transform.Rotate(0, 0, 360 * Time.deltaTime/5);
            // GameObject.FindWithTag("FrontEnd").GetComponent<SpineAnimator>().bones[1].transform.Rotate(0, 0, 360 * Time.deltaTime/5);
            // GameObject.FindWithTag("FrontEnd").GetComponent<SpineAnimator>().bones[2].transform.Rotate(0, 0, 360 * Time.deltaTime/5);
            // GameObject.FindWithTag("FrontEnd").GetComponent<SpineAnimator>().bones[3].transform.Rotate(0, 0, 360 * Time.deltaTime/5);

            // GameObject.FindWithTag("MainCamera").transform.RotateAround(GameObject.FindWithTag("BackEnd").transform.position, GameObject.FindWithTag("FrontEnd").transform.forward, 60 * Time.deltaTime);
            GameObject.FindWithTag("Dodec").transform.RotateAround(GameObject.FindWithTag("BackEnd").transform.position, GameObject.FindWithTag("FrontEnd").transform.forward, 80 * Time.deltaTime);
        }
    }
    void OnTriggerExit(Collider other)
    {   //remove MainCamera from parent
        if (other.gameObject.tag == "MainCamera")
        {
            other.transform.parent = null;
        }
        if (other.gameObject.tag == "MainCamera")
        {
            GameObject.FindWithTag("FrontEnd").GetComponent<Boid>().control = false;
        }

    }
}
