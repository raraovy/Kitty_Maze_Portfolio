using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "KittyPrefabData")]
public class KittyPrefabData : ScriptableObject
{
    [SerializeField]
    public GameObject kittyPrefab;
}
