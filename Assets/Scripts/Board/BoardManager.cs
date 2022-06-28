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
        // ‚±‚±‚ÅˆÚ“®‚³‚¹‚½‚¢‹î‚ğ‘I‘ğ‚·‚é
        // «—ˆ“I‚É‚Í‚±‚±‚ÅˆÚ“®‰Â”\‚©‚Ç‚¤‚©’²‚×‚é

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider.CompareTag("Piece"))
                {
                    pieceSlected = true;
                    piece = hit.collider.gameObject;
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
