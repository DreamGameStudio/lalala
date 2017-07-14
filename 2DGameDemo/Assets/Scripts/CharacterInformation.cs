using System;
using UnityEngine;

/// <summary>
/// 记录角色的位置跟旋转信息,用于实现时光倒流的技能
/// </summary>
public class CharacterInformation{
	public Vector3 position;
	public Quaternion rotation;
	public CharacterInformation (Vector3 _position,Quaternion _rotation)
	{
		position = _position;
		rotation = _rotation;
	}
}

