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

    //when Main Camera collides the trigger activates and Dodec locks to the centerof the dodec
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "MainCamera")
        {
            other.transform.parent = transform;
        }
    }
}
