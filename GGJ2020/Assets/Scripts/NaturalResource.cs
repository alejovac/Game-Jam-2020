using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaturalResources {
    List<NaturalResource> naturalResources;
}

public class NaturalResource : MonoBehaviour
{
    string name;
    Sprite spriteResource;

    public enum shapeAction {sphere,row,column};
    public shapeAction typeShape;
    public int rangeAction;
    public int cost;

    //Condition
    public int luminosityNeeded;
    public int humidityNeeded;

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

    public bool CheckTerrain(TileLogic terrain) {
        if (terrain.luminosity >= luminosityNeeded && terrain.humidity >= humidityNeeded && terrain.nutrients >= nutrients && terrain.resource!=null) {
            return true;
        }
        return false;
    }
}
