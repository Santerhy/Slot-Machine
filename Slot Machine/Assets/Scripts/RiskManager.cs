using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RiskManager : MonoBehaviour
{
    public List<GameObject> imageList;
    public GameObject frame;
    public List<GameObject> createdObjects;
    public GameObject latestIcon;
    public float iconPosition;
    public float moveSpeed;
    public float moveSpeedCounter;
    public bool isSpinning = false;
    private RiskReelDestroyChildren destroyChildren;
    private IconCheck iconCheck;

    // Start is called before the first frame update
    void Start()
    {
        destroyChildren = FindObjectOfType<RiskReelDestroyChildren>();
        iconCheck = FindObjectOfType<IconCheck>();
        iconPosition = -50f;
        for (int i = 0; i < 60; i++)
        {
            int rand = Random.Range(0, 3);
            GameObject newIcon = GameObject.Instantiate(imageList[rand], frame.transform);
            //newIcon.transform.parent = frame.transform;
            newIcon.transform.localPosition = new Vector2(0, iconPosition);
            createdObjects.Add(newIcon);
            iconPosition += 24.5f;
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (moveSpeed > 0)
            MoveReel();
        else
            isSpinning = false;
    }

    public void ResetIcons()
    {
        destroyChildren.DestroyChildren();
        iconPosition = -50f;
        frame.transform.localPosition = new Vector2(0, 0);

        for (int i = 0; i < 60; i++)
        {
            int rand = Random.Range(0, 3);
            GameObject newIcon = GameObject.Instantiate(imageList[rand], frame.transform);
            //newIcon.transform.parent = frame.transform;
            newIcon.transform.localPosition = new Vector2(0, iconPosition);
            createdObjects.Add(newIcon);
            iconPosition += 24.5f;
        };
        moveSpeed = 100f;
        moveSpeedCounter = 100f;
    }

    public void MoveReel()
    {
        if (moveSpeedCounter < 40)
            moveSpeed -= 0.5f;

        frame.transform.localPosition -= new Vector3(0, moveSpeed * Time.deltaTime, 0);
        
        moveSpeedCounter -= 0.1f;
    }

    public void BetStart()
    {
        if (!isSpinning)
        {
            ResetIcons();
            isSpinning = true;
        }
    }

    public void BetSpade()
    {
        if (!isSpinning)
        {
            ResetIcons();
            isSpinning = true;
        }
    }

    public void BetHeart()
    {
        if (!isSpinning)
        {
            ResetIcons();
            isSpinning = true;
        }
    }
}
