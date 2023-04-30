using System.Reflection;
using cila.Domain;
using cila.Domain.Serializers;

namespace cila.Aggregator
{

    public class EventsDispatcher
    {
        private Dictionary<Type, List<Type>> _subscriptions = new Dictionary<Type, List<Type>>();

        private IServiceLocator serviceLocator = new ServiceLocator(Program._serviceProvider);

        public EventsDispatcher(){
            RegisterEventHanlders();
        }

        public void DispatchEvent(DomainEvent e){

            var msg = CilaDomainSerializer.DeserializeEvent(e);
            Dispatch(msg);
        }

        public void Dispatch(object msg)
        {
            var msgType = msg.GetType();
            var handlers = _subscriptions[msgType];
            foreach (var handler in handlers)
            {
                var methodInfo = handler.GetMethod("Handle", new[] { msgType });
                var handlerInstance = serviceLocator.GetService(handler);
                methodInfo.Invoke(handlerInstance, new [] { msg });
            }
        }

        private void RegisterEventHanlders()
        {
            var eventHandlerMap = new Dictionary<Type, List<Type>>();
            var eventHandlerTypes = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(type => typeof(IEventHandler).IsAssignableFrom(type) && type != typeof(IEventHandler))
                .ToList();

            foreach (var eventType in eventHandlerTypes.SelectMany(t => t.GetMethods()
                .Where(m => m.Name == "Handle" && m.GetParameters().Length == 1)
                .Select(m => m.GetParameters()[0].ParameterType)).Distinct())
            {
                eventHandlerMap[eventType] = new List<Type>();
                foreach (var handlerType in eventHandlerTypes.Where(t => t.GetMethods()
                    .Any(m => m.Name == "Handle" && m.GetParameters().Length == 1 && m.GetParameters()[0].ParameterType == eventType)))
                {
                    eventHandlerMap[eventType].Add(handlerType);
                }
                _subscriptions = eventHandlerMap;
            }
        }
    }
}