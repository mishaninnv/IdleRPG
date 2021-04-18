using UnityEngine;

public class BackGround : MonoBehaviour
{
    public float speed;
    
    private Vector2 _offset = Vector2.zero;
    private Material _material;
    private GameController _gameController;

    public bool isMove { get; set; } = true;

    void Start()
    {
        _gameController = GameController.GetInstance;
        _gameController.StartFightEvent += SetMotionState;
        _gameController.AfterEnemyDeathEvent += SetMotionState;
        
        _material = GetComponent<Renderer>().material;
        _offset = _material.GetTextureOffset("_MainTex");
    }

    private void Update()
    {
        if (isMove)
        {
            _offset.x += speed * Time.deltaTime;
            _material.SetTextureOffset("_MainTex", _offset);
        }
    }

    private void SetMotionState()
    {
        isMove = !isMove;
    }
}
