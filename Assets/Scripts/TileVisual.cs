using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileVisual : MonoBehaviour
{
    public static NaturalResource target = null;
    public static VisualTileMode mode = VisualTileMode.ShowWater;

    public List<Sprite> spriteTerrains;
    public Sprite spriteSano;
    private SpriteRenderer render;
    private TileLogic data;

    //Color Presets
    public Color white;
    public Color green;
    public Color red;
    public Color humidity;
    public Color light;
    public Color nutrients;


    // Start is called before the first frame update
    void Start()
    {
        OnInit();

    }

    // Update is called once per frame
    void Update()
    {
        int divididor = 15;

        if (data.recovered)
            render.sprite = spriteSano;


        if (data.resource == null)
        {
            switch (mode)
            {
                case VisualTileMode.ShowWater:
                    render.material.color = Color.Lerp(white, humidity, ((float) data.humidity) / divididor);
                    break;
                case VisualTileMode.ShowLight:
                    render.material.color = Color.Lerp(white, light, ((float) data.luminosity) / divididor);
                    break;
                case VisualTileMode.ShowNutrients:
                    render.material.color = Color.Lerp(white, nutrients, ((float) data.nutrients) / divididor);
                    break;
                case VisualTileMode.ShowAvailability:
                    if (target.CheckTerrain(data))
                        render.material.color = Color.Lerp(white, green, 0.3f);
                    else
                        render.material.color = Color.Lerp(white, red, 0.3f);
                    break;
            }
        }
        else
            render.material.color = white;


    }

    public void OnInit(){
        render = GetComponent<SpriteRenderer>();
        int auxIdx = Random.Range(9, 11);
        render.sprite = spriteTerrains[auxIdx];//Solo prueba
        data = GetComponent<TileLogic>();
        render.material.color = Color.white;
    }

    public void OnApplyResource() {
        //int auxIdx = Random.Range(0, 2);
        //render.sprite = spriteTerrains[auxIdx];//Solo prueba
        render.sprite = spriteSano;//Solo prueba
    }
}

public enum VisualTileMode {
    ShowWater = 1,
    ShowLight = 2,
    ShowNutrients = 3,
    ShowAvailability = 4
}