using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public ShopElements[] characters; //Nhân vật trong shop
    public Image shopCharacters; //Avatar nhân vật được chọn
    public Sprite[] characterAvatar; //Avatar các nhân vật
    public int characterIndex;
    public Button buyButton;
    public TextMeshProUGUI totalCoins;

    // Start is called before the first frame update
    void Start()
    {
        foreach (ShopElements c in characters)
        {
            if (c.price != 0)
                c.isLocked = PlayerPrefs.GetInt(c.name, 1) == 1 ? true : false;
        }

        characterIndex = PlayerPrefs.GetInt("SelectedCharacter", 0);
        shopCharacters.sprite = characterAvatar[PlayerPrefs.GetInt("SelectedCharacter", 0)];
        shopCharacters.enabled = true;

        UpdateUIShop();
    }
    public void Update()
    {
        CoinShow();
        

    }
    //Mở khóa nhân vật
    public void UnLockCharacter()
    {
        ShopElements c = characters[characterIndex];
        if (PlayerPrefs.GetInt("TotalCoins", 0) < c.price)
            return;

        int coinRemaining = PlayerPrefs.GetInt("TotalCoins", 0) - characters[characterIndex].price;
        PlayerPrefs.SetInt("TotalCoins", coinRemaining);
        c.isLocked = false;
        PlayerPrefs.SetInt(c.name, 0);
        PlayerPrefs.SetInt("SelectedCharacter", characterIndex);
        UpdateUIShop();
    }
    //Cập nhật nút mua và giá
    public void UpdateUIShop()
    {
        ShopElements c = characters[characterIndex];

        if (c.isLocked)
        {
            buyButton.gameObject.SetActive(true);
            buyButton.GetComponentInChildren<TextMeshProUGUI>().text = c.price + "";

            if (PlayerPrefs.GetInt("TotalCoins", 0) < c.price)
            {
                buyButton.interactable = false;
            }
            else
            {
                buyButton.interactable = true;
            }

        }
        else
        {
            buyButton.gameObject.SetActive(false);
        }
    }
    //Chuyển nhân vật tiếp theo
    public void ChangeNextCharacter()
    {
        shopCharacters.enabled = false;

        characterIndex++;
        if (characterIndex == characters.Length)
            characterIndex = 0;

        shopCharacters.enabled = true;
        shopCharacters.sprite = characterAvatar[characterIndex];
        UpdateUIShop();

        bool isLocked = characters[characterIndex].isLocked;
        if (isLocked)
            return;

        PlayerPrefs.SetInt("SelectedCharacter", characterIndex);

    }
    //Chuyển nhân vật trước đó
    public void ChangePreviousCharacter()
    {
        shopCharacters.enabled = false;

        characterIndex--;
        if (characterIndex == -1)
            characterIndex = characters.Length - 1;

        shopCharacters.enabled = true;
        shopCharacters.sprite = characterAvatar[characterIndex];
        UpdateUIShop();

        bool isLocked = characters[characterIndex].isLocked;
        if (isLocked)
            return;

        PlayerPrefs.SetInt("SelectedCharacter", characterIndex);
    }
    //Hiển thị coin hiện có
    public void CoinShow()
    {
        totalCoins.text = PlayerPrefs.GetInt("TotalCoins").ToString();
    }
}
