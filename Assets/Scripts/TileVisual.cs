using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileVisual : MonoBehaviour
{
    public List<Sprite> spriteTerrains;
    public SpriteRenderer render;
    public TileLogic data;

    // Start is called before the first frame update
    void Start()
    {
        OnInit();
    }

    // Update is called once per frame
    void Update()
    {
        if (!data.recovered)
        {
            render.material.color = Color.Lerp(Color.white, Color.cyan, ((float)data.humidity) / 20);
        }
        else
            render.material.color = Color.green;
    }

    public void OnInit(){
        render = GetComponent<SpriteRenderer>();
        render.sprite = spriteTerrains[14];
        data = GetComponent<TileLogic>();
        render.material.color = Color.Lerp(Color.white, Color.cyan, ((float)data.humidity) / 20);
        //render.material.color = Color.white;
    }

    //Capturar raycast

}
