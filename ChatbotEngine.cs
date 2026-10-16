using System;
using System.Collections.Generic;
using System.Linq;

namespace CybersecurityChatbotGUI
{
// DELEGATE REQUIREMENT - Learning Unit
public delegate string SentimentDelegate(string input, string baseResponse);
public delegate string ResponseDelegate(string keyword);

public class ChatbotEngine
{
// GENERIC COLLECTION REQUIREMENT
private Dictionary<string, List<string>> keywordResponses;
private List<string> phishingTips;
private Random random = new Random();

// MEMORY AND RECALL - Task 5
public string UserName { get; private set; }
public string FavTopic { get; private set; }
public string LastTopic { get; private set; }

public ChatbotEngine()
{
InitCollections();
}

private void InitCollections()
{
// Task 2: Keyword Recognition - at least 3
keywordResponses = new Dictionary<string, List<string>>
{
["password"] = new List<string> {
"Make sure to use strong, unique passwords for each account. Avoid using personal details in your passwords.",
"Tip: Use a passphrase like 'BlueSky$Rain2026!' and a password manager. Never reuse passwords.",
"Password Safety: 12+ characters, mix of upper/lower, numbers, symbols. Change if breached."
},
["scam"] = new List<string> {
"Scam Alert: If it sounds too good to be true, it is. Verify sender emails carefully.",
"Don't click links from unknown numbers. Scammers often create urgency - 'Your account will be closed!'"
},
["privacy"] = new List<string> {
"Privacy Tip: Review app permissions. Does a flashlight app need your contacts? No.",
"Check your social media privacy settings quarterly. Limit who sees your personal info."
},
["phishing"] = new List<string>() // Task 3 uses random list
};

// Task 3: Random Responses
phishingTips = new List<string>
{
"Be cautious of emails asking for personal information. Scammers often disguise themselves as trusted organisations.",
"Phishing Tip: Hover over links to see the real URL before clicking. Look for misspellings like amaz0n.com.",
"Phishing Tip: Legit companies never ask for your password via email. Report phishing to your IT team.",
"Tip: Enable 2FA - even if they steal your password, they can't get in."
};
}

public string GetResponse(string userInput)
{
string inputLower = userInput.ToLower();

// Task 5: Memory and Recall
if (inputLower.Contains("my name is") || inputLower.Contains("i am ") || inputLower.Contains("call me"))
{
UserName = ExtractName(userInput);
return $"Great! I'll remember that your name is {UserName}. It's a crucial part of staying safe online, {UserName}!";
}
if (inputLower.Contains("interested in") || inputLower.Contains("favourite") || inputLower.Contains("favorite"))
{
FavTopic = DetectFavTopic(inputLower);
return $"Great! I'll remember that you're interested in {FavTopic}. It's a crucial part of staying safe online.";
}

// Task 6: Sentiment Detection using DELEGATE
SentimentDelegate sentimentHandler = HandleSentiment;

// Task 4: Conversation Flow - handle follow-ups
if (inputLower.Contains("another tip") || inputLower.Contains("tell me more") || inputLower.Contains("explain more") || inputLower.Contains("give me more"))
{
if (LastTopic!= null)
{
string more = GetRandomResponseForTopic(LastTopic);
return sentimentHandler(inputLower, $"Sure, here's more on {LastTopic}: {more}");
}
return "Of course! What topic would you like more info on? Try: password, scam, privacy, or phishing.";
}

// Task 2 & 3: Keyword Recognition
foreach (var key in keywordResponses.Keys)
{
if (inputLower.Contains(key))
{
LastTopic = key;
string baseResponse;

if (key == "phishing" || inputLower.Contains("phishing tips"))
{
baseResponse = phishingTips[random.Next(phishingTips.Count)];
}
else
{
var responses = keywordResponses[key];
baseResponse = responses[random.Next(responses.Count)];
}

// Task 6: Apply sentiment and Task 5: Personalize
string finalResponse = sentimentHandler(inputLower, baseResponse);
if (UserName!= null) finalResponse = $"{UserName}, {finalResponse}";
if (FavTopic!= null && key == FavTopic) finalResponse = $"As someone interested in {FavTopic}, {finalResponse.ToLower()}";

return finalResponse;
}
}

// Task 7: Error Handling and Edge Cases
return HandleUnknown(inputLower, sentimentHandler);
}

// DELEGATE IMPLEMENTATION
private string HandleSentiment(string input, string baseResponse)
{
if (input.Contains("worried") || input.Contains("anxious") || input.Contains("scared"))
{
return $"It's completely understandable to feel that way. Scammers can be very convincing. Let me share some tips to help you stay safe. {baseResponse}";
}
if (input.Contains("frustrated") || input.Contains("confused") || input.Contains("overwhelmed"))
{
return $"I hear you, it can be overwhelming. Let's take it step by step. {baseResponse}";
}
if (input.Contains("curious") || input.Contains("interested"))
{
return $"Love your curiosity! Knowledge is your best defense. {baseResponse}";
}
return baseResponse;
}

private string HandleUnknown(string input, SentimentDelegate sentimentHandler)
{
if (string.IsNullOrWhiteSpace(input)) return "I didn't catch that. Could you type something?";
// Must share a tip and not make user enter again - per task 6 note
string tip = phishingTips[random.Next(phishingTips.Count)];
return sentimentHandler(input, $"I'm not sure I understand. Can you try rephrasing? In the meantime, here's a tip: {tip}");
}

private string GetRandomResponseForTopic(string topic)
{
ResponseDelegate responder = (k) => {
if (k == "phishing") return phishingTips[random.Next(phishingTips.Count)];
return keywordResponses.ContainsKey(k)? keywordResponses[k][random.Next(keywordResponses[k].Count)] : "Here's a general tip: Keep your software updated!";
};
return responder(topic);
}

private string ExtractName(string input) {
var parts = input.Split(' ');
return parts.Last().Trim('.', '!', '?');
}
private string DetectFavTopic(string input) {
if (input.Contains("password")) return "password";
if (input.Contains("privacy")) return "privacy";
if (input.Contains("scam")) return "scam";
return "cybersecurity";
}
}
}
