using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Asset/CharacterData")]
public class CharacterProfile : ScriptableObject
{
    public string Name;
    public string LvL;
    public int Health;
}
