using System;
using System.Collections.Generic;
using System.Linq;

namespace CybersecurityChatbot
{
    public class ChatEngine
    {
        // Dictionary for keyword responses (Question 2 & 8)
        private Dictionary<string, List<string>> keywordResponses;

        // Dictionary for random response categories (Question 3 & 8)
        private Dictionary<string, List<string>> randomResponseCategories;

        // Memory storage (Question 5)
        private Dictionary<string, string> userMemory;

        // Conversation tracking (Question 4)
        private string currentTopic;
        private Random random;

        public ChatEngine()
        {
            InitializeKeywordResponses();
            InitializeRandomResponses();
            userMemory = new Dictionary<string, string>();
            random = new Random();
            currentTopic = "";
        }

        // Initialize all keyword-based responses (Question 2 - 3 keywords minimum)
        private void InitializeKeywordResponses()
        {
            keywordResponses = new Dictionary<string, List<string>>()
            {
                ["password"] = new List<string>
                {
                    "[PASSWORD SECURITY] Use strong passwords that are at least 12 characters long. Include uppercase letters, lowercase letters, numbers, and special symbols.",
                    "[PASSWORD SECURITY] Never reuse passwords across different accounts. Use a password manager to generate and store unique passwords.",
                    "[PASSWORD SECURITY] Enable Two-Factor Authentication (2FA) whenever possible. This adds an extra layer of security beyond just your password.",
                    "[PASSWORD SECURITY] Avoid using personal information like birthdays, pet names, or family names in your passwords."
                },
                ["scam"] = new List<string>
                {
                    "[SCAM ALERT] Be cautious of unsolicited messages asking for personal information. Legitimate companies never ask for passwords via email.",
                    "[SCAM ALERT] Never click on suspicious links or download attachments from unknown sources. Scammers create urgency to trick you.",
                    "[SCAM ALERT] If something sounds too good to be true, it probably is. Always verify the sender through official channels.",
                    "[SCAM ALERT] Check for red flags: poor grammar, urgent requests, generic greetings, and requests for gift cards or wire transfers."
                },
                ["privacy"] = new List<string>
                {
                    "[PRIVACY PROTECTION] Review your privacy settings on social media platforms regularly. Limit what personal information is publicly visible.",
                    "[PRIVACY PROTECTION] Use a VPN when connecting to public Wi-Fi networks. This encrypts your internet traffic and protects your data.",
                    "[PRIVACY PROTECTION] Be mindful of app permissions. Only grant access to information that is necessary for the app to function.",
                    "[PRIVACY PROTECTION] Regularly check which third-party apps have access to your accounts and remove those you no longer use."
                },
                ["phishing"] = new List<string>
                {
                    "[PHISHING WARNING] Always check the sender's email address carefully. Scammers use addresses that look similar to legitimate ones.",
                    "[PHISHING WARNING] Hover over links before clicking to see the actual URL. Do not enter personal information on suspicious websites.",
                    "[PHISHING WARNING] Look for spelling mistakes, poor grammar, and generic greetings like 'Dear Customer' - these are phishing red flags.",
                    "[PHISHING WARNING] If an email creates urgency or threatens account closure, verify through official channels before taking action."
                }
            };
        }

        // Initialize random response categories (Question 3)
        private void InitializeRandomResponses()
        {
            randomResponseCategories = new Dictionary<string, List<string>>()
            {
                ["greeting"] = new List<string>
                {
                    "Hello! How can I help you stay safe online today?",
                    "Greetings! Ready to learn about cybersecurity?",
                    "Hi there! What cybersecurity topic would you like to explore?",
                    "Welcome! I am here to help protect you online. What would you like to know?"
                },
                ["thanks"] = new List<string>
                {
                    "You are welcome! Stay safe online!",
                    "Happy to help! Cybersecurity is everyone's responsibility.",
                    "My pleasure! Feel free to ask if you have more questions.",
                    "Glad I could help! Knowledge is power in the digital world."
                },
                ["help"] = new List<string>
                {
                    "I can help with: Passwords, Scams, Privacy, and Phishing. Just ask!",
                    "Try asking: 'Tell me about password safety' or 'How to spot a scam?'",
                    "I specialize in cybersecurity awareness. Feel free to ask about any online safety topic!"
                }
            };
        }

