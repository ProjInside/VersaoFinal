using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SlotsMenu : MonoBehaviour
{
    public GameObject Button;
    public GameObject TelaCarregamento;

    [SerializeField]
    public Text _title;

    [HideInInspector]
    public SaveController saveManager;

    public AudioSource SemSave;
    public AudioSource SaveDeletado;
    public AudioSource Carregando;

    [SerializeField]
    public AudioSource NovoJogo;

    public AudioSource JogarOndeParou;

    [SerializeField]
    public AudioSource jogo;
    void Awake()
    {
        saveManager = GameObject.Find("Slots").GetComponent<SaveController>();
        _title.text = PlayerPrefs.GetString("jogar", _title.text);
    }

    void Start()
    {
        if(_title.text != "Novo jogo")
        {
            jogo.clip = JogarOndeParou.clip;
        }
        else
        {
            jogo.clip = NovoJogo.clip;
        }
    }

    public void salvar()
    {
        if (_title.text == "Novo jogo")
        {
            saveManager.Save(1, 1, 1);
        }
        if(_title.text != "Novo jogo")
        {
            saveManager.LoadMenu();
        }
        jogo.clip = JogarOndeParou.clip;
        TelaCarregamento.SetActive(true);
        _title.text = "Save 1: dia " + DateTime.Now;
        PlayerPrefs.SetString("jogar", _title.text);
        StartCoroutine(espera());
    }
    IEnumerator espera()
    {
        Carregando.Play();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(2);
    }
    public void deletar()
    {
        if(_title.text != "Novo jogo")
        {
            jogo.clip = NovoJogo.clip;
            SaveDeletado.Play();
            _title.text = "Novo jogo";
            PlayerPrefs.SetString("jogar", _title.text);
            saveManager.Delete();
            PlayerPrefs.DeleteKey(_title.text);
        }
        else
        {
            SemSave.Play();
        }
        
    }
}
