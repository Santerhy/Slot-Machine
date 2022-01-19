using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconCheck : MonoBehaviour
{
    public int winIcon;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("HeartIcon"))
            winIcon= 0;
        else if (collision.CompareTag("SpadeIcon"))
            winIcon = 1;
        else if (collision.CompareTag("StarIcon"))
            winIcon = 2;
    }

    public int GetWinIcon()
    {
        return winIcon;
    }

}
