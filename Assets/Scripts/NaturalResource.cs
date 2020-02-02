using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaturalResources {
    List<NaturalResource> naturalResources;
}

public class NaturalResource : MonoBehaviour
{
    public string name;
    public Sprite spriteResource;

    public enum shapeAction {sphere,row,column};
    public shapeAction typeShape;
    public int rangeAction;
    public int cost;
    public int rangeHealing;

    //Condition
    public Vector2 luminosityNeeded;
    public Vector2 humidityNeeded;

    public int luminosity;
    public int humidity;
    public int nutrients;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CheckTerrain(TileLogic terrain)
    {
        bool enoughMoney = GameController.instance.money >= cost;
        bool luminosidadBien = Mathf.Max(0,terrain.luminosity) >= luminosityNeeded.x && Mathf.Min(10,terrain.luminosity) <= luminosityNeeded.y;
        bool humedadBien = Mathf.Max(0, humidityNeeded.x) <= terrain.humidity && humidityNeeded.y >= Mathf.Min(10,terrain.humidity);
        if (luminosidadBien && humedadBien && terrain.nutrients >= -nutrients && terrain.resource == null && enoughMoney) {
            print("CheckTerrain: true");
            return true;
        }
        print("CheckTerrain: false");
        return false;
    }
}
