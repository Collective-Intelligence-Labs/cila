using System;
namespace cila.Domain.Routers
{
    public interface ICilaRouter
    {
        OmnichainRoute CalculateRoute(Command operation);
    }

    public class OmnichainRoute
    {
        public double? Confidence { get; set; }

        public int? EstimatedCost { get; set; }

        public TimeSpan? EstimatedDuration { get; set; }

        public double? EstiamtedSecurity { get; set; }

        public string ChainId { get; set; }

        public List<string> OtherChainsCalls { get; set; }
    }
}

