public static class RoomEvents
{
    public static System.Action<RoomInstance> OnEntered;
    public static System.Action<RoomInstance> OnExited;
    
    public static void RaiseEntered(RoomInstance room) => OnEntered?.Invoke(room);
    
    public static void RaiseExited(RoomInstance room) => OnExited?.Invoke(room);
}