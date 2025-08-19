using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] players;

    public void CheckWinState()
    {
        int alivePlayers = 0;
        foreach (GameObject p in players)
        {
            if (p != null && p.activeSelf)
            {
                alivePlayers++;
            }
        }

        if (alivePlayers <= 1)
        {
            Invoke(nameof(NewRound), 3f);
        }
    }

    private void NewRound()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
