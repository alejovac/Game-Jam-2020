using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
	private Camera cam;
	private TileLogic originTile;
	private NaturalResource plant;

    // Start is called before the first frame update
    void Start()
    {
        cam =  Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            DragNaturalResource();
        } else if (Input.GetMouseButtonUp(0) && plant != null) {
            DropNaturalResource();
        }
    }

    void DragNaturalResource() {
        RaycastHit2D hit = Physics2D.Raycast(cam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider != null) {
            GameObject hitGO = hit.transform.gameObject;

            if (hitGO.tag == "ShopTile") {
                originTile = null;
                plant = hitGO.GetComponent<ShopTile>().content;
            } else if (hitGO.tag == "MapTile") {
                originTile = hitGO.GetComponent<TileLogic>();
                plant = originTile.resource;
                print("YaY");
            }
        } else {
            print("Nay!");
        }
    }

    void DropNaturalResource() {
        //Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        //RaycastHit2D hit = Physics2D.Raycast(cam.ScreenToWorld(Input.mousePosition, Vector2.zero));
        //if (Physics2D.Raycast(ray, out hit)) {
        //    GameObject hitGO = hit.transform.gameObject;

        //    if (hitGO.tag == "MapTile") {
        //        hitGO.GetComponent<TileLogic>().OnApplyResource(plant);
        //        if (originTile != null) {
        //            originTile.OnRemoveResource(plant);
        //        } else {

        //        }
        //    } else if (hitGO.tag == "MapTile") {
        //        if (originTile != null) {
        //            originTile.OnRemoveResource(plant);
        //        }
        //    }
        //}

        //plant = null; 
        //originTile = null;
    }
}
