using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InfoPopup : MonoBehaviour
{
    public TextMeshProUGUI Name;
    public TextMeshProUGUI SpeedText;
    public Button ToggleButton;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
