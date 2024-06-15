using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField]
    private Color _baseColor, _offsetColor;
    [SerializeField]
    private SpriteRenderer _renderer;

    [SerializeField]
    private GameObject _highlight;
    public void Init(bool offset)
    {
        _renderer.color = offset ? _offsetColor : _baseColor;
    }
    private void OnMouseEnter()
    {
        _highlight.SetActive(true);
    }

    private void OnMouseExit() 
    {
        _highlight.SetActive(false);
    }
    private void OnMouseDown()
    {
        Debug.Log(name);
    }
}
