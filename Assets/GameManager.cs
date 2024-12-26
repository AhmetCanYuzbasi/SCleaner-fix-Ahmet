using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public GameObject DeathScreenUI;

    public int EnemyCounter = 0;
    public int BulletCounter = 0;
    public int KillCounter = 0;
    

    public void quitGame()
    {
        Application.Quit();
    }

    public void Game2MainMenu()
    {
        SceneManager.LoadScene("Main_Menu");
        Time.timeScale = 1f;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("odalar1");
        Time.timeScale = 1f;
    }

    public void EnemyStat()
    {
        EnemyCounter++;
        //Debug.Log(EnemyCounter);
    }

    public void BulletStat()
    {
        BulletCounter++;
        //Debug.Log(BulletCounter);
    }

    public void KillStat()
    {
        KillCounter++;
        Debug.Log(KillCounter);
        if(KillCounter > EnemyCounter)
        {
            KillCounter = EnemyCounter;
        }
    }

    public Text EnemyStatText;
    public Text KillStatText;
    public Text BulletStatText;
   
    public void EndGame()
    {
        DeathScreenUI.SetActive(true);
        Time.timeScale = 0f;
        EnemyStatText.text = EnemyCounter.ToString();
        KillStatText.text = KillCounter.ToString();
        BulletStatText.text = BulletCounter.ToString();
    }


}
