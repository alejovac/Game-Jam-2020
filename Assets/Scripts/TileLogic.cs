using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileLogic : MonoBehaviour
{
    public int luminosity;
    public int humidity;
    public int nutrients;

    public bool recovered;

    int logicPosX;
    int logicPosY;

    public NaturalResource resource;
   
    // Start is called before the first frame update
    void Start()
    {
        resource = null;
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    public void OnInit(int x, int y)
    {
        logicPosX = x;
        logicPosY = y;
        luminosity = Random.Range(0, 10);
        humidity = Random.Range(0, 10);
        nutrients = Random.Range(0, 10);
        recovered = false;

    }

    public bool OnApplyResource(NaturalResource _resource) {
        print("Aplico");
        if (_resource.CheckTerrain(this))
        {
            print("Funciono");
            resource = _resource;
            recovered = true;
            return true;
        }
        else return false;
    }

    public void OnReceiveInfluence(NaturalResource _resource, float percentageInfluence) {
        luminosity -= _resource.luminosity;
        humidity -= _resource.humidity;
        nutrients -= _resource.nutrients;
    }

    public void OnRemoveResource(NaturalResource _resource) {
    }


}
