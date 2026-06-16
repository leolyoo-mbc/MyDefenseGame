using UnityEngine;
using UnityEngine.UI;

public class ImageTest : MonoBehaviour
{
    #region Variables
    [SerializeField] private Button _skillButton;
    [SerializeField] private float _skillCooldown = 0f;
    private float _cooldown;
    private bool isReady;
    #endregion

    #region Unity Event Method
    private void Start()
    {
        isReady = true;
        _cooldown = _skillCooldown;
    }

    private void Update()
    {
        if (!isReady)
        {
            _cooldown = _cooldown > Time.deltaTime ? _cooldown - Time.deltaTime : 0f;
            if (_cooldown <= 0f)
            {
                isReady = true;
                _skillButton.interactable = isReady;
                
            }
            _skillButton.image.fillAmount = 1 - (_cooldown / _skillCooldown);
        }
    }
    #endregion

    #region Custom Method
    public void UseSkill()
    {
        //스킬 기능 구현

        isReady = false;
        _skillButton.interactable = isReady;
        _cooldown = _skillCooldown;
    }
    #endregion
}
