using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public int coinsCount;
    public static Inventory instance;
    public Text CoinCoutTexte;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("Attention, instance > 1 !");
            return;
        }
        instance = this;
    }

    public void AddCoins(int count)
    {
        coinsCount += count;
        CoinCoutTexte.text = coinsCount.ToString() + " ";
    }
}
