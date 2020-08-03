using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preview : MonoBehaviour
{
    public static Preview instance;
    
    public GameObject[] previewBlocks;

    GameObject currentActive;

    private void Awake()
    {
        instance = this;
    }

    public void ShowPreview(int index)
    {
        Destroy(currentActive);

        currentActive = Instantiate(previewBlocks[index], transform.position, Quaternion.identity) as GameObject;
    }
}
