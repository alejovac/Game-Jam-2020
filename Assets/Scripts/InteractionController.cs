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
            OnCheckRayCast();
        }
        else if (Input.GetMouseButtonUp(0) && resourceSelected != null)
        {
            //OnReleaseResource();
            //DropNaturalResource();

        }
    }

    public void SetNaturalResourceSelected(int i) {
        SetNaturalResource(GameController.instance.naturalResources[i]);
        AkSoundEngine.PostEvent("pl_buy", gameObject);
        AkSoundEngine.PostEvent("menu_play", gameObject);
    }

    public void OnPointerDown(PointerEventData eventData){
        flagSelected = false;
        this.transform.position = new Vector3(0.0f,135.0f,0.0f);
    }

    public void OnCheckRayCast() {
        RaycastHit2D hit = Physics2D.Raycast(cam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider != null)
        {
            GameObject hitGO = hit.transform.gameObject;        
            if (hitGO.tag == "MapTile")
            {
                originTile = hitGO.GetComponent<TileLogic>();
                if (originTile.resource == null)
                {
                    if (resourceSelected != null)
                        originTile.OnApplyResource(resourceSelected);
                }
				else
                    SetNaturalResource(originTile.resource);
            }
        }
        else {
            SetNaturalResource(null);
        }
    }

    public void SetNaturalResource(NaturalResource resource)
    {
        resourceSelected = resource;

        if (resource != null)
        {
            this.transform.position = Input.mousePosition;
            GetComponent<Image>().sprite = resourceSelected.spriteResource;
            GetComponent<Image>().enabled = true;
            flagSelected = true;
            TileVisual.target = resourceSelected;
            TileVisual.mode = VisualTileMode.ShowAvailability;
        }
        else 
        {
            flagSelected = false;
            GetComponent<Image>().enabled = false;
            TileVisual.mode = VisualTileMode.ShowWater;
        }
    }

    void OnReleaseResource()
    {
        RaycastHit2D hit = Physics2D.Raycast(cam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider != null)
        {
            GameObject hitGO = hit.collider.gameObject;

            if (hitGO.tag == "ShopTile")
            {
                if (originTile != null)
                {
                    print("venta");
                    flagSelected = false;
                    originTile.OnRemoveResource(resourceSelected);
                }
            }
        }

        resourceSelected = null;
        originTile = null;
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
                    print("venta");
                    flagSelected = false;
                    originTile.OnRemoveResource(resourceSelected);
                }
            }
        }

        resourceSelected = null; 
        originTile = null;
    }

    public void UpdateTileView(int mode)
    {
        SetNaturalResource(null);
        TileVisual.mode = (VisualTileMode)mode;
    }
}
