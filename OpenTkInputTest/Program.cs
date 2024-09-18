namespace OpenTkInputTest
{
    using OpenTK.Windowing.Desktop;
    using System;

    public class Program
    {
        private readonly GameWindow m_window;
        JoystickAdapter m_joystickAdapter;

        static void Main()
        {
            GameWindow window = new GameWindow(new() { UpdateFrequency = 144, RenderFrequency = 144 }, new() { Size = (800, 600) });
            Program program = new Program(window);
            window.Run();
        }

        public Program(GameWindow window)
        {
            m_window = window;
            m_window.UpdateFrame += Window_UpdateFrame;
            m_window.JoystickConnected += this.M_window_JoystickConnected;
            m_joystickAdapter = new JoystickAdapter(window.JoystickStates, 0.2f);

            m_joystickAdapter.ButtonPressed += this.M_joystickAdapter_ButtonPressed;
            m_joystickAdapter.AxisMoved += this.M_joystickAdapter_AxisMoved;
            m_joystickAdapter.HatMoved += this.M_joystickAdapter_HatMoved;
        }

        private void M_window_JoystickConnected(OpenTK.Windowing.Common.JoystickEventArgs obj)
        {
            m_joystickAdapter.RedetectJoysticks();
        }

        private void M_joystickAdapter_HatMoved(object source, HatEventArgs args)
        {
            Console.WriteLine($"Joystick: {args.JoystickId}, Hat: {args.HatId}, Direction: {args.HatDirection}");
        }

        private void M_joystickAdapter_AxisMoved(object source, AxisEventArgs args)
        {
            Console.WriteLine($"Joystick: {args.JoystickId}, Axis: {args.AxisId}, Position: {args.AbsolutePosition}, Relative: {args.DeltaPosition}, Corrected: {args.CorrectedPosition}");
        }

        private void M_joystickAdapter_ButtonPressed(object source, ButtonEventArgs args)
        {
            Console.WriteLine($"Joystick: {args.JoystickId}, Button: {args.ButtonId}, State: {(args.IsPressed ? "Pressed" : "Released")}");
        }

        private void Window_UpdateFrame(OpenTK.Windowing.Common.FrameEventArgs obj)
        {
            m_joystickAdapter.SampleJoystickStates();
        }
    }
}
