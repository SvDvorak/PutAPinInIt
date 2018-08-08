using Assets;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ActivateOnGameOver : MonoBehaviour
{
    public TextMeshProUGUI Score;
    private bool _hasActivated;

    public void Update()
    {
        if (GameState.GameOver && !_hasActivated)
        {
            _hasActivated = true;
            GetComponent<Animator>().SetTrigger("Activated");
            Score.text = GameState.GrenadesSpawned + " points";
        }

        if (Input.GetMouseButtonUp(0) && _hasActivated)
        {
            GameState.Reset();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}