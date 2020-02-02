//using System.Col1ections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public float initXpos;
    public float initYpos;
    public static MapController instance;
    // TileGround[] tiles = new TileGround [30];
    static int width = 15;
    static int height = 15;
    GameObject[,] tiles = new GameObject[width, height];

    public GameObject tileGO;

    private void Awake(){
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        OnCreateMap();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCreateMap(){

        SpriteRenderer spriteRenderer = tileGO.GetComponent<SpriteRenderer>();
        Vector3 sizeTile = spriteRenderer.sprite.bounds.size;
        Vector3 tileVisualPosition; //esta es la variable que contiene la posición real en la pantalla

        Vector2 initWorld = new Vector2(initXpos, initYpos) * sizeTile;
        Vector2 initLocal = new Vector3(initXpos, initYpos, 0.0f);
        int totalTiles = 15; // N de tiles

        float height = Camera.main.orthographicSize * 2.0f;
        float width = height * Camera.main.aspect;

        Vector3 offset = new Vector3(-(sizeTile.x * totalTiles) * 0.5f, (sizeTile.y * totalTiles * 0.5f) - height) + sizeTile * 0.5f;

        for (int j = 0; j < totalTiles; j++)
        {
            for (int i = 0; i < totalTiles; i++)
            {
                var screenPos = new Vector3(
                    initWorld.x + i * sizeTile.x,
                    initWorld.y + (totalTiles - 1.0f - j) * sizeTile.y,
                    0.0f
                );

                tileVisualPosition = (screenPos);
                tileVisualPosition += offset;
                tileVisualPosition.z = 0.0f;

                tiles[i, j] = Instantiate(tileGO, tileVisualPosition, Quaternion.identity);//arreglar aquí posicionamiento
                TileLogic logic = tiles[i, j].GetComponent<TileLogic>();
                logic.map = this;
                InitRiver(logic, i, j);
                tiles[i, j].transform.SetParent(this.transform);

                tiles[i, j].name = string.Format("tile {0}, {1}", i, j);
            }
        }
    }

    void InitRandom(TileLogic logic, int x ,int y)
    {
        logic.OnInit(x, y);
    }

    void InitRiver(TileLogic logic, int x, int y)
    {
        int food = 8;
        int light = 9;
        int water = Mathf.Clamp(10 - Mathf.Abs(x - y), 0, 10);

        logic.OnInit(x, y, light, water, food);
    }

    void InitCoast(TileLogic logic, int x, int y)
    {
        int food = 10;
        int light = 8;
        int water = Mathf.Clamp(10 - x / 2, 0, 10);

        logic.OnInit(x, y, light, water, food);
    }

    public bool OnApplyResource(int x, int y, NaturalResource _resource)
    {
        TileLogic tile = tiles[x, y].GetComponent<TileLogic>();
        if (_resource.CheckTerrain(tile))
        {
            ApplyResourceEffect(_resource, x, y);
            AkSoundEngine.PostEvent("pl_plant_add", gameObject);
            //if (_resource.typeShape == NaturalResource.shapeAction.sphere)
            //{
            //    var range = _resource.rangeAction;
            //
            //    List<Vector2> tileAlreadyInflueced = new List<Vector2>();
            //    tileAlreadyInflueced.Add(new Vector2(x, y));
            //
            //    for (int i = 0; i < range; i++)
            //    {
            //
            //    }
            //
            //}

            return true;
        }
        else
        {
            return false;
        }

    }

    void ApplyResourceEffect(NaturalResource resource, int x, int y)
    {
        for (int xx = -resource.rangeAction; xx < resource.rangeAction; xx++)
        {
            for (int yy = -resource.rangeAction; yy < resource.rangeAction; yy++)
            {
                bool xValid = x + xx >= 0 && x + xx < 15;
                bool yValid = y + yy >= 0 && y + yy < 15;
                bool inRange = Mathf.Abs(xx) + Mathf.Abs(yy) <= resource.rangeAction;
                if (xValid && yValid && inRange && (xx != 0 || yy != 0))
                {
                    TileLogic currTile = tiles[x+xx, y+yy].GetComponent<TileLogic>();
                    currTile.luminosity += resource.luminosity;
                    currTile.humidity += resource.humidity;
                    currTile.nutrients += resource.nutrients;
                }
            }
        }

        for (int xx = -resource.rangeHealing; xx < resource.rangeHealing; xx++)
        {
            for (int yy = -resource.rangeHealing; yy < resource.rangeHealing; yy++)
            {
                bool xValid = x + xx >= 0 && x + xx < 15;
                bool yValid = y + yy >= 0 && y + yy < 15;
                bool inRange = Mathf.Abs(xx) + Mathf.Abs(yy) < resource.rangeHealing;
                if (xValid && yValid && inRange)
                {
                    TileLogic currTile = tiles[x+xx, y+yy].GetComponent<TileLogic>();
                    currTile.recovered = true;

                }
            }
        }
    }

    void InflueceRecursivity(int x, int y, List<Vector2> tileAlreadyInflueced, int range, int totalRange, NaturalResource _resource)
    {
        // TODO: revisar si es mayor igual o mayor
        if (x < 0 && x >= width) return;
        if (y < 0 && y >= height) return;

        if (tileAlreadyInflueced.Contains(new Vector2(x, y))) return;


        tileAlreadyInflueced.Add(new Vector2(x, y));
        tiles[x, y].GetComponent<TileLogic>().OnReceiveInfluence(_resource, range / totalRange);
        if (range != 0)
        {
            InflueceRecursivity(x + 1, y, tileAlreadyInflueced, range - 1, totalRange, _resource);
            InflueceRecursivity(x - 1, y, tileAlreadyInflueced, range - 1, totalRange, _resource);
            InflueceRecursivity(x, y + 1, tileAlreadyInflueced, range - 1, totalRange, _resource);
            InflueceRecursivity(x, y - 1, tileAlreadyInflueced, range - 1, totalRange, _resource);
        }
    }

    public int OnTilesRecovered(){
        int totalTilesRecovered = 0;
        for (int i = 0; i < 15; i++){
            for (int j = 0; j < 15; j++){
                totalTilesRecovered += tiles[i, j].GetComponent<TileLogic>().recovered ? 1 : 0;
            }
        }
        return totalTilesRecovered;
    }

    public void CalculateHumidity()
    {

    }

}
