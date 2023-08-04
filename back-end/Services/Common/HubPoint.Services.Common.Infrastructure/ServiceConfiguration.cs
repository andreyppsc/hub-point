namespace HubPoint.Services.Common.Infrastructure;

public class ServiceConfiguration
{
    public bool UseInMemoryOutbox { get; set; } = true;
    
    public string RabbitMqConnectionString { get; set; } = default!;
    
    public List<Type> MediatorOpenBehaviors { get; } = new();
    
    public void AddOpenBehavior(Type type)
    {
        MediatorOpenBehaviors.Add(type);
    }
    
    public List<Type> MediatorPreProcessors { get; } = new();
    
    public void AddPreProcessor(Type type)
    {
        MediatorPreProcessors.Add(type);
    }
    
    public List<Type> MediatorPostProcessors { get; } = new();
    
    public void AddPostProcessor(Type type)
    {
        MediatorPostProcessors.Add(type);
    }
}