using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 3.0f;
    private bool firstMove;
    private bool isAtSquare;
    private float positionX;
    private float positionZ;
    private float initX;
    private float initZ;

    private float gridSize = 1.5f;
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

        if (Input.GetKey("left")) // z+ -> x-
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, -90f, transform.rotation.z);
            //animator.SetTrigger("Walking");
            movingDir = EnumMovingDir.Left;
            if (firstMove)
            {
                firstMove = false;
                initX = transform.position.x;
                PlayAnimation();
            }

            positionX -= movePerFrame;
        }
        else if (Input.GetKey("up")) // x+ -> z+
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, 0f, transform.rotation.z);
            //animator.SetTrigger("Walking");
            movingDir = EnumMovingDir.Up;
            if (firstMove)
            {
                firstMove = false;
                initZ = transform.position.z;
                PlayAnimation();
            }

            positionZ += movePerFrame;
        }
        else if (Input.GetKey("down")) // x- -> z-
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, 180f, transform.rotation.z);
            //animator.SetTrigger("Walking");
            movingDir = EnumMovingDir.Down;
            if (firstMove)
            {
                firstMove = false;
                initZ = transform.position.z;
                PlayAnimation();
            }

            positionZ -= movePerFrame;
        }
        else if (Input.GetKey("right")) // z- -> x+
        {
            //animator.SetTrigger("Walking");
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
                animator.ResetTrigger("Walking");
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
                        animator.ResetTrigger("Walking");
                        animator.SetTrigger("Idle");
                    }
                    break;
                case EnumMovingDir.Right:
                    positionX += movePerFrame;
                    gridOffset = gridOffset - movePerFrame;
                    if(gridOffset <= 0)
                    {
                        isAtSquare = true;
                        animator.ResetTrigger("Walking");
                        animator.SetTrigger("Idle");
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
                        animator.ResetTrigger("Walking");
                        animator.SetTrigger("Idle");
                    }
                    break;
                case EnumMovingDir.Down:
                    positionZ -= movePerFrame;
                    gridOffset = gridOffset - movePerFrame;
                    if (gridOffset <= 0)
                    {
                        isAtSquare = true;
                        animator.ResetTrigger("Walking");
                        animator.SetTrigger("Idle");
                    }
                    break;
                default:
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
