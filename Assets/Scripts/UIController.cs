using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    public NaturalResource dragableResource;

    public Image filledBar;
    public Text moneyLabel;

    private void Awake()
    {
        instance = this;
    }

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
        Debug.Log(MapController.instance.OnTilesRecovered());
        float progress = MapController.instance.OnTilesRecovered()/225.0f;
        progress = Mathf.Min(progress / 0.75f, 1);
        filledBar.fillAmount = progress;
        float progresspor = 99;
        print(progresspor);
        AkSoundEngine.SetRTPCValue("wildlife", progresspor, this.gameObject);
    }

    public void UpdateMoney() {
        moneyLabel.text = "RECURSOS: "+GameController.instance.money.ToString();
    }
}
