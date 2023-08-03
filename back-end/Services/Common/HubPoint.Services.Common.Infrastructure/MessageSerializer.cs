using System.Collections.Concurrent;
using EasyNetQ;

namespace HubPoint.Services.Common.Infrastructure;

public class MessageSerializer : ITypeNameSerializer
{
    private readonly ConcurrentDictionary<Type, string> _serializedTypes = new();
    
    public string Serialize(Type type)
    {
        return _serializedTypes.GetOrAdd(type, t => type.Name.Replace("\\B(?:([A-Z])(?=[a-z]))|(?:(?<=[a-z0-9])([A-Z]))", "-$1$2").ToLowerInvariant());
    }

    public Type DeSerialize(string typeName)
    {
        return (from kvp in _serializedTypes where kvp.Value == typeName select kvp.Key).First();
    }
}