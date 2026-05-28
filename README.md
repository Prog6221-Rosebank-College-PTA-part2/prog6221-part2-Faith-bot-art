[![Review Assignment Due Date](https://classroom.github.com/assets/deadline-readme-button-22041afd0340ce965d47ae6ef1cefeee28c7c493a6346c4f15d667ab976d596c.svg)](https://classroom.github.com/a/Apa4hIya)
# CYBERBOT - Cybersecurity Awareness Chatbot

## Overview

**Cyberbot** is an intelligent, GUI-based cybersecurity awareness chatbot that helps users learn about online safety. It features keyword recognition, sentiment detection, memory recall, random responses, and voice support. Built with C# Windows Forms, this application provides an engaging and interactive way to learn about cybersecurity topics.

---

## Features

### 1. Graphical User Interface (GUI)
- Modern dark-themed design with proper spacing and colors
- ASCII art logo displayed prominently
- Chat display area, input box, and control buttons
- Status bar showing current application state

### 2. Keyword Recognition
Recognizes and provides detailed guidance on:
- **Password Security** - Strong password creation and management
- **Scam Detection** - Identifying and avoiding online scams
- **Privacy Protection** - Keeping personal data safe
- **Phishing Awareness** - Spotting fake emails and messages

### 3. Random Responses
- Multiple predefined responses for phishing tips
- Random selection using arrays/lists for varied interactions
- Makes conversations feel natural and engaging

### 4. Conversation Flow
- Handles follow-up questions like "Tell me more" or "Another tip"
- Maintains current topic context
- Continues discussions without restarting the conversation

### 5. Memory and Recall
- Stores user's name when told: "My name is [name]"
- Remembers user's interests: "I am interested in [topic]"
- Personalizes responses using stored information
- Example: "John, as someone interested in privacy, here's a tip for you..."

### 6. Sentiment Detection
Detects and responds appropriately to:
- **Worried** - Provides reassurance and empathetic responses
- **Frustrated** - Simplifies information and offers support
- **Curious** - Gives enthusiastic and encouraging answers

### 7. Error Handling
- Graceful handling of unrecognized inputs
- Professional default responses
- No crashes or unexpected terminations

### 8. Voice Support
- Text-to-speech functionality using System.Speech
- Speak button to read user input aloud
- Automatic voice responses from Cyberbot

### 9. Code Optimization
- Dictionaries and lists for efficient response management
- Object-Oriented Programming principles
- Methods for specific functionalities
- Ready for future expansion

---

## Technologies Used

| Technology | Purpose |
|------------|---------|
| C# | Main programming language |
| Windows Forms | GUI framework |
| .NET 6.0+ | Runtime environment |
| System.Speech | Text-to-speech functionality |

---

## Project Structure
Cyberbot/
├── Chatbotform.cs # Main GUI and user interaction
├── Chatbotform.Designer.cs # Auto-generated designer code
├── ChatEngine.cs # Chatbot logic, memory, sentiment
├── Program.cs # Application entry point
└── README.md # Project documentation
