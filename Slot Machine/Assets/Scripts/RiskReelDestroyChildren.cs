using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiskReelDestroyChildren : MonoBehaviour
{

    public void DestroyChildren()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
