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
        // すべてのマスの座標を二次元配列でいれておく
        // マスの位置情報と座標を対応させる
        // cubeから位置情報を貰っておく必要がありそう？
        // ここではマスかぶりを防ぐ

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
        // ここで移動させたい駒を選択する
        // 将来的にはここで移動可能かどうか調べる

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
