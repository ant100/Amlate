using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   // [SerializeField]
    private float speed = 4.5f;
    private bool firstMove;
    private bool isAtSquare;
    private float positionX;
    private float positionZ;

    private float initX;
    private float initZ;

    private float gridSize = 1.5f;
    private float movePerFrame;
    private float gridOffset = 0;

    private enum EnumMovingDir { None, Left,Right, Up, Down};
    private EnumMovingDir movingDir;

    void Start()
    {
        firstMove  = true;
        isAtSquare = true;
        movePerFrame = 0.01f * speed;
    }

    void Update()
    {
        movePlayer();
    }

    void movePlayer()
    {
        positionX = transform.position.x;
        positionZ = transform.position.z;

        if (Input.GetKey("left")) // z+ -> x-
        {
            movingDir = EnumMovingDir.Left;
            if (firstMove)
            {
                firstMove = false;
                initX = transform.position.x;
            }

            positionX -= movePerFrame;
        }
        else if (Input.GetKey("up")) // x+ -> z+
        {
            movingDir = EnumMovingDir.Up;
            if (firstMove)
            {
                firstMove = false;
                initZ = transform.position.z;
            }

            positionZ += movePerFrame;
        }
        else if (Input.GetKey("down")) // x- -> z-
        {
            movingDir = EnumMovingDir.Down;
            if (firstMove)
            {
                firstMove = false;
                initZ = transform.position.z;
            }

            positionZ -= movePerFrame;
        }
        else if (Input.GetKey("right")) // z- -> x+
        {
            movingDir = EnumMovingDir.Right;
            if (firstMove)
            {
                firstMove = false;
                initX = transform.position.x;
            }

            positionX += movePerFrame;
        }

        if (Input.GetKeyUp("right") || Input.GetKeyUp("left") || Input.GetKeyUp("up") || Input.GetKeyUp("down"))
        {
            float n = 0;
            float m = 0;
            firstMove = true;

            switch (movingDir)
            {
                case EnumMovingDir.None:
                    break;
                case EnumMovingDir.Left:
                    n = initX - transform.position.x;
                    m = n % gridSize;
                    break;
                case EnumMovingDir.Right:
                    n = transform.position.x - initX;
                    m = n % gridSize;
                    break;
                case EnumMovingDir.Up:
                    n = transform.position.z - initZ;
                    m = n % gridSize;
                    break;
                case EnumMovingDir.Down:
                    n = initZ - transform.position.z;
                    m = n % gridSize;
                    break;
                default:
                    break;
            }

            if(m != 0)
            {
                isAtSquare = false;
            }
            gridOffset = gridSize - m;
            //Debug.Log("needs to walk " + gridOffset);
        }

        if(!isAtSquare)
        {
            switch (movingDir)
            {
                case EnumMovingDir.None:
                    break;
                case EnumMovingDir.Left:
                    positionX -= movePerFrame;
                    gridOffset = gridOffset - movePerFrame;
                    //Debug.Log("movePerFrame " + movePerFrame);
                    //Debug.Log("left to walk " + gridOffset);
                    if (gridOffset <= 0)
                    {
                        isAtSquare = true;
                    }
                    break;
                case EnumMovingDir.Right:
                    positionX += movePerFrame;
                    gridOffset = gridOffset - movePerFrame;
                    if(gridOffset <= 0)
                    {
                        isAtSquare = true;
                    }
                    break;
                case EnumMovingDir.Up:
                    positionZ += movePerFrame;
                    gridOffset = gridOffset - movePerFrame;
                    //Debug.Log("movePerFrame " + movePerFrame);
                    //Debug.Log("left to walk " + gridOffset);
                    if (gridOffset <= 0)
                    {
                        isAtSquare = true;
                    }
                    break;
                case EnumMovingDir.Down:
                    positionZ -= movePerFrame;
                    gridOffset = gridOffset - movePerFrame;
                    if (gridOffset <= 0)
                    {
                        isAtSquare = true;
                    }
                    break;
                default:
                    break;
            }
        }

        transform.position = new Vector3(positionX, transform.position.y, positionZ);
    }
}
