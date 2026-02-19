namespace SpaceInvaders.Config
{
    public static class Configuration
    {
        public static short ScreenWidth = 22;
        public static short ScreenHeight = 12;
        public static int PlayerStartPosition = 10;
        public static char InvaderSymbol = 'W';
        public static char PlayerSymbol = 'A';
        public static char BulletSymbol = '|';

        public const int ScreenFrames = 60;
        public const int RefreshRate = 1000 / ScreenFrames;

        public const int InvaderSideMoveFrameInterval = 10;
        public const int InvaderDownMoveFrameInterval = 200;

        public const string Developer = "Yordan Milanov";
    }
}
