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

    void OnMouseDown() // 마우스 좌버튼 눌렀을 때
    {
        _isDragging = true;
        _offset = transform.position - GetMouseWorldPosition(); // 객체의 현재위치 - 마우스의 월드 위치를 하여 중심점부터 마우스 현재 위치까지의 Offset을 찾는다.
    }

    void OnMouseUp() // 마우스 좌버튼 땠을 때
    {
        _isDragging = false;
        SnapToClosestTile();
    }

    void Update()
    {
        if (_isDragging) // isDrag즉, 좌버튼을 누르고 있는 상태라면
        {
            transform.position = GetMouseWorldPosition() + _offset; // 마우스의 월드 위치와, Offset을 더해 객체의 위치를 옮긴다.
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
            /// Z축을 이 객체의 Z축과 알맞도록 수정.
            Vector3 snappedPos = closestTile.transform.position;
            snappedPos.z = transform.position.z;
            transform.position = closestTile.transform.position;

        }
    }
}
