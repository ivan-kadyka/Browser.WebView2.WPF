using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace App;

public static class TextBoxPlaceholderBehavior
{
    public static readonly DependencyProperty PlaceholderProperty = 
        DependencyProperty.RegisterAttached(
            "Placeholder", 
            typeof(string), 
            typeof(TextBoxPlaceholderBehavior), 
            new PropertyMetadata(string.Empty, OnPlaceholderChanged));

    public static string GetPlaceholder(DependencyObject obj)
    {
        return (string)obj.GetValue(PlaceholderProperty);
    }

    public static void SetPlaceholder(DependencyObject obj, string value)
    {
        obj.SetValue(PlaceholderProperty, value);
    }

    private static void OnPlaceholderChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
    {
        if (dependencyObject is TextBox textBox)
        {
            textBox.Loaded += (_, _) => AddPlaceholder(textBox, (string)e.NewValue);
            textBox.GotFocus += (_, _) => RemovePlaceholder(textBox);
            textBox.LostFocus += (_, _) => AddPlaceholder(textBox, (string)e.NewValue);
        }
    }

    private static void AddPlaceholder(TextBox textBox, string placeholder)
    {
        if (string.IsNullOrEmpty(textBox.Text))
        {
            textBox.Text = placeholder;
            textBox.Foreground = new SolidColorBrush(Colors.Gray);
        }
    }

    private static void RemovePlaceholder(TextBox textBox)
    {
        if (textBox.Text == GetPlaceholder(textBox))
        {
            textBox.Text = string.Empty;
            textBox.Foreground = new SolidColorBrush(Colors.Black);
        }
    }
}
