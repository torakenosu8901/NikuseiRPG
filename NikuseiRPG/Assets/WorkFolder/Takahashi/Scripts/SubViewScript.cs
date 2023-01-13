using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UnityEngine.EventSystems;

public class SubViewScript : MonoBehaviour
{
    [SerializeField] private ViewType m_targetType;

    [SerializeField] private MenuViewScript m_mainScript;
    [SerializeField] private Button m_backBtn;
    void Start()
    {
        m_mainScript.m_viewType.Subscribe(type => {
            if (type == m_targetType)
            {
                this.gameObject.SetActive(true);
                EventSystem.current.SetSelectedGameObject(m_backBtn.gameObject);
            }
            else
            {
                this.gameObject.SetActive(false);
            }
        }).AddTo(this);

        m_backBtn.OnClickAsObservable().Subscribe(_ => {
            m_mainScript.m_viewType.Value = ViewType.Menu;
        }).AddTo(this);
    }
}