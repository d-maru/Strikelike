using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public GameObject piece;
    public bool on = true;


    // Start is called before the first frame update
    void Start()
    {
        // ���ׂẴ}�X�̍��W��񎟌��z��ł���Ă���
        // �}�X�̈ʒu���ƍ��W��Ή�������
        // cube����ʒu�������Ă����K�v�����肻���H
        // �����ł̓}�X���Ԃ��h��

        int?[][]cubeMap =
        {
            new int?[]{null, null, null, null},
            new int?[]{null, null, null, null},
            new int?[]{null, null, null, null},
            new int?[]{null, null, null, null}
        };
    }

    // Update is called once per frame
    void Update()
    {
        // �����ňړ������������I������
        // �����I�ɂ͂����ňړ��\���ǂ������ׂ�

        if (Input.GetMouseButtonDown(0) && on == true)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Piece"))
            {
                Debug.Log("aaa");
                on = false;
            }
        }

        if (Input.GetMouseButtonDown(0) && on == false)
        {
            Ray raySecond = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitSecond;
            Debug.Log("bbb");

            if (Physics.Raycast(raySecond, out hitSecond) && hitSecond.collider.CompareTag("Cube"))
            {
                Debug.Log("ccc");
                float x = Mathf.RoundToInt(hitSecond.point.x);
                float z = Mathf.RoundToInt(hitSecond.point.z);
                piece.transform.position = new Vector3(x, 0, z);
                on = true;
            }
        }
    }
}
