using WindowsInput;
using System.Runtime.InteropServices;

class InputManager
{
    [DllImport("user32.dll")]
    private static extern bool SetCursorPos(int x, int y);

    public static void Click()
    {
        InputSimulator inputSimulator = new();
        inputSimulator.Mouse.LeftButtonDown();
        Thread.Sleep(50);
        inputSimulator.Mouse.LeftButtonUp();
    }

    public static void MoveAndClick(int x = 0, int y = 0)
    {
        SetCursorPos(x, y);
        Thread.Sleep(100);
        Click();
    }
}