using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;



public class MergeSort : SortAlgorithmBase
{
	private IEnumerator Partition(int Low, int High)
	{
		// 피벗보다 작은 값들을 왼쪽부터 배치하기 위한 인덱스
		int i = Low - 1;
		//Mid = 0;

		// 가장 오른쪽의 값을 피벗으로 선택합니다
		Box Pivot = Boxes[High];

		for (int k = Low; k < High; ++k)
		{
			Boxes[k].ColorGreen();
			Pivot.ColorGreen();

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
		yield return Box.SwapCoroutine(Boxes[++i], Pivot);

		//Mid = i;

		yield return null;
	}



	private IEnumerator Merge(int Left, int Right)
	{
		int Mid = Left + (Right - Left) / 2;

		int LeftSize = Mid - Left + 1;
		int RightSize = Right - Mid;

		Box[] LeftBoxes = new Box[LeftSize];
		Box[] RightBoxes = new Box[RightSize];

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
		yield return Sort(0, Length - 1);
	}
}
