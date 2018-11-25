using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Variables/Dictionary<int, Go>", fileName = "Dictionary_")]
public class DictVariable : ScriptableObject 
{
	public Dictionary<int, GameObject> Value;
}
