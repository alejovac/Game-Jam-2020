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
                var screenPos = new Vector3
                    (
                        initWorld.x + i * sizeTile.x,
                        initWorld.y + (totalTiles - 1.0f - j) * sizeTile.y,
                        0.0f
                    );

                tileVisualPosition = (screenPos);
                tileVisualPosition += offset;
                tileVisualPosition.z = 0.0f;

                tiles[i, j] = Instantiate(tileGO, tileVisualPosition, Quaternion.identity);//arreglar aquí posicionamiento
                tiles[i, j].GetComponent<TileLogic>().OnInit(i, j);
                tiles[i, j].transform.SetParent(this.transform);

                tiles[i, j].name = string.Format("tile {0}, {1}", i, j);
            }
        }
    }

    void OnApplyResource(int x, int y, NaturalResource _resource)
    {

        if (tiles[x, y].GetComponent<TileLogic>().OnApplyResource(_resource))
        {
            if (_resource.typeShape == NaturalResource.shapeAction.sphere)
            {
                var range = _resource.rangeAction;

                List<Vector2> tileAlreadyInflueced = new List<Vector2>();
                tileAlreadyInflueced.Add(new Vector2(x, y));

                for (int i = 0; i < range; i++)
                {

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
