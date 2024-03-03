
using System;
using System.Collections.Generic;
using System.Reflection;


public class PreDefinedAssemblyUtils
{

    public enum AssemblyType
    {
        AssemblyCSharp,
        AssemblyCSharpEditor,
        AssemblyCSharpEditorFirstPass,
        AssemblyCSharpFirstPass
    }

    static AssemblyType? GetAssemblyType(string assemblyName)
    {
        return assemblyName switch
        {
            "Assembly-CSharp" => AssemblyType.AssemblyCSharp,
            "Assembly-CSharp-Editor" => AssemblyType.AssemblyCSharpEditor,
            "Assembly-CSharp-Editor-firstpass" => AssemblyType.AssemblyCSharpEditorFirstPass,
            "Assembly-CSharp-firstpass" => AssemblyType.AssemblyCSharpFirstPass,
            _ => null
        } ;
    }

    static void AddAssemblyTypes(Type[] assemblyTypes, Type interfaceType, ICollection<Type> results)
    {
        if (assemblyTypes == null) { return; }

        for (int i = 0; i < assemblyTypes.Length; i++)
        {
            Type type = assemblyTypes[i];

            if (type != interfaceType && interfaceType.IsAssignableFrom(type))
            {
                results.Add(type);
            }
        }
    }

    public static List<Type> GetTypes(Type interfaceType)
    {
        Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
        Dictionary<AssemblyType, Type[]> assemblyTypes=new Dictionary<AssemblyType, Type[]>();

        List<Type> types = new List<Type>();

        for(int i=0;i<assemblies.Length;i++)
        {
            AssemblyType? type = GetAssemblyType(assemblies[i].GetName().Name); 

            if(type != null)
            {
                assemblyTypes.Add((AssemblyType)type, assemblies[i].GetTypes());
            }
        }

        assemblyTypes.TryGetValue(AssemblyType.AssemblyCSharp, out var assemblyCSharpTypes);
        AddAssemblyTypes(assemblyCSharpTypes, interfaceType, types);

        assemblyTypes.TryGetValue(AssemblyType.AssemblyCSharpFirstPass, out var assemblyCSharpFirstPassTypes);
        AddAssemblyTypes(assemblyCSharpFirstPassTypes, interfaceType, types);

        

        return types;
    }
}

