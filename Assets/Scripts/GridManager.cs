using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class GridManager : MonoBehaviour
{
    [SerializeField]
    private int _rows; // 행 > 행의 수
    [SerializeField]
    private int _columns; // 열 > 한 행의 타일 수

    [SerializeField] 
    private Tile _tilePrefab; // 타일 프리펩

    [SerializeField]
    private Transform _cameraTrans; // 카메라

    [SerializeField]
    private Dictionary<Vector2, Tile> _tileDict = new Dictionary<Vector2, Tile>(); // 타일 Dict > 모든 타일의 위치와 정보를 지니고 있음
    private void Start()
    {
        GenerateGrid();
    }
    void GenerateGrid()
    {
        for(int y = 0; y < _rows; y++) 
        { 
            for(int x = 0; x < _columns; x++) 
            {
                Vector2 tilePos = new Vector2(x, y);
                Tile tile = Instantiate(_tilePrefab, tilePos, Quaternion.identity);
                tile.name = "Tile(" + x + "," + y + ")";

                bool offset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                tile.Init(tilePos, offset);


                _tileDict.Add(new Vector2(x, y), tile);
            }
        }

        _cameraTrans.position = new Vector3(_rows / 2f - 0.5f, 0f, -10f);
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
