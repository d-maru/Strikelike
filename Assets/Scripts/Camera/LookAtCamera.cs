using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // ���[���h���W����ɁA��]���擾
        Vector3 worldAngle = transform.eulerAngles;
        worldAngle.x= Camera.main.transform.rotation.eulerAngles.x;

        transform.eulerAngles = worldAngle;


    }
}
