using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

// 타일을 캐릭터 타일, 아이템 타일 이런식으로 나눠야 할까?
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

    private Character _character;

    private Transform _overObject; // 타일위에 있는 객체, 객체(마우스로 배치하려 하고 있는)가 나(타일) 위에 있을 때 그 객체
    public void Init(Vector2 pos, bool offset)
    {
        _pos = pos;
        _renderer.color = offset ? _offsetColor : _baseColor;
    }
    public void InitCharacter(Character character) 
    {
        _character = character;
        // _character가 없어지는 건(다른 Tile로 드래그 드롭하는 경우) 어떻게 구현할까?

        // 1,2 중 하나 택하기.
        // 1. 매니저에게 내 위치에 플레이어가 생겼다고 알려주기
        // 2. 어차피 매니저가 나를 Dict처럼 관리하고 있으니까 필요할 때, 내가 가진 정보를 꺼내 쓸 수 있도록 구축해놓기.
    }
    /// <summary>
    /// 타일 위의 캐릭터 정보를 초기화 시킨다.<br></br>
    /// 캐릭터 및 기타 객체가 타일에서 나갔을 때 호출<br></br>
    /// 지금은 Character만 존재 > 나중에 객체마다 함수 오버로딩 해야할 듯?
    /// </summary>
    public void ClearTile(Character character)
    {
        _character = null;
    }
    public void SetHighlightTile(bool value)
    {
        _highlight.SetActive(value);
    }
    private void OnMouseDown()
    {
        if(_character != null)
        {
            Debug.Log("타일의 위치 : " + _pos);
            Debug.Log("캐릭터 이름 : " +_character.name);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Character"))
        {
            // 지금은 여러개가 같이 빛난다 > 하나만 빛나게 하고 싶다(배치되는 친구만).
            _overObject = collision.transform;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Character"))
        {
            _overObject = null;
        }
    }
}
