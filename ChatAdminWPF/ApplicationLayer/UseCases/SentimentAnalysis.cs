﻿using ChatAdminWPF.DomainLayer;
using ChatAdminWPF.Infrastructure.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace ChatAdminWPF.ApplicationLayer.UseCases
{
    public class SentimentAnalysis : ISentimentAnalysis
    {
        private readonly Dictionary<string, List<string>> _sentimentKeywords;

        public SentimentAnalysis(Dictionary<string, string> sentimentFilePaths)
        {
            _sentimentKeywords = LoadAllSentimentKeywords(sentimentFilePaths);
        }

        // Load sentiment keywords from file paths
        private Dictionary<string, List<string>> LoadAllSentimentKeywords(Dictionary<string, string> sentimentFilePaths)
        {
            return sentimentFilePaths.ToDictionary(
                sentiment => sentiment.Key,
                sentiment => KeywordRepository.LoadKeywordsFromFile(sentiment.Value)
            );
        }

        public Dictionary<string, int> AnalyseSentiment(List<Message> messages)
        {
            
            var sentimentCounts = _sentimentKeywords.Keys.ToDictionary(sentiment => sentiment, _ => 0);

            foreach (var message in messages)
            {
                UpdateSentimentCountsForMessage(sentimentCounts, message.Content);
            }

            return sentimentCounts;
        }

        // Update sentiment counts for a specific message
        private void UpdateSentimentCountsForMessage(Dictionary<string, int> sentimentCounts, string messageContent)
        {
            var lowerMessageContent = messageContent.ToLower();

            foreach (var sentiment in _sentimentKeywords)
            {
                foreach (var keyword in sentiment.Value)
                {
                    var lowerKeyword = keyword.ToLower();

                    if (lowerMessageContent.Contains(lowerKeyword))
                    {
                        sentimentCounts[sentiment.Key]++;
                    }
                }
            }
        }


        // Determine the overall sentiment
        public string DetermineOverallSentiment(Dictionary<string, int> sentimentCounts)
        {
            //get the sentiment with the highest count
            return sentimentCounts
                .OrderByDescending(sc => sc.Value)
                .FirstOrDefault().Key;
        }
    }
}
