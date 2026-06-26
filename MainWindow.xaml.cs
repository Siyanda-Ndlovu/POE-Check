using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace POE_Check
{ //start of namespace
    public partial class MainWindow : Window
    {
        public ArrayList reply = new ArrayList();
        public ArrayList ignore = new ArrayList();

        user_name check_name = new user_name();
        training train_ai = new training();
        tasks manage_tasks = new tasks();
        Quiz_Question_Load load_Quiz = new Quiz_Question_Load();

        string username = string.Empty;
        string pre_question = string.Empty;
        string task_name = string.Empty;
        string task_description = string.Empty;
        string task_dueDate = string.Empty;
        string task_status = string.Empty;
        int counting = 0;

        int currentQuestionIndex = 0;
        int score = 0;
        string selectedAnswer = null;
        List<Button> answerButtons;
        List<Question_in_quiz> questions;

        public MainWindow()
        {
            InitializeComponent();

            new respond(reply, ignore);

            try
            {
                train_ai.Train();
            }
            catch
            {
                // If ML package/model fails, normal keyword matching still works.
            }

            manage_tasks.CreateTableIfNotExists();

            voice_greeting greet = new voice_greeting();
            greet.greet();

            addAllButtons();
            load_Quiz.autoLoadQuiz(ref questions);
            loadQuestions();
        }

        private void start(object sender, RoutedEventArgs e)
        {
            home_grid.Visibility = Visibility.Hidden;
            username_grid.Visibility = Visibility.Visible;
        }

        private void submit_name(object sender, RoutedEventArgs e)
        {
            if (usernames_input.Text.Trim() == "")
            {
                MessageBox.Show("Please enter your username before continuing.",
                                "Username Required",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
                usernames_input.Focus();
                return;
            }

            username = check_name.submit_name(usernames_input, chats);

            username_grid.Visibility = Visibility.Hidden;
            MainPage.Visibility = Visibility.Visible;

            addLog("User logged in as " + username);
        }

        private void chatting(object sender, RoutedEventArgs e)
        {
            chat_page.Visibility = Visibility.Visible;
            HistoryPage.Visibility = Visibility.Hidden;
            reminderPage.Visibility = Visibility.Hidden;
            LogPage.Visibility = Visibility.Hidden;
            gamePage.Visibility = Visibility.Hidden;
        }

        private void reminders(object sender, RoutedEventArgs e)
        {
            chat_page.Visibility = Visibility.Hidden;
            HistoryPage.Visibility = Visibility.Hidden;
            reminderPage.Visibility = Visibility.Visible;
            LogPage.Visibility = Visibility.Hidden;
            gamePage.Visibility = Visibility.Hidden;

            autoLoad_task();
            addLog("Viewed task reminders");
        }

        private void activity(object sender, RoutedEventArgs e)
        {
            chat_page.Visibility = Visibility.Hidden;
            HistoryPage.Visibility = Visibility.Hidden;
            reminderPage.Visibility = Visibility.Hidden;
            LogPage.Visibility = Visibility.Visible;
            gamePage.Visibility = Visibility.Hidden;
        }

        private void game(object sender, RoutedEventArgs e)
        {
            chat_page.Visibility = Visibility.Hidden;
            HistoryPage.Visibility = Visibility.Hidden;
            reminderPage.Visibility = Visibility.Hidden;
            LogPage.Visibility = Visibility.Hidden;
            gamePage.Visibility = Visibility.Visible;
            addLog("Opened cybersecurity quiz");
        }

        private void exit(object sender, RoutedEventArgs e)
        {
            addLog("Exited application");
            Environment.Exit(0);
        }

        private void addAllButtons()
        {
            answerButtons = new List<Button>
            {
                optionButtonOne,
                optionButtonTwo,
                optionButtonThree,
                optionButtonFour
            };
        }

        private void buttonReset()
        {
            foreach (Button clickButton in answerButtons)
            {
                clickButton.ClearValue(Button.BackgroundProperty);
                clickButton.Background = Brushes.Gray;
            }
        }

        private void loadQuestions()
        {
            if (questions == null || questions.Count == 0)
            {
                return;
            }

            if (currentQuestionIndex >= questions.Count)
            {
                MessageBox.Show("Great job! You completed the quiz with a score of " + score + ".\nThe game will now reset.");
                addLog("Completed quiz with score " + score);
                currentQuestionIndex = 0;
                score = 0;
                selectedAnswer = null;
                loadQuestions();
                return;
            }

            Question_in_quiz foundQuestion = questions[currentQuestionIndex];
            question_asked.Text = foundQuestion.Text;
            score_count.Text = "Score\n" + score;
            selectedAnswer = null;
            buttonReset();

            List<string> allAnswers = new List<string>(foundQuestion.wrongAnswer);
            allAnswers.Add(foundQuestion.correctAnswer);

            Random getIndex = new Random();
            for (int count = 0; count < allAnswers.Count; count++)
            {
                int found_index = getIndex.Next(count, allAnswers.Count);
                string temp = allAnswers[count];
                allAnswers[count] = allAnswers[found_index];
                allAnswers[found_index] = temp;
            }

            for (int count = 0; count < answerButtons.Count; count++)
            {
                answerButtons[count].Content = allAnswers[count];
            }
        }

        private void optionSelected(object sender, RoutedEventArgs e)
        {
            buttonReset();
            Button clickButton = sender as Button;
            clickButton.Background = Brushes.Green;
            selectedAnswer = clickButton.Content.ToString();
        }

        private void answerButton(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(selectedAnswer))
            {
                MessageBox.Show("Please select an answer first!");
                return;
            }

            if (selectedAnswer == questions[currentQuestionIndex].correctAnswer)
            {
                score += 5;
                addLog("Answered quiz question correctly");
            }
            else
            {
                addLog("Answered quiz question incorrectly");
            }

            currentQuestionIndex++;
            loadQuestions();
        }

        private void remind_append_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (view_tasks.SelectedValue == null)
            {
                return;
            }

            string get_selected_value = view_tasks.SelectedValue.ToString();
            if (get_selected_value == "No task found")
            {
                return;
            }

            string get_id = get_selected_value.Split(' ')[0];
            int id;

            if (!int.TryParse(get_id, out id))
            {
                return;
            }

            if (get_selected_value.ToLower().EndsWith("done"))
            {
                manage_tasks.delete_task(id);
                addLog("Deleted completed task ID " + id);
            }
            else
            {
                manage_tasks.update_taskStatus(id);
                addLog("Marked task ID " + id + " as done");
            }

            autoLoad_task();
        }

        private void autoLoad_task()
        {
            manage_tasks.load_tasks(view_tasks);
        }

        private void send(object sender, RoutedEventArgs e)
        {
            string rawQuestion = question.Text.ToString();

            if (string.IsNullOrWhiteSpace(rawQuestion))
            {
                error_method("ChatBot", "Please enter a question.");
                return;
            }

            string cleanQuestion = RemoveSpecialCharacters(rawQuestion);

            error_method(username, rawQuestion);
            addLog("User asked: " + rawQuestion);

            auto_show_interest();
            ai_check(cleanQuestion);
        }

        private void ai_check(string questions)
        {
            if (string.IsNullOrWhiteSpace(questions))
            {
                error_method("ChatBot", "I couldn't understand that.");
                question.Clear();
                return;
            }

            questions = questions.ToLower().Trim();
            Random indexer = new Random();

            // TASK COMMANDS MUST COME BEFORE NORMAL KEYWORD RECOGNITION
            if ((questions.Contains("add") && questions.Contains("task")) || questions.StartsWith("add task"))
            {
                handleAddTask(questions);
                question.Clear();
                return;
            }

            if (questions.StartsWith("yes remind me in") || questions.StartsWith("yes reminder me in") || questions.Contains("remind me in"))
            {
                handleReminder(questions);
                question.Clear();
                return;
            }

            if (questions.Contains("show tasks") || questions.Contains("view tasks") || questions.Contains("task reminders"))
            {
                reminders(null, null);
                error_method("ChatBot", "I opened your task reminders page.");
                question.Clear();
                return;
            }

            if (questions.Contains("start quiz") || questions.Contains("open quiz") || questions.Contains("play quiz"))
            {
                game(null, null);
                error_method("ChatBot", "I opened the cybersecurity quiz for you.");
                question.Clear();
                return;
            }

            if (questions.Contains("activity log") || questions.Contains("show activity"))
            {
                activity(null, null);
                error_method("ChatBot", "I opened the activity log.");
                question.Clear();
                return;
            }

            if (questions.Contains("tell me more") || questions.Contains("another tip") || questions.Contains("continue") || questions.Contains("explain more"))
            {
                if (!string.IsNullOrWhiteSpace(pre_question))
                {
                    List<string> foundTips = getAnswersForKeyword(pre_question);
                    if (foundTips.Count > 0)
                    {
                        error_method("ChatBot", "Here is another tip: " + foundTips[indexer.Next(foundTips.Count)]);
                        question.Clear();
                        return;
                    }
                }

                error_method("ChatBot", "Please ask about a topic first, such as password, phishing, privacy, or safe browsing.");
                question.Clear();
                return;
            }

            if (questions.Contains("interested in") || questions.Contains("i like") || questions.Contains("my interest"))
            {
                saveInterest(questions);
                question.Clear();
                return;
            }

            if (questions.Contains("worried") || questions.Contains("scared"))
            {
                error_method("ChatBot", "It is understandable to feel worried. Cyber threats are common, but safe habits help protect you.");
                question.Clear();
                return;
            }

            if (questions.Contains("frustrated") || questions.Contains("confused"))
            {
                error_method("ChatBot", "Cybersecurity can feel overwhelming sometimes, but I will guide you step by step.");
                question.Clear();
                return;
            }

            Dictionary<string, string[]> topicKeywords = new Dictionary<string, string[]>()
            {
                { "password", new string[] { "password", "passwords", "login", "credential", "account security" } },
                { "phishing", new string[] { "phishing", "scam", "email scam", "fake email", "fraud" } },
                { "privacy", new string[] { "privacy", "private data", "personal information", "data protection" } },
                { "browsing", new string[] { "safe browsing", "browser safety", "malicious website", "internet safety", "browsing" } },
                { "2fa", new string[] { "2fa", "two factor", "authentication", "otp" } },
                { "cybersecurity", new string[] { "cybersecurity", "online safety", "cyber safety" } }
            };

            foreach (KeyValuePair<string, string[]> topic in topicKeywords)
            {
                foreach (string keyword in topic.Value)
                {
                    if (questions.Contains(keyword))
                    {
                        List<string> topicAnswers = getAnswersForKeyword(topic.Key);
                        if (topicAnswers.Count > 0)
                        {
                            pre_question = topic.Key;
                            task_description = topicAnswers[indexer.Next(topicAnswers.Count)];
                            error_method("ChatBot", task_description);
                            question.Clear();
                            return;
                        }
                    }
                }
            }

            if (IsWordMatch(questions, "hello") || IsWordMatch(questions, "hi") || IsWordMatch(questions, "hey"))
            {
                error_method("ChatBot", getFirstAnswer("hello", "Hello " + username + ". How can I help you today?"));
                question.Clear();
                return;
            }

            if (questions.Contains("how are you") || questions.Contains("how r u"))
            {
                error_method("ChatBot", getFirstAnswer("how are you", "I am doing well and ready to help you stay safe online."));
                question.Clear();
                return;
            }

            if (questions.Contains("purpose") || questions.Contains("what do you do"))
            {
                error_method("ChatBot", getFirstAnswer("purpose", "My purpose is to educate you about cybersecurity awareness."));
                question.Clear();
                return;
            }

            if (questions.Contains("help") || questions.Contains("what can i ask") || questions.Contains("what questions"))
            {
                error_method("ChatBot", getFirstAnswer("help", "You can ask me about passwords, phishing, privacy, scams, 2FA and safe browsing."));
                question.Clear();
                return;
            }

            if (questions.Contains("thank") || questions.Contains("thanks"))
            {
                error_method("ChatBot", getFirstAnswer("thanks", "You are welcome " + username + "."));
                question.Clear();
                return;
            }

            if (questions.Contains("bye") || questions.Contains("goodbye") || questions.Contains("exit"))
            {
                error_method("ChatBot", getFirstAnswer("bye", "Goodbye " + username + ". Stay safe online."));
                question.Clear();
                return;
            }

            try
            {
                string unknownResponse = train_ai.CheckAndLearn(questions, username);
                error_method("ChatBot", unknownResponse);
            }
            catch
            {
                error_method("ChatBot", "I didn't fully understand that. Please ask about passwords, phishing, privacy, tasks, or the quiz.");
            }

            question.Clear();
        }

        private void handleAddTask(string questions)
        {
            string[] taskKeywords =
            {
                "password", "passwords", "phishing", "scam", "privacy", "browsing", "safe browsing", "2fa", "cybersecurity", "vpn", "firewall", "malware"
            };

            string foundTopic = string.Empty;

            foreach (string keyword in taskKeywords)
            {
                if (questions.Contains(keyword))
                {
                    foundTopic = keyword;
                    break;
                }
            }

            if (foundTopic == string.Empty)
            {
                foundTopic = questions.Replace("add task", "")
                                      .Replace("add", "")
                                      .Replace("to task", "")
                                      .Replace("task", "")
                                      .Trim();
            }

            if (foundTopic == string.Empty)
            {
                error_method("ChatBot", "Please tell me what task you want to add. Example: add password to task");
                return;
            }

            task_name = foundTopic;
            task_description = getTaskDescription(foundTopic);
            task_dueDate = "No reminder set";
            task_status = "pending";

            manage_tasks.insert_task(task_name, task_description, task_dueDate, task_status);

            error_method("ChatBot", "Great, I added " + task_name + " to your tasks. Would you like a reminder? Try: yes remind me in 3 days");
            addLog("Added task: " + task_name);
        }

        private void handleReminder(string questions)
        {
            if (string.IsNullOrWhiteSpace(task_name))
            {
                error_method("ChatBot", "Please add a task first before setting a reminder.");
                return;
            }

            string days_number = Regex.Replace(questions, @"[^0-9]", "");

            int days;
            if (!int.TryParse(days_number, out days))
            {
                error_method("ChatBot", "Please include the number of days. Example: yes remind me in 3 days");
                return;
            }

            DateTime user_reminder = DateTime.Now.AddDays(days);
            string format_date = user_reminder.ToString("MMMM dd yyyy");

            task_dueDate = format_date;
            task_status = "pending";

            manage_tasks.update_latest_task_reminder(task_name, task_dueDate);

            error_method("ChatBot", "Good, I will remind you in " + days + " days, on " + format_date + ". Click Task Reminders to view it.");
            addLog("Set reminder for " + task_name + " on " + format_date);
        }

        private string getTaskDescription(string topic)
        {
            string normalized = topic.ToLower();

            if (normalized.Contains("password"))
                return "Review password safety and create strong unique passwords.";

            if (normalized.Contains("phishing") || normalized.Contains("scam"))
                return "Review phishing and scam prevention tips.";

            if (normalized.Contains("privacy"))
                return "Review privacy settings and protect personal information.";

            if (normalized.Contains("browsing"))
                return "Review safe browsing habits and avoid unsafe websites.";

            if (normalized.Contains("2fa"))
                return "Enable and review two-factor authentication.";

            if (normalized.Contains("cybersecurity"))
                return "Review general cybersecurity safety practices.";

            return "Cybersecurity related task.";
        }

        private List<string> getAnswersForKeyword(string keyword)
        {
            List<string> answers = new List<string>();

            foreach (object replyObj in reply)
            {
                string item = replyObj.ToString();
                int pipeIndex = item.IndexOf('|');

                if (pipeIndex > 0)
                {
                    string storedKeyword = item.Substring(0, pipeIndex).ToLower().Trim();
                    string answer = item.Substring(pipeIndex + 1).Trim();

                    if (storedKeyword == keyword.ToLower())
                    {
                        answers.Add(answer);
                    }
                }
            }

            return answers;
        }

        private string getFirstAnswer(string keyword, string fallback)
        {
            List<string> answers = getAnswersForKeyword(keyword);
            if (answers.Count > 0)
            {
                return answers[0];
            }
            return fallback;
        }

        private bool IsWordMatch(string sentence, string word)
        {
            string[] words = sentence.Split(' ');
            foreach (string item in words)
            {
                if (item == word)
                {
                    return true;
                }
            }
            return false;
        }

        private void saveInterest(string questions)
        {
            string[] words = questions.ToLower().Split(new char[] { ' ', ',', '.', '?', '!', ';', ':' }, StringSplitOptions.RemoveEmptyEntries);
            HashSet<string> currentInterests = new HashSet<string>();

            foreach (string interest in words)
            {
                string clean = Regex.Replace(interest.ToLower().Trim(), @"[^a-zA-Z0-9\s]", "");

                if (!ignore.Contains(clean) && clean != "interested" && clean != "interest" && clean != "and" && clean != "in" && clean != "like" && clean.Length >= 3)
                {
                    currentInterests.Add(clean);
                }
            }

            if (currentInterests.Count == 0)
            {
                error_method("ChatBot", "Please specify what you're interested in. Example: I am interested in privacy.");
                return;
            }

            string store_interests = string.Join(", ", currentInterests);
            string filename = "interested_topic.txt";
            bool userFound = false;

            if (File.Exists(filename))
            {
                string[] lines = File.ReadAllLines(filename);

                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].StartsWith(username))
                    {
                        userFound = true;

                        string existing = lines[i].Replace(username + " interested in:", "").ToLower();
                        HashSet<string> existingSet = new HashSet<string>(existing.Split(',').Select(x => x.Trim()).Where(x => x != ""));

                        foreach (string item in currentInterests)
                        {
                            existingSet.Add(item);
                        }

                        string finalList = string.Join(", ", existingSet);
                        lines[i] = username + " interested in: " + finalList;
                        File.WriteAllLines(filename, lines);

                        error_method("ChatBot", "Great, I added " + store_interests + " to your interests.");
                        addLog("Updated interests: " + store_interests);
                        return;
                    }
                }
            }

            if (!userFound)
            {
                File.AppendAllText(filename, username + " interested in: " + store_interests + Environment.NewLine);
                error_method("ChatBot", "Great, I will remember that you are interested in " + store_interests + ".");
                addLog("Saved interests: " + store_interests);
            }
        }

        private string RemoveSpecialCharacters(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            StringBuilder sanitized = new StringBuilder();

            foreach (char c in input)
            {
                if (char.IsLetterOrDigit(c) || char.IsWhiteSpace(c) || c == '\'' || c == '-')
                {
                    sanitized.Append(c);
                }
                else
                {
                    sanitized.Append(' ');
                }
            }

            string result = sanitized.ToString();
            result = Regex.Replace(result, @"\s+", " ").Trim();
            return result;
        }

        private void auto_show_interest()
        {
            if (counting == 3)
            {
                string filename = "interested_topic.txt";

                if (File.Exists(filename))
                {
                    string[] lines = File.ReadAllLines(filename);

                    foreach (string line in lines)
                    {
                        if (line.StartsWith(username))
                        {
                            int colonIndex = line.IndexOf("interested in:");
                            if (colonIndex >= 0)
                            {
                                string interests = line.Substring(colonIndex + 14).Trim();
                                error_method("ChatBot", "Just a reminder, you are interested in " + interests + ". Would you like another tip?");
                                break;
                            }
                        }
                    }
                }

                counting = 0;
            }
            else
            {
                counting++;
            }
        }

        private void addLog(string action)
        {
            string log = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " - " + action;
            log_append.Items.Add(log);
            File.AppendAllText("activity_log.txt", log + Environment.NewLine);
        }

        private void error_method(string name, string message)
        {
            Border messageBorder = new Border
            {
                Margin = new Thickness(0, 2, 0, 2),
                Padding = new Thickness(5, 3, 5, 3),
                CornerRadius = new CornerRadius(5)
            };

            if (name.ToLower().Contains("chatbot") || name.ToLower().Contains("chat"))
            {
                messageBorder.Background = new SolidColorBrush(Color.FromRgb(240, 248, 255));
                messageBorder.BorderBrush = new SolidColorBrush(Color.FromRgb(173, 216, 230));
            }
            else
            {
                messageBorder.Background = new SolidColorBrush(Color.FromRgb(245, 245, 245));
                messageBorder.BorderBrush = new SolidColorBrush(Color.FromRgb(211, 211, 211));
            }

            messageBorder.BorderThickness = new Thickness(1);

            TextBlock messageText = new TextBlock
            {
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(2)
            };

            Brush nameColor = (name.ToLower().Contains("chatbot") || name.ToLower().Contains("chat")) ? Brushes.DarkBlue : Brushes.DarkGreen;

            messageText.Inlines.Add(new Run
            {
                Text = name + ": ",
                Foreground = nameColor,
                FontWeight = FontWeights.Bold
            });

            messageText.Inlines.Add(new Run
            {
                Text = message,
                Foreground = Brushes.Black
            });

            messageBorder.Child = messageText;
            chats.Items.Add(messageBorder);
            chats.ScrollIntoView(chats.Items[chats.Items.Count - 1]);
        }
    }
}
