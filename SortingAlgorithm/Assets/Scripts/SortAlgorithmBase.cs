using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SortAlgorithmBase : MonoBehaviour
{
	public static WaitForSeconds RepeatWaitTime = new(0.1f);

	public static WaitForSeconds SwapWaitTime = new(0.2f);

	protected Box[] Boxes;

	protected int BoxCount;



	private void Start()
	{
		Boxes = BoxManager.Instance.Boxes;
		BoxCount = BoxManager.Instance.BoxCount;
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
