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

        //���C���J�����̌�������Ɍ�������
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.rotation.eulerAngles, new Vector3(1, 1, 1));
        transform.eulerAngles = cameraForward;


    }
}
