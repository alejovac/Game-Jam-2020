using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileVisual : MonoBehaviour
{
    public static NaturalResource target = null;
    public static VisualTileMode mode = VisualTileMode.ShowWater;

    public List<Sprite> spriteTerrains;
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
        switch (mode) {
            case VisualTileMode.ShowWater:
                render.material.color = Color.Lerp(white, humidity, ((float)data.humidity) / 20);
                break;
            case VisualTileMode.ShowLight:
                render.material.color = Color.Lerp(white, light, ((float)data.luminosity) / 20);
                break;
            case VisualTileMode.ShowNutrients:
                render.material.color = Color.Lerp(white, nutrients, ((float)data.nutrients) / 20);
                break;
            case VisualTileMode.ShowAvailability:
                if (target.CheckTerrain(data))
                    render.material.color = Color.Lerp(white, green, 0.3f);
                else
                    render.material.color = Color.Lerp(white, red, 0.3f);
                break;
        }

        if (data.recovered)
            render.material.color = Color.Lerp(render.material.color ,green, 0.4f);
    }

    public void OnInit(){
        render = GetComponent<SpriteRenderer>();
        int auxIdx = Random.Range(9, 11);
        render.sprite = spriteTerrains[auxIdx];//Solo prueba
        data = GetComponent<TileLogic>();
        render.material.color = Color.white;
    }

    public void OnApplyResource() {
        int auxIdx = Random.Range(0, 2);
        render.sprite = spriteTerrains[auxIdx];//Solo prueba
    }
}

public enum VisualTileMode {
    ShowWater,
    ShowLight,
    ShowNutrients,
    ShowAvailability
}