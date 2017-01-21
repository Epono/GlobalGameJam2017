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
		//TODO: gerer input pour orienter le _scanCollider.transform.rotation et l'action de Scan qui met _scanCollider.DoScan a true
	}

	//TODO: Set l'angle les données input
}
