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
				Boxes[j].ColorGreen();
				Boxes[j + 1].ColorGreen();

				if (Boxes[j] > Boxes[j + 1])
				{
					yield return Box.SwapCoroutine(Boxes[j], Boxes[j + 1]);
				}
				else
				{
					yield return RepeatWaitTime;

					Boxes[j].ColorWhite();
					Boxes[j + 1].ColorWhite();
				}
			}
		}
	}
}
