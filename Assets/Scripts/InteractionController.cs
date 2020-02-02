using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InteractionController : MonoBehaviour
{
	private Camera cam;
	public TileLogic originTile;
	public NaturalResource resourceSelected;
    bool flagSelected;

    // Start is called before the first frame update
    void Start()
    {
        cam =  Camera.main;
        flagSelected = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (flagSelected) this.transform.position = Input.mousePosition;

        if (Input.GetMouseButtonDown(0))
        {
            //DragNaturalResource();
            OnPutResource();
        }
        else if (Input.GetMouseButtonUp(0) && resourceSelected != null)
        {
            //DropNaturalResource();

        }
    }

    public void SetNaturalResourceSelected(int i) {
        resourceSelected = GameController.instance.naturalResources[i];
        this.transform.position = Input.mousePosition;
        GetComponent<Image>().sprite = resourceSelected.spriteResource;
        flagSelected = true;
        //.spriteResource
    }

    public void OnPointerDown(PointerEventData eventData){
        flagSelected = false;
        this.transform.position = new Vector3(0.0f,135.0f,0.0f);
    }

    public void OnPutResource() {
        RaycastHit2D hit = Physics2D.Raycast(cam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider != null)
        {
            GameObject hitGO = hit.transform.gameObject;        
            if (hitGO.tag == "MapTile")
            {
                originTile = hitGO.GetComponent<TileLogic>();
                bool validTerrain = originTile.OnApplyResource(resourceSelected);//Send feedback


            }
        }

    }


    void DragNaturalResource()
    {
        RaycastHit2D hit = Physics2D.Raycast(cam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider != null) {
            GameObject hitGO = hit.transform.gameObject;

            if (hitGO.tag == "ShopTile") {
                originTile = null;
                resourceSelected = hitGO.GetComponent<NaturalResource>();
                print("Compra");
            } else if (hitGO.tag == "MapTile") {
                originTile = hitGO.GetComponent<TileLogic>();
                resourceSelected = originTile.resource;
                originTile.resource = resourceSelected;
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
                hitGO.GetComponent<TileLogic>().OnApplyResource(resourceSelected);
                if (originTile != null) {
                    originTile.OnRemoveResource(resourceSelected);
                } else {

                }
            } else if (hitGO.tag == "ShopTile") {
                if (originTile != null) {
                    originTile.OnRemoveResource(resourceSelected);
                }
            }
        }

        resourceSelected = null; 
        originTile = null;
    }
}