        // Main method to get response (Question 4 - Conversation flow)
        public string GetResponse(string userInput)
        {
            string lowerInput = userInput.ToLower();

            // Step 1: Detect sentiment (Question 6)
            string sentiment = DetectSentiment(lowerInput);

            // Step 2: Handle follow-up questions (Question 4)
            if (IsFollowUpQuestion(lowerInput))
            {
                return HandleFollowUp(sentiment);
            }

            // Step 3: Memory - Store user name (Question 5)
            if (lowerInput.Contains("my name is") || lowerInput.Contains("call me"))
            {
                StoreUserName(lowerInput);
                return GetPersonalizedGreeting();
            }

            // Step 4: Memory - Store user preference (Question 5)
            if (lowerInput.Contains("interested in") || lowerInput.Contains("like to learn about"))
            {
                StoreUserPreference(lowerInput);
                string preference = userMemory["preference"];
                return "Great! I will remember that you are interested in " + preference + ". It is a crucial part of staying safe online!";
            }

            // Step 5: Check for help request
            if (lowerInput.Contains("help") || lowerInput.Contains("what can you do"))
            {
                return GetRandomResponse("help");
            }

            // Step 6: Check for thanks
            if (lowerInput.Contains("thank") || lowerInput.Contains("thanks"))
            {
                return GetRandomResponse("thanks");
            }

            // Step 7: Check for greetings
            if (IsGreeting(lowerInput))
            {
                return GetPersonalizedGreeting();
            }

            // Step 8: Check for keywords (Question 2)
            foreach (var keyword in keywordResponses)
            {
                if (lowerInput.Contains(keyword.Key))
                {
                    currentTopic = keyword.Key;
                    string response = GetRandomResponse(keyword.Value);
                    return AddSentimentPrefix(sentiment, PersonalizeResponse(response));
                }
            }

            // Step 9: Default response for unknown input (Question 7)
            return GetDefaultResponse(sentiment);
        }

        // Sentiment detection (Question 6)
        private string DetectSentiment(string input)
        {
            if (input.Contains("worried") || input.Contains("concerned") || input.Contains("nervous") || input.Contains("scared") || input.Contains("anxious"))
                return "worried";

            if (input.Contains("frustrated") || input.Contains("annoyed") || input.Contains("confused") || input.Contains("angry") || input.Contains("upset"))
                return "frustrated";

            if (input.Contains("curious") || input.Contains("interested") || input.Contains("wondering") || input.Contains("excited") || input.Contains("eager"))
                return "curious";

            return "neutral";
        }

        // Add sentiment-appropriate prefix (Question 6)
        private string AddSentimentPrefix(string sentiment, string response)
        {
            switch (sentiment)
            {
                case "worried":
                    return "I understand your concern. Do not worry - let me help you with that! " + response;
                case "frustrated":
                    return "I know cybersecurity can be frustrating at times. Let me simplify this for you: " + response;
                case "curious":
                    return "That is a great question! I am glad you are curious about this. " + response;
                default:
                    return response;
            }
        }

        // Check if input is a follow-up question (Question 4)
        private bool IsFollowUpQuestion(string input)
        {
            string[] followUps = { "tell me more", "explain more", "another tip", "more about",
                                   "elaborate", "go on", "continue", "what else", "and then" };
            return followUps.Any(f => input.Contains(f));
        }

