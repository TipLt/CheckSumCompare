using System.IO;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Win32;

namespace CheckSumCompare;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void BrowseButton_Click(object sender, RoutedEventArgs e)
    {
        var openFileDialog = new OpenFileDialog
        {
            Title = "Select a file to calculate checksum",
            Filter = "All Files (*.*)|*.*"
        };

        if (openFileDialog.ShowDialog() == true)
        {
            FilePathTextBox.Text = openFileDialog.FileName;
            CalculatedChecksumTextBox.Clear();
            ResultsTextBox.Clear();
        }
    }

    private async void CalculateButton_Click(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(FilePathTextBox.Text))
        {
            MessageBox.Show("Please select a file first.", "No File Selected", 
                MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        if (!File.Exists(FilePathTextBox.Text))
        {
            MessageBox.Show("The selected file does not exist.", "File Not Found", 
                MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        try
        {
            // Disable controls during calculation
            SetControlsEnabled(false);
            CalculatedChecksumTextBox.Text = "Calculating...";
            ResultsTextBox.Clear();

            var selectedAlgorithm = ((ComboBoxItem)HashAlgorithmComboBox.SelectedItem).Content.ToString();
            var checksum = await CalculateChecksumAsync(FilePathTextBox.Text, selectedAlgorithm);

            CalculatedChecksumTextBox.Text = checksum;
            
            var fileInfo = new FileInfo(FilePathTextBox.Text);
            ResultsTextBox.Text = $"File: {fileInfo.Name}\n" +
                                 $"Size: {FormatFileSize(fileInfo.Length)}\n" +
                                 $"Algorithm: {selectedAlgorithm}\n" +
                                 $"Checksum: {checksum}";
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error calculating checksum: {ex.Message}", "Error", 
                MessageBoxButton.OK, MessageBoxImage.Error);
            CalculatedChecksumTextBox.Clear();
        }
        finally
        {
            SetControlsEnabled(true);
        }
    }

    private void CompareButton_Click(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(CalculatedChecksumTextBox.Text) || 
            CalculatedChecksumTextBox.Text == "Calculating...")
        {
            MessageBox.Show("Please calculate the checksum first.", "No Checksum", 
                MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        if (string.IsNullOrWhiteSpace(ExpectedChecksumTextBox.Text))
        {
            MessageBox.Show("Please enter the expected checksum.", "No Expected Checksum", 
                MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        var calculated = CalculatedChecksumTextBox.Text.Replace("-", "").Replace(" ", "").ToUpperInvariant();
        var expected = ExpectedChecksumTextBox.Text.Replace("-", "").Replace(" ", "").ToUpperInvariant();

        bool isMatch = calculated == expected;

        if (isMatch)
        {
            ResultsTextBox.Background = new SolidColorBrush(Color.FromRgb(200, 255, 200));
            ResultsTextBox.Text = "✓ MATCH - Checksums are identical!\n\n" +
                                 $"Calculated: {CalculatedChecksumTextBox.Text}\n" +
                                 $"Expected:   {ExpectedChecksumTextBox.Text}\n\n" +
                                 "The file integrity is verified.";
        }
        else
        {
            ResultsTextBox.Background = new SolidColorBrush(Color.FromRgb(255, 200, 200));
            ResultsTextBox.Text = "✗ MISMATCH - Checksums are different!\n\n" +
                                 $"Calculated: {CalculatedChecksumTextBox.Text}\n" +
                                 $"Expected:   {ExpectedChecksumTextBox.Text}\n\n" +
                                 "The file may be corrupted or different.";
        }
    }

    private async Task<string> CalculateChecksumAsync(string filePath, string? algorithm)
    {
        return await Task.Run(() =>
        {
            using var stream = File.OpenRead(filePath);
            using var hashAlgorithm = CreateHashAlgorithm(algorithm);
            var hashBytes = hashAlgorithm.ComputeHash(stream);
            return BitConverter.ToString(hashBytes).Replace("-", "");
        });
    }

    private HashAlgorithm CreateHashAlgorithm(string? algorithm)
    {
        return algorithm?.ToUpperInvariant() switch
        {
            "MD5" => MD5.Create(),
            "SHA1" => SHA1.Create(),
            "SHA256" => SHA256.Create(),
            "SHA384" => SHA384.Create(),
            "SHA512" => SHA512.Create(),
            _ => SHA256.Create()
        };
    }

    private string FormatFileSize(long bytes)
    {
        string[] sizes = { "B", "KB", "MB", "GB", "TB" };
        double len = bytes;
        int order = 0;
        while (len >= 1024 && order < sizes.Length - 1)
        {
            order++;
            len = len / 1024;
        }
        return $"{len:0.##} {sizes[order]}";
    }

    private void SetControlsEnabled(bool enabled)
    {
        BrowseButton.IsEnabled = enabled;
        HashAlgorithmComboBox.IsEnabled = enabled;
        CalculateButton.IsEnabled = enabled;
        CompareButton.IsEnabled = enabled;
        ExpectedChecksumTextBox.IsEnabled = enabled;
    }
}