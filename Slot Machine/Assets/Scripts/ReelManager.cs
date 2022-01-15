using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReelManager : MonoBehaviour
{
    public Sprite[] slotIcons;
    private bool isSpinning = false;
    private int tempImgId;
    public List<Point> allReelIcons = new List<Point>();

    // Start is called before the first frame update
    void Start()
    {
        //allReelIcons.Add(new PointList());
        //allReelIcons.Add(new List<Point>());
        //allReelIcons.Add(new PointList());
        //allReelIcons.Add(new PointList());
        //allReelIcons.Add(new List<int> { });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spin()
    {
        if (!isSpinning)
        {
            ClearList();
            isSpinning = true;
            StartCoroutine(StartSpinning());
        }
    }

    public IEnumerator StartSpinning() 
    {
        foreach (Transform child in transform)
        {
            child.transform.GetChild(3).GetComponent<Image>().gameObject.SetActive(true);
        }
        SuffleIcons();

        foreach (Transform child in transform)
        {
            yield return new WaitForSeconds(2);
            child.transform.GetChild(3).GetComponent<Image>().gameObject.SetActive(false);
        }
        isSpinning = false;
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
            for (int i = 0; i < 3; i++)
            {
                tempImgId = Random.Range(0, 5);
                transform.GetChild(x).transform.GetChild(i).GetComponent<Image>().sprite = slotIcons[tempImgId];
                allReelIcons[x].list.Add(tempImgId);
            }
        }
    }
}
