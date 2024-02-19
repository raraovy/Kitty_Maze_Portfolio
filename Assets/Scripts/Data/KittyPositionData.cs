using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
//고양이 출발 및 도착지 위치 정보를 관리하는 클래스
public class KittyPositionData
{
    public Vector3 startPosition;
    public Quaternion startRotation;
    public Vector3 endPosition;
}
