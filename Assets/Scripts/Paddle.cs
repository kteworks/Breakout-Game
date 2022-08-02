using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class Paddle : MonoBehaviour 
{
	public float speed;
	public GameManager manager;			
	public bool canMove;			
	public Rigidbody2D rig;			

	void Update ()
	{

		if (canMove){															
			if(Input.GetKey(KeyCode.LeftArrow)){								
				rig.velocity = new Vector2(-1 * speed * Time.deltaTime, 0);		
			}
			else if(Input.GetKey(KeyCode.RightArrow)){							
				rig.velocity = new Vector2(1 * speed * Time.deltaTime, 0);		
			}
			else{
				rig.velocity = Vector2.zero;									
			}

			transform.position = new Vector3(Mathf.Clamp(transform.position.x, Sdata.minX + (this.GetComponent<SpriteRenderer>().bounds.size.x / 2), Sdata.maxX - (this.GetComponent<SpriteRenderer>().bounds.size.x / 2)), transform.position.y, 0) ;	
		}
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if(col.gameObject.tag == "Ball"){											
			col.gameObject.GetComponent<Ball>().SetDirection(transform.position);	
		}
	}

	public void ResetPaddle ()
	{
		transform.position = new Vector3(0, transform.position.y, 0);
	}
}
