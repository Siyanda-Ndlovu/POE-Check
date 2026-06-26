using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;


namespace POE_Check
{ //start of namespace
    public class AI_Chat
    { //start of class

        //declaring variables
        private string lastTopic = string.Empty;
        private int messageCount = 0;

        private ArrayList reply;
        private ArrayList ignore;
        
        //creating an constructor
        public AI_Chat(ArrayList reply, ArrayList ignore)
        {
            this.reply = reply;
            this.ignore = ignore;
        }

        public void ai_check(string questions, string username, ListBox chats, TextBox question)
        {
            questions = RemoveSpecialCharacters(questions.ToLower().Trim());
            Random random = new Random();

            messageCount++;

            //  follow up
            if (questions.Contains("tell me more") ||
                questions.Contains("more") ||
                questions.Contains("continue") ||
                questions.Contains("another tip") ||
                questions.Contains("why") ||
                questions.Contains("explain"))
            {
                respondWithTopic(lastTopic, random, chats, question, "Here is another tip: ");
                return;
            }

            // Topics before greeting
            Dictionary<string, string[]> topicKeywords = new Dictionary<string, string[]>()
            {
                { "password", new string[] { "password", "passwords", "login", "credential", "account security" } },
                { "phishing", new string[] { "phishing", "scam", "email scam", "fake email", "fraud" } },
                { "privacy", new string[] { "privacy", "private data", "personal information", "data protection" } },
                { "browsing", new string[] { "safe browsing", "browser safety", "malicious website", "internet safety", "browsing" } },
                { "2fa", new string[] { "2fa", "two factor", "authentication", "otp" } }
            };

            foreach (KeyValuePair<string, string[]> topic in topicKeywords)
            {
                foreach (string keyword in topic.Value)
                {
                    if (questions.Contains(keyword))
                    {
                        lastTopic = topic.Key;

                        if (messageCount % 3 == 0) //used modulus to remind user the interest
                        {
                            showInterestReminder(username, chats);
                        }

                        respondWithTopic(topic.Key, random, chats, question, "");
                        return;
                    }
                }
            }

            //if statements to check keywords
            if (questions.Contains("interested in") || questions.Contains("i like") || questions.Contains("my interest"))
            {
                saveInterest(questions, username, chats);
                question.Clear();
                return;
            }

            if (questions.Contains("how are you") || questions.Contains("how r u"))
            {
                error_method("CyberBuddy", "I am functioning properly and ready to help you stay safe online.", chats);
                question.Clear();
                return;
            }

            if (questions.Contains("purpose") || questions.Contains("what do you do"))
            {
                error_method("CyberBuddy", "My purpose is to educate users about cybersecurity awareness.", chats);
                question.Clear();
                return;
            }

            if (questions.Contains("help") || questions.Contains("what can i ask") || questions.Contains("what questions"))
            {
                error_method("CyberBuddy", "You can ask me about passwords, phishing, scams, privacy, and browsing.", chats);
                question.Clear();
                return;
            }

            if (questions.Contains("worried") || questions.Contains("scared"))
            {
                error_method("CyberBuddy", "It is understandable to feel worried. Cyber threats are common, but safe habits help protect you.", chats);
                question.Clear();
                return;
            }

            if (questions.Contains("frustrated") || questions.Contains("confused"))
            {
                error_method("CyberBuddy", "Cybersecurity can feel overwhelming sometimes, but I will help you step by step.", chats);
                question.Clear();
                return;
            }

            //greeting
            if (IsWordMatch(questions, "hello") || IsWordMatch(questions, "hi") || IsWordMatch(questions, "hey"))
            {
                error_method("CyberBuddy", "Hello " + username + "! How can I help you today?", chats);
                question.Clear();
                return;
            }

            if (questions.Contains("thank") || questions.Contains("thanks"))
            {
                error_method("CyberBuddy", "You are welcome " + username + ". Stay safe online.", chats);
                question.Clear();
                return;
            }

            if (questions.Contains("bye") || questions.Contains("goodbye") || questions.Contains("exit"))
            {
                error_method("CyberBuddy", "Goodbye " + username + ". Stay safe online.", chats);
                question.Clear();
                return;
            }

            string[] fallbackMessages =
            {
                "I’m not fully sure what you mean. Try asking about passwords, phishing, scams, or safe browsing.",
                "Could you rephrase that? I can help with cybersecurity topics.",
                "Try asking things like: 'How do I make a strong password?' or 'What is phishing?'",
                "I didn't quite understand. Ask me about online safety, passwords, privacy, or scams."
            };

            error_method("CyberBuddy", fallbackMessages[random.Next(fallbackMessages.Length)], chats);
            question.Clear();
        }

        private bool IsWordMatch(string sentence, string word)
        {
            string[] words = sentence.Split(' '); //split to separate paragraphs

            foreach (string item in words)
            {
                if (item == word)
                {
                    return true;
                }
            }

            return false;
        }

