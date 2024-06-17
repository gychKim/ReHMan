using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField]
    private Vector2 _pos = Vector2.zero; // 타일의 위치
    [SerializeField]
    private Color _baseColor, _offsetColor;
    [SerializeField]
    private SpriteRenderer _renderer;

    [SerializeField]
    private GameObject _highlight;

    private bool _isOverCharacter; // 캐릭터(마우스로 배치하려 하고 있는)가 나(타일) 위에 있으면 true
    public void Init(Vector2 pos, bool offset)
    {
        _pos = pos;
        _renderer.color = offset ? _offsetColor : _baseColor;
    }

    private void OnMouseDown()
    {
        Debug.Log(name);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Character"))
        {
            // 지금은 여러개가 같이 빛난다 > 하나만 빛나게 하고 싶다(배치되는 친구만).
            _isOverCharacter = true;
            CheckOver();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Character"))
        {
            _isOverCharacter = false;
            CheckOver();
        }
    }
    private void CheckOver()
    {
        if(_isOverCharacter)
        {
            _highlight.SetActive(true);
        }
        else
        {
            _highlight.SetActive(false);
        }
    }
}
