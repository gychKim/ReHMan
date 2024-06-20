using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 지금은 Character지만, 후에 DragDropSnap으로 바꿔야 할 듯?
/// </summary>
public class Character : MonoBehaviour
{
    private bool _isDragging = false;
    private Vector3 _offset;

    private Tile _prevTile; // 본인이 이전에 있었던 타일
    private Tile _closestTile; // 본인이 놓여있는 타일

    void OnMouseDown() // 마우스 좌버튼 눌렀을 때
    {
        _isDragging = true;
        _offset = transform.position - GetMouseWorldPosition(); // 객체의 현재위치 - 마우스의 월드 위치를 하여 중심점부터 마우스 현재 위치까지의 Offset을 찾는다.
    }

    void OnMouseUp() // 마우스 좌버튼 땠을 때
    {
        _isDragging = false;
        if(_prevTile != null) // 이전에 머물렀던 타일이 있었다면, 
        {
            _prevTile.ClearTile(this); // 정보를 초기화 시켜준다.
        }

        if(_closestTile != null)
        {
            _closestTile.InitCharacter(this);

            /// Z축을 이 객체의 Z축과 알맞도록 수정.
            Vector3 snappedPos = _closestTile.transform.position;
            snappedPos.z = transform.position.z;
            transform.position = _closestTile.transform.position;

            /// 아니면 아예 tile에 자식으로 넣는 로직을 만들어서, 함수를 호출하도록?
        }
        //SnapToClosestTile();
    }

    void Update()
    {
        if (_isDragging) // isDrag즉, 좌버튼을 누르고 있는 상태라면
        {
            transform.position = GetMouseWorldPosition() + _offset; // 마우스의 월드 위치와, Offset을 더해 객체의 위치를 옮긴다.
            HighlightTile();
        }
    }

    /// <summary>
    /// 마우스의 월드 위치를 받아오는 함수
    /// </summary>
    /// <returns></returns>
    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePoint = Input.mousePosition; // 마우스 위치를 찾은 후
        mousePoint.z = 0f; // z는 0으로 설정한 다음
        return Camera.main.ScreenToWorldPoint(mousePoint); // 마우스의 World위치를 리턴한다.
    }

    private void SnapToClosestTile()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.5f); // 객체 중심으로 반지름이 0.5인 원형 Collider를 생성 > 현재 객체의 Scale이 1이라 0.5로 설정
        Collider2D closestTile = null; // 가까운 타일을 받는 변수
        float minDistance = Mathf.Infinity; // 가장 가까운 타일과의 거리

        foreach (Collider2D collider in colliders) // Collider에 닿인 모든 Collider들을 순회하여, 가장 근접한 Tile을 closestTile에 넣는다.
        {
            if (collider.CompareTag("Tile"))
            {
                float distance = Vector2.Distance(transform.position, collider.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestTile = collider;
                }
            }
        }

        if (closestTile != null)
        {
            Tile tile = closestTile.GetComponent<Tile>();

            tile.InitCharacter(this);

            /// Z축을 이 객체의 Z축과 알맞도록 수정.
            Vector3 snappedPos = closestTile.transform.position;
            snappedPos.z = transform.position.z;
            transform.position = closestTile.transform.position;

            /// 아니면 아예 tile에 자식으로 넣는 로직을 만들어서, 함수를 호출하도록?
        }
    }

    /// <summary>
    /// 내 아래에 타일이 있다면 그 타일 중 가장 가까운 타일을 빛나게 만든다<br> </br>
    /// 타일에서 하려하니, 가장 가까운 타일을 찾기 위해서 Update를 돌려야 한다 > 손해 <br> </br>
    /// 객체에서 처리한다 > Tile에서 처리하는게 자연스럽지만, 이게 더 가벼움 > 사실 이것도 Update써야 하긴 함. <br> </br>
    /// 이건 기능만 있는 그 컴포넌트이므로 적어도 괜찮을 듯? <br> </br>
    /// </summary>
    private void HighlightTile()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, new Vector2(1.0f, 1.0f), 0f); // 현재 객체의 Scale로 변경해도 괜찮을 듯
        Tile newClosestTile = null;
        float minDistance = Mathf.Infinity;

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Tile"))
            {
                float distance = Vector2.Distance(transform.position, collider.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    newClosestTile = collider.GetComponent<Tile>();
                }
            }
        }

        if (newClosestTile != _closestTile) // 더 가까운 Tile이 생겼다면,
        {
            if (_closestTile != null) // 기존타일의 하이라이트를 끈다.
            {
                _closestTile.SetHighlightTile(false);
                _prevTile = _closestTile;
            }

            _closestTile = newClosestTile; // 새로운 타일을 등록
            if (_closestTile != null) // 새로운 타일의 하이라이트를 킨다.
            {
                _closestTile.SetHighlightTile(true);
            }
        }
    }
}
