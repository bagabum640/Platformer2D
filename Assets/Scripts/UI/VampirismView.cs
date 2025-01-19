using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class VampirismView : MonoBehaviour
{
    [SerializeField] private Vampirism _vampirism;
    [SerializeField] private Image _image;
    [SerializeField] private Text _text;

    private Color _defaultColor = new(1, 1, 1, 1f);
    private Color _activeColor = new(1, 1, 1, 0.5f);

    private void Awake()
    {
        _text.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _vampirism.Reloading += ReloadView;
        _vampirism.Activated += Activate;
    }

    private void OnDisable()
    {
        _vampirism.Reloading -= ReloadView;
        _vampirism.Activated -= Activate;
    }

    public void Activate()
    {
        _image.color = _activeColor;
    }

    public void ReloadView()
    {
        StartCoroutine(UpdateCooldown());
    }

    private IEnumerator UpdateCooldown()
    {
        _text.gameObject.SetActive(true);

        float cooldownTime = _vampirism.Cooldown;      

        while (cooldownTime > 0)
        {
            cooldownTime -= Time.deltaTime;

            _text.text = cooldownTime.ToString("0");

            yield return null;
        }

        _image.color = _defaultColor;
        _text.gameObject.SetActive(false);
    }
}