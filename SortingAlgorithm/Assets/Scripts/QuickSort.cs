using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;



/// <summary>
/// 퀵 정렬
/// 분할-정복 방식의 정렬 알고리즘
/// 피벗을 기준으로 피벗보다 작은 값, 큰 값을 좌우로 분할하는 과정을 재귀적으로 실행하여 정렬하는 알고리즘이다.
/// 평균적으로 O(n log n)이나, 최악의 경우는 O(n^2)
/// **피벗의 선택에 따라서, 또는 이미 어느 정도 정렬되어 있거나, 중복 요소가 얼마나 있는가**에 따라 성능이 달라진다.
/// 정렬 과정에서 추가 메모리는 거의 필요하지 않다.
/// </summary>
public class QuickSort : SortAlgorithmBase
{
	private int Mid = 0;



	// 피벗을 기준으로 왼쪽은 피벗보다 작게, 오른쪽은 피벗보다 크게 분할
	private IEnumerator Partition(int Low, int High)
	{
		// 피벗보다 작은 값들을 왼쪽부터 배치하기 위한 인덱스
		int i = Low - 1;
		Mid = 0;

		// 가장 오른쪽의 값을 피벗으로 선택합니다
		Box Pivot = Boxes[High];

		for (int k = Low; k < High; ++k)
		{
			Boxes[k].ColorGreen();
			Pivot.ColorBlue();

			// 피벗보다 작은 값이라면
			if (Boxes[k] < Pivot)
			{
				// 인덱스를 더하면서 k번 인덱스와 스왑해줍니다
				yield return Box.SwapCoroutine(Boxes[++i], Boxes[k]);
			}
			else
			{
				yield return RepeatWaitTime;
			}

			Boxes[k].ColorWhite();
			Pivot.ColorWhite();
		}

		// 마지막으로 피벗과 인덱스를 스왑하면 분할 완료
		if (Boxes[i + 1] != Pivot)
		{
			yield return Box.SwapCoroutine(Boxes[++i], Pivot);
		}
		else
		{
			++i;
		}

		Mid = i;

		yield return null;
	}



	private IEnumerator ExecuteQuickSort(int Low, int High)
	{
		if (Low < High)
		{
			yield return Partition(Low, High);
			int NewPivot = Mid;

			// 좌측 절반에 대해 재귀
			yield return ExecuteQuickSort(Low, NewPivot - 1);

			// 우측 절반에 대해 재귀
			yield return ExecuteQuickSort(NewPivot + 1, High);
		}
	}



	protected override IEnumerator SortCoroutine()
	{
		yield return ExecuteQuickSort(0, BoxCount - 1);
	}
}
