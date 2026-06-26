# CyberBuddy - Cybersecurity Awareness Chatbot (Part 3)

## Student Information

**Student Name:** Siyanda Ndlovu  
**Module:** Programming (POE Part 3)  
**Project:** CyberBuddy – Cybersecurity Awareness Chatbot  
**Development Environment:** Visual Studio 2022  
**Language:** C#  
**Framework:** WPF (.NET Framework)

---

# Project Overview

CyberBuddy is an intelligent desktop chatbot developed using C# and Windows Presentation Foundation (WPF). The application educates users about cybersecurity through interactive conversations while providing additional productivity and learning features.

The chatbot allows users to ask cybersecurity-related questions, complete a quiz, manage reminders, and view their activity history through an easy-to-use graphical interface.

---

# Features

## User Authentication
- User enters a username before accessing the chatbot.
- Validation prevents users from continuing without entering a username.
- Personalized welcome message.

## Cybersecurity Chatbot
The chatbot can answer questions about topics including:

- Password Security
- Phishing
- Malware
- Ransomware
- Safe Browsing
- VPN
- Firewall
- Social Engineering
- Privacy
- Two-Factor Authentication (2FA)

The chatbot recognises keywords and responds with relevant cybersecurity advice.

---

## Memory and Personalisation

CyberBuddy remembers:

- User interests
- Previous conversation context
- Follow-up questions
- Conversation history

The chatbot provides reminders based on previous interactions.

---

## Sentiment Detection

CyberBuddy detects simple user emotions such as:

- Worried
- Confused
- Frustrated

It responds with encouraging cybersecurity guidance.

---

## Cybersecurity Quiz

The application includes an interactive quiz where users:

- Answer multiple-choice questions
- Receive immediate feedback
- View their score

---

## Task Reminder System

Users can:

- Add cybersecurity-related tasks
- View reminders
- Mark tasks as completed
- Delete completed tasks

---

## Activity Log

The chatbot records user activities, including:

- Opening the application
- Asking questions
- Completing quizzes
- Managing reminders

---

## Voice Greeting

The application plays a welcome voice greeting when it starts.

---

## User Interface

The interface was designed using WPF and includes:

- Rounded controls
- Easy navigation
- Multiple application pages
- Chat interface
- Reminder page
- Activity log page
- Quiz page

---

# Technologies Used

- C#
- Windows Presentation Foundation (WPF)
- XAML
- Visual Studio 2022
- .NET Framework

---

# Project Structure

```
POE Check
│
├── AI_Chat.cs
├── MainWindow.xaml
├── MainWindow.xaml.cs
├── App.xaml
├── App.xaml.cs
├── manage_tasks.cs
├── activity_log.cs
├── Question_in_quiz.cs
├── Quiz_Question_Load.cs
├── training.cs
├── voice_greeting.cs
├── user_name.cs
└── Resources
```

---

# How to Run

1. Open the solution in Visual Studio 2022.
2. Restore NuGet packages if prompted.
3. Build the solution.
4. Run the application.
5. Enter a username.
6. Start chatting with CyberBuddy.

---

# Example Questions

You can ask questions such as:

- What is phishing?
- How do I create a strong password?
- Explain ransomware.
- What is a VPN?
- Tell me about firewall security.
- What is social engineering?
- How can I browse safely online?
- Why is two-factor authentication important?

---

# Learning Outcomes

This project demonstrates:

- Object-Oriented Programming
- Event-Driven Programming
- WPF User Interface Design
- File Handling
- Exception Handling
- Collections
- Modular Programming
- User Experience Design

---

# Future Improvements

Possible future enhancements include:

- Speech recognition
- AI-powered responses using Natural Language Processing
- Cloud database integration
- User accounts and authentication
- More cybersecurity quiz questions
- Advanced reporting and analytics

---

# Author

**Siyanda Ndlovu**

CyberBuddy was developed as part of the Programming Part 3 Portfolio of Evidence.
