using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PauseMenu : MonoBehaviour
{
    public float transitionTime = 3f;
    private float timer;
    public AudioClip audioOnClick;
    [SerializeField]private GameObject[] availablePanels; 


    private void Start() {
        foreach(GameObject i in availablePanels)
        {
            i.SetActive(false);
        }
        availablePanels[0].SetActive(true);
    }



    public void PauseGame()
    {
        Time.timeScale = 0f;
    }
    public void UnpauseGame()
    {
        Time.timeScale = 1f;
    }

    public void StartGame()
    {
        StartCoroutine(StarGameCoroutine());
    }

    public IEnumerator StarGameCoroutine()
    {   
        StartCoroutine(TurnMusicDown());
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(1);        
    }

    public void QuitGame()
    {
        StartCoroutine(QuitGameCoroutine());
    }

    public IEnumerator QuitGameCoroutine()
    {   

        StartCoroutine(TurnMusicDown());   
        yield return new WaitForSeconds(transitionTime);
        Application.Quit();
        
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator TurnMusicDown()
    {

        AudioManager.Instance.PlaySFX(1f, audioOnClick); 

        if (AudioManager.Instance.CurrentMusicVolume() <= 0f) yield return new WaitForSeconds(transitionTime);

        else
        {
            float musicVolume;

            while (AudioManager.Instance.CurrentMusicVolume() > 0f)
            {
                timer += Time.deltaTime;
                musicVolume = Mathf.Lerp(1f, 0f, timer / transitionTime);
                AudioManager.Instance.ChangeMusicVolume(musicVolume);
                yield return null;
            }
        }  

    }
}
