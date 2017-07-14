using System;
using UnityEngine;

/// <summary>
/// 记录角色的碰撞信息
/// </summary>
public class CollisionInformation{
	public bool IsGround {
		get;
		set;
	}
	public bool IsStandingPlatform {
		get;
		set;
	}
	public bool HasCollisions {
		get{
			return IsGround || IsStandingPlatform;
		}
	}
		

	public void Reset(){
		IsGround = false;
		IsStandingPlatform = false;
	}
}

