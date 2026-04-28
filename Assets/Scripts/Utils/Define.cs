public class Define
{
    public enum WorldObject
    {
        Unknown,
        Player,
        Monster,
    }

    public enum State
    {
        Moving,
        Idle,
        Die,
        Skill,
    }
    public enum Scene
    {
        Unknown,
        Login,
        Lobby,
        Game,

    }
    public enum Layer
    {
        Monster = 8,
        Ground = 9,
        Block = 10,

    }

    public enum Sound
    {
        Bgm,
        Effect,
        MaxCount,
    }

    public enum UIEvent
    {
        Click,
        Drag,
    }

    public enum MouseEvent
    {
        Press,
        PointerDown,
        PointerUp,
        Click,
    }
    public enum CameraMode
    {
        QuarterView,
    }
}