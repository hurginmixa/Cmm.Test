namespace CMM.Test.GUI.Wrappers
{
    public interface ICmmFormatProperty
    {
        string Name { get; }
        
        string DisplayName { get; }

        bool DoHaseCreatingRtp { get; }
    }
}