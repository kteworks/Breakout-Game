using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour 
{
	public GameManager manager;

	void OnTriggerEnter2D (Collider2D col)
	{
		if(col.gameObject.tag == "Ball"){												
			manager.score++;															
			col.gameObject.GetComponent<Ball>().SetDirection(transform.position);		
			manager.blocks.Remove(gameObject);
			Destroy(gameObject);
			if (manager.blocks.Count == 0)												
				manager.WinGame();																									
		}
	}
}
