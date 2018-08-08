namespace Assets
{
    public static class GameState
    {
        public static bool GameOver;
        public static int PinsSet;
        public static int GrenadesSpawned;

        public static void Reset()
        {
            GameOver = false;
            PinsSet = 0;
            GrenadesSpawned = 0;
        }
    }
}