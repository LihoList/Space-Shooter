
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class MoneyAndScore : MonoBehaviour
{
    TextMeshProUGUI moneyTextMenuMap;
    TextMeshProUGUI moneyTextShop;

    [Header("-----Money-----")]
    public int money;

    int destroyedEnemiesCount;

    private void Start()
    {
        LoadMoney();

        InvokeRepeating("UpdateProgressEverySecond", 0.5f, 0.5f);  //0.5s delay, repeat every 0.5s
    }

    void UpdateProgressEverySecond()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            moneyTextMenuMap = GameObject.Find("Money Text").GetComponent<TextMeshProUGUI>();
            moneyTextShop = GameObject.Find("Money Text Shop").GetComponent<TextMeshProUGUI>();

            moneyTextMenuMap.text = "$" + money.ToString();
            moneyTextShop.text = "$" + money.ToString();
        }
        
        MoneySave();
    }

   

    public void AddScoreToMoney(int scoreToAdd)
    {
        Debug.Log("money added");
        Debug.Log("added " + scoreToAdd);

        money += scoreToAdd;
        moneyTextMenuMap.text = "$" + money.ToString();

        Debug.Log("money "+ money);
    }

    void MoneySave()
    {
        PlayerPrefs.SetInt("money", money);
    }

    void LoadMoney()
    {
        money =  PlayerPrefs.GetInt("money", 0);
    }

}
