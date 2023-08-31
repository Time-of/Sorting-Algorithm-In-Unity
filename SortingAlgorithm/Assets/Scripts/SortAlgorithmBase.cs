using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SortAlgorithmBase : MonoBehaviour
{
	public static WaitForSeconds SortWaitTime = new(0.1f);

	protected Box[] Boxes;

	protected int Length;



	private void Start()
	{
		Boxes = BoxManager.Instance.Boxes;
		Length = BoxManager.Instance.BoxCount;
	}



	public void Sort()
    {
		StartCoroutine(SortCoroutine());
	}



	protected virtual IEnumerator SortCoroutine()
	{
		yield return null;
	}
}
