using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Assets.Scripts;

public class MenuUI : MonoBehaviour 
{
	public GameObject selectButton;
	public GameObject quitButton;
	public GameObject playButton;
	public GameObject backButton;
	public GameObject normalPlayButton;
	public GameObject imagePlayButton;
	public GameObject normalModeObjects;
	public GameObject imageModeObjects;
	public GameObject explanation;
	public GameObject pathText;
	public GameObject xValue;
	public GameObject yValue;

	public void Start()
    {
		selectButton.SetActive(true);
		quitButton.SetActive(true);
		normalPlayButton.SetActive(false);
		imagePlayButton.SetActive(false);
		playButton.SetActive(false);
		backButton.SetActive(false);
		normalModeObjects.SetActive(false);
		imageModeObjects.SetActive(false);

		explanation.GetComponent<Text>().text = "";

		Sdata.mode = -1;
	}

	public void SelectButton()
    {
		selectButton.SetActive(false);
		normalPlayButton.SetActive(true);
		imagePlayButton.SetActive(true);
	}

	public void NormalPlayButton()
    {
		normalPlayButton.SetActive(false);
		imagePlayButton.SetActive(false);
		normalModeObjects.SetActive(true);
		playButton.SetActive(true);
		backButton.SetActive(true);

		explanation.GetComponent<Text>().text = "X:1～21 Y:1～16";

		Sdata.mode = 0;
	}

	public void ImagePlayButton()
    {
		normalPlayButton.SetActive(false);
		imagePlayButton.SetActive(false);
		imageModeObjects.SetActive(true);
		playButton.SetActive(true);
		backButton.SetActive(true);

		explanation.GetComponent<Text>().text = "画像形式pngのみ 解像度上限 21x16まで";

		Sdata.mode = 1;
    }

	public void ImageSelect()
    {
		System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();
		ofd.Filter = "PNG files (*.png)|*.png";
		ofd.Title = "PNG画像ファイルを選択してください";
		ofd.ShowDialog();

		Sdata.path = ofd.FileName;
		pathText.GetComponent<Text>().text = Sdata.path;
	}

	public void PlayButton ()
	{
		if(Sdata.mode == 0)
        {
			Sdata.x = int.Parse(xValue.GetComponent<Text>().text);
			Sdata.y = int.Parse(yValue.GetComponent<Text>().text);
		}

		SceneManager.LoadScene(1);
	}

	public void QuitButton ()
	{
		UnityEngine.Application.Quit();
	}
}
