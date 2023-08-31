using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Box : MonoBehaviour
{
	private int InternalLength = 1;
	


	public int Length
	{
		get { return InternalLength; }
		set
		{
			InternalLength = value;
			transform.localScale = new Vector3(1, InternalLength, 1);
		}
	}



	public void Swap(Box Target)
	{
		Vector3 OldPos = transform.position;
		transform.position = Target.transform.position;
		Target.transform.position = OldPos;
	}
}
