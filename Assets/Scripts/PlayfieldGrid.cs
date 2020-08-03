using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayfieldGrid : MonoBehaviour
{
    public static PlayfieldGrid instance;

    public int gridSizeX, gridSizeY, gridSizeZ;
    [Header("Blocks")]
    public GameObject[] blockList;
    public GameObject[] ghostList;
    
    [Header("Playfield Settings")]
    [SerializeField] GameObject bottomPlane;
    [SerializeField] GameObject wallE, wallN, wallW, wallS;

    int randomIndex;

    public Transform[,,] theGrid;

    private void Awake()
    {
        instance = this;
    }


    private void Start()
    {
        theGrid = new Transform[gridSizeX, gridSizeY, gridSizeZ];
        CalculatePreview();
        SpawnNewBlock();
    }
    public Vector3 Round(Vector3 v)
    {
        return new Vector3(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.y), Mathf.RoundToInt(v.z));
    }


    public bool CheckInsideGrid(Vector3 pos)
    {
        return ((int)pos.x >= 0 && (int)pos.x < gridSizeX && (int)pos.z >=0 && (int)pos.z < gridSizeZ && 
                 (int)pos.y >= 0); 
    }

    public void UpdateGrid(TetrisBlock block)
    {
        
        // delete all possible parent objects
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int z = 0; z < gridSizeZ; z++)
            {
                for (int y = 0; y < gridSizeY; y++)
                {
                    if (theGrid[x, y, z] != null)
                    {
                        if (theGrid[x, y, z].parent == block.transform)
                        {
                            // empty this out
                            theGrid[x, y, z] = null;
                        }
                    }
                }
            }
        }


        // fill in all child objects
        foreach (Transform child in block.transform)
        {
            Vector3 pos = Round(child.position);
            if (pos.y < gridSizeY)
            {
                theGrid[(int)pos.x, (int)pos.y, (int)pos.z] = child;
            }
        }
    }

    public Transform GetTransformOnGridPos(Vector3 pos)
    {
        // if we are in the heights of gris
        if (pos.y > gridSizeY -1)
        {
            return null;
        } else
        {
            return theGrid[(int)pos.x, (int)pos.y, (int)pos.z];
        }
    }

    public void SpawnNewBlock()
    {
        // we want to spawn blocks on the very top center of the grid
        Vector3 spawnPos = new Vector3((int)(transform.position.x + (float)gridSizeX / 2),
                                    (int)transform.position.y + gridSizeY,
                                    (int)(transform.position.z + (float)gridSizeZ / 2));

        

        // spawn
        GameObject newBlock = Instantiate(blockList[randomIndex], spawnPos, Quaternion.identity) as GameObject;

        //ghost
        GameObject newGhost = Instantiate(ghostList[randomIndex], spawnPos, Quaternion.identity) as GameObject;
        newGhost.GetComponent<GhostBlock>().SetParent(newBlock);

        CalculatePreview();
        Preview.instance.ShowPreview(randomIndex);
    }

    public void CalculatePreview()
    {
        randomIndex = Random.Range(0, blockList.Length);
    }

    public void DeleteLayer()
    {
        int layersCleared = 0;
        
        for (int y = gridSizeY-1; y >= 0; y--)
        {
            if (CheckFullLayer(y))
            {
                layersCleared++;
                DeleteLayerAt(y);
                MoveAllLayerDown(y);
            }
        }

        if (layersCleared > 0)
        {
            GameManager.instance.LayersCleared(layersCleared);
        }
    }
    
    // check if we have a full line
    bool CheckFullLayer(int y)
    {
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int z = 0; z < gridSizeZ; z++)
            {
                if (theGrid[x, y, z] == null)
                {
                    return false;
                }
            }
        }

        return true;
    }

    //deleting layer
    void DeleteLayerAt(int y)
    {
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int z = 0; z < gridSizeZ; z++)
            {
                Destroy(theGrid[x, y, z].gameObject);
                theGrid[x, y, z] = null;
            }
        }
    }

    // moving higher layers down
    void MoveAllLayerDown(int y)
    {
        for (int i = 0; i < gridSizeY; i++)
        {
            MoveOneLayerDown(i);
        }
    }

    void MoveOneLayerDown(int y)
    {
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int z = 0; z < gridSizeZ; z++)
            {
                if (theGrid[x, y, z] != null)
                {
                    theGrid[x, y - 1, z] = theGrid[x, y, z];
                    theGrid[x, y, z] = null;
                    theGrid[x, y - 1, z].position += Vector3.down;
                }
            }
        }
    }
    
    private void OnDrawGizmos()
    {
        if (bottomPlane != null)
        {
            // resize bottom plane
            Vector3 scaler = new Vector3((float)gridSizeX / 10, 1, (float)gridSizeZ / 10);
            bottomPlane.transform.localScale = scaler;

            // reposition bottom plane

            bottomPlane.transform.position = new Vector3(transform.position.x + (float)gridSizeX / 2,
                                                            transform.position.y, transform.position.z + (float)gridSizeZ / 2);

            // retile material

            bottomPlane.GetComponent<MeshRenderer>().sharedMaterial.mainTextureScale = new Vector2(gridSizeX, gridSizeZ);

        }

        if (wallN != null)
        {
            // resize bottom plane
            Vector3 scaler = new Vector3((float)gridSizeX / 10, 1, (float)gridSizeY / 10);
            wallN.transform.localScale = scaler;

            // reposition bottom plane

            wallN.transform.position = new Vector3(transform.position.x + (float)gridSizeX / 2,
                                                            transform.position.y + (float)gridSizeY/2, transform.position.z + gridSizeZ);

            // retile material

            wallN.GetComponent<MeshRenderer>().sharedMaterial.mainTextureScale = new Vector2(gridSizeX, gridSizeY);

        }

        if (wallS != null)
        {
            // resize bottom plane
            Vector3 scaler = new Vector3((float)gridSizeX / 10, 1, (float)gridSizeY / 10);
            wallS.transform.localScale = scaler;

            // reposition bottom plane

            wallS.transform.position = new Vector3(transform.position.x + (float)gridSizeX / 2,
                                                            transform.position.y + (float)gridSizeY / 2, transform.position.z);


        }

        if (wallE != null)
        {
            // resize bottom plane
            Vector3 scaler = new Vector3((float)gridSizeZ / 10, 1, (float)gridSizeY / 10);
            wallE.transform.localScale = scaler;

            // reposition bottom plane

            wallE.transform.position = new Vector3(transform.position.x + gridSizeX,
                                                            transform.position.y + (float)gridSizeY / 2, transform.position.z + (float)gridSizeZ / 2);

            // retile material

            wallE.GetComponent<MeshRenderer>().sharedMaterial.mainTextureScale = new Vector2(gridSizeZ, gridSizeY);

        }

        if (wallW != null)
        {
            // resize bottom plane
            Vector3 scaler = new Vector3((float)gridSizeZ / 10, 1, (float)gridSizeY / 10);
            wallW.transform.localScale = scaler;

            // reposition bottom plane

            wallW.transform.position = new Vector3(transform.position.x,
                                                            transform.position.y + (float)gridSizeY / 2, transform.position.z + (float)gridSizeZ / 2);

        }
    }
}
