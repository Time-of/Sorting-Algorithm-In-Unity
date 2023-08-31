using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class BoxManager : MonoBehaviour
{
	[HideInInspector]
	public List<Box> Boxes;

	public int BoxCount = 0;

	[SerializeField]
	private Box BoxPrefab = null;



	private void Awake()
	{
		MakeBoxes();
	}



	private void Start()
    {
		ShuffleBoxes();

		Camera.main.transform.position = new Vector3(0.5f * BoxCount, 1.1f * BoxCount, -1.2f * BoxCount);
		Camera.main.transform.rotation = Quaternion.Euler(15.0f, 0.0f, 0.0f);
	}



	private void MakeBoxes()
	{
		for (int i = 0; i < BoxCount; ++i)
		{
			Box BoxInst = Instantiate(BoxPrefab, new Vector3(i, 0, 0), Quaternion.identity);
			BoxInst.Length = i + 1;

			Boxes.Add(BoxInst);
		}
	}



	private void ShuffleBoxes()
	{
		for (int i = BoxCount - 1; i > 0; --i)
		{
			int k = Random.Range(0, i);
			Boxes[i].Swap(Boxes[k]);
		}
	}
}
