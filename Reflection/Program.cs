// See https://aka.ms/new-console-template for more information

using System.Reflection;

Console.Write("Please enter the name of the type: ");
var name = Console.ReadLine() ?? string.Empty;
var type = GetTypeFromInput(name);
if (type != null)
{
    Console.WriteLine("Type found: " + type.FullName);
}
else
{
    Console.WriteLine("Type not found");
    return;
}

PrintTypeInfo(type);

Console.Write("Please enter the name of the method: ");
name = Console.ReadLine() ?? string.Empty;
var instance = Activator.CreateInstance(type);
var property = type.GetProperty("Name");
if (property != null) property.SetValue(instance, "Mehran");
var method = type.GetMethod(name);
TryInvokeParameterlessMethod(instance, method);
//name = Console.ReadLine() ?? string.Empty;
//method = type.GetMethod(name);
TryInvokeParameterizedMethod(instance, method);


void TryInvokeParameterlessMethod(object? instance, MethodInfo? method)
{
    if (method?.GetParameters().Length == 0)
        method.Invoke(instance, null);
}

void TryInvokeParameterizedMethod(object? instance, MethodInfo? method)
{
    if (method?.GetParameters().Length == 0)
        return;
    Console.WriteLine("Please enter the values for parameters of the method: ");
    var parameters = method?.GetParameters();
    var parameterValues = new List<object>();
    foreach (var parameter in parameters)
    {
        Console.Write($"{parameter.Name}: ");
        var valueOfParameter = Console.ReadLine() ?? string.Empty;
        parameterValues.Add(int.Parse(valueOfParameter));
    }

    var result = method?.Invoke(instance, parameterValues.ToArray());
    Console.WriteLine(result);
}


Type? GetTypeFromInput(string typeName)
{
    var type = Type.GetType(typeName);
    if (type != null) return type;

    foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
    {
        type = assembly.GetType(typeName);
        if (type != null) return type;
    }

    return null;
}

void PrintTypeInfo(Type type)
{
    Console.WriteLine("Properties: ");
    foreach (var property in type.GetProperties())
        Console.WriteLine(
            $"{property.Name}: {property.PropertyType.FullName} - {(property.CanRead ? "[Can Read]" : "")} {(property.CanWrite ? "[Can Write]" : "")}");
    Console.WriteLine("Methods: ");
    var methods = type.GetMethods().ToList();
    methods.RemoveAll(x => x.Name is "get_Name" or "set_Name" or "GetType" or "ToString" or "Equals" or "GetHashCode");
    foreach (var method in methods.OrderBy(x => x.Name))
    {
        Console.Write($"{method.Name}(");
        foreach (var parameter in method.GetParameters().OrderBy(x => x.Name))
        {
            Console.Write($"{parameter.ParameterType.Name}");
            if (method.GetParameters().Last() != parameter) Console.Write(", ");
        }

        Console.WriteLine($"): {method.ReturnType.FullName}");
    }
}