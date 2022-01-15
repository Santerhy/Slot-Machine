using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    public Text saldo;
    public Text winngins;
    public Text bet;
    // Start is called before the first frame update
    void Start()
    {
        saldo.text = "100€";
        winngins.text = "0€";
        bet.text = "0,5€";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
