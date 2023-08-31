using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BubbleSort : SortAlgorithmBase
{
	public override void Sort()
	{
		StartCoroutine(SortCoroutine());
	}



	private IEnumerator SortCoroutine()
	{
		Box[] Boxes = BoxManager.Instance.Boxes;
		int Length = BoxManager.Instance.BoxCount;

		for (int i = 0; i < Length; ++i)
		{
			// 우측으로부터 i번째까지의 원소는 이미 정렬된 상태이므로, 정렬하지 않습니다.
			for (int j = 0; j < Length - 1 - i; ++j)
			{
				BoxManager.Instance.ColorBoxGreen(Boxes[j]);
				BoxManager.Instance.ColorBoxGreen(Boxes[j + 1]);

				if (Boxes[j] > Boxes[j + 1])
				{
					Box.Swap(Boxes[j], Boxes[j + 1]);
				}

				yield return SortWaitTime;

				BoxManager.Instance.ColorBoxWhite(Boxes[j]);
				BoxManager.Instance.ColorBoxWhite(Boxes[j + 1]);
			}
		}
	}
}
