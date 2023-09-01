using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// 버블 정렬
/// 각 원소와 원소의 오른쪽 원소 값을 비교하여, 원소가 더 크다면 스왑
/// </summary>
public class BubbleSort : SortAlgorithmBase
{
	protected override IEnumerator SortCoroutine()
	{
		for (int i = 0; i < Length; ++i)
		{
			// 우측으로부터 i번째까지의 원소는 이미 정렬된 상태이므로, 정렬하지 않습니다.
			for (int j = 0; j < Length - 1 - i; ++j)
			{
				BoxManager.Instance.ColorBoxGreen(Boxes[j]);
				BoxManager.Instance.ColorBoxGreen(Boxes[j + 1]);

				if (Boxes[j] > Boxes[j + 1])
				{
					Box.Swap(Boxes[j], Boxes[j + 1], true);

					yield return SwapWaitTime;
				}
				else
				{
					yield return RepeatWaitTime;
				}

				BoxManager.Instance.ColorBoxWhite(Boxes[j]);
				BoxManager.Instance.ColorBoxWhite(Boxes[j + 1]);
			}
		}
	}
}
