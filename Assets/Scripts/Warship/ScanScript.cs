using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanScript : MonoBehaviour
{
	[SerializeField]
	private Transform _warshipTransform;

	[SerializeField]
	private ScanCollider _scanCollider;

	public void Update()
	{
		_scanCollider.WarshipPosition = _warshipTransform.position;
	}
}
