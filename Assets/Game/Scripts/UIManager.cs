using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _ammoText;
    [SerializeField]
    private TextMeshProUGUI _loadsText;
    [SerializeField]
    private TextMeshProUGUI _coinText;
    [SerializeField]
    private Image _coinImage;


    public void UpdateAmmo(int v)
    {
        _ammoText.text = "Ammo: " + v;
    }

    public void UpdateLoads(int v)
    {
        _loadsText.text = "Loads: " + v;
    }

    public void UpdateCoin(bool v)
    {
        _coinText.text = "Coin: " + v;
        _coinImage.enabled = v;

    }
}
