using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
	private Camera cam;
	public TileLogic originTile;
	public NaturalResource plant;

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
            AkSoundEngine.PostEvent("pl_buy", gameObject);
        } else if (Input.GetMouseButtonUp(0) && plant != null) {
            DropNaturalResource();
            AkSoundEngine.PostEvent("pl_plant_add", gameObject);
        }
    }

    void DragNaturalResource()
    {
        RaycastHit2D hit = Physics2D.Raycast(cam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider != null) {
            GameObject hitGO = hit.transform.gameObject;

            if (hitGO.tag == "ShopTile") {
                originTile = null;
                plant = hitGO.GetComponent<NaturalResource>();
                print("Compra");
            } else if (hitGO.tag == "MapTile") {
                originTile = hitGO.GetComponent<TileLogic>();
                plant = originTile.resource;
                print("YaY");
            }
        } else {
            print("Nay!");
        }
    }

    void DropNaturalResource()
    {
        RaycastHit2D hit = Physics2D.Raycast(cam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider != null) {
            GameObject hitGO = hit.collider.gameObject;

            if (hitGO.tag == "MapTile") {
                hitGO.GetComponent<TileLogic>().OnApplyResource(plant);
                if (originTile != null) {
                    originTile.OnRemoveResource(plant);
                } else {

                }
            } else if (hitGO.tag == "ShopTile") {
                if (originTile != null) {
                    originTile.OnRemoveResource(plant);
                }
            }
        }

        plant = null; 
        originTile = null;
    }
}
