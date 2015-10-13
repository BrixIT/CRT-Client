namespace CRT
{
    interface ModuleInterface
    {
        string ModuleName { get;}
        bool isSupportedPlatform();
    }
}
