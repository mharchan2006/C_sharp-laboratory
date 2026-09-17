using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

public class MDIForm : Form
{
    public static List<string> SavedNames = new List<string>();
    static string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "names.txt");

    MenuStrip menuStrip;
    ToolStripMenuItem fileMenu, newWindow, customDialog, exitMenu;
    ToolStripMenuItem windowMenu, cascadeMenu, tileHMenu, tileVMenu, closeAllMenu, viewNamesMenu;
    StatusStrip statusStrip;
    ToolStripStatusLabel statusLabel;
    int childCount = 0;

    public MDIForm()
    {
        Text = "MDI Application";
        Size = new Size(900, 600);
        IsMdiContainer = true;
        LoadNamesFromFile();

        menuStrip = new MenuStrip();
        fileMenu = new ToolStripMenuItem("File");
        newWindow = new ToolStripMenuItem("New Window");
        customDialog = new ToolStripMenuItem("Custom Dialog");
        viewNamesMenu = new ToolStripMenuItem("View Saved Names");
        exitMenu = new ToolStripMenuItem("Exit");

        newWindow.Click += NewWindow_Click;
        customDialog.Click += CustomDialog_Click;
        viewNamesMenu.Click += ViewNames_Click;
        exitMenu.Click += (s, e) => Application.Exit();

        fileMenu.DropDownItems.AddRange(new ToolStripItem[] { newWindow, customDialog, viewNamesMenu, new ToolStripSeparator(), exitMenu });

        windowMenu = new ToolStripMenuItem("Window");
        cascadeMenu = new ToolStripMenuItem("Cascade");
        tileHMenu = new ToolStripMenuItem("Tile Horizontal");
        tileVMenu = new ToolStripMenuItem("Tile Vertical");
        closeAllMenu = new ToolStripMenuItem("Close All");

        cascadeMenu.Click += (s, e) => LayoutMdi(MdiLayout.Cascade);
        tileHMenu.Click += (s, e) => LayoutMdi(MdiLayout.TileHorizontal);
        tileVMenu.Click += (s, e) => LayoutMdi(MdiLayout.TileVertical);
        closeAllMenu.Click += (s, e) => { foreach (Form f in MdiChildren) f.Close(); };

        windowMenu.DropDownItems.AddRange(new ToolStripItem[] { cascadeMenu, tileHMenu, tileVMenu, new ToolStripSeparator(), closeAllMenu });
        menuStrip.MdiWindowListItem = windowMenu;

        menuStrip.Items.Add(fileMenu);
        menuStrip.Items.Add(windowMenu);

        statusStrip = new StatusStrip();
        statusLabel = new ToolStripStatusLabel($"Open: 0 | Saved Names: {SavedNames.Count}");
        statusStrip.Items.Add(statusLabel);

        Controls.Add(menuStrip);
        Controls.Add(statusStrip);
        MainMenuStrip = menuStrip;
    }

    private void NewWindow_Click(object sender, EventArgs e)
    {
        childCount++;
        ChildForm child = new ChildForm();
        child.MdiParent = this;
        child.Text = $"Child Window {childCount}";
        child.FormClosed += (s, ev) => UpdateStatus();
        child.Show();
        UpdateStatus();
    }

    private void CustomDialog_Click(object sender, EventArgs e)
    {
        CustomInputDialog dlg = new CustomInputDialog();
        dlg.ShowDialog(this);
        UpdateStatus();
    }

    private void ViewNames_Click(object sender, EventArgs e)
    {
        ShowSavedNames();
    }

    public void ShowSavedNames()
    {
        Form viewForm = new Form { Text = "Saved Names", Size = new Size(350, 300), StartPosition = FormStartPosition.CenterParent };
        ListBox lb = new ListBox { Dock = DockStyle.Fill };
        lb.Items.AddRange(SavedNames.ToArray());
        if (SavedNames.Count == 0) lb.Items.Add("(No names saved yet)");
        viewForm.Controls.Add(lb);
        viewForm.ShowDialog();
    }

    void UpdateStatus() => statusLabel.Text = $"Open: {MdiChildren.Length} | Saved Names: {SavedNames.Count}";

    public static void SaveNamesToFile()
    {
        File.WriteAllLines(filePath, SavedNames);
    }
    public static void LoadNamesFromFile()
    {
        if (File.Exists(filePath))
            SavedNames = new List<string>(File.ReadAllLines(filePath));
    }

    [STAThread]
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new MDIForm());
    }
}

// NEW CUSTOM DIALOG WITH 3 NAMES
public class CustomInputDialog : Form
{
    TextBox txt1, txt2, txt3;
    public CustomInputDialog()
    {
        Text = "Custom Dialog - Add Names";
        Size = new Size(380, 300);
        StartPosition = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false; MinimizeBox = false;

        Label l1 = new Label { Text = "Name 1:", Location = new Point(20, 20), AutoSize = true };
        txt1 = new TextBox { Location = new Point(20, 40), Width = 320 };
        Label l2 = new Label { Text = "Name 2:", Location = new Point(20, 75), AutoSize = true };
        txt2 = new TextBox { Location = new Point(20, 95), Width = 320 };
        Label l3 = new Label { Text = "Name 3:", Location = new Point(20, 130), AutoSize = true };
        txt3 = new TextBox { Location = new Point(20, 150), Width = 320 };

        Button btnSave = new Button { Text = "Save All", Location = new Point(20, 200), Width = 90 };
        Button btnView = new Button { Text = "View Saved", Location = new Point(130, 200), Width = 90 };
        Button btnCancel = new Button { Text = "Close", Location = new Point(240, 200), Width = 90 };

        btnSave.Click += (s, e) =>
        {
            int added = 0;
            if (!string.IsNullOrWhiteSpace(txt1.Text)) { MDIForm.SavedNames.Add(txt1.Text.Trim()); added++; }
            if (!string.IsNullOrWhiteSpace(txt2.Text)) { MDIForm.SavedNames.Add(txt2.Text.Trim()); added++; }
            if (!string.IsNullOrWhiteSpace(txt3.Text)) { MDIForm.SavedNames.Add(txt3.Text.Trim()); added++; }

            if (added > 0)
            {
                MDIForm.SaveNamesToFile();
                MessageBox.Show($"{added} name(s) saved!\nTotal: {MDIForm.SavedNames.Count}", "Saved");
                txt1.Clear(); txt2.Clear(); txt3.Clear();
            }
            else
            {
                MessageBox.Show("Please enter at least one name.");
            }
        };

        btnView.Click += (s, e) =>
        {
            if (MDIForm.SavedNames.Count == 0) MessageBox.Show("No names saved yet.");
            else MessageBox.Show(string.Join("\n", MDIForm.SavedNames), $"Saved Names - {MDIForm.SavedNames.Count}");
        };

        btnCancel.Click += (s, e) => Close();

        Controls.AddRange(new Control[] { l1, txt1, l2, txt2, l3, txt3, btnSave, btnView, btnCancel });
    }
}

public class ChildForm : Form
{
    public ChildForm()
    {
        Size = new Size(400, 250);
        Label label = new Label { Text = "This is a Child Form", Font = new Font("Arial", 16, FontStyle.Bold), AutoSize = true, Location = new Point(80, 100) };
        Controls.Add(label);
    }
}
