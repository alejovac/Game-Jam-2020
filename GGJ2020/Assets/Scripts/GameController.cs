﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public int money;

    //List<NaturalResource> resourcesBought;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnResourceBought(NaturalResource newResource) {
        money -= newResource.cost;
        //resourcesBought.Add(newResource);
    }

    public void OnResourceSell(NaturalResource newResource)
    {
        money += newResource.cost;
        //resourcesBought.Add(newResource);
    }
}
