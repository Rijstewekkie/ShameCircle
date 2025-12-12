using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScene : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text text;

    private float loadTimer = 3;
    void Start()
    {
        if (Scoremanager.WrongGuesses == 0)
        {
            text.text = "Goed gedaan! \n Je had geen fouten gemaakt!";
        }
        else if (Scoremanager.WrongGuesses < 4)
        {
            text.text = "Goed gedaan! \n Je hebt " + Scoremanager.WrongGuesses + " fouten gemaakt! \n Wat een mooie score!";
        }
        else if (Scoremanager.WrongGuesses < 10)
        {
            text.text = "Goed zo, \n Je hebt " + Scoremanager.WrongGuesses + " fouten gemaakt! \n Maar dit kan je beter, ik geloof in je!";
        }
        else
        {
            text.text = "Oei... \n Je hebt " + Scoremanager.WrongGuesses + " fouter gemaakt... \n Nog een keer proberen?";
        }
    }

    void Update()
    {
        loadTimer -= Time.deltaTime;
        
        if (Input.touchCount > 0 && loadTimer <= 0 || Input.GetMouseButton(0) && loadTimer <= 0)
        {
            GameManager.Reset = true;
            SceneManager.LoadScene(0);
        }
    }
}
