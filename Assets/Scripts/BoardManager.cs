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
        // ここで移動させたい駒を選択する
        // 将来的にはここで移動可能かどうか調べる

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider.CompareTag("Piece"))
                {
                    pieceSlected = true;
                }
                else if(hit.collider.CompareTag("Cube") && pieceSlected)
                {
                    pieceSlected = false;
                    float x = hit.collider.gameObject.transform.position.x;
                    float z = hit.collider.gameObject.transform.position.z;
                    piece.transform.position = new Vector3(x, 0, z);
                }
            }
        }
    }
}
