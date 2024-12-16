using System.Collections;
using System.Collections.Generic;
using UnityEngine;
	
[CreateAssetMenu(fileName = "New StarSystemSO", menuName = "Star Systen Data", order = 51)]

public class StarSystemSO : ScriptableObject
{
	[SerializeField]
	private string systemName;
	[SerializeField]
	private int planetcount;
	[SerializeField]
	private Sprite starsprite;
	[SerializeField]
	[Tooltip
		("Civilization can be SpaceFaring, Advanced, Warlike")]
	private string civtype;
	public int population;
	
	public string GetsystemName
	{
		get
		{
			return systemName;
		}
	}
}
