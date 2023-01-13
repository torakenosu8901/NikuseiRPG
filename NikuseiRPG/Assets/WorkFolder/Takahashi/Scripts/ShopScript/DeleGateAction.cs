using System.Collections;
using UnityEngine;
using System;
using UnityEngine.Events;
using System.Reflection;
using UnityEngine.UI;

public class DeleGateAction : MonoBehaviour
{
    private const string Coin_KEY = "Coin";
    private Action<int> onCoinChange = (coin) => {};

    [SerializeField]
    private GameObject yesNoCanvas;

    [SerializeField]
    private Text coinText;

    public void Init(Action<int> onCoinChange)
    {
        this.onCoinChange += onCoinChange;
        this.onCoinChange += (coin) => { coinText.text = coin.ToString(); };

        yesNoCanvas.transform.Find("YesNoPanel/Yes").GetComponent<Button>().onClick.AddListener(() => { SubCoin(yesNoCanvas.GetComponent<ShowItemData>().GetPrice); });

        this.onCoinChange(LoadCoin());
    }

    private int LoadCoin ()
    {
        return PlayerPrefs.GetInt(Coin_KEY, 0);
    }

    public void AddCoin(int coin)
    {
        if(coin < 0)
        {
            return;
        }

        CoinChange(LoadCoin() + coin);
    }

    private void SubCoin(int coin)
    {
        if(coin < 0)
        {
            return;
        }

        if(LoadCoin() - coin < 0)
        {
            return;
        }

        CoinChange(LoadCoin() - coin);
    }

    private void CoinChange(int coin)
    {
        PlayerPrefs.SetInt(Coin_KEY, coin);
        PlayerPrefs.Save();
        onCoinChange(PlayerPrefs.GetInt(Coin_KEY, 0)); ;
    }
}
