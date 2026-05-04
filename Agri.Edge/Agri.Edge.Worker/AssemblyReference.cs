using System.Reflection;

namespace Agri.Edge.Worker;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}

