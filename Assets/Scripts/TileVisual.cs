using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileVisual : MonoBehaviour
{
    public List<Sprite> spriteTerrains;

    // Start is called before the first frame update
    void Start()
    {
        OnInit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnInit(){
        GetComponent<SpriteRenderer>().sprite = spriteTerrains[Random.Range(0, 15)];


    }

    //Capturar raycast

}
