using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookY : MonoBehaviour
{
    [SerializeField]
    private float _sens = 3.5f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float mouseY = Input.GetAxis("Mouse Y");

        Vector3 newRotation = transform.localEulerAngles;
        newRotation.x += mouseY * _sens;

        float rotX = newRotation.x;
        if (rotX > 85)
        {
            if (rotX < 100)
            {
                newRotation.x = 85;
            }
            else
            {
                if (rotX < 275)
                {
                    newRotation.x = 275;
                }
            }
            
        }
       
 
        transform.localEulerAngles = newRotation;

    }
}
