using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Model;


public class buttonVRleft : MonoBehaviour
{
    public GameObject button;
    public UnityEvent onPress;
    public UnityEvent onRelease;
    private BoardGame boardGame;
    private Square[,] matrix;
    private GameObject[,] cubesMatrix;
    GameObject presser;
    bool isPressed;

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
    public void move()
    {
        boardGame.changeDirection(Square.LEFT);
    }
}
