using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;



/// <summary>
/// 합병 정렬
/// 분할-정복 방식의 정렬 알고리즘
/// 1. 정렬 대상을 각 분할된 컨테이너의 길이가 1이 될 때까지 분할한다.
/// 2. 각 좌우 대상의 원소를 비교하며 결과 컨테이너에 넣으며 정렬된 리스트를 만든다.
/// 3. 좌/우측 컨테이너 중 하나라도 정렬에 모두 사용했다면, 남은 컨테이너를 통째로 결과 컨테이너에 넣는다.
/// 4. 이렇게 모두 합쳐지고 정렬된 컨테이너가 상위 호출로 반환된다.
/// 정렬 과정에서 추가 메모리가 필요하다.
/// 최악의 경우에도 항상 O(n log n)의 속도를 가지나, 일반적으로는 퀵 정렬이 더 빠르다.
/// 안정 정렬이다. (정렬 후에도 원본의 순서를 유지한다. 이는 순서쌍의 정렬을 해 보면 바로 알 수 있을 것)
/// </summary>
public class MergeSort : SortAlgorithmBase
{
	private IEnumerator Merge(int Left, int Right)
	{
		int Mid = Left + (Right - Left) / 2;

		int LeftSize = Mid - Left + 1;
		int RightSize = Right - Mid;

		Box[] LeftBoxes = new Box[LeftSize];
		Box[] RightBoxes = new Box[RightSize];

		// 중앙을 기준으로 좌, 우 박스들 복사 수행 (여기서는 복사했다고 가정하고, 인덱스로 스왑 수행)
		for (int t = 0; t < LeftSize; ++t)
		{
			LeftBoxes[t] = Boxes[Left + t];
			Boxes[Left + t].ColorGreen();
		}

		for (int t = 0; t < RightSize; ++t)
		{
			RightBoxes[t] = Boxes[Mid + 1 + t];
			Boxes[Mid + 1 + t].ColorGreen();
		}

		int i = 0, j = 0, k = Left;

		// 좌측 컨테이너와 우측 컨테이너를 비교하여 작은 원소부터 컨테이너에 넣습니다.
		while (i < LeftSize && j < RightSize)
		{
			if (LeftBoxes[i] <= RightBoxes[j])
			{
				yield return Box.SwapByIndexCoroutine(Boxes[k++].Index, LeftBoxes[i++].Index);
			}
			else
			{
				yield return Box.SwapByIndexCoroutine(Boxes[k++].Index, RightBoxes[j++].Index);
			}
		}

		// 좌우 비교가 끝난 뒤 남은 컨테이너는 순서대로 전부 집어넣습니다.
		while (i < LeftSize)
		{
			yield return Box.SwapByIndexCoroutine(Boxes[k++].Index, LeftBoxes[i++].Index);
		}

		while (j < RightSize)
		{
			yield return Box.SwapByIndexCoroutine(Boxes[k++].Index, RightBoxes[j++].Index);
		}

		yield return null;
	}



	private IEnumerator Sort(int Left, int Right)
	{
		if (Left < Right)
		{
			int Mid = Left + (Right - Left) / 2;

			// 좌측 절반에 대해 재귀
			yield return Sort(Left, Mid);

			// 우측 절반에 대해 재귀
			yield return Sort(Mid + 1, Right);

			yield return Merge(Left, Right);
		}
	}



	protected override IEnumerator SortCoroutine()
	{
		yield return Sort(0, BoxCount - 1);
	}
}
