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

        Vector3 sizeTile = tileGO.GetComponent<SpriteRenderer>().sprite.bounds.size;
        Vector3 tileVisualPosition; //esta es la variable que contiene la posición real en la pantalla

        Vector2 initWorld = Camera.main.ScreenToWorldPoint(new Vector3(initXpos, initYpos, 0.0f));
        for (int j = 0; j < 15; j++)
        {
            for (int i = 0; i < 15; i++)
            {
                tileVisualPosition = new Vector3(initWorld.x + i * sizeTile.x, initWorld.y - j * sizeTile.y, 0.0f);
                tiles[i, j] = Instantiate(tileGO, tileVisualPosition, Quaternion.identity);//arreglar aquí posicionamiento
                tiles[i, j].GetComponent<TileLogic>().OnInit(i, j);
                tiles[i, j].transform.SetParent(this.transform);

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
}
