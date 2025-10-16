using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_InGame : MonoBehaviour
{
    [SerializeField] private Transform _inGameTransform;
    [SerializeField] private Image dashImage;
    [SerializeField] private Image counterAttackImage;
    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerManager.instance.player;
        _inGameTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player==null || player.health.isDead)
        {
            if (_inGameTransform!=null && _inGameTransform.gameObject.activeSelf)
            {
                _inGameTransform.gameObject.SetActive(false);
            }
            return;
        } 
        else
        {
            if (_inGameTransform != null && !_inGameTransform.gameObject.activeSelf)
            {
                _inGameTransform.gameObject.SetActive(true);
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            SetCoolDown(dashImage);
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            SetCoolDown(counterAttackImage);
        }

        UpdateCoolDown(dashImage, player.dashCoolTime);
        UpdateCoolDown(counterAttackImage, player.counterAttackCoolTime);
    }

    private void SetCoolDown(Image _image)
    {
        if(_image.fillAmount <=0)
        {
            _image.fillAmount = 1;
        }
    }

    private void UpdateCoolDown(Image _image, float _coodown)
    {
        if(_image.fillAmount >0)
        {
            _image.fillAmount -= 1 / _coodown * Time.deltaTime;
        }
    }
}
