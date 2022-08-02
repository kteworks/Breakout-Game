using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class Ball : MonoBehaviour
{
	public float speed;				
	public float maxSpeed;			
	public Vector2 direction;		
	public Rigidbody2D rig;			
	public GameManager manager;		
	public bool goingLeft;			
	public bool goingDown;			

	void Start ()
	{
		transform.position = new Vector3(0,-2,0);		
		direction = Vector2.down;				
		StartCoroutine("ResetBallWaiter");
	}

	public void Update ()
	{
		rig.velocity = direction * speed * Time.deltaTime;

		if(transform.position.x > Sdata.maxX && !goingLeft){					
			direction = new Vector2(-direction.x, direction.y);		
			goingLeft = true;										
		}
		if(transform.position.x < Sdata.minX && goingLeft){					
			direction = new Vector2(-direction.x, direction.y);		
			goingLeft = false;										
		}
		if(transform.position.y > Sdata.maxY && !goingDown){					
			direction = new Vector2(direction.x, -direction.y);		
			goingDown = true;										
		}
		if(transform.position.y < Sdata.minY)
		{								
			ResetBall();											
		}
	}

	
	public void SetDirection (Vector3 target)
	{
		direction = new Vector2();

		direction = transform.position - target;
		direction.x *= -1;
		direction.Normalize();											

		speed += manager.ballSpeedIncrement;

		if(speed > maxSpeed)					
			speed = maxSpeed;					

		if(direction.x > 0)							
			goingLeft = false;
		if(direction.x < 0)							
			goingLeft = true;	
		if(direction.y > 0)							
			goingDown = false;
		if(direction.y < 0)							
			goingDown = true;
	}

	
	public void ResetBall ()
	{
		transform.position = new Vector3(0, Sdata.ballStartY, 0);
		direction = Vector2.down;				
		StartCoroutine("ResetBallWaiter");		
		manager.LiveLost();
	}

	
	IEnumerator ResetBallWaiter ()
	{
		speed = 0;
		yield return new WaitForSeconds(1.0f);
		speed = 200;
	}
}
