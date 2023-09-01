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



	public static void Swap(Box A, Box B, bool bUseTweening = false)
	{
		if (bUseTweening)
		{
			Vector3 OldAPos = A.transform.position;
			A.transform.DOMove(B.transform.position, 0.18f);
			B.transform.DOMove(OldAPos, 0.18f);
		}
		else
		{
			(B.transform.position, A.transform.position) = (A.transform.position, B.transform.position);
		}

		BoxManager.Instance.ColorBoxRed(A);
		BoxManager.Instance.ColorBoxRed(B);

		(BoxManager.Instance.Boxes[B.Index], BoxManager.Instance.Boxes[A.Index]) = (BoxManager.Instance.Boxes[A.Index], BoxManager.Instance.Boxes[B.Index]);
		(B.Index, A.Index) = (A.Index, B.Index);
	}



	public static bool operator >(Box a, Box b)
	{
		return a.InternalLength > b.InternalLength;
	}



	public static bool operator <(Box a, Box b)
	{
		return a.InternalLength < b.InternalLength;
	}
}
