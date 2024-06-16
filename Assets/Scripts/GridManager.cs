using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class GridManager : MonoBehaviour
{
    [SerializeField]
    private int _rows; // �� > ���� ��
    [SerializeField]
    private int _columns; // �� > �� ���� Ÿ�� ��

    [SerializeField] 
    private Tile _tilePrefab; // Ÿ�� ������

    [SerializeField]
    private Transform _cameraTrans; // ī�޶�

    [SerializeField]
    private Dictionary<Vector2, Tile> _tileDict = new Dictionary<Vector2, Tile>(); // Ÿ�� Dict > ��� Ÿ���� ��ġ�� ������ ���ϰ� ����
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
                Tile tile = Instantiate(_tilePrefab, new Vector3(x,y), Quaternion.identity);
                tile.name = "Tile(" + x + "," + y + ")";

                bool offset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                tile.Init(offset);


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
