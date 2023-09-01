using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// 힙 정렬
/// 정렬 대상 컨테이너를 힙 상태로 만들고, 힙의 루트 노드를 빼내는 방식으로 정렬을 수행하는 알고리즘
/// 힙 구성에 O(n)의 시간 소요
/// 최종적으로 평균 및 최악의 경우에도 **O(n log n)의 안정적 성능** 제공
/// 정렬 중 추가 메모리 거의 필요 없음
/// 원소들 간 비교 횟수가 적다
/// **이미 정렬된 경우에 대한 최적화 어려움**
/// 배열을 사용하나 캐시 효율성은 떨어짐 (바로 근처의 원소는 거의 사용하지 않으므로)
/// </summary>
public class HeapSort : SortAlgorithmBase
{
	// 현재 인덱스를 힙 규칙에 따라 아래로 내려보내는 작업
	private IEnumerator MaxHeapify(int Size, int i)
	{
		int Largest = i;

		// 왼쪽, 오른쪽 자식을 인덱스로 구하는 것은, 힙이라는 특수한 조건 안에서만 가능
		// 왼쪽 자식의 인덱스
		int Left = 2 * i + 1;
		// 오른쪽 자식의 인덱스
		int Right = 2 * i + 2;

		// 왼쪽 자식이 현재 인덱스(부모)보다 크다면 왼쪽 자식을 '선택'
		if (Left < Size && Boxes[Left] > Boxes[Largest])
		{
			Largest = Left;

			Boxes[Largest].ColorGreen();

			yield return RepeatWaitTime;

			Boxes[Largest].ColorWhite();
		}

		// 오른쪽 자식이 현재 인덱스(부모)보다 크다면 오른쪽 자식을 '선택'
		if (Right < Size && Boxes[Right] > Boxes[Largest])
		{
			Largest = Right;

			Boxes[Largest].ColorGreen();

			yield return RepeatWaitTime;

			Boxes[Largest].ColorWhite();
		}

		// 현재 인덱스(부모)와 선택된 자식이 있다면(자식 중 하나라도 부모보다 크다면)
		if (Largest != i)
		{
			// 해당 자식과 부모의 위치 변경
			yield return Box.SwapCoroutine(Boxes[Largest], Boxes[i]);

			// 이를 자식 노드에 대해 재귀적으로 수행합니다
			yield return MaxHeapify(Size, Largest);
		}
	}



	private IEnumerator ExecuteHeapSort()
	{
		// 최대 힙 트리로 만들어줍니다.
		// 절반의 인덱스부터 진행하는 이유?
		// 힙 자료구조의 특성 상, 앞쪽 절반의 인덱스는 자식이 존재, 뒤쪽 절반은 자식이 없음.
		// 즉, 자식이 없는 노드에는 '힙 규칙에 따라 재귀적으로 내려보내기' 작업을 수행할 필요가 없습니다.
		for (int i = BoxCount / 2 - 1; i >= 0; --i)
		{
			yield return MaxHeapify(BoxCount, i);
		}

		// 최대 힙은 루트 노드가 가장 큰 값입니다.
		// 루트 노드를 뒤로 보내고, 남은 힙을 다시 최대 힙 구조로 만드는 과정을 반복합니다.
		// 이런 방식으로 루트 노드를 계속해서 빼내다 보면, 정렬이 완성되어 있게 됩니다.
		for (int i = BoxCount - 1; i >= 0; --i)
		{
			yield return Box.SwapCoroutine(Boxes[0], Boxes[i]);

			yield return MaxHeapify(i, 0);
		}
	}



	protected override IEnumerator SortCoroutine()
	{
		yield return ExecuteHeapSort();
	}
}
