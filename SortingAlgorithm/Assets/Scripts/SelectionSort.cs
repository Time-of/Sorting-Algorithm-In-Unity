using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// 선택 정렬
/// 매 반복마다 최솟값을 탐색하고, 왼쪽부터 차례대로 정렬
/// </summary>
public class SelectionSort : SortAlgorithmBase
{
	protected override IEnumerator SortCoroutine()
	{
		// 마지막 인덱스는 j에 의해 검사됩니다.
		for (int i = 0; i < Length - 1; ++i)
		{
			int MinIndex = i;

			// i 바로 다음부터 오른쪽 끝까지 검사
			for (int j = i + 1; j < Length; ++j)
			{
				Boxes[j].ColorGreen();

				// 최솟값을 탐색
				if (Boxes[j] < Boxes[MinIndex])
				{
					MinIndex = j;

					Boxes[MinIndex].ColorBlue();
				}

				yield return RepeatWaitTime;

				Boxes[j].ColorWhite();
			}

			// 최솟값을 앞에서부터 차례대로 세워나가며 정렬한다.
			if (i != MinIndex)
			{
				yield return Box.SwapCoroutine(Boxes[i], Boxes[MinIndex]);
			}
			else
			{
				yield return RepeatWaitTime;
				
				Boxes[i].ColorWhite();
			}
		}
	}
}
