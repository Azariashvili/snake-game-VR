using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Model;


public class startGame : MonoBehaviour
{
    public GameObject button;
    private float update;

    private bool rotate;
    public UnityEvent onPress;
    public UnityEvent onRelease;
    private BoardGame boardGame;
    private Square[,] matrix;
    private GameObject[,] cubesMatrix;
    GameObject presser;
    bool isPressed;
    XRIDefaultInputActions actions;


    // Start is called before the first frame update
    void Start()
    {
        isPressed = false;
    }

    private void onTriggerEnter(Collider other)
    {
        if (!isPressed)
        {
            button.transform.localPosition = new Vector3(1, 1, 1);
            presser = other.gameObject;
            onPress.Invoke();
            isPressed = true;
        }
    }

    private void onTriggerExit(Collider other)
    {
        if (other.gameObject == presser)
        {
            button.transform.localPosition = new Vector3(0, 0.015f, 0);
            onPress.Invoke();
            isPressed = true;
        }
    }

    public void start()
    {

        boardGame = new BoardGame();
        cubesMatrix = new GameObject[boardGame.getBoardGameSize(), boardGame.getBoardGameSize()];
        matrix = boardGame.getBoardGame();

        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                Square cube = matrix[i, j];
                GameObject cubeObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cubeObject.transform.localPosition = new Vector3(cube.getPosX(), cube.getPosY(), -1);
                cubeObject.transform.localScale = new Vector3(cube.getSize(), cube.getSize(), cube.getSize());
                cubeObject.GetComponent<MeshRenderer>().material.color = getColor(cube.getCurrentColor());
                cubesMatrix[i, j] = cubeObject;
            }
        }
    }
    public Color getColor(char c)
    {
        if (c == Square.BLACK)
            return Color.black;

        if (c == Square.DARK_GRAY)
            return new Color((20 / 255.0f), (20 / 255.0f), (20 / 255.0f));

        if (c == Square.DARK_GREEN)
            return new Color((53 / 255.0f), (121 / 255.0f), (33 / 255.0f));

        if (c == Square.GREEN)
            return new Color((158 / 255.0f), (178 / 255.0f), (55 / 255.0f));

        if (c == Square.RED)
            return new Color((185 / 255.0f), (46 / 255.0f), (77 / 255.0f));

        return Color.red;
    }

    private void OnEnable()
    {
        actions.Enable();
    }

    private void Awake()
    {
        update = 0.0f;
        actions = new XRIDefaultInputActions();
    }

    public void Update()
    {
        update += Time.deltaTime;
        if (update > 0.3f)
        {
            update = 0.0f;
            if (!rotate)
                boardGame.move();
        }


        if (rotate)
        {
            transform.Rotate(transform.up * Time.deltaTime * 45f, Space.Self);
        }

        if (cubesMatrix != null)
        {
            for (int i = 0; i < cubesMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < cubesMatrix.GetLength(1); j++)
                {
                    Square cube = matrix[i, j];
                    GameObject cubeObject = cubesMatrix[i, j];
                    cubeObject.GetComponent<MeshRenderer>().material.color = getColor(cube.getCurrentColor());


                }
            }
        }
    }
}
