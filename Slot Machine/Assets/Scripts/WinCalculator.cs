using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCalculator : MonoBehaviour
{
    public MoneyManager moneyManager;
    // Start is called before the first frame update
    void Start()
    {
        moneyManager = FindObjectOfType<MoneyManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void CalculateWinnings(List<Point> listIn)
    {
        List<float> winMultipliers = new List<float>();
        bool scatterHit = false;

        //Check if all icons are same

        //Check if first reel has only one icons
        if (listIn[0].list[0] == listIn[0].list[1] && listIn[0].list[2] == listIn[0].list[1])
        { //Check if second reel has same icons
            if (listIn[1].list[0] == listIn[1].list[1] && listIn[1].list[2] == listIn[1].list[1] && listIn[1].list[0] == listIn[0].list[0])
            {//Check if third reel also has same icons
                if (listIn[2].list[0] == listIn[2].list[1] && listIn[2].list[2] == listIn[2].list[1] && listIn[2].list[0] == listIn[0].list[0])
                { //All icons in reel are the same
                    winMultipliers.Add(GetMultiplierFull(listIn[0].list[0]));
                }
            }
            //Check if row has same icons
        } if (listIn[0].list[0] == listIn[1].list[0])
        {
            if (listIn[2].list[0] == listIn[1].list[0])
            {
                //Top row has same icons
                //Check if icons are Scatter
                if (listIn[0].list[0] == 4)
                {
                    scatterHit = true;
                }
                else
                    winMultipliers.Add(GetMultiplier(listIn[0].list[0]));
            }
        } if (listIn[0].list[1] == listIn[1].list[1])
        {
            if (listIn[2].list[1] == listIn[1].list[1])
            {
                //Middle row has same icons
                //Check if icons are Scatter
                if (listIn[0].list[1] == 4)
                {
                    scatterHit = true;
                }
                else
                    winMultipliers.Add(GetMultiplier(listIn[0].list[1]));
            }
            
        }
        if (listIn[0].list[2] == listIn[1].list[2])
        {
            if (listIn[2].list[2] == listIn[1].list[2])
            {
                //Bottom row has same icons
                //Check if icons are Scatter
                if (listIn[0].list[2] == 4)
                {
                    scatterHit = true;
                }
                else
                    winMultipliers.Add(GetMultiplier(listIn[0].list[2]));
            }
        }

        //Check for diagonal wins
        if (listIn[0].list[0] == listIn[1].list[1] && listIn[2].list[0] == listIn[1].list[1])
        {
            //Diagonal win #1
            winMultipliers.Add(GetMultiplier(listIn[0].list[0]));
        }
        if (listIn[0].list[1] == listIn[1].list[0] && listIn[0].list[1] == listIn[2].list[1])
        {
            //Diagonal win #2
            winMultipliers.Add(GetMultiplier(listIn[0].list[1]));
        }
        if (listIn[0].list[1] == listIn[1].list[2] && listIn[0].list[1] == listIn[2].list[1])
        {
            //Diagonal win #3
            winMultipliers.Add(GetMultiplier(listIn[0].list[1]));
        }
        if (listIn[0].list[2] == listIn[1].list[1] && listIn[0].list[2] == listIn[2].list[2])
        {
            //Diagonal win #4
            winMultipliers.Add(GetMultiplier(listIn[0].list[2]));
        }

        for (int i = 0; i < winMultipliers.Count; i++)
        {
            Debug.Log("Voitto nro " + i + ": " + winMultipliers[i]);
        }
        if (winMultipliers.Count != 0)
            moneyManager.AddWinnings(winMultipliers, scatterHit);
    }

    //Get the right multiplier for icon
    public float GetMultiplier(int icon)
    {
        Debug.Log("Iconi " + icon);
        switch (icon)
        {
            case 0:
                return 1.0f;
            case 1:
                return 1.5f;
            case 2:
                return 2.0f;
            case 3:
                return 5.0f;
            default:
                return 1f;
        }
    }

    public float GetMultiplierFull(int icon)
    {
        switch (icon)
        {
            case 0:
                return 3.0f;
            case 1:
                return 6.0f;
            case 2:
                return 8.0f;
            case 3:
                return 40.0f;
            default:
                return 1f;
        }
    }


}
