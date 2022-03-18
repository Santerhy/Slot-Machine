using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    public Text saldoText;
    public Text winnginsText;
    public Text betText;
    public Text errorText;

    public GameObject riskButtons;
    public GameObject riskObjects;

    private ReelManager reelManager;

    public ParticleSystem winEffect;

    public float bet;
    public float playerMoney;
    public float lastWin;
    public int betIndex;
    public bool isSpinngin;
    public List<float> betList = new List<float> {0.2f, 0.4f, 0.6f, 0.8f, 1, 1.2f, 1.4f, 1.6f, 1.8f, 2, 3, 4, 5, 10, 25 };
    public float winningsTemp;
    public float winCounter;
    // Start is called before the first frame update
    void Start()
    {

        reelManager = FindObjectOfType<ReelManager>();
        betIndex = 4;
        bet = betList[betIndex];
        UpdateMoneys();
    }

    void Update()
    {
        isSpinngin = reelManager.GetIsSpinngin();
    }

    public void BetIncrease()
    {
        if (!isSpinngin)
        {
            if (betIndex < betList.Count - 1)
                betIndex++;
            bet = betList[betIndex];
            UpdateMoneys();
        }
 
    }

    public void BetDecrease()
    {
        if (!isSpinngin)
        {
            if (betIndex > 0)
                betIndex--;
            bet = betList[betIndex];
            UpdateMoneys();
        }
    }

    public void MaxBet()
    {
        if (!isSpinngin)
        {
            betIndex = betList.Count - 1;
            bet = betList[betIndex];
            UpdateMoneys();
        }
    }

    public void UpdateMoneys()
    {
        playerMoney = Mathf.Round(playerMoney * 100f) / 100f;
        saldoText.text = playerMoney.ToString() + "€";
        betText.text = bet.ToString() + "€";
        if (lastWin != 0)
        {
            StartCoroutine(winAnimation());
        }
        else reelManager.isSpinning = false;
    }

    public IEnumerator winAnimation()
    {
        winCounter = 0f;
        winnginsText.gameObject.SetActive(true);
        while (winCounter < lastWin)
        {
            winCounter += lastWin / 10;
            winnginsText.text = winCounter.ToString("F2") + "€";
            yield return new WaitForSeconds(0.2f);
        }
        winnginsText.text = lastWin.ToString() + "€";
        winEffect.Play();
        riskButtons.SetActive(true);
    }

    public void Spinned()
    {
        winnginsText.gameObject.SetActive(false);
        playerMoney -= bet;
        lastWin = 0;
        UpdateMoneys();
    }

    public bool IsAbleToSpin()
    {
        if (playerMoney - bet >= 0)
        {
            errorText.gameObject.SetActive(false);
            return true;
        }
        else
        {
            errorText.gameObject.SetActive(true);
            errorText.text = "Not enough money";
            return false;
        }
    }

    public void AddWinnings(List<float> multipliers, bool scatter)
    {
        winningsTemp = 0;
        for (int i = 0; i < multipliers.Count; i ++)
        {
            winningsTemp += multipliers[i] * bet;
        }
        lastWin = winningsTemp;

        if (scatter)
            reelManager.StartFreeSpins();
    }

    public void KeepMoney()
    {
        playerMoney += winningsTemp;
        reelManager.isSpinning = false;
        riskButtons.SetActive(false);
        lastWin = 0;
        UpdateMoneys();
    }

    public void RiskMoney()
    {
        Debug.Log("Riskmoney");
        reelManager.isSpinning = false;
        riskButtons.SetActive(false);
        riskObjects.transform.GetChild(0).gameObject.SetActive(true);
        riskObjects.GetComponent<RiskManager>().SetBetting(lastWin);
    }
}
