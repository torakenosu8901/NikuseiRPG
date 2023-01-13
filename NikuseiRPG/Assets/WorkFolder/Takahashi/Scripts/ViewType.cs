using UniRx;

public enum ViewType
{
    Menu,
    ImageA,
    ImageB,
    ImageC
}

[System.Serializable]
public class ViewTypeReactiveProperty : ReactiveProperty<ViewType>
{
    public ViewTypeReactiveProperty()
    {

    }
}

[UnityEditor.CustomPropertyDrawer(typeof(ViewTypeReactiveProperty))]
public class ViewTypeReactivePropertyDrawer : InspectorDisplayDrawer
{

}