        // Handle follow-up questions (Question 4)
        private string HandleFollowUp(string sentiment)
        {
            if (!string.IsNullOrEmpty(currentTopic))
            {
                if (keywordResponses.ContainsKey(currentTopic))
                {
                    string response = GetRandomResponse(keywordResponses[currentTopic]);
                    return AddSentimentPrefix(sentiment, "Here is another tip about " + currentTopic + ": " + response);
                }
            }
            return "What specific topic would you like to learn more about? You can ask about passwords, scams, privacy, or phishing!";
        }

        // Store user's name in memory (Question 5)
        private void StoreUserName(string input)
        {
            try
            {
                string searchPhrase = "my name is";
                if (!input.Contains(searchPhrase))
                    searchPhrase = "call me";

                int nameIndex = input.IndexOf(searchPhrase) + searchPhrase.Length;
                if (nameIndex > searchPhrase.Length && nameIndex < input.Length)
                {
                    string name = input.Substring(nameIndex).Trim().Split(' ')[0];
                    userMemory["name"] = name;
                }
            }
            catch (Exception) { }
        }

        // Store user's preference in memory (Question 5)
        private void StoreUserPreference(string input)
        {
            try
            {
                int prefIndex = input.IndexOf("interested in") + 13;
                if (prefIndex > 13 && prefIndex < input.Length)
                {
                    string topic = input.Substring(prefIndex).Trim();
                    userMemory["preference"] = topic;
                }
            }
            catch (Exception) { }
        }

        // Get personalized greeting using stored memory (Question 5)
        private string GetPersonalizedGreeting()
        {
            if (userMemory.ContainsKey("name"))
            {
                return "Hello " + userMemory["name"] + "! " + GetRandomResponse("greeting");
            }
            return GetRandomResponse("greeting");
        }

        // Personalize response using stored user information (Question 5)
        private string PersonalizeResponse(string response)
        {
            if (userMemory.ContainsKey("name") && userMemory.ContainsKey("preference"))
            {
                return userMemory["name"] + ", as someone interested in " + userMemory["preference"] + ", " + response.ToLower();
            }
            else if (userMemory.ContainsKey("name"))
            {
                return userMemory["name"] + ", " + response;
            }
            else if (userMemory.ContainsKey("preference"))
            {
                return "Since you are interested in " + userMemory["preference"] + ", " + response.ToLower();
            }
            return response;
        }

        // Get random response from a list (Question 3)
        private string GetRandomResponse(List<string> responses)
        {
            return responses[random.Next(responses.Count)];
        }

        // Get random response from a category (Question 3)
        private string GetRandomResponse(string category)
        {
            if (randomResponseCategories.ContainsKey(category))
            {
                var responses = randomResponseCategories[category];
                return responses[random.Next(responses.Count)];
            }
            return "How can I help you with cybersecurity today?";
        }

        // Check if input is a greeting
        private bool IsGreeting(string input)
        {
            string[] greetings = { "hello", "hi", "hey", "greetings", "good morning", "good afternoon", "good evening" };
            return greetings.Any(g => input.Contains(g));
        }

        // Default response for unrecognized input (Question 7 - Error Handling)
        private string GetDefaultResponse(string sentiment)
        {
            if (sentiment == "frustrated")
            {
                return "I apologize for the confusion. Could you please rephrase your question? Try asking about passwords, scams, or privacy.";
            }
            else if (sentiment == "worried")
            {
                return "I am here to help. Could you please rephrase your question? Try asking me about password safety or how to spot scams.";
            }

            string[] defaults = {
                "I am not sure I understand. Can you try rephrasing? Try asking about passwords, scams, privacy, or phishing.",
                "Could you please rephrase that? I specialize in cybersecurity topics like password security, scam detection, and privacy protection.",
                "I did not quite catch that. Would you like to know about password security, how to spot scams, or privacy protection tips?"
            };
            return defaults[random.Next(defaults.Length)];
        }

        // Reset conversation but keep memory (Question 4)
        public void ResetConversation()
        {
            currentTopic = "";
            // User memory (name and preference) is preserved
        }
    }
}