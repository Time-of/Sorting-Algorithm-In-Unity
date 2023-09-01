using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// 삽입 정렬
/// 카드 게임을 할 때 손 안의 카드를 정렬하는 방식과 유사하다.
/// 매 반복마다 현재 인덱스 왼쪽의 값들과 현재 인덱스의 값 중 큰 값을 오른쪽으로 계속해서 밀어내는 방식
/// 최선의 경우 무려 O(n)이라는 굉장한 성능이 나오기도 한다.
/// </summary>
public class InsertionSort : SortAlgorithmBase
{
	protected override IEnumerator SortCoroutine()
	{
		for (int i = 0; i < Length; ++i)
		{
			int Key = Boxes[i].Length;

			int j = i - 1;

			BoxManager.Instance.ColorBoxGreen(Boxes[i]);

			// j번 인덱스의 값이 Key보다 커질 커질 때까지
			while (j >= 0 && Boxes[j].Length > Key)
			{
				// 원소를 오른쪽으로 밀어냅니다.
				Box.Swap(Boxes[j + 1], Boxes[j], true);

				--j;

				yield return SwapWaitTime;

				BoxManager.Instance.ColorBoxWhite(Boxes[j + 2]);
				BoxManager.Instance.ColorBoxWhite(Boxes[j + 1]);
			}

			yield return RepeatWaitTime;
			
			BoxManager.Instance.ColorBoxWhite(Boxes[i]);
		}
	}
}
