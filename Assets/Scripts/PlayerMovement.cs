using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 3.0f;
    [SerializeField] private string punchKey = "x";

    private bool firstMove;
    private bool isAtSquare;
    private float positionX;
    private float positionZ;
    private float initX;
    private float initZ;

    private float gridSize = 3f;
    private float movePerFrame;
    private float gridOffset = 0;

    private Animator animator;

    private bool hitEnded = true;

    private enum EnumMovingDir { None, Left,Right, Up, Down};
    private EnumMovingDir movingDir;

    void Start()
    {
        animator     = GetComponent<Animator>();
        firstMove    = true;
        isAtSquare   = true;
        movePerFrame = 0.01f * speed;
    }

    void Update()
    {
        if (hitEnded)
            movePlayer();
    }

    void movePlayer()
    {
        positionX = transform.position.x;
        positionZ = transform.position.z;

        if (Input.GetKey("left") && !Input.GetKey("right") && !Input.GetKey("up") && !Input.GetKey("down") && !Input.GetKey(punchKey)) // z+ -> x-
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, -90f, transform.rotation.z);
            movingDir = EnumMovingDir.Left;
            if (firstMove)
            {
                firstMove = false;
                initX = transform.position.x;
                PlayAnimation();
            }

            positionX -= movePerFrame;
        }
        else if (Input.GetKey("up") && !Input.GetKey("right") && !Input.GetKey("left") && !Input.GetKey("down") && !Input.GetKey(punchKey)) // x+ -> z+
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, 0f, transform.rotation.z);
            movingDir = EnumMovingDir.Up;
            if (firstMove)
            {
                firstMove = false;
                initZ = transform.position.z;
                PlayAnimation();
            }

            positionZ += movePerFrame;
        }
        else if (Input.GetKey("down") && !Input.GetKey("right") && !Input.GetKey("up") && !Input.GetKey("left") && !Input.GetKey(punchKey)) // x- -> z-
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, 180f, transform.rotation.z);
            movingDir = EnumMovingDir.Down;
            if (firstMove)
            {
                firstMove = false;
                initZ = transform.position.z;
                PlayAnimation();
            }

            positionZ -= movePerFrame;
        }
        else if (Input.GetKey("right") && !Input.GetKey("left") && !Input.GetKey("up") && !Input.GetKey("down") && !Input.GetKey(punchKey)) // z- -> x+
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, 90f, transform.rotation.z);
            movingDir = EnumMovingDir.Right;

            if (firstMove)
            {
                firstMove = false;
                initX = transform.position.x;
                PlayAnimation();
            }

            positionX += movePerFrame;
        }
        //else if((!Input.GetKey("left")  && !Input.GetKey("right"))  || (Input.GetKey("left") && Input.GetKey("right"))  ||
        //        (!Input.GetKey("left")  && !Input.GetKey("up"))     || (Input.GetKey("left") && Input.GetKey("up"))     ||
        //        (!Input.GetKey("left")  && !Input.GetKey("down"))   || (Input.GetKey("left") && Input.GetKey("down"))   ||
        //        (!Input.GetKey("right") && !Input.GetKey("up"))     || (Input.GetKey("left") && Input.GetKey("up"))     ||
        //        (!Input.GetKey("left")  && !Input.GetKey("down"))   || (Input.GetKey("left") && Input.GetKey("down"))   ||
        //        (!Input.GetKey("up")    && !Input.GetKey("down"))   || (Input.GetKey("up")   && Input.GetKey("down")))
        else
        {
            if (movingDir == EnumMovingDir.None) return;

            movingDir = EnumMovingDir.None;
            animator.SetTrigger("Idle");
            firstMove = true;
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
            else
            {
                animator.SetTrigger("Idle");
            }
            gridOffset = gridSize - m;
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
                    if (gridOffset <= 0)
                    {
                        isAtSquare = true;
                        animator.SetTrigger("Idle");
                    }
                    break;
                case EnumMovingDir.Right:
                    positionX += movePerFrame;
                    gridOffset = gridOffset - movePerFrame;
                    if(gridOffset <= 0)
                    {
                        isAtSquare = true;
                        animator.SetTrigger("Idle");
                    }
                    break;
                case EnumMovingDir.Up:
                    positionZ += movePerFrame;
                    gridOffset = gridOffset - movePerFrame;
                    if (gridOffset <= 0)
                    {
                        isAtSquare = true;
                        animator.SetTrigger("Idle");
                    }
                    break;
                case EnumMovingDir.Down:
                    positionZ -= movePerFrame;
                    gridOffset = gridOffset - movePerFrame;
                    if (gridOffset <= 0)
                    {
                        isAtSquare = true;
                        animator.SetTrigger("Idle");
                    }
                    break;
            }
        }

        Vector3 movement = new Vector3(positionX, transform.position.y, positionZ);
        transform.rotation = Quaternion.LookRotation(transform.forward, Vector3.up);
        transform.position = movement;
    }

    private void EndHit()
    {
        hitEnded = true;
        animator.SetTrigger("Idle");
    }

    private void StartHit()
    {
        hitEnded = false;
    }

    private void PlayAnimation()
    {
        animator.SetTrigger("Walking");
    }
}
