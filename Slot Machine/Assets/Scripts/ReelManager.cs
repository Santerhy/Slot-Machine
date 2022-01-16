using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReelManager : MonoBehaviour
{
    public Sprite[] slotIcons;
    public int randIconId;
    public bool isSpinning = false;
    private int tempImgId;
    public List<Point> allReelIcons = new List<Point>();
    public List<int> iconsPerReel = new List<int>();
    public MoneyManager moneyManager;
    public WinCalculator winCalculator;

    // Start is called before the first frame update
    void Start()
    {
        moneyManager = FindObjectOfType<MoneyManager>();
        winCalculator = FindObjectOfType<WinCalculator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spin()
    {
        if (!isSpinning)
        {
            if (moneyManager.IsAbleToSpin())
            {
                ClearList();
                moneyManager.Spinned();
                isSpinning = true;
                StartCoroutine(StartSpinning());
            }
        }
        else
            moneyManager.KeepMoney();
    }

    public IEnumerator StartSpinning() 
    {
        foreach (Transform child in transform)
        {
            child.transform.GetChild(3).GetComponent<Image>().gameObject.SetActive(true);
        }
        SuffleIcons();
        winCalculator.CalculateWinnings(allReelIcons);

        foreach (Transform child in transform)
        {
            yield return new WaitForSeconds(2);
            child.transform.GetChild(3).GetComponent<Image>().gameObject.SetActive(false);
        }
        moneyManager.UpdateMoneys();
    }

    public void ClearList()
    {
        foreach (Transform child in transform)
        {

            for (int i = 0; i < 4; i++)
            {
                allReelIcons[i].list.Clear();
            }
        }
    }

    public void SuffleIcons()
    {
        for (int x = 0; x < transform.childCount; x++)
        {
            iconsPerReel.Clear();
            for (int y = 0; y < 19; y++)
            {
                iconsPerReel.Add(y);
            }
            for (int i = 0; i < 3; i++)
            {
                randIconId = Random.Range(1, iconsPerReel.Count) - 1;
                if (randIconId <= 6)
                    tempImgId = 0;
                else if (randIconId >= 7 && randIconId <= 11)
                    tempImgId = 1;
                else if (randIconId >= 12 && randIconId <= 14)
                    tempImgId = 2;
                else if (randIconId >= 15 && randIconId <= 17)
                    tempImgId = 3;
                else if (randIconId >= 18)
                    tempImgId = 4;

                transform.GetChild(x).transform.GetChild(i).GetComponent<Image>().sprite = slotIcons[tempImgId];
                allReelIcons[x].list.Add(tempImgId);
                iconsPerReel.RemoveAt(randIconId);
            }
        }
    }

    public bool GetIsSpinngin()
    {
        return isSpinning;
    }

    public void StartFreeSpins()
    {

    }
}
