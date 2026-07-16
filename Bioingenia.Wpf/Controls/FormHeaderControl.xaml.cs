using System.Windows;
using System.Windows.Controls;

namespace Bioingenieria.Controls;

public partial class FormHeaderControl : UserControl
{
    public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
        nameof(Title), typeof(string), typeof(FormHeaderControl), new PropertyMetadata(string.Empty));

    public FormHeaderControl()
    {
        InitializeComponent();
    }

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }
}
