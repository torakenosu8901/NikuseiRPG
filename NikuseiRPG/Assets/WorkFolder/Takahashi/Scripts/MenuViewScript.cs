using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UnityEngine.EventSystems;

public class MenuViewScript : MonoBehaviour
{
    public ViewTypeReactiveProperty m_viewType;

    [SerializeField]
    private Button m_btnA;

    [SerializeField]
    private Button m_btnB;
    
    [SerializeField]
    private Button m_btnC;

    void Start()
    {
        m_viewType.Subscribe(type =>
        {
            if (type == ViewType.Menu)
            {
                EventSystem.current.SetSelectedGameObject(m_btnA.gameObject);
            }
        }).AddTo(this);

        m_btnA.OnClickAsObservable().Subscribe(_ => {
            m_viewType.Value = ViewType.ImageA;
        }).AddTo(this);
        m_btnB.OnClickAsObservable().Subscribe(_ => {
            m_viewType.Value = ViewType.ImageB;
        }).AddTo(this);
        m_btnC.OnClickAsObservable().Subscribe(_ => {
            m_viewType.Value = ViewType.ImageC;
        }).AddTo(this);
    }
}
