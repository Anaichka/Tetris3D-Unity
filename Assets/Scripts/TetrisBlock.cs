using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisBlock : MonoBehaviour
{
    // time the last block has been falling down once
    float prevTime;
    float fallTime = 1f;

    void Start()
    {
        InputHolder.instance.SetActiveBlock(gameObject, this);
        fallTime = GameManager.instance.ReadFallSpeed();
        if (!CheckValidMove())
        {
            GameManager.instance.SetGameIsOver();
        }
    }

   
    void Update()
    {
        if (Time.time - prevTime > fallTime)
        {

            transform.position += Vector3.down;

            if (!CheckValidMove())
            {
                transform.position += Vector3.up;

                // delete layer if possible
                PlayfieldGrid.instance.DeleteLayer();
                enabled = false;

                // create a new tetris block
                if (!GameManager.instance.ReadGameIsOver())
                {
                    PlayfieldGrid.instance.SpawnNewBlock();
                }
            }
            else
            {
                // upd

                PlayfieldGrid.instance.UpdateGrid(this);
            }
            

            prevTime = Time.time;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SetInput(Vector3.left);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SetInput(Vector3.right);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            SetInput(Vector3.forward);
            //SetRotationInput(new Vector3(90, 0, 0));
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            SetInput(Vector3.back);
            //SetRotationInput(new Vector3(-90, 0, 0));
        }
    }

    public void SetInput(Vector3 dir)
    {
        transform.position += dir;

        if (!CheckValidMove())
        {
            transform.position -= dir;
        } 
        else
        {
            PlayfieldGrid.instance.UpdateGrid(this);
        }
    } 

    public void SetRotationInput(Vector3 rot)
    {
        transform.Rotate(rot, Space.World);

        if (!CheckValidMove())
        {
            transform.Rotate(-rot, Space.World);
        } 
        else
        {
            PlayfieldGrid.instance.UpdateGrid(this);
        }
    }

    bool CheckValidMove()
    {
        foreach(Transform child in transform)
        {
            // store each vector of each child and round it's position
            Vector3 pos = PlayfieldGrid.instance.Round(child.position);
            if (!PlayfieldGrid.instance.CheckInsideGrid(pos))
            {
                return false;
            }
        }

        foreach (Transform child in transform)
        {
            Vector3 pos = PlayfieldGrid.instance.Round(child.position);
            Transform t = PlayfieldGrid.instance.GetTransformOnGridPos(pos);

            if (t != null && t.parent != transform)
            {
                return false;
            }
        }

        return true;
    }

    public void SetSpeed()
    {
        fallTime = 0.1f;
    }
}
