using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public NaturalResource dragableResource;

    public Image filledBar;
    public Text moneyLabel;

    // Start is called before the first frame update
    void Start()
    {
        UpdateMoney();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSetDragableObject(int i) {
        dragableResource = GameController.instance.naturalResources[i];
        //dragableResource.spriteResource

    }

    public void CalculateProgress(){
        float progress = 2.25f * MapController.instance.OnTilesRecovered();
        filledBar.fillAmount = progress;
    }

    public void UpdateMoney() {
        moneyLabel.text = "RECURSOS: "+GameController.instance.money.ToString();
    }
}
