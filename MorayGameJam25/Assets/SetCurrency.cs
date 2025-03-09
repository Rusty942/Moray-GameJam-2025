using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetCurrency : MonoBehaviour
{
    public TextMeshProUGUI amm;
    void Start()
    {
        amm.text = (StartData.currencyAmm.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        amm.text = (StartData.currencyAmm.ToString());
    }
}
