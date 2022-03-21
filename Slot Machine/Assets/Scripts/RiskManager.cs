using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RiskManager : MonoBehaviour
{
    public List<GameObject> imageList;
    public GameObject frame;
    public List<GameObject> createdObjects;
    public float iconPosition;
    public float moveSpeed;
    public float moveSpeedCounter;
    public float playerMoney;
    public bool isSpinning = false;
    public bool iconAsked;
    public Text heartMoney;
    public Text starMoney;
    public Text spadeMoney;
    public Text currentMoney;
    public float heartSum;
    public float starSum;
    public float spadeSum;
    public int playerChoise;
    public int winIcon;
    public RiskReelDestroyChildren destroyChildren;
    public IconCheck iconCheck;


    // Start is called before the first frame update
    void Start()
    {
        //destroyChildren = FindObjectOfType<RiskReelDestroyChildren>();
        //iconCheck = FindObjectOfType<IconCheck>();
        iconPosition = -50f;
        iconAsked = true;
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
        if (!iconAsked && !isSpinning)
            GetWinIcon();
    }

    public void SetBetting(float money)
    {
        playerMoney = money;
        SetBetOptions();
        //ResetIcons();
    }

    void SetBetOptions()
    {
        heartSum = playerMoney * 2;
        spadeSum = playerMoney * 2;
        starSum = playerMoney * 10;
        heartMoney.text = heartSum + "€";
        spadeMoney.text = spadeSum + "€";
        starMoney.text = starSum + "€";
        currentMoney.text = playerMoney + "€";
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
        
        moveSpeedCounter -= 0.2f;
    }

    public void BetStar()
    {
        if (!isSpinning)
        {
            playerChoise = 2;
            iconAsked = false;
            ResetIcons();
            isSpinning = true;
        }
    }

    public void BetSpade()
    {
        if (!isSpinning)
        {
            playerChoise = 1;
            iconAsked = false;
            ResetIcons();
            isSpinning = true;
        }
    }

    public void BetHeart()
    {
        if (!isSpinning)
        {
            playerChoise = 0;
            iconAsked = false;
            ResetIcons();
            isSpinning = true;
        }
    }

    void GetWinIcon()
    {
        iconAsked = true;
        winIcon = iconCheck.GetWinIcon();
    }

    void CheckWin()
    {
        if (winIcon == playerChoise)
        {
            if (playerChoise == 1 || playerChoise == 0)
                playerMoney = heartSum;
            else
                playerMoney = starSum;
            SetBetOptions();
        }
        else
        {
            playerMoney = 0;
            currentMoney.text = playerMoney.ToString() + "€";
            isSpinning = true;
        }
            
    }
}
