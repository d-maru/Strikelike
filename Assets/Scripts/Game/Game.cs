using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public Player User;
    public CPU Cpu;
    public BoardManager board;
    private bool isPlayerTurn = true;

    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("DogPolyart (1)").GetComponent<PieceBase>().OnCube = GameObject.Find("Cube_2_1").GetComponent<CubeBase>();
        GameObject.Find("DogPolyart (2)").GetComponent<PieceBase>().OnCube = GameObject.Find("Cube_1_1").GetComponent<CubeBase>();
        GameObject.Find("OpponenntDog (1)").GetComponent<PieceBase>().OnCube = GameObject.Find("Cube_0_3").GetComponent<CubeBase>();
        GameObject.Find("OpponenntDog (2)").GetComponent<PieceBase>().OnCube = GameObject.Find("Cube_3_3").GetComponent<CubeBase>();

        GameObject.Find("Cube_2_1").GetComponent<CubeBase>().Piece = GameObject.Find("DogPolyart (1)").GetComponent<PieceBase>();
        GameObject.Find("Cube_1_1").GetComponent<CubeBase>().Piece = GameObject.Find("DogPolyart (2)").GetComponent<PieceBase>();
        GameObject.Find("Cube_0_3").GetComponent<CubeBase>().Piece = GameObject.Find("OpponenntDog (1)").GetComponent<PieceBase>();
        GameObject.Find("Cube_3_3").GetComponent<CubeBase>().Piece = GameObject.Find("OpponenntDog (2)").GetComponent<PieceBase>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerTurn == true)
        {
            var played = User.Play();
            if (played)
            {
                isPlayerTurn = false;
            }
        }
        else
        {
            var played = Cpu.Play();
            if (played)
            {
                isPlayerTurn = true;
            }
        }
    }
}