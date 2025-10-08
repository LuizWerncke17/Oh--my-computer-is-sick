using TMPro;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI moneyUI;

    private void OnGUI()
    {
        moneyUI.text = LevelManager.main.money.ToString();
    }

    public void SetSelected()
    {

    }
}
