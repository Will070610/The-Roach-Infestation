using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Image Health;
    [SerializeField]
    private Sprite[] HealthSprites;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HealthUpdate(int _playerHealth)
    {

        Health.sprite = HealthSprites[_playerHealth];

    }

}
