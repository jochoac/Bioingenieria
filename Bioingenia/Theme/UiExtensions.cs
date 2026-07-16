using System.Drawing.Drawing2D;

namespace Bioingenieria.Theme;

public static class UiExtensions
{
    private static readonly Icon? CachedAppIcon = LoadAppIcon();

    public static void ApplyAppIcon(this Form form)
    {
        if (CachedAppIcon is not null)
        {
            form.Icon = CachedAppIcon;
        }
    }

    public static void LoadAppLogo(this PictureBox pictureBox)
    {
        var logoPath = Path.Combine(AppContext.BaseDirectory, "Resources", "logo.png");
        if (File.Exists(logoPath))
        {
            pictureBox.Image = Image.FromFile(logoPath);
        }
    }

    public static void ApplyPrimaryStyle(this Button button)
    {
        button.FlatStyle = FlatStyle.Flat;
        button.FlatAppearance.BorderSize = 0;
        button.FlatAppearance.MouseOverBackColor = AppColors.PrimaryDark;
        button.BackColor = AppColors.Primary;
        button.ForeColor = Color.White;
        button.UseVisualStyleBackColor = false;
        button.Cursor = Cursors.Hand;
    }

    public static void ApplySecondaryStyle(this Button button)
    {
        button.FlatStyle = FlatStyle.Flat;
        button.FlatAppearance.BorderSize = 1;
        button.FlatAppearance.BorderColor = AppColors.Border;
        button.FlatAppearance.MouseOverBackColor = AppColors.Surface;
        button.BackColor = Color.White;
        button.ForeColor = AppColors.PrimaryDark;
        button.UseVisualStyleBackColor = false;
        button.Cursor = Cursors.Hand;
    }

    public static void ApplyDangerStyle(this Button button)
    {
        button.FlatStyle = FlatStyle.Flat;
        button.FlatAppearance.BorderSize = 1;
        button.FlatAppearance.BorderColor = AppColors.Critical;
        button.BackColor = Color.White;
        button.ForeColor = AppColors.Critical;
        button.UseVisualStyleBackColor = false;
        button.Cursor = Cursors.Hand;
    }

    public static void SetToggleState(this Button button, bool active)
    {
        button.FlatStyle = FlatStyle.Flat;
        button.UseVisualStyleBackColor = false;

        if (active)
        {
            button.BackColor = AppColors.Primary;
            button.ForeColor = Color.White;
            button.FlatAppearance.BorderSize = 0;
        }
        else
        {
            button.BackColor = Color.White;
            button.ForeColor = AppColors.PrimaryDark;
            button.FlatAppearance.BorderSize = 1;
            button.FlatAppearance.BorderColor = AppColors.Border;
        }
    }

    public static void ApplyTheme(this DataGridView grid)
    {
        grid.EnableHeadersVisualStyles = false;
        grid.BorderStyle = BorderStyle.None;
        grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
        grid.BackgroundColor = Color.White;
        grid.GridColor = AppColors.Border;
        grid.RowTemplate.Height = 32;

        grid.ColumnHeadersHeight = 34;
        grid.ColumnHeadersDefaultCellStyle.BackColor = AppColors.Primary;
        grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        grid.ColumnHeadersDefaultCellStyle.SelectionBackColor = AppColors.Primary;
        grid.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;
        grid.ColumnHeadersDefaultCellStyle.Font = new Font(grid.Font, FontStyle.Bold);

        grid.DefaultCellStyle.BackColor = Color.White;
        grid.DefaultCellStyle.ForeColor = AppColors.Text;
        grid.DefaultCellStyle.SelectionBackColor = Tint(AppColors.Primary, 0.85);
        grid.DefaultCellStyle.SelectionForeColor = AppColors.Text;
        grid.AlternatingRowsDefaultCellStyle.BackColor = AppColors.Surface;
    }

    private static Color Tint(Color baseColor, double amountWhite)
    {
        var r = (int)(baseColor.R + (255 - baseColor.R) * amountWhite);
        var g = (int)(baseColor.G + (255 - baseColor.G) * amountWhite);
        var b = (int)(baseColor.B + (255 - baseColor.B) * amountWhite);
        return Color.FromArgb(r, g, b);
    }

    public static void ApplyRoundedCorners(this Control control, int radius)
    {
        void UpdateRegion()
        {
            if (control.Width <= 0 || control.Height <= 0)
            {
                return;
            }

            var effectiveRadius = Math.Min(radius, Math.Min(control.Width, control.Height) / 2);
            using var path = RoundedRectPath(new Rectangle(0, 0, control.Width, control.Height), effectiveRadius);
            control.Region = new Region(path);
        }

        UpdateRegion();
        control.Resize += (_, _) => UpdateRegion();
    }

    public static GraphicsPath RoundedRectPath(Rectangle bounds, int radius)
    {
        var path = new GraphicsPath();
        if (radius <= 0)
        {
            path.AddRectangle(bounds);
            return path;
        }

        var diameter = radius * 2;
        var arc = new Rectangle(bounds.Location, new Size(diameter, diameter));

        path.AddArc(arc, 180, 90);
        arc.X = bounds.Right - diameter;
        path.AddArc(arc, 270, 90);
        arc.Y = bounds.Bottom - diameter;
        path.AddArc(arc, 0, 90);
        arc.X = bounds.Left;
        path.AddArc(arc, 90, 90);
        path.CloseFigure();
        return path;
    }

    private static Icon? LoadAppIcon()
    {
        var iconPath = Path.Combine(AppContext.BaseDirectory, "Resources", "app.ico");
        return File.Exists(iconPath) ? new Icon(iconPath) : null;
    }
}
