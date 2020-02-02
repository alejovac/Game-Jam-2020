using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileVisual : MonoBehaviour
{
    public List<Sprite> spriteTerrains;
    public SpriteRenderer render;
    public TileLogic data;

    //Color Presets
    public Color whiteHumidity;// = new Color(1.0f,1.0f,1.0f,0.2f);
    public Color blueHumidity;// = new Color(0.2f, 0.2f, 0.0f, 0.2f);

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
            render.material.color = Color.Lerp(whiteHumidity, blueHumidity, ((float)data.humidity) / 20);
        }
        else
            render.material.color = Color.green;
    }

    public void OnInit(){
        render = GetComponent<SpriteRenderer>();
        int auxIdx = Random.Range(9, 11);
        render.sprite = spriteTerrains[auxIdx];//Solo prueba
        data = GetComponent<TileLogic>();
        render.material.color = Color.Lerp(whiteHumidity, blueHumidity, ((float)data.humidity) / 20);
        //render.material.color = Color.white;
    }

    public void OnApplyResource() {
        int auxIdx = Random.Range(0, 2);
        render.sprite = spriteTerrains[auxIdx];//Solo prueba
    }

}
