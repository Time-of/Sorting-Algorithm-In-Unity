﻿using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;



public class BoxManager : MonoBehaviour
{
	private static BoxManager _Instance;

	[HideInInspector]
	public Box[] Boxes;

	public int BoxCount = 0;

	[SerializeField]
	private Box BoxPrefab = null;

	public Material BoxOriginalColor;

	public Material BoxGreenColor;

	public Material BoxRedColor;

	public Material BoxBlueColor;



	public static BoxManager Instance
	{
		get { return _Instance; }
		private set { _Instance = value; }
	}



	private void Awake()
	{
		Instance = this;
		MakeBoxes();

		DOTween.Init(true, true);
	}



	private void Start()
    {
		ShuffleBoxes();

		Camera.main.transform.position = new Vector3(0.5f * BoxCount, 1.1f * BoxCount, -1.2f * BoxCount);
		Camera.main.transform.rotation = Quaternion.Euler(15.0f, 0.0f, 0.0f);
	}



	private void MakeBoxes()
	{
		Boxes = new Box[BoxCount];

		for (int i = 0; i < BoxCount; ++i)
		{
			Box BoxInst = Instantiate(BoxPrefab, new Vector3(i, 0, 0), Quaternion.identity);
			BoxInst.Length = i + 1;
			BoxInst.Index = i;

			Boxes[i] = BoxInst;
		}
	}



	private void ShuffleBoxes()
	{
		for (int i = BoxCount - 1; i > 0; --i)
		{
			int k = Random.Range(0, i);
			Box.Swap(Boxes[i], Boxes[k]);
		}

		foreach (Box box in Boxes)
		{
			box.ColorWhite();
		}
	}
}
