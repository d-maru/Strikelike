using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SelectSceneSetup : MonoBehaviour
{
    // �����z�u���t�@�C������ǂݍ��ޏꍇ�̃t�@�C����
    public string defaultCubeMapFile = "defaultCubeMap.csv";

    // �t�@�C������ǂݍ��܂Ȃ�/�ǂݍ��߂Ȃ��ꍇ�̃}�b�v�T�C�Y
    public int mapSize = 5;

    // �L���[�u1������̑傫��
    public float cubeSize = 1.5f;

    // �L���[�u�z�u�̊�_�ʒu
    public Vector3 defaultPosition = new (-3.526728f, 7.115623f, 2.859495f);

    // �L���[�u�̎�ސ�
    public int cubePrefabTypeCount = 2;
    public GameObject prefab_Cube;
    public GameObject prefab_WaterCube;


    // �e�ƂȂ�{�[�h�I�u�W�F�N�g
    public GameObject gameBoard;

    /// <summary>
    /// �L���[�u�̔z�u�̃��X�g�𐶐�����
    /// �t�@�C����ǂݍ���Ń}�b�s���O��2�������X�g�ŕԋp����
    /// </summary>
    /// <returns>�t�@�C������ǂݍ��񂾃L���[�u�̃}�b�s���O���L�ڂ���int�^2�������X�g</returns>
    public List<List<int>> MakeCubeList()
    {
        // �����z�u�̃L���[�u���X�g
        List<List<int>> cubeLists = new();

        try
        {
            // �t�@�C���ǂݍ���
            if (File.Exists(Application.dataPath + "/" + defaultCubeMapFile))
            {
                using StreamReader sr = new(Application.dataPath + "/" + defaultCubeMapFile, System.Text.Encoding.UTF8);

                // �����܂ŌJ��Ԃ�
                while (!sr.EndOfStream)
                {
                    // 1�s���ǂݍ���ŃJ���}��؂�Ń��X�g�Ɋi�[����
                    List<string> stringList = new(sr.ReadLine().Split(','));
                    cubeLists.Add(stringList.ConvertAll(x => int.Parse(x)));
                }
            }
            else
            {
                // �t�@�C����������ΑS��-1
                List<int> cubeListOneRow = new(System.Linq.Enumerable.Repeat<int>(-1, mapSize));
                for (int index = 0; index < mapSize; index++)
                {
                    cubeLists.Add(cubeListOneRow);
                }
            }
        }
        catch
        {
            // ���X�g�������
            cubeLists.Clear();

            // �t�@�C���ǂݍ��݂�^�ϊ��Ɏ��s������S��-1
            List<int> cubeListOneRow = new(System.Linq.Enumerable.Repeat<int>(-1, mapSize));
            for (int index = 0; index < mapSize; index++)
            {
                cubeLists.Add(cubeListOneRow);
            }
        }
        return cubeLists;
    }

    private void Awake()
    {
        // �{�[�h�̉������|��
        foreach (Transform n in gameBoard.transform)
        {
            GameObject.Destroy(n.gameObject);
        }

        // �t�@�C������ǂݍ���Ń��X�g�쐬
        List<List<int>> cubeLists = MakeCubeList();

        // �L���[�u�A���p�̃L���[�u���X�g
        List<CubeBase> CubeBaseList = new();

        // �e�s�Ƀ��[�v
        int rowIndex = 0;
        foreach (List<int> cubeList in cubeLists)
        {
            // �i��Ƀ��[�v
            int colIndex = 0;
            foreach (int cubeType in cubeList)
            {
                // �z�u����L���[�u
                GameObject cube=null;

                // �l���������Ȃ���΃����_��
                int cubeTypeValid = cubeType;
                if (cubeType < 0 || cubeType > cubePrefabTypeCount)
                {
                    cubeTypeValid = Random.Range(0, cubePrefabTypeCount + 1);
                }
                switch (cubeTypeValid)
                {
                    // 1�̎���Cube
                    case 1:
                        cube = Instantiate(prefab_Cube, new Vector3(defaultPosition.x + rowIndex * cubeSize, defaultPosition.y-8, defaultPosition.z + colIndex * cubeSize), Quaternion.identity, gameBoard.transform);
                        break;
                    // 2�̎���WaterCube
                    case 2:
                        cube = Instantiate(prefab_WaterCube, new Vector3(defaultPosition.x + rowIndex * cubeSize, defaultPosition.y-8, defaultPosition.z + colIndex * cubeSize), Quaternion.identity , gameBoard.transform);
                        break;
                    // 0�̎��͋�
                    case 0:
                        break;
                }

                // ���O��ݒ�
                if(cube is not null)
                    cube.name = "Cube_" + rowIndex + "_" + colIndex;

                // �אڃL���[�u�ݒ�
                CubeBase cubeBase = cube?.GetComponent<CubeBase>();
                if(cubeBase is not null){

                    // �אڃL���[�u�̏����l�Ƃ���null��ݒ�
                    cubeBase.AdjacentCubes = new() { { Direction.North, null }, { Direction.East, null }, { Direction.West, null }, { Direction.South, null } };
                    
                    if (rowIndex > 0)
                    {
                        CubeBase linkCube = CubeBaseList[(rowIndex - 1) * mapSize + colIndex]?.GetComponent<CubeBase>();
                        if(linkCube is not null)
                        {
                            cubeBase.AdjacentCubes[Direction.North] = linkCube;
                            linkCube.AdjacentCubes[Direction.South] = cubeBase;
                        }
                    }
                    if (colIndex > 0)
                    {
                        CubeBase linkCube = CubeBaseList[rowIndex * mapSize + colIndex - 1]?.GetComponent<CubeBase>();
                        if (linkCube is not null)
                        {
                            cubeBase.AdjacentCubes[Direction.West] = linkCube;
                            linkCube.AdjacentCubes[Direction.East] = cubeBase;
                        }
                    }
                }
                CubeBaseList.Add(cubeBase);
                colIndex++;
            }
            rowIndex++;
        }

    }
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
