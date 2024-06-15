using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class GridManager : MonoBehaviour
{
    [SerializeField]
    private int _width;
    [SerializeField]
    private int _height;

    [SerializeField] 
    private Tile _tilePrefab;

    [SerializeField]
    private Transform _cameraTrans;

    [SerializeField]
    private Dictionary<Vector2, Tile> _tileDict = new Dictionary<Vector2, Tile>();
    private void Start()
    {
        GenerateGrid();
    }
    void GenerateGrid()
    {
        for(int x = 0; x < _width; x++) 
        { 
            for(int y = 0; y < _height; y++) 
            {
                Tile tile = Instantiate(_tilePrefab, new Vector3(x,y), Quaternion.identity);
                tile.name = "Tile(" + x + "," + y + ")";

                bool offset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                tile.Init(offset);


                _tileDict.Add(new Vector2(x, y), tile);
            }
        }

        _cameraTrans.position = new Vector3(_width / 2f - 0.5f, _height / 2f - 0.5f, -10f);
    }
    public Tile GetTilePoisition(Vector2 pos)
    {
        Tile tempTile = null;
        if(_tileDict.TryGetValue(pos, out tempTile))
        {
            return tempTile;
        }
        return null;
    }
}
