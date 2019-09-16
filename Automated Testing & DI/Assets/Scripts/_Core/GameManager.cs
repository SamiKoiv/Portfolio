using UniRx;
using Zenject;

public class GameManager : IInitializable, ITickable
{
    SceneLoader sceneLoader;

    public static ReactiveProperty<bool> InMenu { get; private set; }
    public static ReactiveProperty<bool> Loading { get; private set; }

    GameParameters m_gameParameters;

    [Inject]
    public GameManager(GameParameters gameParameters)
    {
        m_gameParameters = gameParameters;

        InMenu = new ReactiveProperty<bool>(false);
        Loading = new ReactiveProperty<bool>(false);
    }

    public void Initialize()
    {
        // Initialization
    }

    // Updates per frame
    public void Tick()
    {

    }
}