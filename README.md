# PROG6221 PART 2 - ST10454777 - Cybersecurity Chatbot GUI

## How to Run
1. Open in Visual Studio 2022
2. Press F5
3. GUI shows ASCII header, plays Assets/greeting.wav, starts chat

## Features
- Dictionary<string,List<string>> for random responses
- List for chat history
- Delegate for response handling (Mainwindow.xaml.cs)
- Keyword detection: password, phishing, scam, privacy
- Conversation flow: follow up "tell me more" about phishing
- Memory: "my favorite topic is phishing" -> recall later
- Sentiment: detects worried/anxious, shows empathy
- Validation: blank and random input handling

## Structure
Mainwindow.xaml - GUI layout
ChatbotEngine.cs - Dictionary logic
Assets/greeting.wav - Voice (Copy if newer)

## CI
GitHub Actions builds.NET 8.0 WPF

## Video
[YouTube Unlisted Link]

ST10454777
