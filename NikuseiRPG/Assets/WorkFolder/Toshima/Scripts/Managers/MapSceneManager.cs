using UnityEngine;
using UnityEngine.UI;
using System;

public class MapSceneManager : MonoBehaviour
{
    [SerializeField]
    private PlayerCollider _playerCollider;

    [SerializeField]
    private SceneLoader _sceneRoder;

    private void Awake()
    {
        _playerCollider.SceneLoad = _sceneRoder.SceneLodding;

    }

    private void Start()
    {
        //_playerCollider.SceneLoad = _sceneRoder.SceneLodding;
    }
}