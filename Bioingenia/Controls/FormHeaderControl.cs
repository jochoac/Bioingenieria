namespace Bioingenieria.Controls;

public partial class FormHeaderControl : UserControl
{
    public FormHeaderControl()
    {
        InitializeComponent();
    }

    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
    public string Title
    {
        get => titleLabel.Text;
        set => titleLabel.Text = value;
    }
}
