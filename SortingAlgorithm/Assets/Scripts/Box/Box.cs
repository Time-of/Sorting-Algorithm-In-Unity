using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Box : MonoBehaviour
{
	private int InternalLength = 1;

	public int Index = -1;

	public Renderer RenderComp;



	public int Length
	{
		get { return InternalLength; }
		set
		{
			InternalLength = value;
			transform.localScale = new Vector3(1, InternalLength, 1);
		}
	}



	public static void Swap(Box A, Box B, bool bDoNotChangePosition = false)
	{
		Swap(A, B, BoxManager.Instance.Boxes, bDoNotChangePosition);
	}



	public static void Swap(Box A, Box B, Box[] BoxArray, bool bDoNotChangePosition = false)
	{
		if (!bDoNotChangePosition)
		{
			(B.transform.position, A.transform.position) = (A.transform.position, B.transform.position);
		}

		A.ColorRed();
		B.ColorRed();

		(BoxArray[B.Index], BoxArray[A.Index]) = (BoxArray[A.Index], BoxArray[B.Index]);
		(B.Index, A.Index) = (A.Index, B.Index);
	}



	public static IEnumerator SwapCoroutine(Box A, Box B)
	{
		Vector3 OldAPos = A.transform.position;
		A.transform.DOMove(B.transform.position, 0.18f);
		B.transform.DOMove(OldAPos, 0.18f);

		Swap(A, B, true);

		yield return SortAlgorithmBase.SwapWaitTime;

		A.ColorWhite();
		B.ColorWhite();

		yield return null;
	}



	public static IEnumerator SwapByIndexCoroutine(int AIndex, int BIndex)
	{
		Box A = BoxManager.Instance.Boxes[AIndex];
		Box B = BoxManager.Instance.Boxes[BIndex];

		Vector3 OldAPos = A.transform.position;
		A.transform.DOMove(B.transform.position, 0.18f);
		B.transform.DOMove(OldAPos, 0.18f);

		Swap(A, B, true);

		yield return SortAlgorithmBase.SwapWaitTime;

		A.ColorWhite();
		B.ColorWhite();

		yield return null;
	}



	public static bool operator >(Box a, Box b)
	{
		return a.InternalLength > b.InternalLength;
	}



	public static bool operator <(Box a, Box b)
	{
		return a.InternalLength < b.InternalLength;
	}



	public static bool operator >=(Box a, Box b)
	{
		return a.InternalLength >= b.InternalLength;
	}



	public static bool operator <=(Box a, Box b)
	{
		return a.InternalLength <= b.InternalLength;
	}



	public static bool operator !=(Box a, Box b)
	{
		return a.InternalLength != b.InternalLength;
	}



	public static bool operator ==(Box a, Box b)
	{
		return a.InternalLength == b.InternalLength;
	}



	public void ColorWhite()
	{
		RenderComp.material = BoxManager.Instance.BoxOriginalColor;
	}



	public void ColorGreen()
	{
		RenderComp.material = BoxManager.Instance.BoxGreenColor;
	}



	public void ColorRed()
	{
		RenderComp.material = BoxManager.Instance.BoxRedColor;
	}



	public void ColorBlue()
	{
		RenderComp.material = BoxManager.Instance.BoxBlueColor;
	}
}
