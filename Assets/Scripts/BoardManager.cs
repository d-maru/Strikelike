using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public GameObject piece;
    public bool pieceSlected = false;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // �����ňړ������������I������
        // �����I�ɂ͂����ňړ��\���ǂ������ׂ�

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Piece"))
                {
                    pieceSlected = true;
                }
                else if(hit.collider.CompareTag("Cube") && pieceSlected)
                {
                    pieceSlected = false;
                    float x = Mathf.RoundToInt(hit.point.x);
                    float z = Mathf.RoundToInt(hit.point.z);
                    piece.transform.position = new Vector3(x, 0, z);
                }
            }
        }
    }
}
