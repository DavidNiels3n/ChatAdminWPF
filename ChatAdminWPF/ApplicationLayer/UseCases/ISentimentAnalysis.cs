using ChatAdminWPF.DomainLayer;
using System.Collections.Generic;

namespace ChatAdminWPF.ApplicationLayer.UseCases
{
    public interface ISentimentAnalysis
    {
        Dictionary<string, int> AnalyseSentiment(List<Message> messages);
        string DetermineOverallSentiment(Dictionary<string, int> sentimentCounts);
    }
}
