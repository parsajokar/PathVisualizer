using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PathVisualizer;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        for (int i = 0; i < 400; i++)
        {
            var btn = new Button();
            btn.Tag = i;
            btn.Background = new SolidColorBrush(Colors.Yellow);
            btn.Click += OnButtonClick;
            _isVisited[i] = false;
            btn.Style = FindResource("FlatButton") as Style;

            ButtonGrid.Children.Add(btn);
        }

        Button start = (Button)ButtonGrid.Children[0];
        Button end = (Button)ButtonGrid.Children[399];

        start.Content = "Start";
        end.Content = "End";

        start.FontSize = 14.0;
        end.FontSize = 14.0;
        
        start.FontWeight = FontWeights.Bold;
        end.FontWeight = FontWeights.Bold;
        
        start.Background = new SolidColorBrush(Colors.LightBlue);
        end.Background = new SolidColorBrush(Colors.LightBlue);
        
        start.Click -= OnButtonClick;
        end.Click -= OnButtonClick;
    }

    private void OnButtonClick(object sender, RoutedEventArgs e)
    {
        if (_isRunning)
            return;
        
        var btn = (Button)sender;

        if (_isVisited[(int)btn.Tag])
        {
            btn.Background = new SolidColorBrush(Colors.Yellow);
            _isVisited[(int)btn.Tag] = false;
        }
        else
        {
            btn.Background = new SolidColorBrush(Colors.Black);
            _isVisited[(int)btn.Tag] = true;
        }
    }

    private async void OnRunDfs(object sender, RoutedEventArgs e)
    {
        if (_isRunning)
            return;
        
        _isRunning = true;
        _foundPath = false;
        
        _stack.Clear();
        _stack.Push(0);
        _isVisited[0] = true;
        
        await PerformDfsAsync();
        
        ShowResults();
        Reset();
    }
    
    private async Task PerformDfsAsync()
    {
        while (_stack.Count > 0)
        {
            int v = _stack.Pop();
            var (x, y) = (v % 20, v / 20);

            if (v != 0)
            {
                var btn = (Button)ButtonGrid.Children[v];
                btn.Background = new SolidColorBrush(Colors.Red);
            }
        
            int[] dx = [-1, 1, 0, 0];
            int[] dy = [0, 0, -1, 1];
            for (int i = 0; i < 4; i++)
            {
                int nx = x + dx[i];
                int ny = y + dy[i];
                if (nx < 0 || ny < 0 || nx > 19 || ny > 19)
                {
                    continue;
                }

                int u = nx + ny * 20;
                if (!_isVisited[u])
                {
                    _stack.Push(u);
                    _isVisited[u] = true;
                    _parent[u] = v;
                
                    if (u == 399)
                    {
                        _foundPath = true;
                        _isRunning = false;
                        return;
                    }
                }
            }
            
            await Task.Delay(10);
        }
        
        _isRunning = false;
    }

    private async void OnRunBfs(object sender, RoutedEventArgs e)
    {
        if (_isRunning)
            return;
        
        _isRunning = true;
        _foundPath = false;
        
        _queue.Clear();
        _queue.Enqueue(0);
        _isVisited[0] = true;
        
        await PerformBfsAsync();

        ShowResults();
        Reset();
    }
    
    private async Task PerformBfsAsync()
    {
        while (_queue.Count > 0)
        {
            int v = _queue.Dequeue();
            var (x, y) = (v % 20, v / 20);

            if (v != 0)
            {
                var btn = (Button)ButtonGrid.Children[v];
                btn.Background = new SolidColorBrush(Colors.Red);
            }
        
            int[] dx = [-1, 1, 0, 0];
            int[] dy = [0, 0, -1, 1];
            for (int i = 0; i < 4; i++)
            {
                int nx = x + dx[i];
                int ny = y + dy[i];
                if (nx < 0 || ny < 0 || nx > 19 || ny > 19)
                {
                    continue;
                }

                int u = nx + ny * 20;
                if (!_isVisited[u])
                {
                    _queue.Enqueue(u);
                    _isVisited[u] = true;
                    _parent[u] = v;
                
                    if (u == 399)
                    {
                        _foundPath = true;
                        _isRunning = false;
                        return;
                    }
                }
            }
            
            await Task.Delay(10);
        }
        
        _isRunning = false;
    }

    private void ShowResults()
    {
        if (_foundPath)
        {
            for (int v = _parent[399]; v != 0; v = _parent[v])
            {
                var btn = (Button)ButtonGrid.Children[v];
                btn.Background = new SolidColorBrush(Colors.Green);
            }
            
            MessageBox.Show("Found a path!");
        }
        else
        {
            MessageBox.Show("No path found!");
        }
    }

    private void Reset()
    {
        for (int i = 0; i < 400; i++)
        {
            if (i != 0 && i != 399)
            {
                var btn = (Button)ButtonGrid.Children[i];
                btn.Background = new SolidColorBrush(Colors.Yellow);
            }
            _isVisited[i] = false;
        }
    }

    private bool _isRunning = false;
    private bool _foundPath = false;
    
    private bool[] _isVisited = new bool[400];
    private int[] _parent = new int[400];
    
    private Stack<int> _stack = new Stack<int>();
    private Queue<int> _queue = new Queue<int>();
}