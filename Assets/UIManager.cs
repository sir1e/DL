using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject damageTextPrefab;
    public GameObject healthTextPrefab;
    public GameObject manaTextPrefab;
    public Damagble damagblePlayer;
    public Canvas gameCanvas;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        gameCanvas = FindObjectOfType<Canvas>();
        damagblePlayer = player.GetComponent<Damagble>();

    }
    public void OnEnable()
    {
      CharactersEvents.characterDamaged += CharacterTookDamage;
        CharactersEvents.characterHealed += CharacterHealed;
        CharactersEvents.characterHealed += CharacterHealed;
    }

    private void OnDisable()
    {
        CharactersEvents.characterDamaged -= CharacterTookDamage;
        CharactersEvents.characterHealed -= CharacterHealed;
        CharactersEvents.characterHealed -= CharacterHealed;
    }
    public void CharacterTookDamage(GameObject character, int damageReceived)
    {
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);
        TMP_Text tmpText = Instantiate(damageTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();

        tmpText.text = damageReceived.ToString();
    }

    public void CharacterHealed(GameObject character, int healReceived)
    {
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);
        TMP_Text tmpText = Instantiate(healthTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();

        tmpText.text = healReceived.ToString();
    }

    public void CharacterMana(GameObject character, int manaChange)
    {
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);
        TMP_Text tmpText = Instantiate(healthTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();

        tmpText.text = manaChange.ToString();
    }
    public void OnGameExit(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            SceneManager.LoadScene(0);

        }
    }

    
public void DeathScreen()
    {
        if (!damagblePlayer.IsAlive)
        {
            StartCoroutine(LoadSceneAfterDelay(0, 7f));
        }
    }

    private IEnumerator LoadSceneAfterDelay(int sceneIndex, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneIndex);
    }
    private void Update()
    {
        DeathScreen();
    }
   
}