using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SurveyCreator
{
    public partial class MainWindow : Window
    {
        private List<string> answerOptions = new List<string>();
        private List<string> surveyQuestions = new List<string>();

        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void OnAddOptionClick(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            string optionText = AnswerOptionTextBox.Text;
            if (!string.IsNullOrWhiteSpace(optionText))
            {
                answerOptions.Add(optionText);
                OptionsListBox.ItemsSource = null;
                OptionsListBox.ItemsSource = answerOptions;
                AnswerOptionTextBox.Clear();
            }
        }
        
        private void OnAddQuestionClick(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            string questionText = QuestionTextBox.Text;
            string questionType = SingleChoiceRadio.IsChecked == true ? "Jednokrotnego wyboru" : "Wielokrotnego wyboru";
            string category = (CategoryComboBox.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "Ogólne";

            if (!string.IsNullOrWhiteSpace(questionText) && answerOptions.Count > 0)
            {
                StringBuilder questionBuilder = new StringBuilder();
                questionBuilder.AppendLine($"Pytanie: {questionText}");
                questionBuilder.AppendLine($"Typ: {questionType}");
                questionBuilder.AppendLine($"Kategoria: {category}");
                questionBuilder.AppendLine("Opcje odpowiedzi:");

                foreach (var option in answerOptions)
                {
                    questionBuilder.AppendLine($" - {option}");
                }

                surveyQuestions.Add(questionBuilder.ToString());
                QuestionsListBox.ItemsSource = null;
                QuestionsListBox.ItemsSource = surveyQuestions;
                
                QuestionTextBox.Clear();
                answerOptions.Clear();
                OptionsListBox.ItemsSource = null;
            }
        }
        
        private void OnSaveSurveyClick(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            string surveyContent = string.Join("\n\n", surveyQuestions);
            
            string projectDirectory = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..");
            string filePath = Path.Combine(projectDirectory, "ankieta.txt");
            File.WriteAllText(filePath, surveyContent);
            
            Console.WriteLine("Ankieta zostala zapisana w folderze projektu(Dokładnie to SurveyCreator obok pliku .sln)");
        }
    }
}
