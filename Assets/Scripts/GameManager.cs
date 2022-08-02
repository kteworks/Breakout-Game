using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;

public class GameManager : MonoBehaviour
{
	public int score;
	public int lives;
	public int stageNo;
	public int ballSpeedIncrement;
	public bool gameOver;
	public bool wonGame;

	public GameObject paddle;
	public GameObject ball;

	public GameUI gameUI;
	
	public GameObject blockPrefab;
	public GameObject linePrefab;

	public List<GameObject> blocks = new List<GameObject>();

	public GameObject wall;

	public int blockCountX;
	public int blockCountY;

	public Color[] colors;

	void Start ()
	{
		StartGame(); 
	}

	
	public void StartGame ()
	{
		score = 0;
		lives = 3;
		gameOver = false;
		wonGame = false;
		paddle.SetActive(true);
		ball.SetActive(true);
		paddle.GetComponent<Paddle>().ResetPaddle();
		if(Sdata.mode == 0)
        {
			blockCountX = Sdata.x;
			blockCountY = Sdata.y;
			CreateBlockArray();
		}
		else if(Sdata.mode == 1)
        {
			ImageCreateBlockArray();
		}
		
	}

	public void CreateWall()
	{
		float ballSize = ball.GetComponent<SpriteRenderer>().bounds.size.x / 2;

		wall = Instantiate(linePrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
		LineRenderer line = wall.GetComponent<LineRenderer>();
		line.positionCount = 4;

		line.SetPosition(0, new Vector3(Sdata.minX - ballSize, Sdata.minY - ballSize, 0));
		line.SetPosition(1, new Vector3(Sdata.minX - ballSize, Sdata.maxY +ballSize, 0));
		line.SetPosition(2, new Vector3(Sdata.maxX + ballSize, Sdata.maxY + ballSize, 0));
		line.SetPosition(3, new Vector3(Sdata.maxX + ballSize, Sdata.minY - ballSize, 0));
	}

	public void CreateBlockArray()
	{
		CreateWall();

		int colorId = 0;

		Vector3 pos;
		GameObject block;

		bool odd;

		float space;
		for (int y = 0; y < blockCountY; y++)
		{
			if (odd = (blockCountX % 2 == 1))
			{
				pos = new Vector3(0, y * Sdata.blockH + Sdata.blockLowerLimit, 0);
				block = Instantiate(blockPrefab, pos, Quaternion.identity) as GameObject;
				block.GetComponent<Block>().manager = this;
				block.GetComponent<SpriteRenderer>().color = colors[colorId];
				blocks.Add(block);
			}

			for (int x = -(blockCountX / 2); x < (blockCountX / 2); x++)
			{
				space = Sdata.blockW / 2 + (x * Sdata.blockW);

				if (odd)
				{
					if (x == 0)
					{
						space = Sdata.blockW;
					}
					else if (x > 0)
					{
						space += Sdata.blockW;
					}
					else
					{
						space = x * Sdata.blockW;
					}
				}
				else
				{
					space = Sdata.blockW / 2 + (x * Sdata.blockW);
				}
				pos = new Vector3(space, y * Sdata.blockH + Sdata.blockLowerLimit, 0);
				block = Instantiate(blockPrefab, pos, Quaternion.identity) as GameObject;
				block.GetComponent<Block>().manager = this;
				block.GetComponent<SpriteRenderer>().color = colors[colorId];
				blocks.Add(block);
			}

			colorId++;

			if (colorId == colors.Length)
				colorId = 0;
		}

		ball.GetComponent<Ball>().ResetBall();
	}

	public void ImageCreateBlockArray()
	{
		string path = Sdata.path;

		Texture2D texture = Image.ReadPng(path);

		int height = texture.height;
		int width = texture.width;

		int colorId;

		Vector3 pos;
		GameObject block;

		Color color;

		bool odd;

		float space = 0;
		
		for (int y = 0; y < height; y++)
		{

			colorId = width / 2;
			if (odd = (width % 2 == 1))
			{
				color = texture.GetPixel(colorId, y);
				if (color.a == 1)
				{
					pos = new Vector3(0, y * Sdata.blockH + Sdata.blockLowerLimit, 0);
					block = Instantiate(blockPrefab, pos, Quaternion.identity) as GameObject;
					block.GetComponent<Block>().manager = this;
					block.GetComponent<SpriteRenderer>().color = color;
					blocks.Add(block);
				}
			}
			
			for (int x = -(width / 2); x < (width / 2); x++)
			{
				if (odd)
				{

					if (x == 0)
					{
						space = Sdata.blockW;
						colorId += 1;
						
					}
					else if (x > 0)
					{
						space += Sdata.blockW;
					}
					else
					{
						space = x * Sdata.blockW;
					}
				}
				else
                {
					space = Sdata.blockW / 2 + (x * Sdata.blockW);
				}

				color = texture.GetPixel(colorId + x, y);
				if (color.a == 0)
					continue;

				pos = new Vector3(space, y * Sdata.blockH + Sdata.blockLowerLimit, 0);
				block = Instantiate(blockPrefab, pos, Quaternion.identity) as GameObject;
				block.GetComponent<Block>().manager = this;
				block.GetComponent<SpriteRenderer>().color = color;
				blocks.Add(block);
			}
		}


		CreateWall();
		ball.GetComponent<Ball>().ResetBall();
	}


	public void WinGame ()
	{
		Destroy(wall);
		wonGame = true;
		paddle.SetActive(false);			
		ball.SetActive(false);
		gameUI.SetWin();

		for (int x = 0; x < blocks.Count; x++)
		{
			Destroy(blocks[x]);
		}
		blocks = new List<GameObject>();
	}

	
	public void LiveLost ()
	{
		lives--;										

		if(lives < 0){
			Destroy(wall);
			gameOver = true;
			paddle.SetActive(false);						
			ball.SetActive(false);
			gameUI.SetGameOver();						

			for(int x = 0; x < blocks.Count; x++){		
				Destroy(blocks[x]);						
			}

			blocks = new List<GameObject>();			
		}
	}
}
