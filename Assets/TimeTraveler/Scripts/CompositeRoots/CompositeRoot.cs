using Model;
using Model.Tasks;
using UnityEngine;

public class CompositeRoot : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private PlayerView _player;
    [SerializeField] private PlayerInput _input;

    private GameMapView _gameMapView;

    private Model.IGameMap _gameMap;


    private class ObservedGameMap : Observed<IGameMap>, IGameMap
    {
        public ObservedGameMap(IGameMap observed) : base(observed)
        {
        }

        public Vector3Int GetCell(Vector3Int position, Movement.Direction direction) => _observed.GetCell(position, direction);

        public Model.Object GetObject(Vector3Int position) => _observed.GetObject(position);

        public Vector3Int GetPosition(Model.Object obj) => _observed.GetPosition(obj);

        public bool IsOccuped(Vector3Int position) => _observed.IsOccuped(position);

        public void Remove(Model.Object obj)
        {
            _observed.Remove(obj);
        }

        public void Set(Model.Object obj, Vector3Int position)
        {
            _observed.Set(obj, position);
            Invoke();
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        var obsGM = new ObservedGameMap(new GameMap());
        _gameMap = obsGM;       
        Instancies.Mover = new Mover(_gameMap);
        FindReferences();
        _gameMapView.SetModel(_gameMap);
        InitPlayer();
        InitObjectsViews();

        _gameMapView.UpdateView();
        obsGM.AddListener(_gameMapView.UpdateView);
    }

    private void InitPlayer()
    {
        var player = new Player();
        _player.SetModel(player);
        _input.Init(player);
    }

    private void FindReferences()
    {
        _gameMapView = FindObjectOfType<GameMapView>();
    }

    private void InitObjectsViews()
    {
        ObjectView[] objectViews = FindObjectsByType<ObjectView>(FindObjectsSortMode.None);
        foreach (ObjectView objectView in objectViews)
        {
            var cell = _gameMapView.Grid.WorldToCell(objectView.transform.position);
            _gameMap.Set(objectView.Model, cell);
            objectView.Init(_gameMapView.Grid);
            _gameMapView.AddObjectView(objectView);
        }
    }
}
