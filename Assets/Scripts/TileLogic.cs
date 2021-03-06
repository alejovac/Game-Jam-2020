﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileLogic : MonoBehaviour
{
    public int luminosity;
    public int humidity;
    public int nutrients;

    public bool recovered;
    public MapController map;
    public SpriteRenderer renderContenido;

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

    public void OnInit(int x, int y, int luminosity, int humidity, int nutrients)
    {
        logicPosX = x;
        logicPosY = y;
        this.luminosity = luminosity;
        this.humidity   = humidity;
        this.nutrients  = nutrients;
        recovered = false;
    }

    public bool OnApplyResource(NaturalResource _resource) {
        if (map.OnApplyResource(logicPosX, logicPosY, _resource))
        {
            resource = _resource;
            renderContenido.sprite = resource.spriteResource;
            MapController.instance.OnTilesRecovered();
            GameController.instance.OnResourceBought(resource);
            UIController.instance.CalculateProgress();
            UIController.instance.UpdateMoney();
            return true;
        }
        else return false;
    }

    public void OnReceiveInfluence(NaturalResource _resource, float percentageInfluence) {
        luminosity -= _resource.luminosity;
        humidity -= _resource.humidity;
        nutrients -= _resource.nutrients;
        GetComponent<TileVisual>().OnApplyResource();
    }

    public void OnRemoveResource(NaturalResource _resource) {
        //Desplantar
    }


}
