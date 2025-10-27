using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour
{
    public TextMeshProUGUI goldText;

    // instance
    public static GameUI instance;

    void Awake()
    {
        instance = this;
    }

    public void UpdateGoldText(int gold)
    {
        goldText.text = "<b>Gold:</b> " + gold;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
