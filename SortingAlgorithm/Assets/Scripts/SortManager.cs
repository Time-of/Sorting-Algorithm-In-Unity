using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SortManager : MonoBehaviour
{
	[Header("정렬 알고리즘 Prefab을 넣고,")]
	[Header("런타임 중 스페이스바 버튼을 누르면 됩니다")]
	[Space(32)]

    [SerializeField]
    private GameObject SortPrefab;

	private SortAlgorithmBase SortAlgorithmInstance;



	private void Awake()
	{
		SortAlgorithmInstance = Instantiate(SortPrefab, transform).GetComponent<SortAlgorithmBase>();
	}



	private void Update()
	{
		if (Input.GetKeyUp(KeyCode.Space))
		{
			if (SortAlgorithmInstance != null)
			{
				SortAlgorithmInstance.Sort();
			}
			else
			{
				Debug.Log("SortManager: 정렬 알고리즘이 유효하지 않음");
			}
		}
	}
}