        private void respondWithTopic(string topic, Random random, ListBox chats, TextBox question, string prefix)
        {
            if (string.IsNullOrWhiteSpace(topic))
            {
                error_method("CyberBuddy", "Please ask about a topic first, such as password, phishing, privacy or safe browsing.", chats);
                question.Clear();
                return;
            }

            List<string> responses = getResponsesForTopic(topic);

            if (responses.Count > 0)
            {
                int index = random.Next(responses.Count);
                error_method("ChatBot", prefix + responses[index], chats);
            }
            else
            {
                error_method("CyberBuddy", "I know the topic, but I do not have a response saved for it yet.", chats);
            }

            question.Clear();
        }

        private List<string> getResponsesForTopic(string topic)
        {
            List<string> responses = new List<string>();

            foreach (object replyObj in reply)
            {
                string replyItem = replyObj.ToString();
                int pipeIndex = replyItem.IndexOf('|');

                if (pipeIndex > 0)
                {
                    string storedKeyword = replyItem.Substring(0, pipeIndex).ToLower().Trim();
                    string answer = replyItem.Substring(pipeIndex + 1).Trim();

                    if (storedKeyword == topic)
                    {
                        responses.Add(answer);
                    }
                }
            }

            return responses;
        }

        public void saveInterest(string input, string username, ListBox chats)
        { //start of saveInterest method

            string[] topics = { "password", "phishing", "privacy", "browsing", "2fa", "cybersecurity" };
            List<string> foundTopics = new List<string>();

            foreach (string topic in topics)
            {
                if (input.Contains(topic))
                {
                    foundTopics.Add(topic);
                }
            }

            if (foundTopics.Count == 0)
            {
                error_method("CyberBuddy", "Please mention a cybersecurity topic you are interested in.", chats);
                return;
            }

            string filename = "interested_topic.txt";
            string interests = string.Join(", ", foundTopics);

            File.AppendAllText(filename, username + " interested in: " + interests + Environment.NewLine);

            error_method("CyberBuddy", "Great! I will remember that you are interested in " + interests + ".", chats);

        }//end of saveInterest method

        public void showInterestReminder(string username, ListBox chats)
        { //start of showInterestReminder

            string filename = "interested_topic.txt";

            if (!File.Exists(filename))
            {
                return;
            }

            string[] lines = File.ReadAllLines(filename);

            foreach (string line in lines)
            {
                if (line.StartsWith(username))
                {
                    int index = line.IndexOf("interested in:");

                    if (index >= 0)
                    {
                        string interests = line.Substring(index + 14).Trim();

                        if (!string.IsNullOrWhiteSpace(interests))
                        {
                            error_method("CyberBuddy", "Reminder: You are interested in " + interests, chats);
                        }
                    }

                    break;
                }
            }
        }//end of showInterestReminder

        public string RemoveSpecialCharacters(string input)
        { //start of RemoveSpecialCharacters method

            if (string.IsNullOrWhiteSpace(input))
            {
                return string.Empty;
            }

            StringBuilder sanitized = new StringBuilder();

            for (int i = 0; i < input.Length; i++)
            {
                if (char.IsLetterOrDigit(input[i]) ||
                    char.IsWhiteSpace(input[i]) ||
                    input[i] == '\'' ||
                    input[i] == '-')
                {
                    sanitized.Append(input[i]);
                }
                else
                {
                    sanitized.Append(' ');
                }
            }

            string result = sanitized.ToString();
            result = Regex.Replace(result, @"\s+", " ").Trim();

            return result;

        }//end of RemoveSpecialCharacters method

        public void error_method(string name, string message, ListBox chats)
        {//start of error_method
            Border messageBorder = new Border
            {
                Margin = new Thickness(0, 2, 0, 2),
                Padding = new Thickness(5),
                CornerRadius = new CornerRadius(8)
            };

            if (name.ToLower().Contains("chat"))
            {
                messageBorder.Background = new SolidColorBrush(Color.FromRgb(240, 248, 255));
            }
            else
            {
                messageBorder.Background = new SolidColorBrush(Color.FromRgb(245, 245, 245));
            }

            TextBlock messageText = new TextBlock
            {
                TextWrapping = TextWrapping.Wrap
            };

            Brush nameColor = name.ToLower().Contains("chat") ? Brushes.DarkBlue : Brushes.DarkGreen;

            messageText.Inlines.Add(new Run
            {
                Text = name + ": ",
                FontWeight = FontWeights.Bold,
                Foreground = nameColor
            });

            messageText.Inlines.Add(new Run
            {
                Text = message,
                Foreground = Brushes.Black
            });

            messageBorder.Child = messageText;
            chats.Items.Add(messageBorder);
            chats.ScrollIntoView(chats.Items[chats.Items.Count - 1]);
        } //end of error_method

    }//end of class
}//end of namespace